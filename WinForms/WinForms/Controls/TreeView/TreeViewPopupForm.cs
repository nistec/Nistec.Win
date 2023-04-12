using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Nistec.WinForms
{
	#region TreeViewPopupForm

	internal class TreeViewPopupForm : Nistec.WinForms.Controls.McPopUpBase
	{
		public TreeViewPopupForm() : this(null)
		{
		}

		public TreeViewPopupForm(Control parentControl) : base(parentControl)
		{
			this.selectedIndex = -1;
			this.components = null;
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.tvNodes = new McTreeView();
			base.SuspendLayout();
			this.tvNodes.BorderStyle = BorderStyle.None;
			this.tvNodes.Dock = DockStyle.Fill;
			this.tvNodes.HideSelection = false;
			this.tvNodes.ImageIndex = -1;
			this.tvNodes.Location = new Point(2, 2);
			this.tvNodes.Name = "tvNodes";
			this.tvNodes.SelectedImageIndex = -1;
			this.tvNodes.Size = new Size(0x128, 0x128);
			this.tvNodes.TabIndex = 0;
			this.tvNodes.DoubleClick += new EventHandler(this.tvNodes_DoubleClick);
			this.AutoScaleBaseSize = new Size(5, 13);
			this.BackColor = SystemColors.Window;
			base.ClientSize = new Size(300, 300);
			base.Controls.AddRange(new Control[] { this.tvNodes });
			base.DockPadding.All = 2;
			base.Name = "TreeViewPopupForm";
			this.Text = "McComboBoxPopupForm";
			base.Paint += new PaintEventHandler(this.TreeViewPopupForm_Paint);
			base.ResumeLayout(false);
		}

//		public void AddTreeNodes(TreeNodeCollection  treeNodes)//TreeNode[]
//		{
//			foreach(TreeNode tn in treeNodes.)
//			{
//             this.tvNodes.Nodes.Add(tn);
//			}
//
//			//this.tvNodes.Nodes.AddRange(treeNodes);
//			this.tvNodes.Visible=true; 
//
//		}


		public override object SelectedItem
		{
			get {return  this.tvNodes.Nodes[selectedIndex]  as object;}
		}

//		public override int SelectedIndex
//		{
//			get {return this.selectedIndex;}
//		}


		private void TreeViewPopupForm_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			Rectangle rectangle1 = new Rectangle(0, 0, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
			graphics1.DrawRectangle(SystemPens.ControlDark, rectangle1);
		}

		private void tvNodes_DoubleClick(object sender, EventArgs e)
		{
			TreeNode node1 = this.tvNodes.SelectedNode;
			if (node1 != null)
			{
                if ((node1.Tag != null) || !((McTreeCombo)base.m_ParentMc).SelectOnlyTagNotNull)
				{
                    ((McTreeCombo)base.m_ParentMc).SelectedItem = node1.Tag;
					TreeValueChangedEventArgs args1 = new TreeValueChangedEventArgs(this.tvNodes, node1.Text);
                    ((McTreeCombo)base.m_ParentMc).InvokeValueChanged(args1);
                    ((McTreeCombo)base.m_ParentMc).Text = args1.Text;
				}
			}
            else if (!((McTreeCombo)base.m_ParentMc).SelectOnlyTagNotNull)
			{
				TreeValueChangedEventArgs args2 = new TreeValueChangedEventArgs(this.tvNodes, string.Empty);
                ((McTreeCombo)base.m_ParentMc).InvokeValueChanged(args2);
                ((McTreeCombo)base.m_ParentMc).SelectedItem = null;
                ((McTreeCombo)base.m_ParentMc).Text = args2.Text;
			}
			this.ClosePopupForm();
		}


		// Fields
		private Container components;
		internal int selectedIndex;
		internal McTreeView tvNodes;
	}


	#endregion

}
