using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace Nistec.WinForms.Design
{
	internal class McDockingTabDesigner : ParentControlDesigner
	{
		public McDockingTabDesigner()
		{
		}

 
		public override bool CanBeParentedTo(IDesigner parentDesigner)
		{
			return (parentDesigner.Component is McDockingPanel);
		}

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.dockingControl = component as McDockingTab;
		}

 
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			using (Pen pen1 = new Pen(SystemColors.ControlDark))
			{
				pen1.DashStyle = DashStyle.Dash;
				pe.Graphics.DrawRectangle(pen1, 0, 0, this.dockingControl.Width - 1, this.dockingControl.Height - 1);
			}
		}

		protected override bool DrawGrid
		{
			get
			{
                return true;
			}
		}

        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.Visible;
            }
        }
 

		// Fields
		private McDockingTab dockingControl;
	}

}
