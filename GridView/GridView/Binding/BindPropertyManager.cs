using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace mControl.WinCtl.Controls
{
		

	public class BindPropertyManager : BindManagerBase
	{
		// Methods
		public BindPropertyManager()
		{
		}

		internal BindPropertyManager(object dataSource) : base(dataSource)
		{
		}

		internal BindPropertyManager(object dataSource, string propName) : this(dataSource)
		{
			this.propInfo = TypeDescriptor.GetProperties(dataSource).Find(propName, true);
			if (this.propInfo == null)
			{
				throw new ArgumentException("PropertyManagerPropDoesNotExist ", dataSource.ToString() );
			}
			this.propInfo.AddValueChanged(dataSource, new EventHandler(this.PropertyChanged));
		}

 
		public override void AddNew()
		{
			throw new NotSupportedException("DataBindingAddNewNotSupportedOnPropertyManager");
		}

		public override void CancelCurrentEdit()
		{
			IEditableObject obj1 = this.Current as IEditableObject;
			if (obj1 != null)
			{
				obj1.CancelEdit();
			}
			base.PushData();
		}

		public override void EndCurrentEdit()
		{
			base.PullData();
			IEditableObject obj1 = this.Current as IEditableObject;
			if (obj1 != null)
			{
				obj1.EndEdit();
			}
		}

 
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return TypeDescriptor.GetProperties(this.dataSource);
		}

		public override string GetListName()
		{
			return (TypeDescriptor.GetClassName(this.dataSource) + "." + this.propInfo.Name);
		}

 
		protected internal override string GetListName(ArrayList listAccessors)
		{
			return "";
		}

		protected internal override void OnCurrentChanged(EventArgs ea)
		{
			base.PushData();
			if (base.onCurrentChangedHandler != null)
			{
				//base.onCurrentChangedHandler(this, ea);
				base.OnCurrentChanged(ea);
			}
		}

		private void PropertyChanged(object sender, EventArgs ea)
		{
			this.EndCurrentEdit();
			this.OnCurrentChanged(EventArgs.Empty);
		}

		public override void RemoveAt(int index)
		{
			throw new NotSupportedException("DataBindingRemoveAtNotSupportedOnPropertyManager");
		}

		public override void ResumeBinding()
		{
			this.OnCurrentChanged(new EventArgs());
			if (!this.bound)
			{
				try
				{
					this.bound = true;
					this.UpdateIsBinding();
				}
				catch (Exception exception1)
				{
					this.bound = false;
					this.UpdateIsBinding();
					throw exception1;
				}
			}
		}

		public override void SetDataSource(object dataSource)
		{
			this.dataSource = dataSource;
		}

		public override void SuspendBinding()
		{
			this.EndCurrentEdit();
			if (this.bound)
			{
				try
				{
					this.bound = false;
					this.UpdateIsBinding();
				}
				catch (Exception exception1)
				{
					this.bound = true;
					this.UpdateIsBinding();
					throw exception1;
				}
			}
		}

 
		protected override void UpdateIsBinding()
		{
			for (int num1 = 0; num1 < base.Bindings.Count; num1++)
			{
				base.Bindings[num1].UpdateIsBinding();
			}
		}

 

		// Properties
		internal override Type BindType
		{
			get
			{
				return this.dataSource.GetType();
			}
		}
		public override int Count
		{
			get
			{
				return 1;
			}
		}
 
		public override object Current
		{
			get
			{
				return this.dataSource;
			}
		}
		public override object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}
		public override bool IsBinding
		{
			get
			{
				return true;
			}
		}
 
		public override int Position
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
 

		// Fields
		private bool bound;
		private object dataSource;
		private PropertyDescriptor propInfo;
	}
 

}
