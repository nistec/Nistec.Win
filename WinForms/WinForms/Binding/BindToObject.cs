using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for BindToObject.
	/// </summary>
	internal class BindToObject
	{
		// Fields
		private BindManagerBase bindingManager;
		private BindingMemberInfo dataMember;
		private object dataSource;
		private PropertyDescriptor fieldInfo;
		private Binder owner;

		// Methods
		internal BindToObject(Binder owner, object dataSource, string dataMember)
		{
			this.owner = owner;
			this.dataSource = dataSource;
			this.dataMember = new BindingMemberInfo(dataMember);
			this.CheckBinding();
		}

		internal void CheckBinding()
		{
			if ((this.owner != null) && (this.owner.Control != null))
			{
				ISite site = this.owner.Control.Site;
				if ((site != null) && site.DesignMode)
				{
					return;
				}
			}
			if (((this.owner.BindManagerBase != null) && (this.fieldInfo != null)) && (this.owner.BindManagerBase.IsBinding && !(this.owner.BindManagerBase is BindManager)))
			{
				this.fieldInfo.RemoveValueChanged(this.owner.BindManagerBase.Current, new EventHandler(this.PropValueChanged));
			}
			if (((this.owner != null) && (this.owner.BindManagerBase != null)) && ((this.owner.Control != null) && this.owner.Control.Created))
			{
				string name = this.dataMember.BindingField;
				this.fieldInfo = this.owner.BindManagerBase.GetItemProperties().Find(name, true);
				if ((this.fieldInfo == null) && (name.Length > 0))
				{
					throw new ArgumentException("ListBindingBindField",  "dataMember :" + name);
				}
				if (((this.fieldInfo != null) && this.owner.BindManagerBase.IsBinding) && !(this.owner.BindManagerBase is BindManager))
				{
					this.fieldInfo.AddValueChanged(this.owner.BindManagerBase.Current, new EventHandler(this.PropValueChanged));
				}
			}
			else
			{
				this.fieldInfo = null;
			}
		}

		internal object GetValue()
		{
			object component = this.bindingManager.Current;
			if (this.fieldInfo != null)
			{
				return this.fieldInfo.GetValue(component);
			}
			return this.bindingManager.Current;
		}

		private void PropValueChanged(object sender, EventArgs e)
		{
			this.bindingManager.OnCurrentChanged(EventArgs.Empty);
		}

		internal void SetBindingManagerBase(BindManagerBase lManager)
		{
			if (this.bindingManager != lManager)
			{
				if (((this.bindingManager != null) && (this.fieldInfo != null)) && (this.bindingManager.IsBinding && !(this.bindingManager is BindManager)))
				{
					this.fieldInfo.RemoveValueChanged(this.bindingManager.Current, new EventHandler(this.PropValueChanged));
					this.fieldInfo = null;
				}
				this.bindingManager = lManager;
				this.CheckBinding();
			}
		}

		internal void SetValue(object value)
		{
			if (this.fieldInfo != null)
			{
				object component = this.bindingManager.Current;
				if (component is IEditableObject)
				{
					((IEditableObject) component).BeginEdit();
				}
				if (!this.fieldInfo.IsReadOnly)
				{
					this.fieldInfo.SetValue(component, value);
				}
			}
			else
			{
				BindManager manager = this.bindingManager as BindManager;
				if (manager != null)
				{
					manager[manager.Position] = value;
				}
			}
		}

		// Properties
		internal BindManagerBase BindManagerBase
		{
			get
			{
				return this.bindingManager;
			}
		}

		internal BindingMemberInfo BindingMemberInfo
		{
			get
			{
				return this.dataMember;
			}
		}

		internal Type BindToType
		{
			get
			{
				if (this.dataMember.BindingField.Length == 0)
				{
					Type c = this.bindingManager.BindType;
					if (typeof(Array).IsAssignableFrom(c))
					{
						c = c.GetElementType();
					}
					return c;
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.PropertyType;
				}
				return null;
			}
		}

		internal object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.dataSource != value)
				{
					object dataSource = this.dataSource;
					this.dataSource = value;
					try
					{
						this.CheckBinding();
					}
					catch
					{
						this.dataSource = dataSource;
						throw;
					}
				}
			}
		}
	}
 

}
