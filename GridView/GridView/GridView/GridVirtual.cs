using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing; 
using System.Data;


namespace Nistec.GridView
{
    /// <summary>
    /// Displays the values of a virtual data source in a table where each column represents a field and each row represents a record. This GridView allows you to select, sort, and edit these items.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(GridVirtual), "Images.GridVirtual.bmp")]
    [Designer("Nistec.GridView.Design.GridVirtualDesigner"), DefaultProperty("DataSource")]//, DefaultEvent("Navigate")]
    public class GridVirtual : Nistec.GridView.Grid    
	{
		#region Members 
        //private const string VirtualMappingName="VirtualGrid";
	    private System.Drawing.Size m_Dimension;
        private DataTable m_VirtualTable;
        private string m_VirtualMappingName;
	  
 
		#endregion

		#region Constructors
        /// <summary>
        /// Initializes a new instance of the Grid Virtual class.
        /// </summary>
		public GridVirtual() 
		{
            //m_Dimension = new Size(2, 2);
            m_VirtualMappingName = "VirtualGrid";
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
        

		#endregion

        #region override
        /// <summary>
        /// OnHandleCreated
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetVirtualGrid(m_Dimension.Height, m_Dimension.Width);
        }
        /// <summary>
        /// Get GridColumnCollection
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override GridColumnCollection Columns
        {
            get { return base.Columns; }
        }
        /// <summary>
        /// Get or Set DataSource
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new object DataSource
        {
            get { return base.DataSource; }
            set
            {
                 base.DataSource = value; 
                //SetDataBinding(value, DefaultSourceName);
            }
        }
        /// <summary>
        /// Get or Set the DataMember of DataSource
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string DataMember
        {
            get { return base.DataMember; }
            set { base.DataMember = value; }
        }

 
        #endregion

        #region Virtual Property

        /// <summary>
        /// Get or Set the virtual Dimension
        /// </summary>
        public System.Drawing.Size Dimension 
		{
			get 
			{
                if (!(m_VirtualTable == null))
                {
                    m_Dimension= new System.Drawing.Size(m_VirtualTable.Columns.Count, m_VirtualTable.Rows.Count);

                }
                   return m_Dimension;
                    //return new System.Drawing.Size(2, 2);
 			}
			set 
			{
				if(m_Dimension !=value)
				{
                    if (((value.Height < 0) || (value.Width < 0)))
                    {
                        throw new Exception("Error size value");
                    }
                    m_Dimension = value;
                    if (IsHandleCreated)
                    {
                        SetVirtualGrid(m_Dimension.Height, m_Dimension.Width);
                    }
					this.Invalidate ();
				}
			}
		}
        /// <summary>
        /// Get the Virtual Source
        /// </summary>
		public DataTable VirtualSource 
		{
			get 
			{
                return m_VirtualTable;
			}
		}
        /// <summary>
        /// Get or Set the MappingName of virtual data source
        /// </summary>
        public new string MappingName
        {
            get { return this.m_VirtualMappingName; }
            set
            {
                if (value == null || value == "")
                    return;
                if (this.m_VirtualMappingName != value)
                {
                    this.m_VirtualMappingName = value;
                    base.MappingName = value;
                }
            }
        }

        
		#endregion

		#region Methods
        /// <summary>
        /// SetVirtualGrid
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void SetVirtualGrid(int rows, int cols)
        {
            if (rows == 0 || cols == 0)
                return;
            if(m_VirtualTable!=null)
                m_VirtualTable.Clear();
            if (base.Columns.Count > 0)
                base.Columns.Clear();
            m_VirtualTable = new DataTable(this.m_VirtualMappingName);

            for (int j = 0; j < cols; j++)
            {
                 string colName = ConvertColIndexToString(j);
                // m_VirtualTable.Columns.Add(new DataColumn(j.ToString(), typeof(object)));
                m_VirtualTable.Columns.Add(new DataColumn(colName, typeof(object)));
            }

            DataRow dr = m_VirtualTable.NewRow();

            for (int i = 0; i < rows; i++)
            {
                dr = m_VirtualTable.NewRow();
                for (int j = 0; j < cols; j++)
                {
                    dr[j] = "";
                }
                m_VirtualTable.Rows.Add(dr);
            }
            base.DataSource = null;
            base.MappingName = this.m_VirtualMappingName;
            base.DataSource = m_VirtualTable;

        }

        private string ConvertColIndexToString(int j)
        {
            const int rng = 26;
            //int ascZ = Strings.Asc('Z');
            int ascA = Strings.Asc('A');
            string colName = "";

            if (j < rng)
            {
                return Strings.Chr(ascA + j).ToString();
            }

            int cnt = j / rng;
   
            for (int i = 0; i < cnt; i++)
            {
                colName += Strings.Chr(i+ascA).ToString();
            }

            colName += Strings.Chr(j - (cnt*rng) + ascA).ToString();
 
            return colName;
        }
        /// <summary>
        /// Perform virtual Dimension by dialog
        /// </summary>
        public void PerformDimension()
        {
            DimensionDlg f = DimensionDlg.Open(m_Dimension.Width, m_Dimension.Height, m_VirtualMappingName);
            if (f.DialogResult != DialogResult.Yes)
            {
                return;
            }
            int r = ((int)(f.Rows.Value));
            int c = ((int)(f.Cols.Value));
            string str = f.VirtualName.Text;
            m_Dimension = new Size(c, r);
            if (str.Length > 0)
            {
                this.MappingName = str;
            }
            //f.Close();
            f.Dispose();

            SetVirtualGrid(r, c);
        }
        
		#endregion

	}
}

