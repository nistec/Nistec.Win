using System;
using System.Security.Permissions;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

using Nistec.GridView;

namespace Nistec.GridView.Design
{

	[SecurityPermission(SecurityAction.Demand)]
	internal class GridColumnMappingNameEditor : UITypeEditor
	{
		// Methods
		public GridColumnMappingNameEditor()
		{
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService service1 = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if ((service1 == null) || (context.Instance == null))
				{
					return value;
				}
				if (this.designBindingPicker == null)
				{
					this.designBindingPicker = new DesignBindPicker(context, false, false);
				}
				object obj2 = context.Instance;
				GridColumnStyle style1 = (GridColumnStyle) context.Instance;
				if ((style1.GridTableStyle == null) || (style1.GridTableStyle.dataGrid == null))
				{
					return value;
				}
				PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(style1.GridTableStyle.dataGrid)["DataSource"];
				if (descriptor1 == null)
				{
					return value;
				}
				object obj1 = descriptor1.GetValue(style1.GridTableStyle.dataGrid);
				if (obj1 != null)
				{
					this.designBindingPicker.Start(context, service1, obj1, new DesignBinding(null, (string) value));
					service1.DropDownControl(this.designBindingPicker);
					if (this.designBindingPicker.SelectedItem != null)
					{
						if (string.Empty.Equals(this.designBindingPicker.SelectedItem.DataMember) || (this.designBindingPicker.SelectedItem.DataMember == null))
						{
							value = "";
						}
						else
						{
							value = this.designBindingPicker.SelectedItem.DataField;
						}
					}
					this.designBindingPicker.End();
					return value;
				}
				this.designBindingPicker.Start(context, service1, obj1, new DesignBinding(null, (string) value));
				service1.DropDownControl(this.designBindingPicker);
				this.designBindingPicker.End();
			}
			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}


		// Fields
		private DesignBindPicker designBindingPicker;
	}
 


}
