using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Collections;

namespace Nistec.WinForms.Design
{
	internal class McDockingPanelDesigner : ParentControlDesigner
	{
		// Fields
		private DesignerVerb addControl;
        private DesignerVerb removeControl;
        private McDockingPanel dockingPanel;
		public McDockingPanelDesigner()
		{
			this.addControl = new DesignerVerb("Add Tab", new EventHandler(this.OnAddControl));
            this.removeControl = new DesignerVerb("Remove Tab", new EventHandler(this.OnRemoveControl));
        }
        public override bool CanBeParentedTo(IDesigner parentDesigner)
        {
            return (parentDesigner.Component is Form);
        }
        public override bool CanParent(Control control)
        {
            return control is McDockingTab;
        }
  		protected override bool GetHitTest(Point point)
		{
			Rectangle rectangle1 = new Rectangle(0, this.dockingPanel.ClientRectangle.Height - 0x16, this.dockingPanel.ClientRectangle.Width, 0x16);
            return  rectangle1.Contains(this.dockingPanel.PointToClient(point));
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.dockingPanel = (McDockingPanel) component;
            ISelectionService service1 = (ISelectionService)this.GetService(typeof(ISelectionService));
            //service1.SelectionChanged += new EventHandler(this.OnSelectionChanged);
		}

		private void OnAddControl(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add Tab");
			McDockingTab control1 = (McDockingTab) host1.CreateComponent(typeof(McDockingTab));
			this.dockingPanel.Controls.Add(control1);
			control1.Dock = DockStyle.Fill;
			this.dockingPanel.SelectedTab = control1;
			transaction1.Commit();
		}
        private void OnRemoveControl(object sender, EventArgs e)
        {
            McDockingTab control1 = this.dockingPanel.SelectedTab;
            if (control1 == null)
                return;
            IDesignerHost host1 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction1 = host1.CreateTransaction("Remove Tab");
            this.dockingPanel.Controls.Remove(control1);
            host1.DestroyComponent(control1);
            transaction1.Commit();

        }
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
		}


        private void OnSelectionChanged(object sender, EventArgs e)
        {
            //if (dockingPanel.IsTitleSelected())
            //{
            //    this.dockingPanel.IsSelected = true;
            //    this.dockingPanel.Invalidate();
            //    return;

            //}


            ICollection collection1 = ((ISelectionService)this.GetService(typeof(ISelectionService))).GetSelectedComponents();

            foreach (Control control1 in collection1)
            {
                if (control1 == this.dockingPanel)
                {
                    this.dockingPanel.IsSelected = true;
                    this.dockingPanel.Invalidate();
                    return;
                }
            }
            this.dockingPanel.IsSelected = false;
            this.dockingPanel.Invalidate();
        }

        //protected override void WndProc(ref Message msg)
        //{
        //    base.WndProc(ref msg);
        //    return;


        //    if (msg.Msg == 0x201)
        //    {
        //        if (dockingPanel.IsTitleSelected())
        //        {
        //            ArrayList list1 = new ArrayList();
        //            list1.Add(dockingPanel);
        //            ((ISelectionService)this.GetService(typeof(ISelectionService))).SetSelectedComponents(list1);
        //            this.dockingPanel.IsSelected = true;
        //            this.dockingPanel.Invalidate();
        //            return;

        //        }
        //        Point point1 = this.dockingPanel.PointToClient(Cursor.Position);
        //        McDockingTab control1 = this.dockingPanel.GetDockingControlAt(point1.X, point1.Y);
        //        if (control1 != null)
        //        {
        //            this.dockingPanel.SelectedTab = control1;
        //            ArrayList list1 = new ArrayList();
        //            list1.Add(control1);
        //            ((ISelectionService)this.GetService(typeof(ISelectionService))).SetSelectedComponents(list1);
        //            this.dockingPanel.IsSelected = true;
        //            this.dockingPanel.Invalidate();
        //        }
        //    }
        //    if (msg.Msg == 0x200)
        //    {
        //        Point point2 = this.dockingPanel.PointToClient(Cursor.Position);
        //        if (this.dockingPanel.TabsBounds.Contains(point2.X, point2.Y))
        //        {
        //            this.dockingPanel.InvalidateTabs();
        //        }
        //    }
        //    if (msg.Msg == 0x2a3)
        //    {
        //        this.dockingPanel.InvalidateTabs();
        //    }
        //    base.WndProc(ref msg);
        //}

        protected override bool DrawGrid
        {
            get
            {
                return true;
            }
        }
 
		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] { this.addControl,this.removeControl });
			}
		}
 



	}
}
