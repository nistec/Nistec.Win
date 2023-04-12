using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;


namespace Nistec.WinForms.Design
{
	internal class McDockingDesigner : ComponentDesigner
	{

		// Fields
		private DesignerVerb addToBottom;
		private DesignerVerb addToLeft;
		private DesignerVerb addToRight;
		//private DesignerVerb addToTop;
		private McDocking dockingManager;



		public McDockingDesigner()
		{
			this.addToLeft = new DesignerVerb("Add to left", new EventHandler(this.OnAddToLeft));
			this.addToRight = new DesignerVerb("Add to right", new EventHandler(this.OnAddToRight));
			//this.addToTop = new DesignerVerb("Add to top", new EventHandler(this.OnAddToTop));
			this.addToBottom = new DesignerVerb("Add to bottom", new EventHandler(this.OnAddToBottom));
		}

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			this.dockingManager = (McDocking) component;
			this.dockingManager.ParentForm = host1.RootComponent as Form;
		}

		private void OnAddToBottom(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add to bottom");
			McDockingPanel panel1 = (McDockingPanel) host1.CreateComponent(typeof(McDockingPanel));
			((Form) host1.RootComponent).Controls.Add(panel1);
			this.dockingManager.DockedPanels.Add(panel1);
			panel1.Manager = this.dockingManager;
			panel1.Dock = DockStyle.Bottom;
			transaction1.Commit();
		}

		private void OnAddToLeft(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add to left");
			McDockingPanel panel1 = (McDockingPanel) host1.CreateComponent(typeof(McDockingPanel));
            ((Form) host1.RootComponent).Controls.Add(panel1);
			this.dockingManager.DockedPanels.Add(panel1);
			panel1.Manager = this.dockingManager;
			panel1.Dock = DockStyle.Left;
			transaction1.Commit();
		}

		private void OnAddToRight(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add to right");
			McDockingPanel panel1 = (McDockingPanel) host1.CreateComponent(typeof(McDockingPanel));
			((Form) host1.RootComponent).Controls.Add(panel1);
			this.dockingManager.DockedPanels.Add(panel1);
			panel1.Manager = this.dockingManager;
			panel1.Dock = DockStyle.Right;
			transaction1.Commit();
		}

 
        //private void OnAddToTop(object sender, EventArgs e)
        //{
        //    IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
        //    DesignerTransaction transaction1 = host1.CreateTransaction("Add to top");
        //    McDockingPanel panel1 = (McDockingPanel) host1.CreateComponent(typeof(McDockingPanel));
        //    ((Form) host1.RootComponent).Controls.Add(panel1);
        //    this.dockingManager.DockedPanels.Add(panel1);
        //    panel1.Manager = this.dockingManager;
        //    panel1.Dock = DockStyle.Top;
        //    transaction1.Commit();
        //}

		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] { this.addToLeft, this.addToRight, /*this.addToTop,*/ this.addToBottom });
			}
		}
 

	}
}
