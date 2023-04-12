using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;


namespace Nistec.WinForms.Design
{
	internal class ToolBarContainerDesigner : ParentControlDesigner
	{
		public ToolBarContainerDesigner()
		{
			this.addToolBar = new DesignerVerb("Add ToolBar", new EventHandler(this.OnAddToolBar));
			this.addPainter =new DesignerVerb("Add Painter", new EventHandler(AddPainter));
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
            this.toolBarContainer = (McToolBarContainer)base.Control;
		}


        public override bool CanParent(Control control)
        {
            return (control is McToolBar);
        }

		private void OnAddToolBar(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add ToolBar");
			McToolBar tb = (McToolBar) host1.CreateComponent(typeof(McToolBar));
			//button1.Dock = DockStyle.Left;
			//button1.ParentBar=this.toolBar;
            this.toolBarContainer.Controls.Add(tb);
            this.toolBarContainer.Controls.SetChildIndex(tb, 0);
			transaction1.Commit();
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
                //if (this.toolBar.UseDesigner)
                //{
                    return new DesignerVerbCollection(new DesignerVerb[] { this.addToolBar, this.addPainter });
                //}

                //return null;
            }
		}

        private void DrawBorder(Graphics graphics)
        {
            Color color1;
            Control control1 = this.Control;
            Rectangle rectangle1 = control1.ClientRectangle;
            if (control1.BackColor.GetBrightness() < 0.5)
            {
                color1 = ControlPaint.Light(control1.BackColor);
            }
            else
            {
                color1 = ControlPaint.Dark(control1.BackColor);
            }
            Pen pen1 = new Pen(color1);
            pen1.DashStyle = DashStyle.Dash;
            rectangle1.Width--;
            rectangle1.Height--;
            graphics.DrawRectangle(pen1, rectangle1);
            pen1.Dispose();
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            if (base.Component is McToolBarContainer)
            {
                McToolBarContainer panel1 = (McToolBarContainer)base.Component;
                this.DrawBorder(pe.Graphics);
            }
            base.OnPaintAdornments(pe);
        }

        //protected override void WndProc(ref Message msg)
        //{

        //    if (msg.Msg == 0x84)//WM_NCHITTEST
        //    {
        //        base.WndProc(ref msg);
        //        if (((int)msg.Result) != -1)
        //        {
        //            return;
        //        }
        //        msg.Result = (IntPtr)1;
        //    }
        //    else if (msg.Msg == 0x201)//WM_LBUTTONDOWN
        //    {
        //        ISelectionService service1 = (ISelectionService)this.GetService(typeof(ISelectionService));

        //        // Extract the mouse position
        //        int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
        //        int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

        //        //McTabControl control1 = (McTabControl) base.Component;
        //        Point point1 = this.toolBarContainer.PointToClient(Cursor.Position);
        //        McToolBar tb = this.toolBarContainer.GetBarAtPoint(point1);
        //        if (tb != null)
        //        {
        //            this.toolBarContainer.SelectedToolBar = tb;
        //            ArrayList list1 = new ArrayList();
        //            list1.Add(tb);
        //            //ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
        //            service1.SetSelectedComponents(list1);
        //        }

        //        base.WndProc(ref msg);
        //    }
        //    //			else if (msg.Msg == (int)Win32.Msgs.WM_LBUTTONDBLCLK)
        //    //			{
        //    //				// Get access to the McTabControl we are the designer for
        //    //				//Nistec.WinForms.McTabControl tabControl = this.SelectionService.PrimarySelection as Nistec.WinForms.McTabControl;
        //    //
        //    //				// Check we have a valid object reference
        //    //				if (tabControl != null)
        //    //				{
        //    //					// Extract the mouse position
        //    //					int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
        //    //					int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);
        //    //
        //    //					// Ask the McTabControl to process a double click over an arrow as a simple
        //    //					// click of the arrow button. In which case we return immediately to prevent
        //    //					// the base class from using the double to generate the default event
        //    //					if (tabControl.WantDoubleClick(msg.HWnd, new Point(xPos, yPos)))
        //    //						return;
        //    //				}
        //    //			}

        //    else
        //    {
        //        base.WndProc(ref msg);
        //    }

        //}


		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
			StyleContainer painter=new  StyleContainer (Control.Container);
			Control.Container.Add (painter);
            this.toolBarContainer.StylePainter = painter;
		}

		#endregion


		// Fields
		private DesignerVerb addToolBar;
		private DesignerVerb addPainter;
		private McToolBarContainer toolBarContainer;
	}




}
