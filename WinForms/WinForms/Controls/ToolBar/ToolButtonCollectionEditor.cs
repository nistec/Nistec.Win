using System;
using System.Security.Permissions;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Nistec.WinForms
{
	[SecurityPermission(SecurityAction.Demand)]
	internal class ToolButtonCollectionEditor : CollectionEditor
	{
		// Methods
        public ToolButtonCollectionEditor(Type type)
            : base(type)
		{
		}
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[] {   
                typeof(McToolButton)
		                        };

		}

        
	}
}
