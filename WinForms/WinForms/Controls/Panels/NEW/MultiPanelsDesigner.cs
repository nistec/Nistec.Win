using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.Security.Permissions;

namespace mControl.WinCtl.Controls.Design
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
	internal class MultiPanelsDesigner : ParentControlDesigner
	{

		// Fields
	
		//private bool disableDrawGrid;
		//private int persistedSelectedIndex;
		//private bool tabControlSelected;

		private CtlMultiPanels tabControl;


		public MultiPanelsDesigner()
		{
			//this.persistedSelectedIndex = 0;
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
			}
			base.Dispose(disposing);
		}

		protected override bool GetHitTest(Point point)
		{
			CtlPanel page1 =this.tabControl.GetPanelAtPoint(this.tabControl.PointToClient(point));
			return (page1 != null);
		}

		internal static CtlPanel GetTabPageOfComponent(object comp)
		{
			if (!(comp is Control))
			{
				return null;
			}
			Control control1 = (Control) comp;
			while ((control1 != null) && !(control1 is CtlPanel))
			{
				control1 = control1.Parent;
			}
			return (CtlPanel) control1;
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.tabControl = (CtlMultiPanels) base.Control;
//			ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
//			if (service1 != null)
//			{
//				service1.SelectionChanged += new EventHandler(this.OnSelectionChanged);
//			}
//			IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
//			((CtlMultiPanels) component).TabIndexChanged += new EventHandler(this.OnTabSelectedIndexChanged);
//			((CtlMultiPanels) component).GotFocus += new EventHandler(this.OnGotFocus);
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


		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
			//this.tabControlSelected = false;
			if (service1 != null)
			{
				ICollection collection1 = service1.GetSelectedComponents();
				//CtlTabControl control1 = (CtlTabControl) base.Component;
				foreach (object obj1 in collection1)
				{
					if (obj1 == this.tabControl)
					{
						//this.tabControlSelected = true;
					}
					CtlPanel page1 = GetTabPageOfComponent(obj1);
					if ((page1 != null) && (page1.Parent == this.tabControl))
					{
						//this.tabControlSelected = false;
						this.tabControl.SelectedPanel = page1;
						return;
					}
				}
			}
		}

		private void OnTabSelectedIndexChanged(object sender, EventArgs e)
		{
			ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
			if (service1 != null)
			{
				ICollection collection1 = service1.GetSelectedComponents();
				//CtlTabControl control1 = (CtlTabControl) base.Component;
				bool flag1 = false;
				foreach (object obj1 in collection1)
				{
					CtlPanel page1 = GetTabPageOfComponent(obj1);
					if (((page1 != null) && (page1.Parent == this.tabControl)) && (page1 == this.tabControl.SelectedPanel))
					{
						flag1 = true;
						break;
					}
				}
				if (!flag1)
				{
					service1.SetSelectedComponents(new object[] { base.Component });
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
					properties[textArray1[num1]] = TypeDescriptor.CreateProperty(typeof(MultiPanelsDesigner), descriptor1, attributeArray1);
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

				// Ask the CtlTabControl to change tabs according to mouse message
				int pnlClick =this.tabControl.ExternalMouseTest(msg.HWnd, new Point(xPos, yPos));
				if(pnlClick>0)
				{
					//CtlTabControl control1 = (CtlTabControl) base.Component;
					Point point1 = this.tabControl.PointToClient(Cursor.Position);
					CtlPanel page1 = this.tabControl.GetPanelAtPoint(point1);
					if (page1 != null)
					{
						this.tabControl.SelectedPanel = page1;
						ArrayList list1 = new ArrayList();
						list1.Add(page1);
						//ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
						service1.SetSelectedComponents(list1);
					}
				}

				
				base.WndProc(ref msg);
			}

			else
			{
				base.WndProc(ref msg);
			}
	
		}

		protected override bool DrawGrid
		{
			get
			{
				return false;
			}
		}




		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
		
			StyleFlat painter=new  StyleFlat (Control.Container);
			Control.Container.Add (painter);
			this.tabControl.StylePainter=painter;
		}

		#endregion


	}

}
