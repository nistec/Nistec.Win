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
	internal class VGridDesigner : ControlDesigner
	{
		#region Memmbers
		private VGrid grid;
		//private DesignerVerbCollection actions;
		// Fields
		private IComponentChangeService changeNotificationService;
		protected DesignerVerbCollection designerVerbs;

		#endregion

		#region Methods


        public VGridDesigner()
        {

            //#if(CLIENT)
            //            //Nistec.Win.Net.nf_1.NetLogoOpen(netGrid.ctlNumber,netGrid.ctlName,netGrid.ctlVersion, "GridDesignerBase" ,"CLT");
            //            throw new Exception("Nistec.Client.Net , Invalid Nistec.Net Reference");
            //#else //if(!DEBUG)
            //            Nistec.Net.GridNet.NetFram("GridDesigner", "DSN");
            this.changeNotificationService = null;
            //#endif

            //this.designerVerbs = new DesignerVerbCollection();
            //this.designerVerbs.Add(new DesignerVerb("GridAutoFormatString", new EventHandler(this.OnAutoFormat)));
        }

		private void DataSource_ComponentRemoved(object sender, ComponentEventArgs e)
		{
            VGrid grid1 = (VGrid)base.Component;
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
            this.grid = component as VGrid;

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
            VGrid grid1 = (VGrid)base.Component;
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

		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			base.OnPaintAdornments (pe);
			if(!grid.ColumnHeadersVisible)
				return;
			if(grid.DataSource!=null)
				return;

  			//grid.Visible=false;
            int top = grid.CaptionVisible ? VGrid.DefaultCaptionHeight + 2 : 1;
			int width=grid.Width-2;
            Rectangle rect = new Rectangle(0, top, width, VGrid.DefaultColumnHeaderHeight);
			int lft=1;  
			int remain=width;  
			bool rtl= grid.RightToLeft==RightToLeft.Yes;
			//MessageBox.Show(rect.ToString());
			//pe.Graphics.FillRectangle(grid.HeaderBackBrush,rect);

			//using(System.Drawing.Brush bb =new System.Drawing.SolidBrush(grid.HeaderBackColor),bf =new System.Drawing.SolidBrush(grid.HeaderForeBrush.HeaderForeColor))
			//{
            using (Brush /*bb =new System.Drawing.SolidBrush(grid.HeaderBackColor),*/ bf = new System.Drawing.SolidBrush(grid.HeaderForeColor))
			{
                /*ControlLayout*/
                pe.Graphics.FillRectangle(grid.GetHeaderBackBrush(false, rect), rect);
				//pe.Graphics.FillRectangle(bb,rect);

				using(System.Drawing.Pen p =grid.LayoutManager.GetPenBorder())//  new System.Drawing.Pen(grid.BorderColor,1))
				{
					pe.Graphics.DrawRectangle (p,rect );
					if(grid.RowHeadersVisible)
					{
						remain= width - grid.RowHeaderWidth;
						Rectangle r=new Rectangle(rect.X+lft,rect.Top,grid.RowHeaderWidth,rect.Height);  
						if(rtl)
						{
							r=new Rectangle(rect.X +rect.Width-lft-grid.RowHeaderWidth,rect.Top,grid.RowHeaderWidth,rect.Height);   
						}
		
						pe.Graphics.DrawRectangle (p,r);
						lft+=grid.RowHeaderWidth;
					}
					using(StringFormat sf=new StringFormat())
					{
						sf.Alignment=StringAlignment.Center;
						sf.FormatFlags |= StringFormatFlags.NoWrap;
						//sf.FormatFlags=StringFormatFlags.NoFontFallback | StringFormatFlags.NoClip;
					
						foreach(GridColumnStyle c in grid.Columns)
						{
							if((c.Width>0) && (c.Visible) && (remain>0))
							{
								int colWidth= (c.Width > remain)? remain:c.Width;
								remain= width - c.Width;
								Rectangle r=new Rectangle(rect.X+lft,rect.Top,colWidth,rect.Height);
								//Rectangle strRect=new Rectangle(rect.X+lft,rect.Top+2,colWidth,rect.Height-2);
								if(rtl)
								{
									r=new Rectangle(rect.X + rect.Width-lft-colWidth,rect.Top,colWidth,rect.Height);
								}
		
								pe.Graphics.DrawRectangle (p,r);
								Rectangle strRect=new Rectangle(r.X,r.Top+2,colWidth,r.Height-2);
								pe.Graphics.DrawString(c.HeaderText,grid.HeaderFont,bf,(RectangleF)strRect,sf);
							
								lft+=colWidth;
							}
						}
					}
				}
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
					//designerVerbs.Add(new DesignerVerb("Fields Collection", new EventHandler(FieldsCollection)));

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

 
		#endregion

	}

}
