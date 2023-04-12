using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.Security.Permissions;

namespace Nistec.WinForms.Design
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
	internal class TabControlDesigner : ParentControlDesigner
	{

		// Fields
		private DesignerVerb removeVerb;
		private DesignerVerbCollection verbs;

		//private bool disableDrawGrid;
		private int persistedSelectedIndex;
		//private bool tabControlSelected;

		private McTabControl tabControl;


		public TabControlDesigner()
		{
			//this.tabControlSelected = false;
			//this.disableDrawGrid = false;
			this.persistedSelectedIndex = 0;
		}

		public override bool CanParent(Control control)
		{
			return (control is McTabPage);
		}

		private void CheckVerbStatus()
		{
			if (this.removeVerb != null)
			{
				this.removeVerb.Enabled =tabControl.TabPages.Count>0;// this.Control.Controls.Count > 0;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
				if (service1 != null)
				{
					service1.SelectionChanged -= new EventHandler(this.OnSelectionChanged);
				}
				IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				if (service2 != null)
				{
					service2.ComponentChanging -= new ComponentChangingEventHandler(this.OnComponentChanging);
					service2.ComponentChanged -= new ComponentChangedEventHandler(this.OnComponentChanged);
				}
			}
			base.Dispose(disposing);
		}

		protected override bool GetHitTest(Point point)
		{
			McTabPage page1 =this.tabControl.GetTabPageAtPoint(this.tabControl.PointToClient(point));
			return (page1 != null);
		}

		internal static McTabPage GetTabPageOfComponent(object comp)
		{
			if (!(comp is Control))
			{
				return null;
			}
			Control control1 = (Control) comp;
			while ((control1 != null) && !(control1 is McTabPage))
			{
				control1 = control1.Parent;
			}
			return (McTabPage) control1;
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.tabControl = (McTabControl) base.Control;
            //if (this.tabControl.TabPages.Count == 0)
            //{
            //    OnAdd(this,EventArgs.Empty);// this.tabControl.TabPages.Add("ctlTabPage1");
            //}
            ISelectionService service1 = (ISelectionService)this.GetService(typeof(ISelectionService));
            if (service1 != null)
            {
                service1.SelectionChanged += new EventHandler(this.OnSelectionChanged);
            }
            IComponentChangeService service2 = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            if (service2 != null)
            {
                service2.ComponentChanging += new ComponentChangingEventHandler(this.OnComponentChanging);
                service2.ComponentChanged += new ComponentChangedEventHandler(this.OnComponentChanged);
             }
            ((McTabControl)component).TabIndexChanged += new EventHandler(this.OnTabSelectedIndexChanged);
            ((McTabControl)component).GotFocus += new EventHandler(this.OnGotFocus);
          }

		private void OnAdd(object sender, EventArgs eevent)
		{
			//McTabControl control1 = (McTabControl) base.Component;
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
					this.tabControl.TabPages.Add(page1);
					//this.tabControl.Controls.Add(page1);
					//this.tabControl.PageAdded(this,new ControlEventArgs(page1));
	
					this.tabControl.Invalidate();
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

		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "TabPages"))
			{
				PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["TabPages"];
				base.RaiseComponentChanging(descriptor1);
			}
			this.CheckVerbStatus();
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

        //protected override void OnPaintAdornments(PaintEventArgs pe)
        //{
        //    try
        //    {
        //        //this.disableDrawGrid = true;
        //        base.OnPaintAdornments(pe);
        //    }
        //    finally
        //    {
        //        //this.disableDrawGrid = false;
        //    }
        //}

		private void OnRemove(object sender, EventArgs eevent)
		{
			//McTabControl control1 = (McTabControl) base.Component;
			if ((this.tabControl != null) && (this.tabControl.TabPages.Count != 0))
			{
				MemberDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
				McTabPage page1 = this.tabControl.SelectedTab;
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

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
			//this.tabControlSelected = false;
			if (service1 != null)
			{
				ICollection collection1 = service1.GetSelectedComponents();
				//McTabControl control1 = (McTabControl) base.Component;
				foreach (object obj1 in collection1)
				{
                    if (obj1 == this.tabControl)
                    {
                        //this.tabControlSelected = true;
                    }
                    else
                    {
                        McTabPage page1 = TabControlDesigner.GetTabPageOfComponent(obj1);
                        if ((page1 != null) && (page1.Parent == this.tabControl))
                        {
                            //this.tabControlSelected = false;
                            this.tabControl.SelectedTab = page1;
                            return;
                        }
                    }
				}
			}
		}

		private void OnTabSelectedIndexChanged(object sender, EventArgs e)
		{
            ISelectionService service1 = (ISelectionService)this.GetService(typeof(ISelectionService));
            if (service1 != null)
            {
                ICollection collection1 = service1.GetSelectedComponents();
                //McTabControl control1 = (McTabControl) base.Component;
                bool flag1 = false;
                foreach (object obj1 in collection1)
                {
                    McTabPage page1 = TabControlDesigner.GetTabPageOfComponent(obj1);
                    if (((page1 != null) && (page1.Parent == this.tabControl)) && (page1 == this.tabControl.SelectedTab))
                    {
                        flag1 = true;
                        break;
                    }
                }
                if (!flag1)//do not select
                {
                    //service1.SetSelectedComponents(new object[] { base.Component });
                }
            }
		}

		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] textArray1 = new string[] { "SelectedIndex" };
			Attribute[] attributeArray1 = new Attribute[0];
			for (int num1 = 0; num1 < textArray1.Length; num1++)
			{
				PropertyDescriptor descriptor1 = (PropertyDescriptor) properties[textArray1[num1]];
				if (descriptor1 != null)
				{
					properties[textArray1[num1]] = TypeDescriptor.CreateProperty(typeof(TabControlDesigner), descriptor1, attributeArray1);
				}
			}
		}

		protected override void WndProc(ref Message msg)
		{

			if (msg.Msg == 0x84)//WM_NCHITTEST
			{
				base.WndProc(ref msg);
				if (((int) msg.Result) != -1)
				{
					return;
				}
				msg.Result = (IntPtr) 1;
			}
			else if (msg.Msg == 0x201)//WM_LBUTTONDOWN
			{
				ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));

				// Extract the mouse position
				int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
				int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

				// Ask the McTabControl to change tabs according to mouse message
				bool isBottonClick =this.tabControl.ExternalMouseTest(msg.HWnd, new Point(xPos, yPos));
				if(!isBottonClick)
				{
					//McTabControl control1 = (McTabControl) base.Component;
					Point point1 = this.tabControl.PointToClient(Cursor.Position);
					McTabPage page1 = this.tabControl.GetTabPageAtPoint(point1);
					if (page1 != null)
					{
						this.tabControl.SelectedTab = page1;
						ArrayList list1 = new ArrayList();
						list1.Add(page1);
						//ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
						service1.SetSelectedComponents(list1);
					}
				}
				
				base.WndProc(ref msg);
			}
//			else if (msg.Msg == (int)Win32.Msgs.WM_LBUTTONDBLCLK)
//			{
//				// Get access to the McTabControl we are the designer for
//				//Nistec.WinForms.McTabControl tabControl = this.SelectionService.PrimarySelection as Nistec.WinForms.McTabControl;
//
//				// Check we have a valid object reference
//				if (tabControl != null)
//				{
//					// Extract the mouse position
//					int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
//					int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);
//
//					// Ask the McTabControl to process a double click over an arrow as a simple
//					// click of the arrow button. In which case we return immediately to prevent
//					// the base class from using the double to generate the default event
//					if (tabControl.WantDoubleClick(msg.HWnd, new Point(xPos, yPos)))
//						return;
//				}
//			}

			else
			{
				base.WndProc(ref msg);
			}
	
		}

		protected override bool DrawGrid
		{
			get
			{
				//if (this.disableDrawGrid)
				//{
					return false;
				//}
				//return base.DrawGrid;
			}
		}
 
		private int SelectedIndex
		{
			get
			{
				return this.persistedSelectedIndex;
			}
			set
			{
				this.persistedSelectedIndex = value;
			}
		}

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this.verbs == null)
				{
					this.removeVerb = new DesignerVerb(("Page Remove"), new EventHandler(this.OnRemove));
					this.verbs = new DesignerVerbCollection();
					this.verbs.Add(new DesignerVerb(("Page Add"), new EventHandler(this.OnAdd)));
					this.verbs.Add(this.removeVerb);
					this.verbs.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
				}
				this.removeVerb.Enabled =tabControl.TabPages.Count>0;// this.Control.Controls.Count > 0;
				return this.verbs;
			}
		}

		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
		
			StyleContainer painter=new  StyleContainer (Control.Container);
			Control.Container.Add (painter);
			this.tabControl.StylePainter=painter;
		}

		#endregion


	}

}
