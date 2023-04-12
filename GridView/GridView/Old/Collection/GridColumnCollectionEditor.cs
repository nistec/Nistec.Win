using System;
using System.Security.Permissions;
using System.ComponentModel.Design;
using System.Windows.Forms;
using mControl.GridStyle.Columns;

namespace mControl.GridStyle
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
	internal class GridColumnCollectionEditor :System.ComponentModel.Design.CollectionEditor
	{

		public GridColumnCollectionEditor(Type type) : base(type)
		{
		}

        
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[] {   typeof(GridTextColumn), 
								  typeof(GridBoolColumn), 
								  typeof(GridComboColumn),
								  typeof(GridDateColumn),
								  typeof(GridLabelColumn),
								  typeof(GridLinkColumn),
								  typeof(GridButtonColumn),
								  typeof(GridProgressColumn),
								  typeof(GridIconColumn),
								  typeof(GridMultiColumn),
								  //typeof(GridEnumColumn),
								  typeof(GridMenuColumn),
								  typeof(GridControlColumn)
							  };
		}


	}

}
