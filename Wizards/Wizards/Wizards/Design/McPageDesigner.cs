using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using Nistec.WinForms.Design;


namespace Nistec.Wizards.Design
{
    internal class McPageDesigner : ParentWizDesignerBase
	{

		// Fields
		private WizardPage tabPage;
		private DesignerVerb moveLeft;
		private DesignerVerb moveRight;


		public McPageDesigner()
		{
			this.moveLeft = new DesignerVerb("Move Left", new EventHandler(this.OnMoveLeft));
			this.moveRight = new DesignerVerb("Move Right", new EventHandler(this.OnMoveRight));

		}

		protected override void PostFilterProperties(IDictionary Properties)
		{

			Properties.Remove("ForeColor");
			Properties.Remove("BackColor");
			Properties.Remove("AutoScroll");
			Properties.Remove("AutoScrollMargin");
			Properties.Remove("AutoScrollMinSize");
			Properties.Remove("BackgroundImage");

		}

        //public override bool CanBeParentedTo(IDesigner parentDesigner)
        //{
        //    return (parentDesigner.Component is WizardPage);
        //}

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.tabPage = component as WizardPage;
		}

        protected override bool DrawGrid
        {
            get
            {
                return true;
            }
        }

		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			try
			{
				base.DrawGrid=true;
				base.OnPaintAdornments(pe);
			}
			finally
			{
				if (this.tabPage != null)
				{
					using (Pen pen1 = new Pen(SystemColors.ControlDark))
					{
						pen1.DashStyle = DashStyle.Dash;
						pe.Graphics.DrawRectangle(pen1, 0, 0, this.tabPage.Width - 1, this.tabPage.Height - 1);
					}
				}

			}
			
		}

		public override SelectionRules SelectionRules
		{
			get
			{
//				SelectionRules rules1 = base.SelectionRules;
//				if (this.Control.Parent is WizardPage)
//				{
//					rules1 &= (SelectionRules) (-16);
//				}
//				return rules1;
				return SelectionRules.Visible;
			}
		}

		private void OnMoveLeft(object sender, EventArgs e)
		{

			int indx = this.tabPage.wizParent.WizardPages.IndexOf(this.tabPage);
			//int num1 = this.tabPage.Parent.Controls.GetChildIndex(this.tabPage);
			if (indx>0)// && num1 > 0)
			{
				try
				{
					IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
					if (host1 == null)return;
		
					IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
					DesignerTransaction transaction1 = host1.CreateTransaction("Move Left");
					this.tabPage.wizParent.WizardPages.MoveTo(indx-1,this.tabPage);
					//service1.OnComponentChanging(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"]);
					//this.tabPage.Parent.Controls.SetChildIndex(this.tabPage, num1 - 1);
					//service1.OnComponentChanged(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"], null, null);
					transaction1.Commit();
					this.tabPage.wizParent.SelectedPage=this.tabPage;//.McTabControl.SelectedTab=this.tabPage;
					this.tabPage.Parent.Invalidate();
					CheckVerbStatus();
				}
				catch (CheckoutException exception1)
				{
					if (exception1 != CheckoutException.Canceled)
					{
						throw exception1;
					}
					return;
				}

			}
		}

 
		private void OnMoveRight(object sender, EventArgs e)
		{
			int indx = this.tabPage.wizParent.WizardPages.IndexOf(this.tabPage);
			//int num1 = this.tabPage.Parent.Controls.GetChildIndex(this.tabPage);
			if (indx < this.tabPage.wizParent.WizardPages.Count-1)//&& num1 < (this.tabPage.Parent.Controls.Count - 1))
			{
				try
				{
					IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
					if (host1 == null)return;

					IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
					DesignerTransaction transaction1 = host1.CreateTransaction("Move Right");
					this.tabPage.wizParent.WizardPages.MoveTo(indx+1,this.tabPage);
					//service1.OnComponentChanging(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"]);
					//this.tabPage.Parent.Controls.SetChildIndex(this.tabPage, num1 + 1);
					//service1.OnComponentChanged(this.tabPage.Parent, TypeDescriptor.GetProperties(this.tabPage.Parent)["Controls"], null, null);
					transaction1.Commit();
					this.tabPage.wizParent.SelectedPage=this.tabPage;//.McTabControl.SelectedTab=this.tabPage;
					this.tabPage.Parent.Invalidate();
					CheckVerbStatus();
				}
				catch (CheckoutException exception1)
				{
					if (exception1 != CheckoutException.Canceled)
					{
						throw exception1;
					}
					return;
				}
			}
		}

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "McTabPage"))
            {
                PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
                base.RaiseComponentChanging(descriptor1);
            }
            this.CheckVerbStatus();
        }

        private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
        {
            if (((e.Component == base.Component) && (e.Member != null)) && (e.Member.Name == "McTabPage"))
            {
                PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Controls"];
                base.RaiseComponentChanging(descriptor1);
            }
        }


		private void CheckVerbStatus()
		{
			int indx = this.tabPage.wizParent.WizardPages.IndexOf(this.tabPage);
			
			if (this.moveRight != null)
				this.moveRight.Enabled=(indx <this.tabPage.wizParent.WizardPages.Count-1);
			if (this.moveLeft != null)
				this.moveLeft.Enabled=(indx > 0);
		}


		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(this.tabPage.Parent!=null)
				{
					if(this.tabPage.wizParent.WizardPages.Contains(this.tabPage))
					{
						this.tabPage.wizParent.WizardPages.Remove(this.tabPage);
					}
				}
//				ISelectionService service1 = (ISelectionService) this.GetService(typeof(ISelectionService));
//				if (service1 != null)
//				{
//					service1.SelectionChanged -= new EventHandler(this.OnSelectionChanged);
//				}
//				IComponentChangeService service2 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
//				if (service2 != null)
//				{
//					service2.ComponentChanging -= new ComponentChangingEventHandler(this.OnComponentChanging);
//					service2.ComponentChanged -= new ComponentChangedEventHandler(this.OnComponentChanged);
//				}

			}
			base.Dispose (disposing);
		}


		public override DesignerVerbCollection Verbs
		{
			get
			{
				CheckVerbStatus();
				return new DesignerVerbCollection(new DesignerVerb[] { this.moveLeft, this.moveRight });
			}
		}

		#region Painter

//		private DesignerVerbCollection addPainter;
//
//		public override DesignerVerbCollection Verbs
//		{
//			get
//			{
//				if(addPainter == null)
//				{
//					addPainter = new DesignerVerbCollection();
//					addPainter.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
//				}
//				return addPainter;
//			}
//		}
//
//		void AddPainter(object sender, EventArgs e)
//		{
//			StyleContainer painter=new  StyleContainer (Control.Container);
//			Control.Container.Add (painter);
//			this.tabPage.StylePainter=painter;
//		}

		#endregion

	}

}
