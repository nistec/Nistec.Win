
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Security.Permissions;

using Nistec.Win32;
using Nistec.Collections;
using Nistec.WinForms;
using Nistec.WinForms.Design;

namespace Nistec.Wizards.Design
{


    internal class McEditListDesigner : ControlDesignerBase//McBaseDesigner
	{
        public McEditListDesigner() { }

		protected override void PostFilterProperties(IDictionary Properties)
		{
			Properties.Remove("ForeColor");
			Properties.Remove("BackColor");
			Properties.Remove("TabStop");
			Properties.Remove("AutoScroll");
			Properties.Remove("AutoScrollMargin");
			Properties.Remove("AutoScrollMinSize");
			Properties.Remove("BackgroundImage");
		}
	}

 
}
