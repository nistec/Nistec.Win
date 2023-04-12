using System;
using System.Security.Permissions;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

using Nistec.WinForms;

namespace Nistec.GridView.Design
{
	[SecurityPermission(SecurityAction.Demand)]
	internal class GridVirtualDesigner : ControlDesigner
	{
		#region Memmbers
		private GridVirtual grid;
		//private DesignerVerbCollection actions;
		// Fields
		private IComponentChangeService changeNotificationService;
		protected DesignerVerbCollection designerVerbs;

		#endregion

		#region Methods


        public GridVirtualDesigner()
        {

            //#if(CLIENT)
            //            //Nistec.Win.Net.nf_1.NetLogoOpen(netGrid.ctlNumber,netGrid.ctlName,netGrid.ctlVersion, "GridDesignerBase" ,"CLT");
            //            throw new Exception("Nistec.Client.Net , Invalid Nistec.Net Reference");
            //#else //if(!DEBUG)
            //Nistec.Net.GridNet.NetFram("GridDesigner", "DSN");
            this.changeNotificationService = null;
            //#endif

            //this.designerVerbs = new DesignerVerbCollection();
            //this.designerVerbs.Add(new DesignerVerb("GridAutoFormatString", new EventHandler(this.OnAutoFormat)));
        }

		private void DataSource_ComponentRemoved(object sender, ComponentEventArgs e)
		{
			GridVirtual grid1 = (GridVirtual) base.Component;
			if (e.Component == grid1.DataSource)
			{
				grid1.DataSource = null;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.changeNotificationService != null))
			{
				this.changeNotificationService.ComponentRemoved -= new ComponentEventHandler(this.DataSource_ComponentRemoved);
			}
			base.Dispose(disposing);
		}

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.grid=component as GridVirtual;

			IDesignerHost host1 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
			if (host1 != null)
			{
				this.changeNotificationService = (IComponentChangeService) host1.GetService(typeof(IComponentChangeService));
				if (this.changeNotificationService != null)
				{
					this.changeNotificationService.ComponentRemoved += new ComponentEventHandler(this.DataSource_ComponentRemoved);
				}
			}
		}


		public void OnPopulateGrid(object sender, EventArgs evevent)
		{
			GridVirtual grid1 = (GridVirtual) base.Component;
			grid.Cursor = Cursors.WaitCursor;
			try
			{
				if (grid1.DataSource == null)
				{
					throw new NullReferenceException("GridPopulateError");
				}
				grid1.SubObjectsSiteChange(false);
				grid1.SubObjectsSiteChange(true);
			}
			finally
			{
				grid1.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region UserDefinedVariables

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if(designerVerbs == null)
				{
					designerVerbs = new DesignerVerbCollection();
					designerVerbs.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
					designerVerbs.Add(new DesignerVerb("Adjust Columns", new EventHandler(AdjustColumns)));
					designerVerbs.Add(new DesignerVerb("Virtual Source", new EventHandler(CreateVirtualSource)));
				}
				return designerVerbs;
			}
		}

		void AddPainter(object sender, EventArgs e)
		{
			StyleGrid painter=new  StyleGrid (Control.Container);
			Control.Container.Add (painter);
			((ILayout)Control).StylePainter=painter;
		}

		void AdjustColumns(object sender, EventArgs e)
		{
            /*bound*/
			this.grid.OnDesignAdjustColumns(true);//.AdjustColumns(false,false);
		}

        void CreateVirtualSource(object sender, EventArgs e)
        {
            this.grid.PerformDimension();
        }
		#endregion

	}

}
