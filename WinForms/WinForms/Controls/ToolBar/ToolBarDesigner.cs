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
	internal class ToolBarDesigner : ParentControlDesigner
	{
		public ToolBarDesigner()
		{
			this.addToolButton = new DesignerVerb("Add ToolButton", new EventHandler(this.OnAddToolButton));
			this.addToolDropDown = new DesignerVerb("Add ToolDropDown", new EventHandler(this.OnAddToolDropDown));
			this.addSeparator = new DesignerVerb("Add Separator", new EventHandler(this.OnAddSeparator));
			this.addPainter =new DesignerVerb("Add Painter", new EventHandler(AddPainter));
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.toolBar = (McToolBar) base.Control;
		}

     
		private void OnAddSeparator(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add Separator");
			McToolButton button1 = (McToolButton) host1.CreateComponent(typeof(McToolButton));
			button1.Dock = DockStyle.Left;
			button1.ButtonStyle = ToolButtonStyle.Separator;
			button1.Width = 8;
			//-button1.ParentBar=this.toolBar;
			this.toolBar.Controls.Add(button1);
			this.toolBar.Controls.SetChildIndex(button1, 0);
			transaction1.Commit();
		}

 
		private void OnAddToolButton(object sender, EventArgs e)
		{
			IDesignerHost host1 = (IDesignerHost) base.GetService(typeof(IDesignerHost));
			DesignerTransaction transaction1 = host1.CreateTransaction("Add ToolButton");
			McToolButton button1 = (McToolButton) host1.CreateComponent(typeof(McToolButton));
			button1.Dock = DockStyle.Left;
			//-button1.ParentBar=this.toolBar;
			this.toolBar.Controls.Add(button1);
			this.toolBar.Controls.SetChildIndex(button1, 0);
			transaction1.Commit();
		}

        private void OnAddToolDropDown(object sender, EventArgs e)
        {
            IDesignerHost host1 = (IDesignerHost)base.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction1 = host1.CreateTransaction("Add ToolDropDown");
            McToolButton button1 = (McToolButton)host1.CreateComponent(typeof(McToolButton));
            button1.Dock = DockStyle.Left;
            button1.ButtonStyle = ToolButtonStyle.DropDownButton;
            button1.Width = 33;
            //-button1.ParentBar = this.toolBar;
            this.toolBar.Controls.Add(button1);
            this.toolBar.Controls.SetChildIndex(button1, 0);
            transaction1.Commit();
        }
 
		protected override bool DrawGrid
		{
			get
			{
				return false;
			}
		}

        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.AllSizeable| SelectionRules.Moveable;
            }
        }

 
		public override DesignerVerbCollection Verbs
		{
			get
			{
                //if (this.toolBar.UseDesigner)
                //{
                    return new DesignerVerbCollection(new DesignerVerb[] { this.addToolButton, this.addSeparator, this.addToolDropDown, this.addPainter });
                //}

                //return null;
            }
		}

		#region Painter

		void AddPainter(object sender, EventArgs e)
		{
			StyleContainer painter=new  StyleContainer (Control.Container);
			Control.Container.Add (painter);
			this.toolBar.StylePainter=painter;
		}

		#endregion


		// Fields
		private DesignerVerb addSeparator;
		private DesignerVerb addToolButton;
		private DesignerVerb addToolDropDown;
		private DesignerVerb addPainter;
		private McToolBar toolBar;
	}


	internal class ToolButtonDesigner : ParentControlDesigner
	{
		public ToolButtonDesigner()
		{
			this.moveLeft = new DesignerVerb("Move Left", new EventHandler(this.OnMoveLeft));
			this.moveRight = new DesignerVerb("Move Right", new EventHandler(this.OnMoveRight));
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.toolButton = component as IMcToolButton;
		}

		private void OnMoveLeft(object sender, EventArgs e)
		{
			int num1 = this.toolButton.Parent.Controls.GetChildIndex((Control)this.toolButton);
			if (num1 < (this.toolButton.Parent.Controls.Count - 1))
			{
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
				IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				DesignerTransaction transaction1 = host1.CreateTransaction("Move Right");
				service1.OnComponentChanging(this.toolButton.Parent, TypeDescriptor.GetProperties(this.toolButton.Parent)["Controls"]);
				this.toolButton.Parent.Controls.SetChildIndex((Control)this.toolButton, num1 + 1);
				service1.OnComponentChanged(this.toolButton.Parent, TypeDescriptor.GetProperties(this.toolButton.Parent)["Controls"], null, null);
				transaction1.Commit();
				this.toolButton.Parent.Invalidate();
			}
		}

		private void OnMoveRight(object sender, EventArgs e)
		{
			int num1 = this.toolButton.Parent.Controls.GetChildIndex((Control)this.toolButton);
			if (num1 > 0)
			{
				IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
				IComponentChangeService service1 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
				DesignerTransaction transaction1 = host1.CreateTransaction("Move Left");
				service1.OnComponentChanging(this.toolButton.Parent, TypeDescriptor.GetProperties(this.toolButton.Parent)["Controls"]);
				this.toolButton.Parent.Controls.SetChildIndex((Control)this.toolButton, num1 - 1);
				service1.OnComponentChanged(this.toolButton.Parent, TypeDescriptor.GetProperties(this.toolButton.Parent)["Controls"], null, null);
				transaction1.Commit();
				this.toolButton.Parent.Invalidate();
			}
		}

		public override DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection(new DesignerVerb[] { this.moveLeft, this.moveRight });
			}
		}

		// Fields
		private DesignerVerb moveLeft;
		private DesignerVerb moveRight;
		private IMcToolButton toolButton;
	}

}
