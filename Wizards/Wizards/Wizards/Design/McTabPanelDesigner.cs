
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

    internal class McTabPanelDesigner : ParentWizDesignerBase
    {

		#region Fields
		private DesignerVerb pageRemove;
		private DesignerVerb pageAdd;
		private DesignerVerb moveBack;
		private DesignerVerb moveNext;
		private DesignerVerb painterAdd;
		private Nistec.Wizards.McTabPanels ctl;
		private ISelectionService _selectionService = null;
		private InheritanceUI inheritanceUI;

		#endregion

		#region Designer

        public McTabPanelDesigner()
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
			this.ctl = component as Nistec.Wizards.McTabPanels;
		}

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

		public override ICollection AssociatedComponents
		{
			get 
			{
				//                if (base.Control is Nistec.Wizards.McTabPanels)
				//                    return ((Nistec.Wizards.McTabPanels)base.Control).WizardPages;
				//                else
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
			if (this.DrawGrid)
			{
				Control control1 = this.Control;
				Rectangle rectangle1 = this.Control.DisplayRectangle;
				rectangle1.Width++;
				rectangle1.Height++;
				ControlPaint.DrawGrid(pe.Graphics, rectangle1, base.GridSize, control1.BackColor);
			}
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
				Nistec.Wizards.McTabPanels wizardControl = this.SelectionService.PrimarySelection as Nistec.Wizards.McTabPanels;

				// Check we have a valid object reference
				if (wizardControl != null)
				{
					Nistec.WinForms.McTabControl tabControl = wizardControl.tabControl;

                    HitControlProperty(msg);

					// Check we have a valid object reference
					if (tabControl != null)
					{
						// Extract the mouse position
						int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
						int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

						Point screenCoord = wizardControl.PointToScreen(new Point(xPos, yPos));
						Point clientCoord = tabControl.PointToClient(screenCoord);

						// Ask the tabControl to change tabs according to mouse message
						//tabControl.ExternalMouseTest(msg.HWnd, clientCoord);
					}
				}
			}
			else
			{
				if (msg.Msg == (int)Win32.Msgs.WM_LBUTTONDBLCLK)
				{
					Nistec.Wizards.McTabPanels wizardControl = this.SelectionService.PrimarySelection as Nistec.Wizards.McTabPanels;
                    
					// Check we have a valid object reference
					if (wizardControl != null)
					{
                        Nistec.WinForms.McTabControl tabControl = wizardControl.tabControl;

						// Check we have a valid object reference
						if (tabControl != null)
						{
							// Extract the mouse position
							int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
							int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

							Point screenCoord = wizardControl.PointToScreen(new Point(xPos, yPos));
							Point clientCoord = tabControl.PointToClient(screenCoord);

							// Ask the tabControl to process a double click over an arrow as a simple
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

		#endregion

		#region DesignerVerb

		private void OnMoveNext(object sender, EventArgs e)
		{
			int num1 = this.ctl.tabControl.SelectedIndex;
			if (num1 < (this.ctl.WizardPages.Count - 1))
			{
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
				//IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				DesignerTransaction transaction1 = host1.CreateTransaction("Move Next");
				//service1.OnComponentChanging(this.ctl.tabControl, TypeDescriptor.GetProperties(this.ctl.tabControl)["Controls"]);
				this.ctl.tabControl.SelectedIndex=num1+1;
				//service1.OnComponentChanged(this.ctl.tabControl, TypeDescriptor.GetProperties(this.ctl.tabControl)["Controls"], null, null);
				transaction1.Commit();
				this.ctl.tabControl.Invalidate();
			}
		}

		private void OnMoveBack(object sender, EventArgs e)
		{
			int num1 = this.ctl.tabControl.SelectedIndex;
			if (num1 > 0)
			{
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
				//IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				DesignerTransaction transaction1 = host1.CreateTransaction("Move Back");
				//service1.OnComponentChanging(this.ctl.tabControl, TypeDescriptor.GetProperties(this.ctl.tabControl.TabPages)["Controls"]);
				this.ctl.tabControl.SelectedIndex=num1-1;
				//service1.OnComponentChanged(this.ctl.tabControl, TypeDescriptor.GetProperties(this.ctl.tabControl)["Controls"], null, null);
				transaction1.Commit();
				this.ctl.tabControl.Invalidate();
			}
		}

		private void OnAdd(object sender, EventArgs eevent)
		{
			McTabPanels wizControl = (McTabPanels) base.Component;
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
					McTabPage page1 = (McTabPage) host1.CreateComponent(typeof(McTabPage));
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
			McTabPanels wizControl = (McTabPanels) base.Component;
			if ((wizControl != null) && (wizControl.WizardPages.Count != 0))
			{
				MemberDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
				McTabPage page1 =(McTabPage) wizControl.tabControl.SelectedTab;
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


		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] {this.pageAdd,this.pageRemove, this.moveBack, this.moveNext ,this.painterAdd});
			}
		}

		#endregion

		#region Painter

		void AddPainter(object sender, EventArgs e)
		{

            StyleContainer painter = new StyleContainer(Control.Container);
			Control.Container.Add (painter);
			this.ctl.StylePainter=painter;
		}

		#endregion

        #region Splitter

        protected override bool GetHitTest(Point point)
        {
            McSplitter split = GetControlAtPoint(this.ctl.splitter1.PointToClient(point));
            return (split != null);
        }

        protected McSplitter GetControlAtPoint(Point p)
        {
            McSplitter split = this.ctl.splitter1;
            {
                Rectangle rect = split.ClientRectangle;
                if (rect.IsEmpty)
                    return null;
                if (rect.Contains(p))
                {
                    return split;
                }
            }
            return null;
        }

 
        private void HitControlProperty(Message msg)
        {
            // Check we have a valid object reference
            McSplitter split = ctl.splitter1;
            if (split !=null)
            {
                // Extract the mouse position
                int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
                int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

                Point screenCoord = ctl.PointToScreen(new Point(xPos, yPos));
                Point clientCoord = ctl.PointToClient(screenCoord);
                McSplitter col = GetControlAtPoint(clientCoord);
            }
        }

  
        #endregion



    }

}
