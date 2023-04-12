using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;


namespace mControl.WinForms.Design
{
    internal class McPageDesigner : ParentControlDesigner
    {

        // Fields
        private DesignerVerb moveLeft;
        private DesignerVerb moveRight;
        private McPage tabPage;

        public McPageDesigner()
        {
            this.moveLeft = new DesignerVerb("Move Left", new EventHandler(this.OnMoveLeft));
            this.moveRight = new DesignerVerb("Move Right", new EventHandler(this.OnMoveRight));
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {

            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");

        }

        public override bool CanBeParentedTo(IDesigner parentDesigner)
        {
            return (parentDesigner.Component is McTabPages);
        }


        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.tabPage = component as McPage;
        }

        protected override bool DrawGrid
        {
            get
            {
                return false;//true;
            }
        }


        private void OnMoveLeft(object sender, EventArgs e)
        {
            int indx = ((McTabPages)this.tabPage.Parent).TabPages.IndexOf(this.tabPage);
            int num1 = this.tabPage.Parent.Controls.GetChildIndex(this.tabPage);
            if (indx > 0)
            {
                IDesignerHost host1 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
                IComponentChangeService service1 = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
                DesignerTransaction transaction1 = host1.CreateTransaction("Move Left");
                //service1.OnComponentChanging(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"]);
                ((McTabPages)this.tabPage.Parent).TabPages.MoveTo(indx - 1, this.tabPage);
                this.tabPage.Parent.Controls.SetChildIndex(this.tabPage, num1 - 1);
                //service1.OnComponentChanged(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"], null, null);
                transaction1.Commit();
                ((McTabPages)this.tabPage.Parent).SelectedTab = this.tabPage;
                this.tabPage.Parent.Invalidate();
            }
        }


        private void OnMoveRight(object sender, EventArgs e)
        {
            int indx = ((McTabPages)this.tabPage.Parent).TabPages.IndexOf(this.tabPage);
            int num1 = this.tabPage.Parent.Controls.GetChildIndex(this.tabPage);
            //if (num1 < (this.tabPage.Parent.Controls.Count - 1))
            if (indx < (((McTabPages)this.tabPage.Parent).TabPages.Count - 1))//&& num1 < (this.tabPage.Parent.Controls.Count - 1))
            {
                IDesignerHost host1 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
                IComponentChangeService service1 = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
                DesignerTransaction transaction1 = host1.CreateTransaction("Move Right");
                //service1.OnComponentChanging(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"]);
                ((McTabPages)this.tabPage.Parent).TabPages.MoveTo(indx + 1, this.tabPage);
                this.tabPage.Parent.Controls.SetChildIndex(this.tabPage, num1 + 1);
                //service1.OnComponentChanged(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"], null, null);
                transaction1.Commit();
                ((McTabPages)this.tabPage.Parent).SelectedTab = this.tabPage;
                this.tabPage.Parent.Invalidate();
            }
        }

        //		protected override void OnPaintAdornments(PaintEventArgs pe)
        //		{
        //			if (this.tabPage != null)
        //			{
        //				using (Pen pen1 = new Pen(SystemColors.ControlDark))
        //				{
        //					pen1.DashStyle = DashStyle.Dash;
        //					pe.Graphics.DrawRectangle(pen1, 0, 0, this.tabPage.Width - 1, this.tabPage.Height - 1);
        //				}
        //			}
        //		}


        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.None;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection(new DesignerVerb[] { this.moveLeft, this.moveRight });
            }
        }

    }

}
