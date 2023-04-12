using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Nistec.WinForms.Design
{

    internal class TaskPanelDesigner : System.Windows.Forms.Design.ParentControlDesigner
    {
        
		// Fields
		private DesignerVerbCollection verbs;

		private DesignerVerb expand;
		private DesignerVerb collaps;
		private DesignerVerb addControl;
        private DesignerVerb addTreeControl;
        private DesignerVerb addHelpLabel;
        private McTaskPanel taskPanel;
		private DesignerVerb addPainter;

		public TaskPanelDesigner()
		{
			this.addControl = new DesignerVerb("Add Item", new EventHandler(this.OnAddControl));
            this.addTreeControl = new DesignerVerb("Add TreeView", new EventHandler(this.OnAddTreeControl));
            this.addHelpLabel = new DesignerVerb("Add HelpLabel", new EventHandler(this.OnAddHelpLabel));

            this.expand = new DesignerVerb("Expand", new EventHandler(this.OnExpand));
			this.collaps = new DesignerVerb("Collaps", new EventHandler(this.OnCollaps));
			this.addPainter =new DesignerVerb("Add Painter", new EventHandler(AddPainter));
		}

		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
			StyleContainer painter=new  StyleContainer (Control.Container);
			Control.Container.Add (painter);
			this.taskPanel.StylePainter=painter;
		}

		#endregion

        //public override bool CanBeParentedTo(IDesigner parentDesigner)
        //{
        //    return (parentDesigner.Component is McTaskBar);
        //}

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.taskPanel = component as McTaskPanel;
		}
        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ControlLayout");
            Properties.Remove("BorderStyle");
        }


		private void OnAddControl(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add Item");
			LinkLabelItem item1 = (LinkLabelItem) host1.CreateComponent(typeof(LinkLabelItem));
			this.taskPanel.AddNewItem(item1);
			this.taskPanel.Invalidate();
			transaction1.Commit();
		}

        private void OnAddTreeControl(object sender, EventArgs e)
        {
            IDesignerHost host1 = (IDesignerHost)base.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction1 = host1.CreateTransaction("Add TreeView");
            McTreeView treeView = (McTreeView)host1.CreateComponent(typeof(McTreeView));
            treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            treeView.BorderStyle = BorderStyle.None;
            treeView.HotTracking = true;
            //treeView.FullRowSelect = true;
            treeView.Location = new System.Drawing.Point(1, 25);
            //treeView.Name = "treeView";
            treeView.Size = new System.Drawing.Size(taskPanel.Width - 2, Math.Max(0, taskPanel.Height - 27));

            this.taskPanel.AddNewItem(treeView);
            this.taskPanel.Invalidate();
            transaction1.Commit();
        }

        private void OnAddHelpLabel(object sender, EventArgs e)
        {
            IDesignerHost host1 = (IDesignerHost)base.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction1 = host1.CreateTransaction("Add HelpLabel");
            McHelpLabel helpLabel = (McHelpLabel)host1.CreateComponent(typeof(McHelpLabel));
            helpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            //helpLabel.BorderStyle = BorderStyle.None;
            helpLabel.Location = new System.Drawing.Point(1, 25);
            helpLabel.Size = new System.Drawing.Size(taskPanel.Width - 2, Math.Max(0, taskPanel.Height - 27));

            this.taskPanel.AddNewItem(helpLabel);
            this.taskPanel.Invalidate();
            transaction1.Commit();
        }

		private void OnExpand(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Expand");
            this.taskPanel.PanelState=PanelState.Expanded;
			this.taskPanel.Invalidate();
			transaction1.Commit();
            this.taskPanel.Refresh();
        }
 
		private void OnCollaps(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Collaps");
			this.taskPanel.PanelState=PanelState.Collapsed;
			this.taskPanel.Invalidate();
			transaction1.Commit();
            this.taskPanel.Refresh();
        }

		private void CheckVerbStatus()
		{
			if (this.collaps != null)
			{
				this.collaps.Enabled = !this.taskPanel.PanelHook;
			}
		}
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "TabPages"))
			{
				PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
				base.RaiseComponentChanging(descriptor1);
			}
			this.CheckVerbStatus();
		}

		private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
		{
			if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "TabPages"))
			{
				PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
				base.RaiseComponentChanging(descriptor1);
			}
		}


		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this.verbs == null)
				{
					//this.removeVerb = new DesignerVerb(("PageRemove"), new EventHandler(this.OnRemove));
					this.verbs = new DesignerVerbCollection();
					//this.verbs.AddRange(new DesignerVerb[] {this.addControl,this.addTreeControl, this.expand,this.collaps ,this.addPainter});
                    this.verbs.AddRange(new DesignerVerb[] { this.addControl, this.addTreeControl, this.addHelpLabel,this.addPainter });
                }
                PanelState state = this.taskPanel.PanelState;
                bool isHook = this.taskPanel.PanelHook;
                //this.collaps.Enabled = !isHook && state== PanelState.Expanded;
                //this.expand.Enabled = !isHook && state == PanelState.Collapsed;
                
                bool isEmpty = this.taskPanel.Controls.Count <= 1;
                //bool hasLabel = IsContainsHelpLabel();
                //bool hasTree = IsContainsTreeControl();
                bool hasItem = IsContainsItemsControl();

                this.addHelpLabel.Enabled =isHook && isEmpty;
                this.addControl.Enabled = hasItem || isEmpty;// !hasTree;
                this.addTreeControl.Enabled = isEmpty;// !hasTree && !hasItem;
                return this.verbs;

				//return new DesignerVerbCollection(new DesignerVerb[] {this.addControl, this.expand,this.collaps });
			}
		}

        public override SelectionRules SelectionRules
        {
            get
            {
                //if (this.taskPanel.PanelState == PanelState.Expanded)
                //{
                //    return SelectionRules.BottomSizeable;
                //}
                return SelectionRules.BottomSizeable;
            }
        }

        private bool IsContainsHelpLabel()
        {
            foreach (Control c in this.taskPanel.Controls)
            {
                if (c is McHelpLabel)
                    return true;
            }

            return false;
        }
        private bool IsContainsTreeControl()
        {
            foreach (Control c in this.taskPanel.Controls)
            {
                if (c is McTreeView)
                    return true;
            }

            return false;
        }
        private bool IsContainsItemsControl()
        {
            foreach (Control c in this.taskPanel.Controls)
            {
                if (c is LinkLabelItem)
                    return true;
            }

            return false;
        }


	}
}
