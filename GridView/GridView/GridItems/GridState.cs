using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;

using System.Security.Permissions;

using Nistec.WinForms;

namespace Nistec.GridView
{

	internal sealed class GridState : ICloneable
	{
		// Methods
		public GridState()
		{
			this.DataSource = null;
			this.DataMember = null;
			this.ListManager = null;
			this.GridRows = new GridRow[0];
			this.GridRowsLength = 0;
			this.GridColumnStyles = null;
			this.FirstVisibleRow = 0;
			this.FirstVisibleCol = 0;
			this.CurrentRow = 0;
			this.CurrentCol = 0;
			this.LinkingRow = null;
		}

		public GridState(Grid dataGrid)
		{
			this.DataSource = null;
			this.DataMember = null;
			this.ListManager = null;
			this.GridRows = new GridRow[0];
			this.GridRowsLength = 0;
			this.GridColumnStyles = null;
			this.FirstVisibleRow = 0;
			this.FirstVisibleCol = 0;
			this.CurrentRow = 0;
			this.CurrentCol = 0;
			this.LinkingRow = null;
			this.PushState(dataGrid);
		}

		public object Clone()
		{
			GridState state1 = new GridState();
			state1.GridRows = this.GridRows;
			state1.DataSource = this.DataSource;
			state1.DataMember = this.DataMember;
			state1.FirstVisibleRow = this.FirstVisibleRow;
			state1.FirstVisibleCol = this.FirstVisibleCol;
			state1.CurrentRow = this.CurrentRow;
			state1.CurrentCol = this.CurrentCol;
			state1.GridColumnStyles = this.GridColumnStyles;
			state1.ListManager = this.ListManager;
			state1.Grid = this.Grid;
			return state1;
		}

		private void DataSource_Changed(object sender,BindItemChangedEventArgs e)
		{
			if (this.Grid != null)
			{
				//this.Grid.ParentRowsDataChanged();
			}
		}

		private void DataSource_MetaDataChanged(object sender, EventArgs e)
		{
			if (this.Grid != null)
			{
				//this.Grid.ParentRowsDataChanged();
			}
		}

 
		public void PullState(Grid dataGrid, bool createColumn)
		{
			dataGrid.Set_ListManager(this.DataSource, this.DataMember, true, createColumn);
			dataGrid.firstVisibleRow = this.FirstVisibleRow;
			dataGrid.firstVisibleCol = this.FirstVisibleCol;
			dataGrid.currentRow = this.CurrentRow;
			dataGrid.currentCol = this.CurrentCol;
			dataGrid.SetGridRows(this.GridRows, this.GridRowsLength);
		}

 
		public void PushState(Grid dataGrid)
		{
			this.DataSource = dataGrid.DataSource;
			this.DataMember = dataGrid.DataMember;
			this.Grid = dataGrid;
			this.GridRows = dataGrid.GridRows;
			this.GridRowsLength = dataGrid.GridRowsLength;
			this.FirstVisibleRow = dataGrid.firstVisibleRow;
			this.FirstVisibleCol = dataGrid.firstVisibleCol;
			this.CurrentRow = dataGrid.currentRow;
			this.GridColumnStyles = new GridColumnCollection(dataGrid.myGridTable);
			this.GridColumnStyles.Clear();
			foreach (GridColumnStyle style1 in dataGrid.myGridTable.GridColumnStyles)
			{
				this.GridColumnStyles.Add(style1);
			}
			this.ListManager = dataGrid.ListManager;
			this.ListManager.ItemChanged += new BindItemChangedEventHandler(this.DataSource_Changed);
			this.ListManager.MetaDataChanged += new EventHandler(this.DataSource_MetaDataChanged);
			this.CurrentCol = dataGrid.currentCol;
		}

		public void RemoveChangeNotification()
		{
			this.ListManager.ItemChanged -= new BindItemChangedEventHandler(this.DataSource_Changed);
			this.ListManager.MetaDataChanged -= new EventHandler(this.DataSource_MetaDataChanged);
		}


		// Properties
//		internal AccessibleObject ParentRowAccessibleObject
//		{
//			get
//			{
//				if (this.parentRowAccessibleObject == null)
//				{
//					this.parentRowAccessibleObject = new GridState.GridStateParentRowAccessibleObject(this);
//				}
//				return this.parentRowAccessibleObject;
//			}
//		}
 

		// Fields
		public int CurrentCol;
		public int CurrentRow;
		public Grid Grid;
		public GridRow[] GridRows;
		public int GridRowsLength;
		public string DataMember;
		public object DataSource;
		public int FirstVisibleCol;
		public int FirstVisibleRow;
		public GridColumnCollection GridColumnStyles;
		public GridRow LinkingRow;
		public BindManager ListManager;
		//private AccessibleObject parentRowAccessibleObject;

		#region Nested Types
//		[ComVisible(true)]
//		internal class GridStateParentRowAccessibleObject : AccessibleObject
//		{
//			// Methods
//			public GridStateParentRowAccessibleObject(GridState owner)
//			{
//				this.owner = null;
//				this.owner = owner;
//			}
//
// 
//			public override AccessibleObject Navigate(AccessibleNavigation navdir)
//			{
//				GridParentRows.GridParentRowsAccessibleObject obj1 = (GridParentRows.GridParentRowsAccessibleObject) this.Parent;
//				switch (navdir)
//				{
//					case AccessibleNavigation.Up:
//					case AccessibleNavigation.Left:
//					case AccessibleNavigation.Previous:
//						return obj1.GetPrev(this);
//
//					case AccessibleNavigation.Down:
//					case AccessibleNavigation.Right:
//					case AccessibleNavigation.Next:
//						return obj1.GetNext(this);
//				}
//				return null;
//			}
//
// 
//
//			// Properties
//			public override Rectangle Bounds
//			{
//				get
//				{
//					GridParentRows rows1 = ((GridParentRows.GridParentRowsAccessibleObject) this.Parent).Owner;
//					Grid grid1 = this.owner.LinkingRow.Grid;
//					Rectangle rectangle1 = rows1.GetBoundsForGridStateAccesibility(this.owner);
//					rectangle1.Y += grid1.ParentRowsBounds.Y;
//					return grid1.RectangleToScreen(rectangle1);
//				}
//			}
//			public override string Name
//			{
//				get
//				{
//					return "AccDGParentRow";
//				}
//			}
// 
//			public override AccessibleObject Parent
//			{
//				get
//				{
//					return this.owner.LinkingRow.Grid.ParentRowsAccessibleObject;
//				}
//			}
//			public override AccessibleRole Role
//			{
//				get
//				{
//					return AccessibleRole.ListItem;
//				}
//			}
//			public override string Value
//			{
//				get
//				{
//					StringBuilder builder1 = new StringBuilder();
//					//CurrencyManager manager1 = (CurrencyManager) this.owner.LinkingRow.Grid.BindingContext[this.owner.DataSource, this.owner.DataMember];
//					BindManager manager1 = (BindManager) this.owner.LinkingRow.Grid.BindingContext[this.owner.DataSource, this.owner.DataMember];
//					int num1 = this.owner.LinkingRow.RowNumber;
//					builder1.Append(this.owner.ListManager.GetListName());
//					builder1.Append(": ");
//					bool flag1 = false;
//					foreach (GridColumnStyle style1 in this.owner.GridColumnStyles)
//					{
//						if (flag1)
//						{
//							builder1.Append(", ");
//						}
//						string text1 = style1.HeaderText;
//						string text2 = style1.PropertyDescriptor.Converter.ConvertToString(style1.PropertyDescriptor.GetValue(manager1[num1]));
//						builder1.Append(text1);
//						builder1.Append(": ");
//						builder1.Append(text2);
//						flag1 = true;
//					}
//					return builder1.ToString();
//				}
//			}
// 
//
//			// Fields
//			private GridState owner;
//		}
		#endregion
	}

}