using System;
using System.Security.Permissions;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Nistec.GridView.Design
{
	[SecurityPermission(SecurityAction.Demand)]
	internal class GridColumnCollectionEditor : CollectionEditor
	{
		// Methods
		public GridColumnCollectionEditor(Type type) : base(type)
		{
		}
		//		protected override Type[] CreateNewItemTypes()
		//		{
		//			return new Type[] { typeof(GridTextColumn)};//, 
		//								  //typeof(GridBoolColumn) };
		//		}
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[] {   typeof(GridTextColumn), 
								  typeof(GridBoolColumn), 
								  typeof(GridComboColumn),
								  typeof(GridDateColumn),
								  typeof(GridLabelColumn),
								  // typeof(GridLinkColumn),
								  typeof(GridButtonColumn),
								  typeof(GridProgressColumn),
								  typeof(GridIconColumn),
								  typeof(GridMultiColumn),
								  typeof(GridNumericColumn),
								  typeof(GridControlColumn),
								  typeof(VGridColumn),
                                  typeof(GridMemoColumn)
		};

		}
	}
}
