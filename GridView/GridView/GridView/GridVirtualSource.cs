using System;
using System.Data; 
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using mControl.Util;
  
namespace mControl.GridView 
{

    [ToolboxItem(true)]
    [DesignTimeVisible(false)]
	public class GridVirtualSource : //System.ComponentModel.Component 
	{
    
		#region Members
	
		private System.ComponentModel.IContainer components;
    
		internal string mVirtualName;
    	internal DataTable m_VirtualTable;
		internal GridView.GridTableStyle mTblStyle;
		//internal GridView.Grid   grid;
    
		#endregion

		#region Constructor

		public GridVirtualSource(int rows, int cols, string VirtualName) 
		{
			// This call is required by the Component Designer.
			InitializeComponent();
			// Add any initialization after the InitializeComponent() call
			InitVirtualGrid(rows, cols, VirtualName);
		}
    
		// Component overrides dispose to clean up the component list.
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
			components = new System.ComponentModel.Container();
		}
    
		#endregion



		private void InitVirtualGrid(int rows, int cols, string VirtualName) 
		{
			m_VirtualTable = new DataTable(VirtualName);
			for (int j = 0; j< cols ; j++) 
			{
				m_VirtualTable.Columns.Add(new DataColumn(j.ToString(), typeof(object)));
			}
	
			DataRow dr = m_VirtualTable.NewRow();
	
			for (int i = 0; i<rows ; i++) 
			{
				dr = m_VirtualTable.NewRow();
				for (int j = 0; j<cols; j++) 
				{
					dr[j.ToString()] = "";
				}
				m_VirtualTable.Rows.Add(dr);
			}

			m_VirtualName = VirtualName;

            
			mTblStyle = new GridView.GridTableStyle ();
			 
			mTblStyle.MappingName = VirtualName;
			GridTextColumn mColX;
			
			for (int j = 0; j<cols; j++) 
			{
				//mTblStyle.CreateGridColumn(
				mColX=new GridTextColumn();
				mColX.SetGridTableInColumn(mTblStyle,true);
				mColX.MappingName=j.ToString();//Strings.Chr(Strings.Asc("A")+j).ToString();
				mTblStyle.GridColumnStyles.Add(mColX);
			}
		}
    
		public GridView.GridTableStyle   VirtualTableStyle 
		{
			get{return mTblStyle;}
		}
    
		public DataView VirtualDataView() 
		{
			return mVirtualTable.DefaultView;
		}
    
		public object this[int row, int col] 
		{
			get 
			{
				return mVirtualTable.Rows[row][col];
			}
			set 
			{
				mVirtualTable.Rows[row][col] = value;
			}
		}
    
		public int Rows 
		{
			get 
			{
				return mVirtualTable.Rows.Count;
			}
		}
    
		public int Cols 
		{
			get 
			{
				return mVirtualTable.Columns.Count;
			}
		}
    
		public virtual void RemoveRow(int row) 
		{
			mVirtualTable.Rows.RemoveAt(row);
		}
    
		public virtual void RemoveCol(int col) 
		{
			mVirtualTable.Columns.RemoveAt(col);
		}
    
		public virtual void ClearAll() 
		{
			for (int i = 0; (i<= (Rows - 1)); i++) 
			{
				for (int j = 0; (j <= (Cols - 1)); j++) 
				{
					this[i, j] = "";
				}
			}
		}

		public static void InitDimension(GridView.Grid   grid) 
		{
			GridView.DimensionDlg f = new GridView.DimensionDlg();
			f.ShowDialog();
			int r = ((int)(f.Rows.Value));
			int c = ((int)(f.Cols.Value));
			string str = f.VirtualName.Text;
			f.Close();
			f.Dispose();
			GridVirtualSource gvs = new GridVirtualSource(r, c, str);
			grid.MappingName= "VirtualGrid";
			grid.DataSource = gvs.VirtualDataView();
		}

	}
}