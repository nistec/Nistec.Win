using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;


namespace Nistec.WinForms.Design
{

	#region McImageIndexEditor

	internal class McImageIndexEditor : UITypeEditor
	{
		public McImageIndexEditor()
		{
			this.imageEditor = (UITypeEditor) TypeDescriptor.GetEditor(typeof(Image), typeof(UITypeEditor));
		}

 
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			this.SetImageList(context);
			return base.GetEditStyle(context);
		}

		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return this.imageEditor.GetPaintValueSupported(context);
		}

		public override void PaintValue(PaintValueEventArgs e)
		{
			this.SetImageList(e.Context);
			if (((this.imageList != null) && (this.imageList.Images.Count != 0)) && ((this.imageEditor != null) && (e.Value is int)))
			{
				int num1 = (int) e.Value;
				if ((num1 >= 0) && (num1 <= this.imageList.Images.Count))
				{
					this.imageEditor.PaintValue(new PaintValueEventArgs(e.Context, this.imageList.Images[num1], e.Graphics, e.Bounds));
				}
			}
		}

		private void SetImageList(ITypeDescriptorContext context)
		{
			foreach (Component component1 in context.Container.Components)
			{
				if (component1 is IMenu )
				{
					this.imageList = ((IMenu) component1).GetImageList();
					//this.imageList = ((MenuProvider) component1).GetImageList((MenuItem) context.Instance);
					return;
				}
//				else if (component1 is PopUpItem )
//				{
//					this.imageList = ((PopUpItem) component1).GetImageList();
//					return;
//				}
			}
		}


		// Fields
		private UITypeEditor imageEditor;
		private ImageList imageList;
	}
	#endregion

	#region McBindListEditor not complit

	internal class McBindListEditor : UITypeEditor
	{
		public McBindListEditor()
		{
			this.listEditor = (UITypeEditor) TypeDescriptor.GetEditor(typeof(object), typeof(UITypeEditor));
		}

 
//		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
//		{
//			this.SetBindList(context);
//			return base.GetEditStyle(context);
//		}

		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return this.listEditor.GetPaintValueSupported(context);
		}

		public override void PaintValue(PaintValueEventArgs e)
		{
			this.SetBindList(e.Context);
			if (((this.List != null) && (this.List.Count != 0)) && ((this.listEditor != null) && (e.Value is int)))
			{
				int num1 = (int) e.Value;
				if ((num1 >= 0) && (num1 <= this.List.Count))
				{
					this.listEditor.PaintValue(new PaintValueEventArgs(e.Context, this.List[num1], e.Graphics, e.Bounds));
				}
			}
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService service1 = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (service1 == null)
				{
					return value;
				}
				if (this.listCombo == null)
				{
					this.listCombo = new  Nistec.WinForms.Controls.McListItems();//context, true, false);
					//this.listCombo.Items.AddRange();
				}
				//this.designBindingPicker.Start(context, service1, null, (DesignBinding) value);
				service1.DropDownControl(this.listCombo);
				value = this.listCombo.SelectedItem;
				//value = this.designBindingPicker.SelectedItem;
				//this.designBindingPicker.End();
			}
			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			this.SetBindList(context);
			return UITypeEditorEditStyle.DropDown;
		}

		private void SetBindList(ITypeDescriptorContext context)
		{
			foreach (Component component1 in context.Container.Components)
			{
				if (component1 is McNavBase )
				{
					this.List =(ArrayList) ((McNavBase) component1).BindingFields();
					//this.imageList = ((MenuProvider) component1).GetImageList((MenuItem) context.Instance);
					return;
				}
			}
		}

    
		// Fields
		private UITypeEditor listEditor;
		private ArrayList List;
		private Nistec.WinForms.Controls.McListItems listCombo;
	}
	#endregion

	
}
