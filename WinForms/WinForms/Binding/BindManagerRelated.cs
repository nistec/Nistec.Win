using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Nistec.WinForms
{
		
	/// <summary>
	/// Summary description for RelatedBindManager.
	/// </summary>
	internal class BindManagerRelated : BindManager
	{
		// Fields
		private string dataField;
		private PropertyDescriptor fieldInfo;
		private BindManagerBase parentManager;

		// Methods
		internal BindManagerRelated(BindManagerBase parentManager, string dataField) : base(null)
		{
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if ((this.fieldInfo == null) || !typeof(IList).IsAssignableFrom(this.fieldInfo.PropertyType))
			{
				throw new ArgumentException("RelatedListManagerChild "+ dataField);
			}
			base.finalType = this.fieldInfo.PropertyType;
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
			listAccessors.Insert(0, this.fieldInfo);
			dataSources.Insert(0, this.DataSource);
			return this.parentManager.GetItemProperties(dataSources, listAccessors);
		}
		//internal
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

		private void ParentManager_CurrentChanged(object sender, EventArgs e)
		{
			int listposition = base.listposition;
			try
			{
				base.PullData();
			}
			catch
			{
			}
			if (this.parentManager is BindManager)
			{
				BindManager parentManager = (BindManager) this.parentManager;
				if (parentManager.Count > 0)
				{
					this.SetDataSource(this.fieldInfo.GetValue(parentManager.Current));
					base.listposition = (this.Count > 0) ? 0 : -1;
				}
				else
				{
					parentManager.AddNew();
					parentManager.CancelCurrentEdit();
				}
			}
			else
			{
				this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
				base.listposition = (this.Count > 0) ? 0 : -1;
			}
			if (listposition != base.listposition)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Properties
		internal BindManager ParentManager
		{
			get
			{
				return (this.parentManager as BindManager);
			}
		}
	}
}
