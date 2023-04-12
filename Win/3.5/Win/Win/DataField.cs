using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using MControl.Win32;
using System.Collections.Generic;


namespace MControl.Win
{

    /// <summary>
    /// DataField class
    /// </summary>
    [Serializable]
    public class DataField:IDataField
    {
        int _Ordinal;
        string _Caption;
        string _ColumnName;
        FieldType _FieldType;

        public int Ordinal
        {
            get { return _Ordinal; }
            set { _Ordinal = value; }
        }
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }

        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        public FieldType FieldType
        {
            get { return _FieldType; }
            set { _FieldType = value; }
        }

        public DataField(string columnName, string caption)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("ColumnName");
            }
            _ColumnName = columnName;
            _Caption = caption;
            _Ordinal = -1;
            _FieldType = FieldType.Text;
        }
        public DataField(string columnName, string caption, int ordinal)
            : this(columnName, caption)
        {
            _Ordinal = ordinal;
            _FieldType = FieldType.Text;
        }
        public DataField(DataColumn c)
            : this(c.ColumnName, c.Caption, c.Ordinal)
        {
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_Caption))
                return ColumnName;
            return Caption;
        }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(_ColumnName); }
        }

        public static DataField[] CreateFields(string[] fields)
        {
            DataField[] ec = new DataField[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                ec[i] = new DataField(fields[i], fields[i]);
            }
            return ec;
        }

        public static DataField[] CreateFields(DataTable dt)
        {
            DataField[] ec = new DataField[dt.Columns.Count];
            int i = 0;
            foreach (DataColumn c in dt.Columns)
            {
                ec[i] = new DataField(c);
                i++;
            }
            return ec;
        }
        public static DataField[] CreateFields(DataTable dt, string[] fields)
        {
            DataField[] ec = new DataField[fields.Length];
            int i = 0;
            foreach (string s in fields)
            {
                ec[i] = new DataField(dt.Columns[s]);
                i++;
            }
            return ec;
        }

        public static object[] CreateRow(DataRow dr, DataField[] fields)
        {
            object[] ec = new object[fields.Length];
            int i = 0;
            foreach (DataField c in fields)
            {
                ec[i] = dr[c.ColumnName];
                i++;
            }
            return ec;
        }
    }

  
    #region DataFieldCollection

   /// <summary>
    /// DataField Collection
   /// </summary>
	public class DataFieldCollection : List<DataField>
	{

        public DataField Add(string ColumnName, string Caption)
        {
            DataField df = new DataField(ColumnName, Caption, this.Count);
            base.Add(df);
            return df;
        }

        public DataField AddNoDuplicate(string ColumnName, string Caption)
        {
            DataField df = this[ColumnName];
            if (df != null)
                return df;
            df = new DataField(ColumnName, Caption, this.Count);
            base.Add(df);
            return df;
        }

		public bool Contains(string columnName)
		{
			return this[columnName]!=null;
		}
		
		public bool AddNoDuplicate(DataField column)
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

	    public DataField this[string columnName]
		{
			get
			{
                foreach(DataField df in this)
                {
			        if (df.ColumnName == columnName)
						return df;
				}
                return null;
			}
		}


	}
	#endregion

}
