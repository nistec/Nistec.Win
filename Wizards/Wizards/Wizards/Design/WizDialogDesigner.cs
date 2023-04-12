
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Security.Permissions;

using MControl.Win32;
using MControl.Collections;
using MControl.WinForms;
using MControl.WinForms.Design;

namespace MControl.Wizards.Design
{


	[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
    internal class McDialogDesigner : ParentWizDesignerBase
	{

		private McDialog wizDialog;
		private DesignerVerb painterAdd;

		public McDialogDesigner()
		{
			this.painterAdd=new DesignerVerb("Add Painter", new EventHandler(AddPainter));
		}
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.wizDialog = component as  McDialog;
		}

		#region Painter

		private DesignerVerbCollection addPainter;

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if(addPainter == null)
				{
					addPainter = new DesignerVerbCollection();
					addPainter.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
				}
				return addPainter;
			}
		}

		void AddPainter(object sender, EventArgs e)
		{
			StyleFlat painter=new  StyleFlat (Control.Container);
			Control.Container.Add (painter);
			this.wizDialog.StylePainter=painter;
		}

		#endregion

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

 
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x84)
			{
				base.WndProc(ref m);
				if (((int) m.Result) != -1)
				{
					return;
				}
				m.Result = (IntPtr) 1;
			}
			else
			{
				base.WndProc(ref m);
			}
		}


		

//		protected override Point DefaultControlLocation
//		{
//			get
//			{
//				McGroupBox box1 = (McGroupBox) this.Control;
//				return new Point(box1.DisplayRectangle.X, box1.DisplayRectangle.Y);
//			}
//		}

		// Fields
		private InheritanceUI inheritanceUI;
	}


}
