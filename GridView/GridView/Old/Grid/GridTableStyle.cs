using System;
using System.ComponentModel;
using System.Drawing; 
using System.Windows.Forms;
using System.Collections;
using mControl.GridStyle.Columns;
  
namespace mControl.GridStyle
{

	[DesignTimeVisible(false), ToolboxItem(false)]
	public class GridTableStyle : Component// DataGridTableStyle //,IGridDataTable
	{
    
		#region Members
		internal Grid owner;
		private string mappingName;
		private GridColumnsCollection cols=null;
		private const int defaultWidth=80;

		private bool allowSorting;
		private bool columnHeadersVisible;
		private bool rowHeadersVisible;
		private bool readOnly;
		//private int PreferredColumnWidth;
		//private int PreferredRowHeight;

		#endregion

		#region constructor

		public GridTableStyle()
		{
			InitializeComponent();
		}

		public GridTableStyle(Grid g)
		{
			this.owner=g;
			InitializeComponent();
		}
		public GridTableStyle(Grid g,string mapName )
		{
			this.owner=g;
			InitializeComponent();
			mappingName =mapName;
		}

	
	
		protected override void Dispose( bool disposing )
		{
			if(disposing)
			{
				GridColumnsCollection collection1 = this.cols;
				if (collection1 != null)
				{
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						collection1[num1].Dispose();
					}
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cols=new GridStyle.GridColumnsCollection (owner);
			allowSorting=true;
		    columnHeadersVisible=true;
		    rowHeadersVisible=true;
		    readOnly=false;
		}

		#endregion

		#region Columns Collection

		public string MappingName
		{
			get{return this.mappingName;} 
			set{this.mappingName=value;}
		}

		[Category("Columns"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public GridColumnsCollection  Columns 
		{
			get
			{
				return cols;
			}
		}

		public System.Windows.Forms.DataGridColumnStyle [] GetColumnArray()
		{
			int cnt=Columns.Count; 
			System.Windows.Forms.DataGridColumnStyle [] arry=new DataGridColumnStyle[cnt];   
            Columns.CopyTo(arry,0);
			return arry;
		}

		#endregion

		#region Property

		[Browsable(false)]
		public string TableName()
		{
			return base.ToString ().Substring (0,base.ToString ().IndexOf ("["));   
		}

		[Category("TableStyle")] 
		public bool AllowSorting
		{
			get{return allowSorting ;}
			set{allowSorting=value;}
		}

		[Category("TableStyle")] 
		public bool ColumnHeadersVisible
		{
			get{return columnHeadersVisible ;}
			set{columnHeadersVisible=value;}
		}

		[Category("TableStyle")] 
		public bool RowHeadersVisible
		{
			get{return rowHeadersVisible ;}
			set{rowHeadersVisible=value;}
		}

		[Category("TableStyle")] 
		public bool ReadOnly
		{
			get{return readOnly ;}
			set{readOnly=value;}
		}

//		[Category("TableStyle")] 
//		public int RowHeaderWidth
//		{
//			get{return tbl.RowHeaderWidth  ;}
//			set{tbl.RowHeaderWidth=value;}
//		}
//
//		[Category("TableStyle")] 
//		public int PreferredColumnWidth
//		{
//			get{return tbl.PreferredColumnWidth ;}
//			set{tbl.PreferredColumnWidth=value;}
//		}
//
//		[Category("TableStyle")] 
//		public int PreferredRowHeight
//		{
//			get{return tbl.PreferredRowHeight  ;}
//			set{tbl.PreferredRowHeight=value;}
//		}

		#endregion

		#region base Property

		/*
		[Browsable(false)]
		public TableStyle TableStyle
		{
			get{return tbl;}
			set{tbl=value;}
		}
		[Category("TableStyle")] 
		public string MappingName
		{
			get{return tbl.MappingName ;}
			set{tbl.MappingName=value;}
		}


		[Category("TableStyle")] 
		public Color HeaderBackColor
		{
			get{return tbl.HeaderBackColor  ;}
			set{tbl.HeaderBackColor=value;}
		}

		[Category("TableStyle")] 
		public Color HeaderForeColor
		{
			get{return tbl.HeaderForeColor   ;}
			set{tbl.HeaderForeColor=value;}
		}

		[Category("TableStyle")] 
		public Color LinkColor
		{
			get{return tbl.LinkColor  ;}
			set{tbl.LinkColor=value;}
		}

		[Category("TableStyle")] 
		public Color BackColor
		{
			get{return tbl.BackColor  ;}
			set{tbl.BackColor=value;}
		}

		[Category("TableStyle")] 
		public Color AlternatingBackColor
		{
			get{return tbl.AlternatingBackColor ;}
			set{tbl.AlternatingBackColor=value;}
		}

		[Category("TableStyle")] 
		public Color ForeColor
		{
			get{return tbl.ForeColor  ;}
			set{tbl.ForeColor=value;}
		}

		[Category("TableStyle")] 
		public Color GridLineColor
		{
			get{return tbl.GridLineColor ;}
			set{tbl.GridLineColor=value;}
		}

		[Category("TableStyle")] 
		public Color SelectionBackColor
		{
			get{return tbl.SelectionBackColor  ;}
			set{tbl.SelectionBackColor=value;}
		}

		[Category("TableStyle")] 
		public Color SelectionForeColor
		{
			get{return tbl.SelectionForeColor   ;}
			set{tbl.SelectionForeColor=value;}
		}

		[Category("TableStyle")] 
		public bool ColumnHeadersVisible
		{
			get{return tbl.ColumnHeadersVisible ;}
			set{tbl.ColumnHeadersVisible=value;}
		}

		[Category("TableStyle")] 
		public bool ReadOnly
		{
			get{return tbl.ReadOnly ;}
			set{tbl.ReadOnly=value;}
		}

		[Category("TableStyle")] 
		public int RowHeaderWidth
		{
			get{return tbl.RowHeaderWidth  ;}
			set{tbl.RowHeaderWidth=value;}
		}

		[Category("TableStyle")] 
		public int PreferredColumnWidth
		{
			get{return tbl.PreferredColumnWidth ;}
			set{tbl.PreferredColumnWidth=value;}
		}

		[Category("TableStyle")] 
		public int PreferredRowHeight
		{
			get{return tbl.PreferredRowHeight  ;}
			set{tbl.PreferredRowHeight=value;}
		}

		[Category("TableStyle")] 
		public bool RowHeadersVisible
		{
			get{return tbl.RowHeadersVisible    ;}
			set{tbl.RowHeadersVisible=value;}
		}
		#endregion

		#region GridProperty
        
		[Browsable(false)]
		public System.Windows.Forms.DataGrid DataGrid
		{
			get{return tbl.DataGrid  ;}
			set{tbl.DataGrid=value;}
		}
*/
		#endregion

		#region DataTable

		private System.Data.DataTable  m_DataTable=null;

		[Category("Data")]
		public System.Data.DataTable  DataTable
		{
			get{ return m_DataTable;}
			set
			{
				m_DataTable=value;
				if(m_DataTable!=null)
				{
					this.MappingName =m_DataTable.TableName; 
				}
			}
		}

		#endregion
	}


	#region IGridDataTable

	public interface IGridDataTable
	{

		Color FocusColor
		{
			get;set;
		}

		/*TableStyle TableStyle
		{
			get;set;
		}*/
		string MappingName
		{
			get;set;
		}

		bool AllowSorting
		{
			get;set;
		}
	
		Color HeaderBackColor
		{
			get;set;
		}

		Color HeaderForeColor
		{
			get;set;
		}

		Color LinkColor
		{
			get;set;
		}

		Color BackColor
		{
			get;set;
		}

		Color AlternatingBackColor
		{
			get;set;
		}

		Color ForeColor
		{
			get;set;
		}

		Color GridLineColor
		{
			get;set;
		}

		Color SelectionBackColor
		{
			get;set;
		}

		Color SelectionForeColor
		{
			get;set;
		}

		bool ColumnHeadersVisible
		{
			get;set;
		}

		bool ReadOnly
		{
			get;set;
		}

		int RowHeaderWidth
		{
			get;set;
		}

		int PreferredColumnWidth
		{
			get;set;
		}

		int PreferredRowHeight
		{
			get;set;
		}

		bool RowHeadersVisible
		{
			get;set;
		}

		System.Windows.Forms.DataGrid DataGrid
		{
			get;set;
		}

	}
	#endregion
}
