using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;

namespace Nistec.WinForms.Design
{
	/// <summary>
	/// Summary description for MultiSourcing.
	/// </summary>
	public class ReportDataSourceEditor: System.Drawing.Design.UITypeEditor
	{
		private IWindowsFormsEditorService edSvc = null;

		

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{

			if (context != null && context.Instance != null && (context.Instance is IDataSourcesProvider )&& provider != null) 
			{

				edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{
					STreeView treeView = new STreeView();
			
					treeView.Nodes.AddRange(((IDataSourcesProvider)context.Instance).DataSources);
					treeView.ValueSelected += new EventHandler(this.ValueSelected);
				

					edSvc.DropDownControl(treeView);
					value=treeView.DataSource;
				}
			}

			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			if (context != null && context.Instance != null) 
			{
				return UITypeEditorEditStyle.DropDown;
			}
			return base.GetEditStyle(context);
		}

		private void ValueSelected(object sender, EventArgs e) 
		{
			if (edSvc != null) 
			{
				edSvc.CloseDropDown();
			}
		}


	}

	public class SNode:TreeNode
	{
		object _DataSource=null;
		
		public object DataSource
		{
			get{return _DataSource;}
		}

		public SNode(string Text,object DataSource ):base(Text)
		{
			this._DataSource=DataSource;
		}

	}

    [ToolboxItem(false)]
	public class STreeView:TreeView
	{
		public event EventHandler ValueSelected;

		object _DataSource=null;
	

		public object DataSource
		{
			get{return _DataSource;}
		}
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect (e);
			if (e.Node.Nodes.Count==0)
			{
				_DataSource=((SNode)e.Node).DataSource;
				OnValueSelected(new EventArgs());
			}
		}

		protected virtual void OnValueSelected(EventArgs e)
		{
			if(ValueSelected!=null)
			{
				ValueSelected(this,e);
			}
		}
	}
	public interface IDataSourcesProvider
	{
		SNode[] DataSources {get;}
	}
}
