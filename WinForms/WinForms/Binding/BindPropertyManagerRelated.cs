using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for BindPropertyManagerRelated.
	/// </summary>
	internal class BindPropertyManagerRelated : BindPropertyManager
	{
		// Fields
		private string dataField;
		private PropertyDescriptor fieldInfo;
		private BindManagerBase parentManager;

		// Methods
		internal BindPropertyManagerRelated(BindManagerBase parentManager, string dataField) : base(parentManager.Current, dataField)
		{
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if (this.fieldInfo == null)
			{
				throw new ArgumentException("RelatedListManagerChild", dataField);
			}
			parentManager.CurrentChanged += new EventHandler(this.ParentManager_CurrentChanged);
			this.ParentManager_CurrentChanged(parentManager, EventArgs.Empty);
		}

		public override PropertyDescriptorCollection GetItemProperties()
		{
			PropertyDescriptorCollection itemProperties = this.GetItemProperties(new ArrayList(), new ArrayList());
			if (itemProperties != null)
			{
				return itemProperties;
			}
			return new PropertyDescriptorCollection(null);
		}

		protected internal override PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			dataSources.Insert(0, this.DataSource);
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetItemProperties(dataSources, listAccessors);
		}

		public override string GetListName()
		{
			string listName = this.GetListName(new ArrayList());
			if (listName.Length > 0)
			{
				return listName;
			}
			return base.GetListName();
		}

		protected internal override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		protected internal override void OnCurrentChanged(EventArgs e)
		{
			this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
			base.OnCurrentChanged(e);
		}

		private void ParentManager_CurrentChanged(object sender, EventArgs e)
		{
			this.EndCurrentEdit();
			this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Properties
		internal override Type BindType
		{
			get
			{
				return this.fieldInfo.PropertyType;
			}
		}
	}

 

}
