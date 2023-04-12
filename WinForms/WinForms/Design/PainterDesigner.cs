using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;


namespace Nistec.WinForms.Design
{
	internal class PainterDesigner : System.ComponentModel.Design.ComponentDesigner
	{

		// Fields
		private DesignerVerb styleForce;
		private DesignerVerb styleRelease;
        private DesignerVerb controlsLayout;
        //private StyleGuide styleGuide;
		private StyleBase painter;

		public PainterDesigner()
		{
			this.styleForce = new DesignerVerb("Force Style", new EventHandler(this.OnStyleForce));
			this.styleRelease = new DesignerVerb("Release Style", new EventHandler(this.OnStyleRelease));
            this.controlsLayout = new DesignerVerb("Controls Layout", new EventHandler(this.OnControlsLayout));
        }

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.painter = (StyleBase) base.Component;
		}

		private void OnStyleForce(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Force Style");
			//this.styleGuide.SetGuideOptions(GuideOptions.Forces);
			this.painter.SetPainterAction("Forces",this.painter.PainterType);
			transaction1.Commit();
		}

		private void OnStyleRelease(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Release Style");
			//this.styleGuide.SetGuideOptions(GuideOptions.Release);
			this.painter.SetPainterAction("Release",this.painter.PainterType);
			transaction1.Commit();
		}
        private void OnControlsLayout(object sender, EventArgs e)
        {
            IDesignerHost host1 = (IDesignerHost)base.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction1 = host1.CreateTransaction("Controls Layout");
            //this.styleGuide.SetGuideOptions(GuideOptions.Forces);
            this.painter.SetPainterAction("ControlLayout", this.painter.PainterType);
            transaction1.Commit();
        }

		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] { this.styleForce,this.styleRelease,this.controlsLayout });
			}
		}

	}
}
