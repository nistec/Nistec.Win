using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace mControl.WinCtl.Controls.Design
{
	[SecurityPermission(SecurityAction.Demand)]
	internal class CtlTextBaseDesigner : ControlDesigner
	{
		public CtlTextBaseDesigner()
		{
			this.autoSizeChanged = null;
			this.multiLineChanged = null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.autoSizeChanged != null)
				{
					((TextBoxBase) this.Control).AutoSizeChanged -= this.autoSizeChanged;
				}
				if (this.multiLineChanged != null)
				{
					((TextBoxBase) this.Control).MultilineChanged -= this.multiLineChanged;
				}
			}
			base.Dispose(disposing);
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.autoSizeChanged = new EventHandler(this.OnControlPropertyChanged);
			((TextBoxBase) this.Control).AutoSizeChanged += this.autoSizeChanged;
			this.multiLineChanged = new EventHandler(this.OnControlPropertyChanged);
			((TextBoxBase) this.Control).MultilineChanged += this.multiLineChanged;
		}

		private void OnControlPropertyChanged(object sender, EventArgs e)
		{
			ISelectionUIService service1 = (ISelectionUIService) this.GetService(typeof(ISelectionUIService));
			if (service1 != null)
			{
				service1.SyncComponent((IComponent) sender);
			}
		}

		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] textArray1 = new string[] { "Text" };
			Attribute[] attributeArray1 = new Attribute[0];
			for (int num1 = 0; num1 < textArray1.Length; num1++)
			{
				PropertyDescriptor descriptor1 = (PropertyDescriptor) properties[textArray1[num1]];
				if (descriptor1 != null)
				{
					properties[textArray1[num1]] = TypeDescriptor.CreateProperty(typeof(TextBoxBaseDesigner), descriptor1, attributeArray1);
				}
			}
		}

		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules rules1 = base.SelectionRules;
				object obj1 = base.Component;
				PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(obj1)["AutoSize"];
				if (descriptor1 != null)
				{
					bool flag1 = (bool) descriptor1.GetValue(obj1);
					PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(obj1)["Multiline"];
					bool flag2 = false;
					if (descriptor2 != null)
					{
						flag2 = (bool) descriptor2.GetValue(obj1);
					}
					if (flag1 && !flag2)
					{
						rules1 &= ~(SelectionRules.BottomSizeable | SelectionRules.TopSizeable);
					}
				}
				return rules1;
			}
		}
 
		private string Text
		{
			get
			{
				return this.Control.Text;
			}
			set
			{
				this.Control.Text = value;
				((TextBoxBase) this.Control).Select(0, 0);
			}
		}

		// Fields
		private EventHandler autoSizeChanged;
		private EventHandler multiLineChanged;
	}
 

}
