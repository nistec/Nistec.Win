using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Nistec.Collections;

namespace Nistec.WinForms.Design
{

    internal class TaskBarDesigner : System.Windows.Forms.Design.ParentControlDesigner
    {
        private ISelectionService _selectionService = null;
        
		#region Properties

		public override ICollection AssociatedComponents
        {
            get 
            {
                if (base.Control is Nistec.WinForms.McTaskBar )
					return ((Nistec.WinForms.McTaskBar)base.Control).Panels ;
				else
                    return base.AssociatedComponents;
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                if (_selectionService == null)
                {
                    _selectionService = (ISelectionService)GetService(typeof(ISelectionService));
                }

                return _selectionService;
            }
        }

 		#endregion
   

		// Fields
		private DesignerVerb resetting;
		private DesignerVerb expandAll;
		private DesignerVerb collapsAll;
		private DesignerVerb addPainter;
        private DesignerVerb addPanel;
        private McTaskBar taskBar;

		public TaskBarDesigner()
		{
			this.resetting = new DesignerVerb("Resetting", new EventHandler(this.OnResetting));
			this.expandAll = new DesignerVerb("Expand All", new EventHandler(this.OnExpandAll));
			this.collapsAll = new DesignerVerb("Collaps All", new EventHandler(this.OnCollapsAll));
			this.addPainter =new DesignerVerb("Add Painter", new EventHandler(this.AddPainter));
            this.addPanel = new DesignerVerb("Add Panel", new EventHandler(this.OnAdd));
        }

		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
			StyleContainer painter=new  StyleContainer (Control.Container);
			Control.Container.Add (painter);
			this.taskBar.StylePainter=painter;
		}

		#endregion

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.taskBar = component as McTaskBar;
		}

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
        }

//		protected override bool DrawGrid
//		{
//			get
//			{
//				return true;
//			}
//		}
 
		private void OnResetting(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Resetting");
			this.taskBar.UpdatePositions();
			this.taskBar.Invalidate();
			transaction1.Commit();
		}

		private void OnExpandAll(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("ExpandAll");
			foreach(McTaskPanel t in this.taskBar.Panels)
			{
                 t.PanelState=PanelState.Expanded;
			}
			this.taskBar.UpdatePositions();
			this.taskBar.Invalidate();
			transaction1.Commit();
		}
 
		private void OnCollapsAll(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("CollapsAll");
			foreach(McTaskPanel t in this.taskBar.Panels)
			{
				t.PanelState=PanelState.Collapsed;
			}
			this.taskBar.UpdatePositions();
			this.taskBar.Invalidate();
			transaction1.Commit();
		}

        private void OnAdd(object sender, EventArgs e)
        {
            try
            {
                IDesignerHost host1 = (IDesignerHost)base.GetService(typeof(IDesignerHost));
                DesignerTransaction transaction1 = host1.CreateTransaction("Add Panel");
                McTaskPanel page1 = (McTaskPanel)host1.CreateComponent(typeof(McTaskPanel));

                string text1 = null;
                PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(page1)["Name"];
                if ((descriptor2 != null) && (descriptor2.PropertyType == typeof(string)))
                {
                    text1 = (string)descriptor2.GetValue(page1);
                }
                if (text1 != null)
                {
                    page1.Text = text1;
                }

                this.taskBar.Panels.Add(page1);
                transaction1.Commit();
                this.taskBar.Invalidate();
            }
            catch { }
        }
		public override bool CanParent(Control control)
		{
			return (control is McTaskPanel);
		}

         
		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] {this.addPanel, this.resetting, this.expandAll,this.collapsAll,this.addPainter });
			}
		}


	}
}
