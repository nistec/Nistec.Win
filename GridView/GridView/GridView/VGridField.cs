using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MControl.WinForms;
using System.ComponentModel;
using MControl.Util;
using System.Collections;

namespace MControl.GridView
{
    /// <summary>
    /// VGrid Field class
    /// </summary>
    public class McField
    {
        private string _Key;
        private object _Value;
        private string _Description="";
        private bool _Enabled = true;
        private bool _ReadOnly = false;
        private string _Filter = "";
        private object _Tag;
        private FieldType _FieldType;
        private VGrid owner;
        private ArrayList m_Items;
        private string _ToolTip = "";

        /// <summary>
        /// Value Changed event
        /// </summary>
        public event EventHandler ValueChanged;
   
        /// <summary>
        /// Initilaized McField
        /// </summary>
        public McField()
        {

        }
        /// <summary>
        /// Initilaized McField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fieldType"></param>
        public McField(string key, object value, FieldType fieldType)
        {
            _Key = key;
            _Value = value;
            _FieldType = fieldType;
        }
        /// <summary>
        /// Initilaized McField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="list"></param>
        public McField(string key, object value, IList list)
        {
            _Key = key;
            _Value = value;
            _FieldType = FieldType.Combo;
            this.Items.AddRange(list);
        }
        /// <summary>
        /// Initilaized McField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public McField(string key, object value, Type type)
        {
            if (value == null)
            {
                value = "";
            }

            _Key = key;
            _Value = value;
            
            if (type.Equals(typeof(bool)))
            {
                _FieldType = FieldType.Boolean;
            }
            else if (type.Equals(typeof(System.DateTime)))
            {
                _FieldType = FieldType.Date;
            }
            else if (((type.Equals(typeof(short)) || type.Equals(typeof(int))) || (type.Equals(typeof(long)) || type.Equals(typeof(ushort)))) || (((type.Equals(typeof(uint)) || type.Equals(typeof(ulong))) || (type.Equals(typeof(decimal)) || type.Equals(typeof(double)))) || ((type.Equals(typeof(float)) || type.Equals(typeof(byte))) || type.Equals(typeof(sbyte)))))
            {
                _FieldType = FieldType.Number;
            }
            else if (type.IsEnum)
            {
                _FieldType = FieldType.Combo;
                this.Items.AddRange(Enum.GetNames(type));
            }
            else //if (type.Equals(typeof(string)))
            {
                _FieldType = FieldType.Text;
            }

        }
        /// <summary>
        /// Initilaized McField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public McField(string key, object value)
            :this(key,value,value==null ? typeof(string) :value.GetType())
        {

        }

 
        /// <summary>
        /// McField destrauctor
        /// </summary>
        ~McField()
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
                if(owner[0].ToString().Equals(this.Key))
                _Value = owner[1];
                //OnValueChanged(e);
            }
        }
        /// <summary>
        /// Raise value changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
            if (owner != null)
                owner.OnFieldChanged(_Key, _Value);
        }

        private void SetDefaultValue()
        {
            if (Text!="")
                return;

            switch (_FieldType)
            {
                case FieldType.Boolean:
                    Value = false;
                    break;
                case FieldType.Color:
                    Value = Grid.DefaultBackColor;
                    break;
                case FieldType.Combo:
                    if (m_Items != null && m_Items.Count > 0)
                    {
                        Value = m_Items[0];
                    }
                    break;
                case FieldType.Date:
                    Value = DateTime.Now;
                    break;
                case FieldType.Font:
                    Value = Grid.DefaultFont;
                    break;
                case FieldType.Number:
                    Value = 0;
                    break;
                default:
                    Value = "";
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
            for(int i=0;i< owner.RowCount;i++)
            {
                if (owner[i, 0] == key)
                    return i;
            }
            return -1;
        }

        internal  void SetValue(object value)
        {
            _Value = value;
            OnValueChanged(EventArgs.Empty);
        }

 
        /// <summary>
        /// Get or Set key field
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                    if (!owner.IsDesignMode)
                    {
                        int row = GetRowIndex(_Key);
                        if (row > -1)
                        {
                            owner[row, 0] = value;
                        }
                        //OnColumnDisplayChangedInternal();
                    }
                }
            }
        }
        /// <summary>
        /// Get or set value field
        /// </summary>
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
             
                if (_Value != value)
                {
                    _Value = value;
                    if (!owner.IsDesignMode)
                    {
                        int row = GetRowIndex(_Key);
                        if (row > -1)
                        {
                            owner[row,1] = value;
                            OnValueChanged(EventArgs.Empty);
                        }
                        //OnColumnDisplayChangedInternal();
                    }
                }
            }
        }
        /// <summary>
        /// Get or Set Value field as string
        /// </summary>
        public string Text
        {
            get
            {
                if (_Value == null)
                    return "";
                return _Value.ToString();
            }
            set
            {
                if (value == null)
                    value = "";
                if (Text != value)
                {
                    Value = value;
                    //OnColumnDisplayChangedInternal();
                }
            }
        }
        /// <summary>
        /// Get or Set description of grid field
        /// </summary>
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (value == null)
                    value = "";
                _Description = value;
            }
        }
        /// <summary>
        /// Get or Set field type
        /// </summary>
        public FieldType FieldType
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
        /// <summary>
        /// Get or Set Enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    //OnColumnDisplayChangedInternal();
                }
            }
        }
        /// <summary>
        /// Get or Set ReadOnly
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                if (_ReadOnly != value)
                {
                    _ReadOnly = value;
                    //OnColumnDisplayChangedInternal();
                }
            }
        }

        /// <summary>
        /// Get or Set Filter field
        /// </summary>
        public string Filter
        {
            get
            {
                return _Filter;
            }
            set
            {
                if (_Filter != value)
                {
                    _Filter = value;
                    //OnColumnDisplayChangedInternal();
                }
            }
        }
        /// <summary>
        /// Get or Set Tag field
        /// </summary>
        public object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                if (_Tag != value)
                {
                    _Tag = value;
                    //OnColumnDisplayChangedInternal();
                }
            }
        }

        /// <summary>
        /// Get or Set ToolTip field
        /// </summary>
        public string ToolTip
        {
            get
            {
                return _ToolTip;
            }
            set
            {
                if (_ToolTip != value)
                {
                    _ToolTip = value;
                    //OnColumnDisplayChangedInternal();
                }
            }
        }
        /// <summary>
        /// Get items collection
        /// </summary>
        [Category("Data"),
Editor("System.Windows.Forms.Design.StringCollectionEditor,System.Design",
    "System.Drawing.Design.UITypeEditor,System.Drawing"),
DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ArrayList Items
        {
            get
            {
                if (m_Items == null)
                {
                    m_Items = new ArrayList();
                }
                return m_Items;
            }

        }

    }


    //public class McFieldEventArgs : EventArgs
    //{
    //    public McField Field;
    //    public McFieldEventArgs(McField f)
    //    {
    //        Field = f;
    //    }
    //}

    /// <summary>
    /// VGrid Field Collection class
    /// </summary>
    public class McFieldCollection : System.Collections.CollectionBase//, IList, ICollection, IEnumerable//, IEnumerator, IEnumerable
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
			if(CollectionChanged!=null)
				CollectionChanged(this,e);
		}
        /// <summary>
        /// Initilaized McFieldCollection
        /// </summary>
		public McFieldCollection()
		{
            //hashList = new Hashtable();
        }

        /// <summary>
        /// Initilaized McFieldCollection
        /// </summary>
        /// <param name="parent"></param>
		public McFieldCollection(VGrid parent)
		{
			this.owner=parent;
            //hashList = new Hashtable();
		}

        internal void SetOwner(VGrid grid)
        {
            this.owner = grid;
          	for(int index = 0; index < Count; index++)
			{
                ((McField)base.List[index]).SetOwner(grid);
			}
       }
        //protected override void OnInsert(int index, object value)
        //{
        //    base.OnInsert(index, value);
        //    if (this.hashList.Contains(((McField)value).Key))
        //    {
        //        RM.ShowWarning(((McField)value).Key + ": allready contains in fields list");
        //    }
        //}
	
        /// <summary>
        /// Add item to collection
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
		public McField Add(McField field)
		{
            try
            {
                 field.SetOwner(this.owner);
                 //hashList.Add(field.Key, field);
                 base.List.Add(field as object);
                 m_Count++;
                OnCollectionChanged(EventArgs.Empty);
                return field;
            }
            catch(Exception ex)
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
        public McField Add(string key, object value)
        {
           return Add(new McField(key,value));
        }

        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="cols"></param>
		public void AddRange(McField[] cols)
		{
			foreach(McField itm in cols)
			{
				Add(itm);
			}
		}

        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="list"></param>
        public void AddRange(Hashtable  list)
        {
            foreach (DictionaryEntry itm in list)
            {
                Add(new McField(itm.Key.ToString(),itm.Value));
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
            McField[] source = new McField[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                source[i] = new McField(dr[keyColumnName].ToString(), dr[valueColumnName]);
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

            McField[] source = new McField[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                source[i] = new McField(keys[i], values[i]);
            }
            AddRange( source);
        }

        /// <summary>
        /// Add items range to collection
        /// </summary>
        /// <param name="keys"></param>
        public void AddRange(string[] keys)
        {
            if (keys==null || keys.Length ==0)
            {
                throw new ArgumentException("Invalid keys");
            }

            McField[] source = new McField[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                source[i] = new McField(keys[i], null);
            }
            AddRange(source);
        }

        /// <summary>
        /// Get indicate item conains in collection
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
		public bool Contains(McField field)
		{
         	for(int index = 0; index < Count; index++)
			{
                McField vf=(McField)base.List[index];
                if (vf.Key == field.Key)// && vf.Value == field.Value && vf.FieldType == field.FieldType)
					return true;
			}
			return false;
		}
		/// <summary>
        /// Add item No Duplicate
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public bool AddNoDuplicate(McField field)
		{
			//bool ok = true;
			if(Contains(field))
			{
				//Remove(field);
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
		public void Insert(McField field, int index)
		{
            try
            {
                field.SetOwner( this.owner);
                //hashList.Add(field.Key, field);
                base.List.Insert(index, field as object);
                OnCollectionChanged(EventArgs.Empty);
            }           
            catch(Exception ex)
            {
                RM.ShowWarning(ex.Message);
            }
		}
        /// <summary>
        /// Copy collection to sysem array of McFields
        /// </summary>
        /// <param name="fields"></param>
        public void CopyTo(McField[] fields)
        {
            for (int index = 0; index < Count; index++)
            {
                fields[index] = (McField)base.List[index];
            }
        }
        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="field"></param>
		public void Remove(McField field)
		{
			base.List.Remove(field as object);
            //hashList.Remove(field.Key);
	
			m_Count--;
    		OnCollectionChanged(EventArgs.Empty);
		}

        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="index"></param>
		public new void RemoveAt(int index)
		{
			if(index < 0 || index >= m_Count)
				return;
			Remove(base.List[index] as McField );
			//base.List.RemoveAt(index);


//			for(; index < m_Count - 1; index++)
//			{
//				columns[index] = columns[index + 1];
//			}
//			m_Count--;
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
		public McField[] GetFieldsArray()
		{
			McField[] ctl=new McField[this.Count];
			int i=0;
			foreach(object o in base.List)
			{
				ctl[i]=o as McField;
				i++;
			}
			return ctl;
		}

        //public new int Count
        //{
        //    get
        //    {
        //        return base.List.Count;// m_Count;
        //    }
        //}

        /// <summary>
        /// Get McField item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public McField this[int index]
		{
			get
			{
				return base.List[index] as McField;
			}
		}
        /// <summary>
        /// Get McField item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		public McField this[string key]
		{
			get
			{
				for(int index = 0; index < Count; index++)
				{
					if(((McField)base.List[index]).Key == key)
						return base.List[index] as McField;
				}
                throw new Exception("McField \"" + key + "\" is not a valid field.");
			}
		}
        /// <summary>
        /// Get index of item
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf(McField value)
		{
			return base.List.IndexOf(value);
		}

        /// <summary>
        /// Convert ToDataField
        /// </summary>
        /// <param name="vgf"></param>
        /// <returns></returns>
        public static MControl.Data.DataField[] ToDataField(McField[] vgf)
        {
            int count=vgf.Length;
            int i = 0;
            MControl.Data.DataField[] dataFields = new MControl.Data.DataField[count];
            foreach (McField vg in vgf)
            {
                dataFields[i] = new MControl.Data.DataField(vg.Key, vg.Value);
                i++;
            }
            return dataFields;
        }
    }

}


