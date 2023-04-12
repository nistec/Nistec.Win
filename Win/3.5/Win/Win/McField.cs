using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using System.Collections;
using System.Windows.Forms;

namespace MControl.Win
{
    public interface IField : IComponent
    {
        string Key{get;set;}
         object Value{get;set;}
         string Text { get;set;}
         object Tag { get;set;}
        FieldType FieldType { get;set;}
         ArrayList Items{get;}
         bool Enabled {get;set;}
         bool ReadOnly {get;set;}
         string Filter {get;set;}
         string Description {get;set;}
         string ToolTip {get;set;}
         string Caption {get;set;}
         string ToString();

    }

    /// <summary>
    /// McField class
    /// </summary>
    [DesignTimeVisible(false),ToolboxItem(false)]
    public class McField : Component, IField//,IDisposable
    {
        private string _Key;
        private object _Value;
        private object _Tag;
        private FieldType _DataType;
        private ArrayList _Items;
        private bool _Enabled = true;
        private bool _ReadOnly = false;
        private string _Filter = "";
        private string _Description = "";
        private string _ToolTip = "";
        private string _Caption="";

        /// <summary>
        /// Value Changed event
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// Property Item Changed event
        /// </summary>
        public event PropertyItemChangedEventHandler PropertyItemChanged;

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
        /// <param name="dataType"></param>
        public McField(string key, object value, FieldType dataType)
        {
            _Key = key;
            _Value = value;
            _DataType = dataType;
        }
        /// <summary>
        /// Initilaized McField
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="list"></param>
        public McField(string key, object value, IList list)
            :this(key, value)
        {
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
                _DataType = FieldType.Bool;
            }
            else if (type.Equals(typeof(System.DateTime)))
            {
                _DataType = FieldType.Date;
            }
            else if (((type.Equals(typeof(short)) || type.Equals(typeof(int))) || (type.Equals(typeof(long)) || type.Equals(typeof(ushort)))) || (((type.Equals(typeof(uint)) || type.Equals(typeof(ulong))) || (type.Equals(typeof(decimal)) || type.Equals(typeof(double)))) || ((type.Equals(typeof(float)) || type.Equals(typeof(byte))) || type.Equals(typeof(sbyte)))))
            {
                _DataType = FieldType.Number;
            }
            else if (type.IsEnum)
            {
                _DataType = FieldType.Number;
                this.Items.AddRange(Enum.GetNames(type));
            }
            else //if (type.Equals(typeof(string)))
            {
                _DataType = FieldType.Text;
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
            //if (owner != null)
            //    this.owner.CellValidated -= new EventHandler(owner_CellValidated);
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Key = null;
                _Value = null;
                _Caption = null;
                _Description = null;
                _Filter = null;
                _Tag = null;
                _ToolTip = null;
                _Caption = null;

                if (_Items != null)
                {
                    _Items.Clear();
                    _Items = null;
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Field Display
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}", this._Key,this.Text);
        }
        
 
        /// <summary>
        /// Raise value changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
            //if (owner != null)
            //    owner.OnFieldChanged(_Key, _Value);
        }

        /// <summary>
        /// Raise PropertyItem changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyItemChanged(PropertyItemChangedEventArgs e)
        {
            if (PropertyItemChanged != null)
                PropertyItemChanged(this, e);
        }

        private void OnPropertyItemChanged(string propertyName, object propertyValue)
        {
            if (PropertyItemChanged != null)
                OnPropertyItemChanged(new PropertyItemChangedEventArgs(propertyName, propertyValue));
        }

         private void SetDefaultValue()
        {
            if (Text != "")
                return;

            switch (_DataType)
            {
                case FieldType.Bool:
                    Value = false;
                    break;
                case FieldType.Date:
                    Value = DateTime.Now;
                    break;
                case FieldType.Number:
                    Value = 0;
                    break;
                default:
                    Value = "";
                    break;
            }
        }

        protected  void SetValue(object value,bool raiseEvent)
        {
            _Value = value;
            if (raiseEvent)
            {
                OnValueChanged(EventArgs.Empty);
            }
        }

 
        /// <summary>
        /// Get or Set key field
        /// </summary>
        public virtual string Key
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
                    if (string.IsNullOrEmpty(_Caption))
                        _Caption = value;
                    OnPropertyItemChanged("Key", value);
                }
            }
        }
        /// <summary>
        /// Get or set value field
        /// </summary>
        public virtual object Value
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
                    OnValueChanged(EventArgs.Empty);
                    OnPropertyItemChanged("Value", value);
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
                if (_Description != value)
                {
                    if (value == null)
                        value = "";
                    _Description = value;
                    OnPropertyItemChanged("Description", value);
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
                    OnPropertyItemChanged("Enabled", value);
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
                    OnPropertyItemChanged("ReadOnly", value);
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
                    OnPropertyItemChanged("Filter", value);
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
                    OnPropertyItemChanged("Tag", value);
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
                    OnPropertyItemChanged("ToolTip", value);
                }
            }
        }
        /// <summary>
        /// Get or Set DataType field
        /// </summary>
        public FieldType FieldType
        {
            get
            {
                return _DataType;
            }
            set
            {
                if (_DataType != value)
                {
                    _DataType = value;
                    SetDefaultValue();
                    OnPropertyItemChanged("DataType", value);
                }
            }
        }
        /// <summary>
        /// Get or Set Caption field
        /// </summary>
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyItemChanged("Caption", value);
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
                if (_Items == null)
                {
                    _Items = new ArrayList();
                }
                return _Items;
            }

        }

    }


    /// <summary>
    /// VGrid Field Collection class
    /// </summary>
    public class McFieldCollection : System.Collections.CollectionBase//, IList, ICollection, IEnumerable//, IEnumerator, IEnumerable
	{
		internal bool m_FireEvents = true;
		
		int m_Count = 0;

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
            
        }

 	
        /// <summary>
        /// Add item to collection
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
		public McField Add(McField field)
		{
            try
            {
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
			if(Contains(field))
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
		public void Insert(McField field, int index)
		{
            try
            {
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
		}
        /// <summary>
        /// Clear collection
        /// </summary>
		public new void Clear()
		{
			m_Count = 0;
			base.List.Clear();
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

    }

}


