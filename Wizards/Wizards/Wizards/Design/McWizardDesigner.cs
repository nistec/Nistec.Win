
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Security.Permissions;

using Nistec.Win32;
using Nistec.Collections;
using Nistec.WinForms;
using Nistec.WinForms.Design;

namespace Nistec.Wizards.Design
{

    internal class McWizardDesigner : ParentWizDesignerBase
    {

		private Nistec.Wizards.McWizard ctl;
 
		#region wizPageDesign

		public McWizardDesigner()
		{
			this.pageAdd =  new DesignerVerb(("Page Add"), new EventHandler(this.OnAdd));
			this.pageRemove=  new DesignerVerb(("Page Remove"), new EventHandler(this.OnRemove));
			this.moveBack = new DesignerVerb("Move Back", new EventHandler(this.OnMoveBack));
			this.moveNext = new DesignerVerb("Move Next", new EventHandler(this.OnMoveNext));
			this.painterAdd=new DesignerVerb("Add Painter", new EventHandler(AddPainter));

		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.ctl = component as Nistec.Wizards.McWizard;
		}

		private void OnMoveNext(object sender, EventArgs e)
		{
            int num1 = this.ctl.TabControl.SelectedIndex;
            if (num1 < (this.ctl.TabControl.TabPages.Count - 1))
			{
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
				IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				DesignerTransaction transaction1 = host1.CreateTransaction("Move Next");
                service1.OnComponentChanging(this.ctl.TabControl, TypeDescriptor.GetProperties(this.ctl.TabControl.TabPages)["Controls"]);
                this.ctl.TabControl.SelectedIndex = num1 + 1;
                service1.OnComponentChanged(this.ctl.TabControl, TypeDescriptor.GetProperties(this.ctl.TabControl)["Controls"], null, null);
				transaction1.Commit();
                this.ctl.TabControl.Invalidate();
			}
		}

		private void OnMoveBack(object sender, EventArgs e)
		{
            int num1 = this.ctl.TabControl.SelectedIndex;
			if (num1 > 0)
			{
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
				IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				DesignerTransaction transaction1 = host1.CreateTransaction("Move Back");
                service1.OnComponentChanging(this.ctl.TabControl, TypeDescriptor.GetProperties(this.ctl.TabControl.TabPages)["Controls"]);
                this.ctl.TabControl.SelectedIndex = num1 - 1;
                service1.OnComponentChanged(this.ctl.TabControl, TypeDescriptor.GetProperties(this.ctl.TabControl)["Controls"], null, null);
				transaction1.Commit();
                this.ctl.TabControl.Invalidate();
			}
		}

		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] {this.pageAdd,this.pageRemove, this.moveBack, this.moveNext ,this.painterAdd});
			}
		}

		// Fields
		private DesignerVerb pageRemove;
		private DesignerVerb pageAdd;
		private DesignerVerb moveBack;
		private DesignerVerb moveNext;
		private DesignerVerb painterAdd;
		#endregion

		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
		
			StyleContainer painter=new  StyleContainer (Control.Container);
			Control.Container.Add (painter);
			this.ctl.StylePainter=painter;
		}

		#endregion

		#region tabControl

		private void OnAdd(object sender, EventArgs eevent)
		{
			McWizard wizControl = (McWizard) base.Component;
			MemberDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			if (host1 != null)
			{
				DesignerTransaction transaction1 = null;
				try
				{
					try
					{
						transaction1 = host1.CreateTransaction("TabControlAddTab " + base.Component.Site.Name );
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
					WizardPage page1 = (WizardPage) host1.CreateComponent(typeof(WizardPage));
					string text1 = null;
					PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(page1)["Name"];
					if ((descriptor2 != null) && (descriptor2.PropertyType == typeof(string)))
					{
						text1 = (string) descriptor2.GetValue(page1);
					}
					if (text1 != null)
					{
						page1.Text = text1;
					}
					//this.tabControl.AddTabPage(page1);
					wizControl.WizardPages.Add(page1);
					//this.tabControl.Controls.Add(page1);
					//this.tabControl.PageAdded(this,new ControlEventArgs(page1));
	
					wizControl.Invalidate();
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

		private void OnRemove(object sender, EventArgs eevent)
		{
			McWizard wizControl = (McWizard) base.Component;
			if ((wizControl != null) && (wizControl.WizardPages.Count != 0))
			{
				MemberDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
                WizardPage page1 = (WizardPage)wizControl.TabControl.SelectedTab;
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
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
						wizControl.WizardPages.Remove(page1); 
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


        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "TabPages"))
            {
                PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["TabPages"];
                base.RaiseComponentChanging(descriptor1);
            }
            //this.CheckVerbStatus();
        }

        private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
        {
            if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "TabPages"))
            {
                PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["TabPages"];
                base.RaiseComponentChanging(descriptor1);
            }
        }

		private void OnGotFocus(object sender, EventArgs e)
		{
			EventHandlerService service1 = (EventHandlerService) this.GetService(typeof(EventHandlerService));
			if (service1 != null)
			{
				Control control1 = service1.FocusWindow;
				if (control1 != null)
				{
					control1.Focus();
				}
			}
		}
		#endregion

		protected override void PostFilterProperties(IDictionary Properties)
		{
			Properties.Remove("ForeColor");
			Properties.Remove("BackColor");
			Properties.Remove("TabStop");
			Properties.Remove("AutoScroll");
			Properties.Remove("AutoScrollMargin");
			Properties.Remove("AutoScrollMinSize");
			Properties.Remove("BackgroundImage");
			Properties.Remove("BindFormat");
			//Properties.Remove("BorderStyle");
			//Properties.Remove("DefaultValue");
		}

        private ISelectionService _selectionService = null;
		private InheritanceUI inheritanceUI;

        public override ICollection AssociatedComponents
        {
            get
            {
                if (base.Control is Nistec.Wizards.McWizard)
                    return ((Nistec.Wizards.McWizard)base.Control).WizardPages;
                else
                    return base.AssociatedComponents;
            }
        }

        protected override bool DrawGrid
        {
            get { return false; }
        }

        public ISelectionService SelectionService
        {
            get
            {
                // Is this the first time the accessor has been called?
                if (_selectionService == null)
                {
                    // Then grab and cache the required interface
                    _selectionService = (ISelectionService)GetService(typeof(ISelectionService));
                }

                return _selectionService;
            }
        }

  
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
            //if (this.DrawGrid)
            //{
            //    Control control1 = this.Control;
            //    Rectangle rectangle1 = this.Control.DisplayRectangle;
            //    rectangle1.Width++;
            //    rectangle1.Height++;
            //    ControlPaint.DrawGrid(pe.Graphics, rectangle1, base.GridSize, control1.BackColor);
            //}
			if (base.Inherited)
			{
				if (this.inheritanceUI == null)
				{
					this.inheritanceUI = (InheritanceUI) this.GetService(typeof(InheritanceUI));
				}
				if (this.inheritanceUI != null)
				{
					pe.Graphics.DrawImage(this.inheritanceUI.InheritanceGlyph, 0, 0);
				}
			}
		}

        protected override void WndProc(ref Message msg)
        {
            // Test for the left mouse down windows message
            if (msg.Msg == (int)Win32.Msgs.WM_LBUTTONDOWN)
            {
                Nistec.Wizards.McWizard wizardControl = this.SelectionService.PrimarySelection as Nistec.Wizards.McWizard;

                // Check we have a valid object reference
                if (wizardControl != null)
                {
                    Nistec.WinForms.McTabControl tabControl = wizardControl.TabControl;

                    // Check we have a valid object reference
                    if (tabControl != null)
                    {
                        // Extract the mouse position
                        int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
                        int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

                        Point screenCoord = wizardControl.PointToScreen(new Point(xPos, yPos));
                        Point clientCoord = tabControl.PointToClient(screenCoord);

                        // Ask the TabControl to change tabs according to mouse message
                        //tabControl.ExternalMouseTest(msg.HWnd, clientCoord);
                    }
                }
            }
            else
            {
                if (msg.Msg == (int)Win32.Msgs.WM_LBUTTONDBLCLK)
                {
                    Nistec.Wizards.McWizard wizardControl = this.SelectionService.PrimarySelection as Nistec.Wizards.McWizard;

                    // Check we have a valid object reference
                    if (wizardControl != null)
                    {
                        Nistec.WinForms.McTabControl tabControl = wizardControl.TabControl;

                        // Check we have a valid object reference
                        if (tabControl != null)
                        {
                            // Extract the mouse position
                            int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
                            int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

                            Point screenCoord = wizardControl.PointToScreen(new Point(xPos, yPos));
                            Point clientCoord = tabControl.PointToClient(screenCoord);

                            // Ask the TabControl to process a double click over an arrow as a simple
                            // click of the arrow button. In which case we return immediately to prevent
                            // the base class from using the double to generate the default event
                            //if (tabControl.WantDoubleClick(msg.HWnd, clientCoord))
                            //   return;
                        }
                    }
                }
            }

            base.WndProc(ref msg);
        }
    }

}
