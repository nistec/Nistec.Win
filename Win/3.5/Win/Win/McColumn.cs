using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using MControl.Win32;
using System.Collections;


namespace MControl.Win
{

 
    public interface IDataField 
    {
        string ColumnName { get;set;}
        string Caption { get;set;}
        int Ordinal { get;set;}
        FieldType FieldType { get;}
        string ToString();
    }

    public interface IColumn : IComponent
    {
        string ColumnName{get;set;}
        string Caption{get;set;}
        int Width{get;set;}
        int Ordinal{get;set;}
        bool Display{get;set;}
        FieldType FieldType { get;set;}
        HorizontalAlignment Alignment{get;set;}
        SortDiraction SortOrder { get;set;}
        int Length { get;}

    }

    public enum SortDiraction
    {
        DESC,
        ASC
    }

	public class McColumnEventArgs : EventArgs
	{
		public McColumn Column;
		public McColumnEventArgs(McColumn col)
		{
			Column = col;
		}
	}

	public delegate void ColumnDisplayChangedHandler(object sender, McColumnEventArgs e);
	public delegate void AddMcColumnHandler(object sender, McColumnCollectionEventArgs e);
	public delegate void RemoveMcColumnHandler(object sender, McColumnCollectionEventArgs e);


	#region McColumn

	/// <summary>
	/// Summary description for McColumn.
	/// </summary>
    [Serializable]
	[DesignTimeVisible(false),ToolboxItem(false)]
    public class McColumn : Component, IColumn, IDataField
	{
	
		public const int DefaultWidth=80;
		private string m_Name;
		private string m_Caption;
		private int m_Width = DefaultWidth;
		private bool m_Display = true;
        private FieldType m_DataType = FieldType.Text;
		private int m_ColOrdinal = -1;
		private HorizontalAlignment  m_Alignment=HorizontalAlignment.Left;

        internal SortDiraction sortOrder = SortDiraction.ASC;
		internal int CalculatedWidth = 0;
		internal Control owner;
        internal int length = 0;

		public event ColumnDisplayChangedHandler ColumnDisplayChanged;

		public McColumn()
		{
		
		}
        
		public McColumn(string name)
		{
			m_Name = name;
		}
		public McColumn(string name, int width)
		{
			m_Name = name;
			Width = width;
		}
		public McColumn(string name, string caption, int width)
		{
			m_Name = name;
			m_Caption=caption;
			Width = width;
		}
		public McColumn(string name, string caption, int width,FieldType dataType)
		{
			m_Name = name;
			m_Caption=caption;
			Width = width;
			m_DataType=dataType;
		}

		public McColumn(string name, bool display)
		{
			m_Name = name;
			m_Display = display;
		}

		protected virtual void OnColumnDisplayChanged(McColumnEventArgs e)
		{
			if(ColumnDisplayChanged != null)
				ColumnDisplayChanged(this, new McColumnEventArgs(this));
		}

		private void OnColumnDisplayChangedInternal()
		{
			if(ColumnDisplayChanged != null)
				ColumnDisplayChanged(this, new McColumnEventArgs(this));
		}

        public void ToggelSortOrder()
        {
            //if (sortOrder == SortOrder.None)
            //    this.sortOrder = SortOrder.Ascending;
            //else
            this.sortOrder = (sortOrder == SortDiraction.ASC) ? SortDiraction.DESC : SortDiraction.ASC;
        }

        public string GetSortExpression()
        {
            switch(sortOrder)
            {
                case SortDiraction.ASC:
                    return this.ColumnName + " ASC" ;
                case SortDiraction.DESC:
                    return this.ColumnName + " DESC";
                default:
                    return this.ColumnName;
            }
        }

        public static string GetSortExpression(string columnName, SortDiraction sort)
        {
            switch (sort)
            {
                case SortDiraction.ASC:
                    return columnName + " ASC";
                case SortDiraction.DESC:
                    return columnName + " DESC";
                default:
                    return columnName;
            }
        }

		#region //Properties

		public string ColumnName
		{
			get
			{
				return m_Name;
			}
			set
			{
//				if(value==null || value=="")
//				{
//					throw new ArgumentException("Invalid column name in " + this.Site.Name);
//				}
				if(m_Name != value)
				{
					m_Name = value;
					OnColumnDisplayChangedInternal();
				}
			}
		}

		public string Caption
		{
			get
			{
				return m_Caption;
			}
			set
			{
//				if(value==null || value=="")
//				{
//					throw new ArgumentException("Invalid caption in " + this.Site.Name);
//				}
				if(m_Caption != value)
				{
					m_Caption = value;
					OnColumnDisplayChangedInternal();
				}
			}
		}

		[DefaultValue(80)]
		public int Width
		{
			get
			{
				return m_Width;
			}
			set
			{
				if(m_Width != value)
				{
					m_Width = value;
					OnColumnDisplayChangedInternal();
				}
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Ordinal
		{
			get
			{
				return m_ColOrdinal;
			}
			set
			{
				if(m_ColOrdinal != value)
				{
					m_ColOrdinal = value;
					OnColumnDisplayChangedInternal();
				}
			}
		}

		[DefaultValue(true)]
		public bool Display
		{
			get
			{
				return m_Display;
			}
			set
			{
				if(m_Display != value)
				{
					m_Display = value;
					OnColumnDisplayChangedInternal();
				}
			}
		}

        [DefaultValue(FieldType.Text)]
        public FieldType FieldType
		{
			get
			{
				return m_DataType;
			}
			set
			{
				if(m_DataType != value)
				{
					m_DataType = value;
					OnColumnDisplayChanged(new McColumnEventArgs(this));
					OnColumnDisplayChangedInternal();
				}
			}
		}
		
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment  Alignment
		{
			get
			{
				return m_Alignment;
			}
			set
			{
				if(m_Alignment != value)
				{
					m_Alignment = value;
					OnColumnDisplayChangedInternal();
				}
			}
		}

        [DefaultValue(SortDiraction.ASC)]
        public SortDiraction SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                if (sortOrder != value)
                {
                    sortOrder = value;
                    OnColumnDisplayChangedInternal();
                }
            }
        }

        [DefaultValue(0)]
        public int Length
        {
            get
            {
                return m_Width;
            }
        }

		#endregion
	}

	#endregion

	#region McColumnCollection

	public class McColumnCollectionEventArgs : EventArgs
	{
		public int Count;
		public McColumn column;
		public McColumnCollectionEventArgs(int count, McColumn col)
		{
			Count = count;
			column = col;
		}
	}

	
	//McColumn collection is similar to an ArrayList but deals only with McColumns.
	//Sure would be nice to have class templates for classes like this one.
	public class McColumnCollection :System.Collections.CollectionBase//, IEnumerator, IEnumerable
	{
//		McColumn[] columns = new McColumn[16];
//		int m_Size = 16;
//		int m_EnumeratorPos;
		internal bool m_FireEvents = true;
		
		int m_Count = 0;
		internal Control owner;

		public event AddMcColumnHandler AddColumnEvent;
		public event RemoveMcColumnHandler RemoveColumnEvent;
		public event EventHandler CollectionChanged;

		protected virtual void OnCollectionChanged(EventArgs e)
		{
			if(CollectionChanged!=null)
				CollectionChanged(this,e);

		}

		public McColumnCollection()
		{
		}

		public McColumnCollection(Control parent)
		{
			this.owner=parent;
		}

		protected override void OnInsert(int index, object value)
		{
			base.OnInsert (index, value);
			if(((McColumn)value).ColumnName==null ||((McColumn)value).ColumnName=="")
				((McColumn)value).ColumnName="ctlColumn"+index.ToString();
			if(((McColumn)value).Caption==null ||((McColumn)value).Caption=="")
				((McColumn)value).Caption="ctlColumn"+index.ToString();
		}

		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
//				||((McColumn)value).Caption==null ||((McColumn)value).Caption=="")
				if(((McColumn)value).ColumnName==null ||((McColumn)value).ColumnName==""||((McColumn)value).Caption==null ||((McColumn)value).Caption=="")
				throw new ArgumentException("Incorrect ColumnName or Caption  ");
		}

		
		public void ItemAdded(object sender, McColumnCollectionEventArgs e)
		{

		}

//		private void CheckGrow()
//		{
//			if(m_Count >= m_Size)
//			{
//				m_Size *= 2;
//				McColumn[] doTemp = new McColumn[m_Size];
//				columns.CopyTo(doTemp, 0);
//				columns = doTemp;
//			}
//		}


		public McColumn Add(McColumn column)
		{
//			if(Contains(column))
//				throw new Exception("Column collection already contains a column named \"" + column.ColumnName + "\"");
			//CheckGrow();
			//columns[m_Count] = column;
			column.owner=this.owner;
//			column.ColumnName=string.Format("ctlColumn{0}",m_Count+1);
//			column.Caption=string.Format("ctlColumn{0}",m_Count+1);
			base.List.Add(column as object);
			m_Count++;
			if(AddColumnEvent != null && m_FireEvents)
			{
				McColumnCollectionEventArgs args = new McColumnCollectionEventArgs(m_Count, column);
				AddColumnEvent(this, args);
				OnCollectionChanged(EventArgs.Empty);
			}
			return column;
		}

		public void AddRange(McColumn[] cols)
		{
			foreach(McColumn itm in cols)
			{
				Add(itm);
			}
		}

		public void AddRange(string[] cols)
		{
			for(int i=0;i<cols.Length;i++)
			{
				McColumn itm=new McColumn();
				itm.owner=this.owner;
				itm.ColumnName=cols[i];
				if(!this.Contains(itm))
					Add(itm);  
			}
		}
	
		public bool Contains(McColumn column)
		{
			for(int index = 0; index < Count; index++)
			{
				if(((McColumn)base.List[index]).ColumnName == column.ColumnName)
					return true;
			}
			return false;
		}
		
		public bool AddNoDuplicate(McColumn column)
		{
			bool ok = true;
			if(Contains(column))
			{
				Remove(column);
				ok = false;
			}
			Add(column);
			return ok;
		}

		public void Insert(McColumn column, int index)
		{

			column.owner=this.owner;
			base.List.Insert(index, column as object);
			OnCollectionChanged(EventArgs.Empty);
	
	
//			CheckGrow();
//			if(index < 0)
//				Insert(column, 0);
//			if(iPos >= m_Count && index != 0)
//				Insert(column, m_Count - 1);
//			McColumn[] doTemp = new McColumn[m_Size];
//			int i = 0;
//			for(; i < index; i++)
//			{
//				doTemp[i] = columns[i];
//			}
//			doTemp[i] = column;
//			for(; i < m_Count; i++)
//			{
//				doTemp[i + 1] = columns[i];
//			}
//			columns = doTemp;
//			m_Count++;

		}


		public void Remove(McColumn column)
		{
			base.List.Remove(column as object);


//			int index = 0;
//			for(; index < m_Count; index++)
//			{
//				if(columns[index].Name == column.Name)
//					break;
//			}
//			if(index == m_Count)
//				return;
//			for(; index < m_Count - 1; index++)
//			{
//				columns[index] = columns[index + 1];
//			}
			m_Count--;
//			Remove(column);
			if(RemoveColumnEvent != null && m_FireEvents)
			{
				McColumnCollectionEventArgs args = new McColumnCollectionEventArgs(m_Count, column);
				RemoveColumnEvent(this, args);
				OnCollectionChanged(EventArgs.Empty);
			}
		}


		public new void RemoveAt(int index)
		{
			if(index < 0 || index >= m_Count)
				return;
			Remove(base.List[index] as McColumn );
			//base.List.RemoveAt(index);


//			for(; index < m_Count - 1; index++)
//			{
//				columns[index] = columns[index + 1];
//			}
//			m_Count--;
		}


//		public void MoveToFront(McColumn column)
//		{
//			m_FireEvents = false;
//			Remove(column);
//			Insert(column, 0);
//			m_FireEvents = true;
//		}


		public new void Clear()
		{
			//m_Size = 16;
			m_Count = 0;
			//columns = new McColumn[m_Size];
			base.List.Clear();
			OnCollectionChanged(EventArgs.Empty);
		}

		public McColumn[] GetColumnsArray()
		{
			McColumn[] ctl=new McColumn[this.Count];
			int i=0;
			foreach(object o in base.List)
			{
				ctl[i]=o as McColumn;
				i++;
			}
			return ctl;
		}

		public McColumn[] GetVisibleColumns()
		{
			int cnt=0;
			foreach(object o in base.List)
			{
				if(((McColumn)o).Display && ((McColumn)o).Width>0)
				cnt++;
			}
			McColumn[] ctl=new McColumn[cnt];
			int i=0;
			foreach(object o in base.List)
			{
				if(((McColumn)o).Display && ((McColumn)o).Width>0)
				{
					ctl[i]=o as McColumn;
					i++;
				}
			}
			return ctl;
		}

        //public ExportField[] GetExportFields()
        //{
        //    McColumn[] ctl=GetVisibleColumns();
        //    int i=0;

        //    ExportField[] col = new ExportField[ctl.Length];
        //    foreach(McColumn c in ctl)
        //    {
        //        col[i] = new ExportField(c.ColumnName,c.Caption,c.ColumnOrdinal);
        //        i++;
        //    }
        //    return col;
        //}

        public void CalculateLength(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new ArgumentNullException("DataTable");
            }
            if (this.Count == 0)
            {
                throw new ArgumentNullException("No Column found");
            }

            foreach (DataRow dr in dt.Rows)
            {
                foreach (McColumn c in this)
                {
                    c.length = Math.Max(c.length, ((string)Types.NZ(dr[c.ColumnName], "")).Length);
                }
            }

        }

        public static float CalculateColumnsWidth(DataTable dt,McColumn[] cols,Font font)
        {

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new ArgumentNullException("DataTable");
            }
            if (cols.Length == 0)
            {
                throw new ArgumentNullException("No Column found");
            }
            float size=font.SizeInPoints/2;
            foreach (McColumn c in cols)
            {
                c.Width = 0;
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (McColumn c in cols)
                {
                    c.length = Math.Max(c.length, ((string)Types.NZ(dr[c.ColumnName], "")).Length);
                    c.Width = Math.Max(c.Width,(int)((float)c.length * size));
                }
            }
            float totalWidth = 0;
            foreach (McColumn c in cols)
            {
                totalWidth += (float)c.Width + (float)0.5f;
            }
            return totalWidth;
        }


		#region //Properties
		public new int Count
		{
			get
			{
				return base.List.Count;// m_Count;
			}
		}

		public McColumn this[int index]
		{
			get
			{
				return base.List[index] as McColumn;// columns[index];
			}
		}

		public McColumn this[string name]
		{
			get
			{
				for(int index = 0; index < Count; index++)
				{
					if(((McColumn)base.List[index]).ColumnName == name)
						return base.List[index] as McColumn;
				}
//				for(int index = 0; index < m_Count; index++)
//				{
//					if(columns[index].Name == name)
//						return columns[index];
//				}
				throw new Exception("Column \"" + name + "\" is not a valid column.");
			}
		}

		public int IndexOf(McColumn value)
		{
			return base.List.IndexOf(value);
		}

		#endregion


//		#region IEnumerator Members
//
//		public IEnumerator GetEnumerator()
//		{
//			m_EnumeratorPos = -1;
//			return (IEnumerator)this;
//		}
//		public void Reset()
//		{
//			m_EnumeratorPos = -1;
//		}
//
//		public object Current
//		{
//			get
//			{
//				return columns[m_EnumeratorPos];
//			}
//		}
//
//		public bool MoveNext()
//		{
//			if(m_EnumeratorPos >= m_Count - 1)
//				return false;
//			else
//			{
//				m_EnumeratorPos++;
//				return true;
//			}
//		}
//
//		#endregion
	}
	#endregion

}
