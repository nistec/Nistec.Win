using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Design;

namespace mControl.WinCtl.Controls
{

	[TypeConverter(typeof(ListBindingConverter))]
	public class Binder : Binding
	{
		#region Fields

		private BindManagerBase bindingManagerBase;
		private BindToObject bindToObject;
		private bool bound;
		private bool inSetPropValue;
		private bool modified;
		
		private ConvertEventHandler onFormat;
		private ConvertEventHandler onParse;
		private PropertyDescriptor propInfo;
		private PropertyDescriptor propIsNullInfo;

		private string propertyName;
		private EventDescriptor changedInfo;
		private CtlBindControl control;
		private EventDescriptor validateInfo;

		// Events
        //public event ConvertEventHandler Format;
        //public event ConvertEventHandler Parse;

		public EventDescriptor ValidateInfo;
		public EventDescriptor ChangedInfo;

		#endregion

		#region Ctor

        public Binder(string propertyName, object dataSource, string dataMember)
            : base(propertyName, dataSource, dataMember)
		{
			this.bindToObject = null;
			this.propertyName = "";
			this.bound = false;
			this.modified = false;
			this.inSetPropValue = false;
			this.onParse = null;
			this.onFormat = null;
			this.bindToObject = new BindToObject(this, dataSource, dataMember);
			this.propertyName = propertyName;
			this.CheckBinding();

			this.propIsNullInfo=null;
			this.propInfo=null;
	
		}

		#endregion

		#region Methods

		private void BindTarget(bool bind)
		{
			if (bind)
			{
				if (this.IsBinding)
				{
					if (this.ChangedInfo != null)
					{
						EventHandler handler1 = new EventHandler(this.Target_PropertyChanged);
						this.ChangedInfo.AddEventHandler(this.Control, handler1);
					}
					if (this.ValidateInfo != null)
					{
						CancelEventHandler handler2 = new CancelEventHandler(this.Target_Validate);
                        this.ValidateInfo.AddEventHandler(this.Control, handler2);
					}
				}
			}
			else
			{
				if (this.ChangedInfo != null)
				{
					EventHandler handler3 = new EventHandler(this.Target_PropertyChanged);
                    this.ChangedInfo.RemoveEventHandler(this.Control, handler3);
				}
				if (this.ValidateInfo != null)
				{
					CancelEventHandler handler4 = new CancelEventHandler(this.Target_Validate);
					this.ValidateInfo.RemoveEventHandler(this.Control, handler4);
				}
			}
		}

		private bool ControlAtDesignTime()
		{
			ISite site1 = this.Control.Site;
			if (site1 == null)
			{
				return false;
			}
			return site1.DesignMode;
		}

		private object FormatObject(object value)
		{
			if (!this.ControlAtDesignTime())
			{
				Type type1 = this.propInfo.PropertyType;
				if (type1 != typeof(object))
				{
					ConvertEventArgs args1 = new ConvertEventArgs(value, type1);
					this.OnFormat(args1);
					object obj1 = args1.Value;
					if (!obj1.GetType().IsSubclassOf(type1) && (obj1.GetType() != type1))
					{
						TypeConverter converter1 = TypeDescriptor.GetConverter(value.GetType());
						if ((converter1 == null) || !converter1.CanConvertTo(type1))
						{
							if (value is IConvertible)
							{
								obj1 = Convert.ChangeType(value, type1);
								if (obj1.GetType().IsSubclassOf(type1) || (obj1.GetType() == type1))
								{
									return obj1;
								}
							}
							throw new FormatException("ListBindingFormatFailed");
						}
						return converter1.ConvertTo(value, type1);
					}
					return obj1;
				}
				return value;
			}
			return value;
		}


        private void FormLoaded(object sender, EventArgs e)
        {
            this.CheckBinding();
        }

		private object GetPropValue()
		{
			bool flag1 = false;
			if (this.propIsNullInfo != null)
			{
				flag1 = (bool) this.propIsNullInfo.GetValue(this.Control);
			}
			if (flag1)
			{
				return Convert.DBNull;
			}
			object obj1 = this.propInfo.GetValue(this.Control);
			if (obj1 == null)
			{
				obj1 = Convert.DBNull;
			}
			return obj1;
		}

		protected override void OnFormat(ConvertEventArgs cevent)
		{
			if (this.onFormat != null)
			{
				this.onFormat(this, cevent);
			}
			if ((!(cevent.Value is DBNull) && (cevent.DesiredType != null)) && (!cevent.DesiredType.IsInstanceOfType(cevent.Value) && (cevent.Value is IConvertible)))
			{
				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType);
			}
		}

 
		protected override void OnParse(ConvertEventArgs cevent)
		{
			if (this.onParse != null)
			{
				this.onParse(this, cevent);
			}
			if (((!(cevent.Value is DBNull) && (cevent.Value != null)) && ((cevent.DesiredType != null) && !cevent.DesiredType.IsInstanceOfType(cevent.Value))) && (cevent.Value is IConvertible))
			{
				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType);
			}
		}

 
		private object ParseObject(object value)
		{
			Type type1 = this.bindToObject.BindToType;
			ConvertEventArgs args1 = new ConvertEventArgs(value, type1);
			this.OnParse(args1);
			if ((args1.Value.GetType().IsSubclassOf(type1) || (args1.Value.GetType() == type1)) || (args1.Value is DBNull))
			{
				return args1.Value;
			}
			TypeConverter converter1 = TypeDescriptor.GetConverter(value.GetType());
			if ((converter1 != null) && converter1.CanConvertTo(type1))
			{
				return converter1.ConvertTo(value, type1);
			}
			if (value is IConvertible)
			{
				object obj1 = Convert.ChangeType(value, type1);
				if (obj1.GetType().IsSubclassOf(type1) || (obj1.GetType() == type1))
				{
					return obj1;
				}
			}
			return null;
		}

 
		internal void PullData()
		{
			if (this.IsBinding && (this.modified || (this.ChangedInfo == null)))
			{
				object obj2;
				object obj1 = this.GetPropValue();
				bool flag1 = false;
				try
				{
					obj2 = this.ParseObject(obj1);
				}
				catch (Exception)
				{
					flag1 = true;
					obj2 = this.bindToObject.GetValue();
				}
				if (obj2 == null)
				{
					flag1 = true;
					obj2 = this.bindToObject.GetValue();
				}
				object obj3 = this.FormatObject(obj2);
				this.SetPropValue(obj3);
				if (!flag1)
				{
					this.bindToObject.SetValue(obj2);
				}
				this.modified = false;
			}
		}

		internal void PushData()
		{
			if (this.IsBinding)
			{
				object obj1 = this.bindToObject.GetValue();
				obj1 = this.FormatObject(obj1);
				this.SetPropValue(obj1);
				this.modified = false;
			}
			else
			{
				this.SetPropValue(null);
			}
		}

		internal void SetListManager(BindManagerBase bindingManagerBase)
		{
			this.bindingManagerBase = bindingManagerBase;
			this.BindToObject.SetBindingManagerBase(bindingManagerBase);
			this.CheckBinding();
		}

 
		private void SetPropValue(object value)
		{
			if (!this.ControlAtDesignTime())
			{
				this.inSetPropValue = true;
				try
				{
					if ((value == null) || Convert.IsDBNull(value))
					{
						if (this.propIsNullInfo != null)
						{
							this.propIsNullInfo.SetValue(this.Control, true);
						}
						else if (this.propInfo.PropertyType == typeof(object))
						{
							this.propInfo.SetValue(this.Control, Convert.DBNull);
						}
						else
						{
							this.propInfo.SetValue(this.Control, null);
						}
					}
					else
					{
						this.propInfo.SetValue(this.Control, value);
					}
				}
				finally
				{
					this.inSetPropValue = false;
				}
			}
		}

		private void Target_PropertyChanged(object sender, EventArgs e)
		{
			if (!this.inSetPropValue && this.IsBinding)
			{
				this.modified = true;
			}
		}

		private void Target_Validate(object sender, CancelEventArgs e)
		{
			try
			{
				this.PullData();
			}
			catch (Exception)
			{
				e.Cancel = true;
			}
		}

		internal void UpdateIsBinding()
		{
            bool flag1 = (this.IsBindable && this.Control.Created) && this.bindingManagerBase.IsBinding;
            //bool flag1 = (this.IsBindable && this.Control.IsBinding) && this.bindingManagerBase.IsBinding;
			if (this.IsBinding != flag1)
			{
				this.bound = flag1;
                
				this.BindTarget(flag1);
                flag1 = this.IsBinding;
                if (flag1)
				{
					this.PushData();
				}
			}
		}

		internal static void UpdateBinding(BindContext newBindingContext, Binder binding)
		{
		      BindManagerBase base1 = binding.BindManagerBase;

		      if (base1 != null)
		      {
		            base1.Bindings.Remove(binding);
		      }
		      if (newBindingContext != null)
		      {
		            if (binding.BindToObject.BindManagerBase is BindPropertyManager)
		            {
		                  BindContext.CheckPropertyBindCycles(newBindingContext, binding);
		            }
		            BindToObject obj1 = binding.BindToObject;
					BindManagerBase base2 = newBindingContext.EnsureBindListManager(obj1.DataSource, obj1.BindingMemberInfo.BindingPath);
					base2.Bindings.Add(binding);
	    
		            //BindingManagerBase base2 = newBindingContext.EnsureListManager(obj1.DataSource, obj1.BindingMemberInfo.BindingPath);
				    //base2.Bindings.Add(binding);
		      }
		}


		#endregion

		#region Properties

		public BindManagerBase BindManagerBase
		{
			get
			{
				return this.bindingManagerBase;
			}
		}
 
        public new BindingMemberInfo BindingMemberInfo
        {
            get
            {
                return this.bindToObject.BindingMemberInfo;
            }
        }

        internal BindToObject BindToObject
        {
            get
            {
                return this.bindToObject;
            }
        }
 
        //[DefaultValue((string) null)]
        //public Control Control
        //{
        //    [SecurityPermission(SecurityAction.LinkDemand)]
        //    get
        //    {
        //        return this.Control;
        //    }
        //}

        public new object DataSource
        {
            get
            {
                return this.bindToObject.DataSource;
            }
        }
 
		internal bool IsBindable
		{
			get
			{
				if (((this.Control != null) && (this.PropertyName.Length > 0)) && (this.bindToObject.DataSource != null))
				{
					return (this.bindingManagerBase != null);
				}
				return false;
			}
		}
        public new bool IsBinding
        {
            get
            {
                return this.bound;
            }
        }
 
        [DefaultValue("")]
        public new string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

		#endregion

		#region base

		private void CheckBinding()
		{
		    this.bindToObject.CheckBinding();
		    if ((this.Control == null) || (this.propertyName.Length <= 0))
		    {
		        this.propInfo = null;
		        this.changedInfo = null;
		        this.validateInfo = null;
		    }
		    else
		    {
		        PropertyDescriptorCollection collection1;
		        this.Control.DataBindings.CheckDuplicates(this);
		        Type type1 = this.Control.GetType();
		        string text1 = this.propertyName + "IsNull";
		        PropertyDescriptor descriptor1 = null;
		        PropertyDescriptor descriptor2 = null;
		        InheritanceAttribute attribute1 = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.Control)[typeof(InheritanceAttribute)];
		        if ((attribute1 != null) && (attribute1.InheritanceLevel != InheritanceLevel.NotInherited))
		        {
		            collection1 = TypeDescriptor.GetProperties(type1);
		        }
		        else
		        {
		            collection1 = TypeDescriptor.GetProperties(this.Control);
		        }
		        for (int num1 = 0; num1 < collection1.Count; num1++)
		        {
		            if (string.Compare(collection1[num1].Name, this.propertyName, true, CultureInfo.InvariantCulture) == 0)
		            {
		                descriptor1 = collection1[num1];
		                if (descriptor2 != null)
		                {
		                    break;
		                }
		            }
		            if (string.Compare(collection1[num1].Name, text1, true, CultureInfo.InvariantCulture) == 0)
		            {
		                descriptor2 = collection1[num1];
		                if (descriptor1 != null)
		                {
		                    break;
		                }
		            }
		        }
		        if (descriptor1 == null)
		        {
		            throw new ArgumentException("ListBindingBindProperty", this.propertyName);
		        }
		        if (descriptor1.IsReadOnly)
		        {
		            throw new ArgumentException("ListBindingBindPropertyReadOnly", this.propertyName);
		        }
		        this.propInfo = descriptor1;
		        Type type2 = this.propInfo.PropertyType;
		        if (((descriptor2 != null) && (descriptor2.PropertyType == typeof(bool))) && !descriptor2.IsReadOnly)
		        {
		            this.propIsNullInfo = descriptor2;
		        }
		        EventDescriptor descriptor3 = null;
		        string text2 = this.propertyName + "Changed";
		        EventDescriptor descriptor4 = null;
		        string text3 = "Validating";
		        EventDescriptorCollection collection2 = TypeDescriptor.GetEvents(this.Control);
		        for (int num2 = 0; num2 < collection2.Count; num2++)
		        {
		            if (string.Compare(collection2[num2].Name, text2, true, CultureInfo.InvariantCulture) == 0)
		            {
		                descriptor3 = collection2[num2];
		                if (descriptor4 != null)
		                {
		                    break;
		                }
		            }
		            if (string.Compare(collection2[num2].Name, text3, true, CultureInfo.InvariantCulture) == 0)
		            {
		                descriptor4 = collection2[num2];
		                if (descriptor3 != null)
		                {
		                    break;
		                }
		            }
		        }
		        this.changedInfo = descriptor3;
		        this.validateInfo = descriptor4;
		    }
		    this.UpdateIsBinding();
		}

		
		internal void SetControl(CtlBindControl value)
		{
		    if (this.Control != value)
		    {
		        CtlBindControl control1 = this.control;
		        this.BindTarget(false);
		        this.Control = value;
		        this.BindTarget(true);
		        try
		        {
		            this.CheckBinding();
		        }
		        catch (Exception exception1)
		        {
		            this.BindTarget(false);
		            this.Control = control1;
		            this.BindTarget(true);
		            throw exception1;
		        }
		        BindContext.UpdateBinding(((this.Control != null) && this.Control.Created) ? ((CtlBindControl)this.Control).BindContext : null, this);
		        Form form1 = value.FindForm();// as Form;
		        if (form1 != null)
		        {
		            form1.Load += new EventHandler(this.FormLoaded);
		        }
		    }
		}

		public new CtlBindControl Control
		{
			get{return this.control;}
			set{this.control=value;}
		}

		#endregion

	}

	#region ControlBindCollection

	[Editor("System.Drawing.Design.UITypeEditor, System.Drawing, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), TypeConverter("System.Windows.Forms.Design.ControlBindingsConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultEvent("CollectionChanged")]
	public class ControlBindCollection : BindCollectionBase
	{
		// Methods


		internal ControlBindCollection(CtlBindControl control)
		{
			this.control = control;
		}

		public void Add(Binder binding)
		{
			this.AddCore(binding);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, binding));
		}

		public Binder Add(string propertyName, object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			Binder binding1 = new Binder(propertyName, dataSource, dataMember);
			this.Add(binding1);
			return binding1;
		}

		protected void AddCore(Binder dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.Control == this.control)
			{
				throw new ArgumentException("BindingsCollectionAdd1");
			}
			if (dataBinding.Control != null)
			{
				throw new ArgumentException("BindingsCollectionAdd2");
			}
			dataBinding.SetControl(this.control);
			base.AddCore(dataBinding);
		}

		internal void CheckDuplicates(Binder binding)
		{
			if (binding.PropertyName.Length != 0)
			{
				for (int num1 = 0; num1 < this.Count; num1++)
				{
					if (((binding != base[num1]) && (base[num1].PropertyName.Length > 0)) && (string.Compare(binding.PropertyName, base[num1].PropertyName, false, CultureInfo.InvariantCulture) == 0))
					{
						throw new ArgumentException("BindingsCollectionDup ", "binding");
					}
				}
			}
		}

		public new void Clear()
		{
			base.Clear();
		}

		protected override void ClearCore()
		{
			int num1 = this.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				this[num2].SetControl(null);
			}
			base.ClearCore();
		}

 
		public void Remove(Binder binding)
		{
			base.Remove(binding);
		}

		public new void RemoveAt(int index)
		{
			base.RemoveAt(index);
		}

		protected  void RemoveCore(Binder dataBinding)
		{
			if (dataBinding.Control != this.control)
			{
				throw new ArgumentException("BindingsCollectionForeign");
			}
			dataBinding.SetControl(null);
			base.RemoveCore(dataBinding);
		}


		// Properties
		public Control Control
		{
			get
			{
				return this.control;
			}
		}
 
		public Binder this[string propertyName]
		{
			get
			{
				foreach (Binder binding1 in base.List)
				{
					if (string.Compare(binding1.PropertyName, propertyName, true, CultureInfo.InvariantCulture) == 0)
					{
						return binding1;
					}
				}
				return null;
			}
		}

		// Fields
		internal CtlBindControl control;
	}
 
	#endregion

	#region BindCollectionBase

	[DefaultEvent("CollectionChanged")]
	public class BindCollectionBase : BaseCollection
	{
		// Events
		[Description("collectionChangedEvent")]
		public event CollectionChangeEventHandler CollectionChanged;

		// Methods
		internal BindCollectionBase()
		{
			
		}

		protected internal void Add(Binding binding)
		{
			this.AddCore(binding);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, binding));
		}

 
		protected virtual void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			this.List.Add(dataBinding);
		}

		protected internal void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

 
		protected virtual void ClearCore()
		{
			this.List.Clear();
		}

		private void OnBadIndex(object index)
		{
			throw new IndexOutOfRangeException("BindingsCollectionBadIndex " + index.ToString());
		}

 
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		protected internal void Remove(Binding binding)
		{
			this.RemoveCore(binding);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, binding));
		}

		protected internal void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		protected virtual void RemoveCore(Binding dataBinding)
		{
			this.List.Remove(dataBinding);
		}

		protected internal bool ShouldSerializeMyAll()
		{
			return (this.Count > 0);
		}


		// Properties
		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return base.Count;
			}
		}
		public Binder this[int index]
		{
			get
			{
				return (Binder) this.List[index];
			}
		}
		protected override ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}


		// Fields
		private ArrayList list;
		//private CollectionChangeEventHandler onCollectionChanged;
	}

	#endregion

}



