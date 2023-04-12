using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Nistec.WinForms
{
		
	/// <summary>
	/// Summary description for BindPropertyManager.
	/// </summary>
	public class BindPropertyManager : BindManagerBase
	{
		// Fields
		private bool bound;
		private object dataSource;
		private PropertyDescriptor propInfo;

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
				throw new ArgumentException("PropertyManagerPropDoesNotExist", propName + " " + dataSource.ToString() );
			}
			this.propInfo.AddValueChanged(dataSource, new EventHandler(this.PropertyChanged));
		}

		public override void AddNew()
		{
			throw new NotSupportedException("DataBindingAddNewNotSupportedOnPropertyManager");
		}

		public override void CancelCurrentEdit()
		{
			IEditableObject obj2 = this.Current as IEditableObject;
			if (obj2 != null)
			{
				obj2.CancelEdit();
			}
			base.PushData();
		}

		public override void EndCurrentEdit()
		{
			base.PullData();
			IEditableObject obj2 = this.Current as IEditableObject;
			if (obj2 != null)
			{
				obj2.EndEdit();
			}
		}

		public override PropertyDescriptorCollection GetItemProperties()
		{
			return TypeDescriptor.GetProperties(this.dataSource);
		}

		//internal
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
			base.OnCurrentChanged(ea);
//			if (base.onCurrentChangedHandler != null)
//			{
//				base.onCurrentChangedHandler(this, ea);
//			}
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
				catch (Exception exception)
				{
					this.bound = false;
					this.UpdateIsBinding();
					throw exception;
				}
			}
		}

		internal override void SetDataSource(object dataSource)
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
				catch (Exception exception)
				{
					this.bound = true;
					this.UpdateIsBinding();
					throw exception;
				}
			}
		}

		protected override void UpdateIsBinding()
		{
			for (int i = 0; i < base.Bindings.Count; i++)
			{
				base.Bindings[i].UpdateIsBinding();
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

		internal override object DataSource
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
	}


}
