using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using Nistec.WinForms;
using System.ComponentModel;

using Nistec.Data;
using Nistec.Win;

namespace Nistec.GridView
{

    #region GridField

    /// <summary>
    /// VGrid Field class
    /// </summary>
    public class GridField : McField
    {
        private MultiType _FieldType;
        private VGrid owner;

        /// <summary>
        /// Initilaized GridField
        /// </summary>
        public GridField()
        {

        }
        /// <summary>
        /// Initilaized GridField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fieldType"></param>
        public GridField(string key, object value, MultiType fieldType):base(key, value)
        {
            _FieldType = fieldType;
        }
        /// <summary>
        /// Initilaized GridField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="list"></param>
        public GridField(string key, object value, IList list)
            : base(key, value, list)
        {
            _FieldType = MultiType.Combo;
        }
        /// <summary>
        /// Initilaized GridField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public GridField(string key, object value, Type type)
            : base(key, value, type)
        {
            if (value == null)
            {
                value = "";
            }

            if (type.Equals(typeof(bool)))
            {
                _FieldType = MultiType.Boolean;
            }
            else if (type.Equals(typeof(System.DateTime)))
            {
                _FieldType = MultiType.Date;
            }
            else if (((type.Equals(typeof(short)) || type.Equals(typeof(int))) || (type.Equals(typeof(long)) || type.Equals(typeof(ushort)))) || (((type.Equals(typeof(uint)) || type.Equals(typeof(ulong))) || (type.Equals(typeof(decimal)) || type.Equals(typeof(double)))) || ((type.Equals(typeof(float)) || type.Equals(typeof(byte))) || type.Equals(typeof(sbyte)))))
            {
                _FieldType = MultiType.Number;
            }
            else if (type.IsEnum)
            {
                _FieldType = MultiType.Combo;
                //this.Items.AddRange(Enum.GetNames(type));
            }
            else if (type.Equals(typeof(string)))
            {
                _FieldType = MultiType.Text;
            }
            else //if (type.Equals(typeof(string)))
            {
                if (type.Equals(typeof(string)))
                {
                    _FieldType = MultiType.Memo;

                    //if (value!=null && value.ToString().Length > 20);//owner.ColumnValue.Width> owner.ColumnValue.WidthToLength())
                    //{
                    //    _FieldType = MultiType.Memo;
                    //    return;
                    //}
                }
                else
                {
                    _FieldType = MultiType.Text;

                }
            }

        }
        /// <summary>
        /// Initilaized GridField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public GridField(string key, object value)
            : this(key, value, value == null ? typeof(string) : value.GetType())
        {

        }


        /// <summary>
        /// GridField destrauctor
        /// </summary>
        ~GridField()
        {
            if (owner != null)
                this.owner.CellValidated -= new EventHandler(owner_CellValidated);

        }

        internal void SetOwner(VGrid grid)
        {
            if (owner != null)
                this.owner.CellValidated -= new EventHandler(owner_CellValidated);
            this.owner = grid;
            if (owner != null)
                this.owner.CellValidated += new EventHandler(owner_CellValidated);

            SetDefaultValue();
        }

        void owner_CellValidated(object sender, EventArgs e)
        {
            if (owner != null)
            {
                if (owner[0].ToString().Equals(this.Key))
                    base.SetValue(owner[1],false);
                //OnValueChanged(e);
            }
        }

        /// <summary>
        /// Raise value changed event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            if (owner != null)
                owner.OnFieldChanged(Key, Value);
        }

        private void SetDefaultValue()
        {
            if (Text != "")
                return;

            switch (_FieldType)
            {
                case MultiType.Boolean:
                    Value = false;
                    break;
                case MultiType.Color:
                    Value = Grid.DefaultBackColor;
                    break;
                case MultiType.Combo:
                    if (Items != null && Items.Count > 0)
                    {
                        Value = Items[0];
                    }
                    break;
                case MultiType.Date:
                    Value = DateTime.Now;
                    break;
                case MultiType.Font:
                    Value = Grid.DefaultFont;
                    break;
                case MultiType.Number:
                    Value = 0;
                    break;
                default:
                    //Value = "";
                    break;
            }
        }
        /// <summary>
        /// Get Row Index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetRowIndex(object key)
        {
            for (int i = 0; i < owner.RowCount; i++)
            {
                if (owner[i, 0] == key)
                    return i;
            }
            return -1;
        }

 
        /// <summary>
        /// Get or Set key field
        /// </summary>
        public override string Key
        {
            get
            {
                return base.Key;
            }
            set
            {
                base.Key = value;
                if (!owner.IsDesignMode)
                {
                    int row = GetRowIndex(value);
                    if (row > -1)
                    {
                        owner[row, 0] = value;
                    }
                }
            }
        }
        /// <summary>
        /// Get or set value field
        /// </summary>
        public override object Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                if (!owner.IsDesignMode)
                {
                    int row = GetRowIndex(Key);
                    if (row > -1)
                    {
                        owner[row, 1] = value;
                    }
                }
            }
        }
 
        /// <summary>
        /// Get or Set field type
        /// </summary>
        public MultiType MultiType
        {
            get
            {
                return _FieldType;
            }
            set
            {
                if (_FieldType != value)
                {
                    _FieldType = value;
                    SetDefaultValue();
                    //OnColumnDisplayChangedInternal();
                }
            }
        }

    }

    #endregion

    #region GridFieldCollection

    /// <summary>
    /// VGrid Field Collection class
    /// </summary>
    public class GridFieldCollection : System.Collections.CollectionBase//, IList, ICollection, IEnumerable//, IEnumerator, IEnumerable
    {
        internal bool m_FireEvents = true;

        int m_Count = 0;
        internal VGrid owner;
        //private Hashtable hashList;
        /// <summary>
        /// Collection Changed event
        /// </summary>
        public event EventHandler CollectionChanged;
        /// <summary>
        /// Raise Collection Changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCollectionChanged(EventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }
        /// <summary>
        /// Initilaized GridFieldCollection
        /// </summary>
        public GridFieldCollection()
        {
            //hashList = new Hashtable();
        }

        /// <summary>
        /// Initilaized GridFieldCollection
        /// </summary>
        /// <param name="parent"></param>
        public GridFieldCollection(VGrid parent)
        {
            this.owner = parent;
            //hashList = new Hashtable();
        }

        internal void SetOwner(VGrid grid)
        {
            this.owner = grid;
            for (int index = 0; index < Count; index++)
            {
                ((GridField)base.List[index]).SetOwner(grid);
            }
        }
  
        /// <summary>
        /// Add item to collection
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public GridField Add(GridField field)
        {
            try
            {
                field.SetOwner(this.owner);
                base.List.Add(field as object);
                m_Count++;
                OnCollectionChanged(EventArgs.Empty);
                return field;
            }
            catch (Exception ex)
            {
                RM.ShowWarning(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Add item to collection
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GridField Add(string key, object value)
        {
            return Add(new GridField(key, value));
        }

        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="cols"></param>
        public void AddRange(GridField[] cols)
        {
            foreach (GridField itm in cols)
            {
                Add(itm);
            }
        }

        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="list"></param>
        public void AddRange(Hashtable list)
        {
            foreach (DictionaryEntry itm in list)
            {
                Add(new GridField(itm.Key.ToString(), itm.Value));
            }
        }
        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="valueColumnName"></param>
        public void AddRange(DataTable dt, string keyColumnName, string valueColumnName)
        {
            GridField[] source = new GridField[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                source[i] = new GridField(dr[keyColumnName].ToString(), dr[valueColumnName]);
            }
            AddRange(source);
        }
        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public void AddRange(string[] keys, object[] values)
        {
            if (keys.Length != values.Length)
            {
                throw new ArgumentException("keys length not equal to values length");
            }

            GridField[] source = new GridField[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                source[i] = new GridField(keys[i], values[i]);
            }
            AddRange(source);
        }

        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="keys"></param>
        public void AddRange(string[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                throw new ArgumentException("Invalid keys");
            }

            GridField[] source = new GridField[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                source[i] = new GridField(keys[i], null);
            }
            AddRange(source);
        }

        /// <summary>
        /// Get indicate item conains in collection
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool Contains(GridField field)
        {
            for (int index = 0; index < Count; index++)
            {
                GridField vf = (GridField)base.List[index];
                if (vf.Key == field.Key)// && vf.Value == field.Value && vf.MultiType == field.MultiType)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Add item No Duplicate
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool AddNoDuplicate(GridField field)
        {
            if (Contains(field))
            {
                return false;
            }
            Add(field);
            return true;
        }
        /// <summary>
        /// Insert item to collection
        /// </summary>
        /// <param name="field"></param>
        /// <param name="index"></param>
        public void Insert(GridField field, int index)
        {
            try
            {
                field.SetOwner(this.owner);
                base.List.Insert(index, field as object);
                OnCollectionChanged(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                RM.ShowWarning(ex.Message);
            }
        }
        /// <summary>
        /// Copy collection to sysem array of GridFields
        /// </summary>
        /// <param name="fields"></param>
        public void CopyTo(GridField[] fields)
        {
            for (int index = 0; index < Count; index++)
            {
                fields[index] = (GridField)base.List[index];
            }
        }
        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="field"></param>
        public void Remove(GridField field)
        {
            base.List.Remove(field as object);
            m_Count--;
            OnCollectionChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="index"></param>
        public new void RemoveAt(int index)
        {
            if (index < 0 || index >= m_Count)
                return;
            Remove(base.List[index] as GridField);
        }
        /// <summary>
        /// Clear collection
        /// </summary>
        public new void Clear()
        {
            //m_Size = 16;
            m_Count = 0;
            //columns = new McColumn[m_Size];
            base.List.Clear();
            //hashList.Clear();

            OnCollectionChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Get Fields Array
        /// </summary>
        /// <returns></returns>
        public GridField[] GetFieldsArray()
        {
            GridField[] ctl = new GridField[this.Count];
            int i = 0;
            foreach (object o in base.List)
            {
                ctl[i] = o as GridField;
                i++;
            }
            return ctl;
        }

        /// <summary>
        /// Get GridField item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GridField this[int index]
        {
            get
            {
                return base.List[index] as GridField;
            }
        }
        /// <summary>
        /// Get GridField item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GridField this[string key]
        {
            get
            {
                for (int index = 0; index < Count; index++)
                {
                    if (((GridField)base.List[index]).Key == key)
                        return base.List[index] as GridField;
                }
                throw new Exception("GridField \"" + key + "\" is not a valid field.");
            }
        }
        /// <summary>
        /// Get index of item
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(GridField value)
        {
            return base.List.IndexOf(value);
        }

        ///// <summary>
        ///// Convert ToDataField
        ///// </summary>
        ///// <param name="vgf"></param>
        ///// <returns></returns>
        //public static Nistec.Data.McField[] ToDataField(GridField[] vgf)
        //{
        //    int count = vgf.Length;
        //    int i = 0;
        //    Nistec.Data.McField[] dataFields = new Nistec.Data.McField[count];
        //    foreach (GridField vg in vgf)
        //    {
        //        dataFields[i] = new Nistec.Data.McField(vg.Key, vg.Value);
        //        i++;
        //    }
        //    return dataFields;
        //}
    }

    #endregion

    #region GridFieldConverter

    /// <summary>
    /// VGridConverter
    /// </summary>
    public class VGridConverter
    {
        internal static DataTable VTemplate(string name)
        {
            DataTable dt = new DataTable();
            dt.TableName = name;
            dt.Columns.AddRange(new DataColumn[] { new DataColumn(VGrid.DefaultKeyName), new DataColumn(VGrid.DefaultValueName), new DataColumn(VGrid.DefaultFieldTypeName) });
            return dt;
        }

        /// <summary>
        /// ConvertToDataTable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tableNname"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(GridField[] source, string tableNname)
        {

            DataTable dt = VTemplate(tableNname);
            foreach (GridField gs in source)
            {
                dt.Rows.Add(gs.Key, gs.Value, (int)gs.MultiType);
            }
            return dt;
        }
        /// <summary>
        /// ConvertToDataTable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tableNname"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(GridFieldCollection source, string tableNname)
        {

            DataTable dt = VTemplate(tableNname);
            foreach (GridField gs in source)
            {
                dt.Rows.Add(gs.Key, gs.Value, (int)gs.MultiType);
            }
            return dt;
        }

        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="valueColumnName"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(DataTable dt, string keyColumnName, string valueColumnName)
        {
            GridField[] source = new GridField[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                source[i] = new GridField(dr[keyColumnName].ToString(), dr[valueColumnName]);
            }
            return source;
        }

        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="valueColumnName"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(DataView dv, string keyColumnName, string valueColumnName)
        {
            return ConvertToGridField(dv.Table,keyColumnName,valueColumnName);
        }

        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(string[] keys, object[] values)
        {
            if (keys.Length != values.Length)
            {
                throw new ArgumentException("keys length not equal to values length");
            }

            GridField[] source = new GridField[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                source[i] = new GridField(keys[i], values[i]);
            }
            return source;
        }
        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(Hashtable values)
        {
            GridField[] source = new GridField[values.Count];
            int i = 0;
            foreach (DictionaryEntry item in values)
            {
                source[i] = new GridField(item.Key.ToString(), item.Value);
                i++;
            }
            return source;
        }
        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(DataRow dr)
        {
            GridField[] source = new GridField[dr.ItemArray.Length];

            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                source[i] = new GridField(dr.Table.Columns[i].ColumnName, dr[i]);
            }
            return source;
        }
        ///// <summary>
        ///// ConvertToGridField
        ///// </summary>
        ///// <param name="dr"></param>
        ///// <param name="keyColumnName"></param>
        ///// <param name="valueColumnName"></param>
        ///// <returns></returns>
        //public static GridField[] ConvertToGridField(DataRow dr, string keyColumnName, string valueColumnName)//        /// <param name="fieldColumnName"></param>, string fieldColumnName)
        //{
        //    GridField[] source = new GridField[dr.ItemArray.Length];

        //    for (int i = 0; i < dr.ItemArray.Length; i++)
        //    {
        //        //MultiType mct = MultiType.Text;
        //        //object val = dr[fieldColumnName];

        //        //if (val != null)
        //        //{
        //        //    if (Info.IsNumeric(val))
        //        //    {
        //        //        mct = (MultiType)Types.ToInt(val, (int)MultiType.Text);
        //        //    }
        //        //    else
        //        //    {
        //        //        mct = (MultiType)Enum.Parse(typeof(MultiType), val.ToString(), true);
        //        //    }
        //        //}
        //        source[i] = new GridField(dr[keyColumnName], dr[i]);//, mct);
        //    }
        //    return source;
        //}

        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyColumnName"></param>
        /// <param name="valueColumnName"></param>
        /// <param name="fieldColumnName"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(DataTable dt, string keyColumnName, string valueColumnName , string fieldColumnName)
        {
            GridField[] source = new GridField[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                MultiType mct = MultiType.Text;
                object val = dr[fieldColumnName];

                if (val != null)
                {
                    if (WinHelp.IsNumeric(val))
                    {
                        mct = (MultiType)Types.ToInt(val, (int)MultiType.Text);
                    }
                    else
                    {
                        mct = (MultiType)Enum.Parse(typeof(MultiType), val.ToString(), true);
                    }
                }
                source[i] = new GridField(dr[keyColumnName].ToString(), dr[valueColumnName], mct);
            }
            return source;
        }

        /// <summary>
        /// ConvertToGridField
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public static GridField[] ConvertToGridField(object cls)
        {
            Type type = cls.GetType();
            GridField[] source = null;
            int i = 0;
            if (type.IsEnum)
            {
                string[] keys = (Enum.GetNames(type));
                //Array values = (Enum.GetValues(type));

                source = new GridField[keys.Length];
                for (i = 0; i < keys.Length; i++)
                {
                    int val =(int) Enum.Parse(type, keys[i], true);
                    source[i] = new GridField(keys[i], val);
                    source[i].ReadOnly = true;
                }
            }
            else if (type.IsArray)
            {
                Array list = (Array)cls;

                source = new GridField[list.Length];
                for (i = 0; i < list.Length; i++)
                {
                    source[i] = new GridField(i.ToString(), list.GetValue(i));
                    source[i].ReadOnly = true;
                }
            }
            else if (type.IsValueType)
            {
                System.Reflection.FieldInfo[] fi = type.GetFields();//System.Reflection.BindingFlags.Public);
                source = new GridField[fi.Length];
                foreach (System.Reflection.FieldInfo f in fi)
                {
                    source[i] = new GridField(f.Name, f.GetValue(cls));
                    if (!f.IsPublic || f.IsInitOnly) source[i].ReadOnly = true;
                    i++;
                }
            }
            else if (type.IsClass || type.IsInterface)
            {
                System.Reflection.PropertyInfo[] pi = type.GetProperties();//System.Reflection.BindingFlags.Public);
                source = new GridField[pi.Length];
                foreach (System.Reflection.PropertyInfo p in pi)
                {
                    source[i] = new GridField(p.Name, p.GetValue(cls, null), p.PropertyType);
                    if (!p.CanWrite) source[i].ReadOnly = true;
                    i++;
                }
            }
            return source;
        }
    }
    
    #endregion
}
