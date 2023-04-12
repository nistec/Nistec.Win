using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Collections;


namespace mControl.WinCtl.Controls.Design
{
    internal class TabPagesDesigner : ParentControlDesigner
    {

        // Fields
        private DesignerVerb addPage;
        private DesignerVerb removePage;
        private DesignerVerb addPainter;
        private CtlTabPages tabControl;

        public TabPagesDesigner()
        {
            this.addPage = new DesignerVerb("Add Page", new EventHandler(this.OnAdd));
            this.removePage = new DesignerVerb("Remove Page", new EventHandler(this.OnRemove));
            this.addPainter = new DesignerVerb("Add Painter", new EventHandler(this.AddPainter));
        }

        protected override bool GetHitTest(Point point)
        {
            if (this.tabControl.TabPosition == TabPosition.Top)//this.tabControl.PositionAtBottom)
            {
                new Rectangle(0, this.tabControl.ClientRectangle.Height - 0x16, this.tabControl.ClientRectangle.Width, 0x16);
            }
            else
            {
                new Rectangle(0, 0, this.tabControl.ClientRectangle.Width, 0x16);
            }
            CtlPage page1 = this.tabControl.GetTabPageAtPoint(this.tabControl.PointToClient(point));
            return (page1 != null);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.tabControl = (CtlTabPages)base.Control;
        }

        private void OnAdd(object sender, EventArgs e)
        {
            try
            {
                IDesignerHost host1 = (IDesignerHost)base.GetService(typeof(IDesignerHost));
                DesignerTransaction transaction1 = host1.CreateTransaction("Add Page");
                CtlPage page1 = (CtlPage)host1.CreateComponent(typeof(CtlPage));

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

                this.tabControl.TabPages.Add(page1);
                //this.tabControl.Controls.Add(page1);
                this.tabControl.PageAdded(this, new ControlEventArgs(page1));
                //page1.Dock = DockStyle.Fill;
                transaction1.Commit();
                this.tabControl.Invalidate();
            }
            catch { }
        }

        private void OnRemove(object sender, EventArgs eevent)
        {
            if ((this.tabControl != null) && (this.tabControl.TabPages.Count != 0))
            {
                MemberDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
                CtlPage page1 = this.tabControl.SelectedTab;
                IDesignerHost host1 = (IDesignerHost)this.GetService(typeof(IDesignerHost));
                if (host1 != null)
                {
                    DesignerTransaction transaction1 = null;
                    try
                    {
                        try
                        {
                            transaction1 = host1.CreateTransaction("TabControlRemoveTab " + page1.Site.Name);// , base.Component.Site.Name );
                            base.RaiseComponentChanging(descriptor1);
                        }
                        catch (CheckoutException exception1)
                        {
                            if (exception1 != CheckoutException.Canceled)
                            {
                                throw exception1;
                            }
                            return;
                        }
                        this.tabControl.TabPages.Remove(page1);
                        //this.tabControl.Controls.Remove(page1);
                        host1.DestroyComponent(page1);
                        base.RaiseComponentChanged(descriptor1, null, null);
                    }
                    finally
                    {
                        if (transaction1 != null)
                        {
                            transaction1.Commit();
                        }
                    }
                }
            }
        }



        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x201)
            {
                Point point1 = this.tabControl.PointToClient(Cursor.Position);
                CtlPage page1 = this.tabControl.GetTabPageAtPoint(point1);
                if (page1 != null)
                {
                    this.tabControl.SelectedTab = page1;
                    ArrayList list1 = new ArrayList();
                    list1.Add(page1);
                    ISelectionService service1 = (ISelectionService)this.GetService(typeof(ISelectionService));
                    service1.SetSelectedComponents(list1);
                }
            }
            if (msg.Msg == 0x200)
            {
                this.tabControl.Invalidate();
            }
            if (msg.Msg == 0x2a3)
            {
                this.tabControl.Invalidate();
            }
            base.WndProc(ref msg);
        }

        protected override bool DrawGrid
        {
            get
            {
                return false;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection(new DesignerVerb[] { this.addPage, this.removePage, this.addPainter });
            }
        }

        #region Painter

        void AddPainter(object sender, EventArgs e)
        {

            StyleFlat painter = new StyleFlat(Control.Container);
            Control.Container.Add(painter);
            this.tabControl.StylePainter = painter;
        }

        #endregion


    }

}
