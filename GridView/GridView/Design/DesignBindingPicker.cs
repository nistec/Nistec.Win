using System;
using System.Security.Permissions;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

using Nistec.Win32;

namespace Nistec.GridView.Design
{

	[ToolboxItem(false), DesignTimeVisible(false), SecurityPermission(SecurityAction.Demand)]
	internal class DesignBindPicker : TreeView
	{
		// Methods
		static DesignBindPicker()
		{
			DesignBindPicker.BINDER_IMAGE = 0;
			DesignBindPicker.COLUMN_IMAGE = 1;
			DesignBindPicker.NONE_IMAGE = 2;
		}

 
		public DesignBindPicker(ITypeDescriptorContext context, bool multipleDataSources, bool selectLists)
		{
			this.expansionSignClicked = false;
			this.allowSelection = false;
			this.selectedItem = null;
			this.selectedNode = null;
			this.multipleDataSources = multipleDataSources;
			this.selectLists = selectLists;
			Image image1 = new Bitmap(typeof(DesignBindPicker), "DataPickerImages.bmp");
			ImageList list1 = new ImageList();
			list1.TransparentColor = Color.Lime;
			list1.Images.AddStrip(image1);
			base.ImageList = list1;
		}

		public void End()
		{
			base.Nodes.Clear();
			this.edSvc = null;
			this.selectedItem = null;
			this.ExpansionSignClicked = false;
		}

 
		protected void FillDataMembers(BindingContext bindingManager, object dataSource, string dataMember, string propertyName, bool isList, TreeNodeCollection nodes, int depth)
		{
			if ((depth <= 10) && (isList || !this.selectLists))
			{
				DesignBindPicker.DataMemberNode node1 = new DesignBindPicker.DataMemberNode(dataMember, propertyName, isList);
				nodes.Add(node1);
				if ((this.selectedItem != null) && this.selectedItem.Equals(dataSource, dataMember))
				{
					this.selectedNode = node1;
				}
				if (isList)
				{
					PropertyDescriptorCollection collection1 = ((CurrencyManager) bindingManager[dataSource, dataMember]).GetItemProperties();
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						ListBindableAttribute attribute1 = (ListBindableAttribute) collection1[num1].Attributes[typeof(ListBindableAttribute)];
						if ((attribute1 == null) || attribute1.ListBindable)
						{
							this.FillDataMembers(bindingManager, dataSource, dataMember + "." + collection1[num1].Name, collection1[num1].Name, typeof(IList).IsAssignableFrom(collection1[num1].PropertyType), node1.Nodes, depth + 1);
						}
					}
				}
			}
		}

 
		protected void FillDataSource(BindingContext bindingManager, object component)
		{
			if (((component is IListSource) || (component is IList)) || (component is Array))
			{
				PropertyDescriptorCollection collection1 = ((CurrencyManager) bindingManager[component]).GetItemProperties();
				if (collection1.Count > 0)
				{
					TreeNodeCollection collection2 = base.Nodes;
					if (this.multipleDataSources)
					{
						TreeNode node1 = new DesignBindPicker.DataSourceNode((IComponent) component);
						base.Nodes.Add(node1);
						if ((this.selectedItem != null) && this.selectedItem.Equals(component, ""))
						{
							this.selectedNode = node1;
						}
						collection2 = node1.Nodes;
					}
					for (int num1 = 0; num1 < collection1.Count; num1++)
					{
						this.FillDataMembers(bindingManager, component, collection1[num1].Name, collection1[num1].Name, typeof(IList).IsAssignableFrom(collection1[num1].PropertyType), collection2, 0);
					}
				}
			}
		}

 
		protected void FillDataSources(ITypeDescriptorContext context, object dataSource)
		{
			base.Nodes.Clear();
			BindingContext context1 = new BindingContext();
			if (this.multipleDataSources)
			{
				foreach (IComponent component1 in context.Container.Components)
				{
					this.FillDataSource(context1, component1);
				}
			}
			else
			{
				this.FillDataSource(context1, dataSource);
			}
			TreeNode node1 = new DesignBindPicker.NoneNode();
			base.Nodes.Add(node1);
			if (this.selectedNode == null)
			{
				this.selectedNode = node1;
			}
			base.SelectedNode = this.selectedNode;
			this.selectedNode = null;
			this.selectedItem = null;
			this.allowSelection = true;
		}

		private int GetMaxItemWidth(TreeNodeCollection nodes)
		{
			int num1 = 0;
			foreach (TreeNode node1 in nodes)
			{
				Rectangle rectangle1 = node1.Bounds;
				int num2 = rectangle1.Left + rectangle1.Width;
				bool flag1 = node1.IsExpanded;
				try
				{
					num1 = Math.Max(num2, num1);
					node1.Expand();
					num1 = Math.Max(num1, this.GetMaxItemWidth(node1.Nodes));
					continue;
				}
				finally
				{
					if (!flag1)
					{
						node1.Collapse();
					}
				}
			}
			return num1;
		}

		private TreeNode GetNodeAtXAndY(int x, int y)
		{
			WinMethods.TV_HITTESTINFO tv_hittestinfo1 = new WinMethods.TV_HITTESTINFO();
			tv_hittestinfo1.pt_x = x;
			tv_hittestinfo1.pt_y = y;
			IntPtr ptr1 = WinMethods.SendMessage(base.Handle, 0x1111, 0, tv_hittestinfo1);
			if (ptr1 == IntPtr.Zero)
			{
				return null;
			}
			if ((tv_hittestinfo1.flags != 4) && (tv_hittestinfo1.flags != 2))
			{
				return null;
			}
			return base.GetNodeAt(x, y);
		}

		protected override bool IsInputKey(Keys key)
		{
			if (key == Keys.Return)
			{
				return true;
			}
			return base.IsInputKey(key);
		}

		protected override void OnAfterCollapse(TreeViewEventArgs e)
		{
			base.OnAfterCollapse(e);
			this.ExpansionSignClicked = false;
		}

		protected override void OnAfterExpand(TreeViewEventArgs e)
		{
			base.OnAfterExpand(e);
			this.ExpansionSignClicked = false;
		}

		protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
		{
			this.ExpansionSignClicked = true;
			base.OnBeforeCollapse(e);
		}

 
		protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			this.ExpansionSignClicked = true;
			base.OnBeforeExpand(e);
		}

 
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.KeyData == Keys.Return)
			{
				this.SetSelectedItem(base.SelectedNode);
				if (this.selectedItem != null)
				{
					this.edSvc.CloseDropDown();
				}
			}
		}

 
		private void SetSelectedItem(TreeNode node)
		{
			this.selectedItem = null;
			if (node is DesignBindPicker.DataMemberNode)
			{
				DesignBindPicker.DataMemberNode node1 = (DesignBindPicker.DataMemberNode) node;
				if (this.selectLists == node1.IsList)
				{
					this.selectedItem = new DesignBinding(node1.DataSource, node1.DataMember);
				}
			}
			else if (node is DesignBindPicker.NoneNode)
			{
				this.selectedItem = DesignBinding.Null;
			}
		}

 
		public void Start(ITypeDescriptorContext context, IWindowsFormsEditorService edSvc, object dataSource, DesignBinding selectedItem)
		{
			this.edSvc = edSvc;
			this.selectedItem = selectedItem;
			this.ExpansionSignClicked = false;
			if ((context != null) && (context.Container != null))
			{
				this.FillDataSources(context, dataSource);
				base.Width = this.GetMaxItemWidth(base.Nodes) + (SystemInformation.VerticalScrollBarWidth * 2);
			}
		}
        /// <summary>
        /// Processes Windows messages
        /// </summary>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x201)
			{
				base.WndProc(ref m);
				if (this.allowSelection)
				{
					this.SetSelectedItem(this.GetNodeAtXAndY((short) ((int) m.LParam), ((int) m.LParam) >> 0x10));
					if ((this.selectedItem != null) && !this.ExpansionSignClicked)
					{
						this.edSvc.CloseDropDown();
					}
					this.ExpansionSignClicked = false;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

 

		// Properties
		public bool AllowSelection
		{
			get
			{
				return this.allowSelection;
			}
			set
			{
				this.allowSelection = value;
			}
		}
 
		private bool ExpansionSignClicked
		{
			get
			{
				return this.expansionSignClicked;
			}
			set
			{
				this.expansionSignClicked = value;
			}
		}
 
		public DesignBinding SelectedItem
		{
			get
			{
				return this.selectedItem;
			}
		}
 

		// Fields
		private bool allowSelection;
		private static readonly int BINDER_IMAGE;
		private static readonly int COLUMN_IMAGE;
		private IWindowsFormsEditorService edSvc;
		private bool expansionSignClicked;
		private const int MaximumDepth = 10;
		private bool multipleDataSources;
		private static readonly int NONE_IMAGE;
		private DesignBinding selectedItem;
		private TreeNode selectedNode;
		private bool selectLists;

		// Nested Types
		internal class DataMemberNode : TreeNode
		{
			// Methods
			public DataMemberNode(string dataMember, string dataField, bool isList) : base(dataField, DesignBindPicker.COLUMN_IMAGE, DesignBindPicker.COLUMN_IMAGE)
			{
				this.dataMember = dataMember;
				this.isList = isList;
			}

 

			// Properties
			public string DataField
			{
				get
				{
					return base.Text;
				}
			}
			public string DataMember
			{
				get
				{
					return this.dataMember;
				}
			}
			public IComponent DataSource
			{
				get
				{
					TreeNode node1 = this;
					while (node1 is DesignBindPicker.DataMemberNode)
					{
						node1 = node1.Parent;
					}
					if (node1 is DesignBindPicker.DataSourceNode)
					{
						return ((DesignBindPicker.DataSourceNode) node1).DataSource;
					}
					return null;
				}
			}
			public bool IsList
			{
				get
				{
					return this.isList;
				}
			}
 

			// Fields
			private string dataMember;
			private bool isList;
		}

		internal class DataSourceNode : TreeNode
		{
			// Methods
			public DataSourceNode(IComponent dataSource) : base(dataSource.Site.Name, DesignBindPicker.BINDER_IMAGE, DesignBindPicker.BINDER_IMAGE)
			{
				this.dataSource = dataSource;
			}


			// Properties
			public IComponent DataSource
			{
				get
				{
					return this.dataSource;
				}
			}
 

			// Fields
			private IComponent dataSource;
		}

		internal class NoneNode : TreeNode
		{
			// Methods
			public NoneNode() : base("GridNoneString", DesignBindPicker.NONE_IMAGE, DesignBindPicker.NONE_IMAGE)
			{
			}


		}
	}
 
	[Editor("Nistec.GridView.Design.DesignBindingEditor", typeof(UITypeEditor)), SecurityPermission(SecurityAction.Demand)]
	internal class DesignBinding
	{
		// Methods
		static DesignBinding()
		{
			DesignBinding.Null = new DesignBinding(null, null);
		}

		public DesignBinding(object dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
		}

 
		public bool Equals(object dataSource, string dataMember)
		{
			if (dataSource == this.dataSource)
			{
				return (string.Compare(dataMember, this.dataMember, true, CultureInfo.InvariantCulture) == 0);
			}
			return false;
		}

 

		// Properties
		public string DataField
		{
			get
			{
				int num1 = this.dataMember.LastIndexOf(".");
				if (num1 == -1)
				{
					return this.dataMember;
				}
				return this.dataMember.Substring(num1 + 1);
			}
		}
 
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
		}
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}
 
		public bool IsNull
		{
			get
			{
				return (this.dataSource == null);
			}
		}

		// Fields
		private string dataMember;
		private object dataSource;
		public static DesignBinding Null;
	}
 
	[SecurityPermission(SecurityAction.Demand)]
	internal class DesignBindingEditor : UITypeEditor
	{
		// Methods
		public DesignBindingEditor()
		{
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
				if (this.designBindingPicker == null)
				{
					this.designBindingPicker = new DesignBindPicker(context, true, false);
				}
				this.designBindingPicker.Start(context, service1, null, (DesignBinding) value);
				service1.DropDownControl(this.designBindingPicker);
				value = this.designBindingPicker.SelectedItem;
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
