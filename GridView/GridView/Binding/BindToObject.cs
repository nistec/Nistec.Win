using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace mControl.WinCtl.Controls
{

	internal class BindToObject
	{
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
				ISite site1 = this.owner.Control.Site;
				if ((site1 != null) && site1.DesignMode)
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
				string text1 = this.dataMember.BindingField;
				this.fieldInfo = this.owner.BindManagerBase.GetItemProperties().Find(text1, true);
				if ((this.fieldInfo == null) && (text1.Length > 0))
				{
					throw new ArgumentException("ListBindingBindField",  text1 );
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
			object obj1 = this.bindingManager.Current;
			if (this.fieldInfo != null)
			{
				return this.fieldInfo.GetValue(obj1);
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
				object obj1 = this.bindingManager.Current;
				if (obj1 is IEditableObject)
				{
					((IEditableObject) obj1).BeginEdit();
				}
				if (!this.fieldInfo.IsReadOnly)
				{
					this.fieldInfo.SetValue(obj1, value);
				}
			}
			else
			{
				BindManager manager1 = this.bindingManager as BindManager;
				if (manager1 != null)
				{
					manager1[manager1.Position] = value;
				}
			}
		}

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
					Type type1 = this.bindingManager.BindType;
					if (typeof(Array).IsAssignableFrom(type1))
					{
						type1 = type1.GetElementType();
					}
					return type1;
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
					object obj1 = this.dataSource;
					this.dataSource = value;
					try
					{
						this.CheckBinding();
					}
					catch
					{
						this.dataSource = obj1;
						throw;
					}
				}
			}
		}

		// Fields
		private BindManagerBase bindingManager;
		private BindingMemberInfo dataMember;
		private object dataSource;
		private PropertyDescriptor fieldInfo;
		private Binder owner;
	}
}