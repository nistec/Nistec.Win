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
using Nistec.Win;

namespace Nistec.GridView.Design
{
//    [Serializable]
	[SecurityPermission(SecurityAction.Demand)]
	internal class GridDesigner : ControlDesigner
	{
		#region Memmbers
		private Grid grid;
		//private DesignerVerbCollection actions;
		// Fields
		private IComponentChangeService changeNotificationService;
		protected DesignerVerbCollection designerVerbs;
        private PropertyDlg propertyColumns;

		#endregion

		#region Methods


		public GridDesigner()
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
			Grid grid1 = (Grid) base.Component;
			if (e.Component == grid1.DataSource)
			{
				grid1.DataSource = null;
			}
		}

		protected override void Dispose(bool disposing)
		{
            if (propertyColumns != null && PropertyDlg.IsOpen)
            {
                propertyColumns.Close();
                propertyColumns = null;
            }
			if (disposing && (this.changeNotificationService != null))
			{
				this.changeNotificationService.ComponentRemoved -= new ComponentEventHandler(this.DataSource_ComponentRemoved);
			}
			base.Dispose(disposing);
		}

 
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.grid=component as Grid;

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
			Grid grid1 = (Grid) base.Component;
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
            base.OnPaintAdornments(pe);
            if (!grid.ColumnHeadersVisible)
                return;
            if (grid.DataSource != null)
                return;

            //grid.Visible=false;
            int top = grid.CaptionVisible ? Grid.DefaultCaptionHeight + 2 : 1;
            int width = grid.Width - 2;
            Rectangle rect = new Rectangle(0, top, width, Grid.DefaultColumnHeaderHeight);
            int lft = 1;
            int remain = width;
            bool rtl = grid.RightToLeft == RightToLeft.Yes;
            //MessageBox.Show(rect.ToString());
            //pe.Graphics.FillRectangle(grid.HeaderBackBrush,rect);

            //using(System.Drawing.Brush bb =new System.Drawing.SolidBrush(grid.HeaderBackColor),bf =new System.Drawing.SolidBrush(grid.HeaderForeBrush.HeaderForeColor))
            //{
            //using (Brush /*bb =/*ControlLayout*//*new System.Drawing.SolidBrush(grid.HeaderBackColor),*/ bf =grid.HeaderForeBrush new System.Drawing.SolidBrush(grid.HeaderForeColor))
            //{
            /*ControlLayout*/
            pe.Graphics.FillRectangle(grid.GetHeaderBackBrush(false, rect), rect);//bb,rect);

            using (System.Drawing.Pen p = grid.LayoutManager.GetPenBorder())//  new System.Drawing.Pen(grid.BorderColor,1))
            {
                pe.Graphics.DrawRectangle(p, rect);
                if (grid.RowHeadersVisible)
                {
                    remain = width - grid.RowHeaderWidth;
                    Rectangle r = new Rectangle(rect.X + lft, rect.Top, grid.RowHeaderWidth, rect.Height);
                    if (rtl)
                    {
                        r = new Rectangle(rect.X + rect.Width - lft - grid.RowHeaderWidth, rect.Top, grid.RowHeaderWidth, rect.Height);
                    }

                    pe.Graphics.DrawRectangle(p, r);
                    lft += grid.RowHeaderWidth;
                }
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.FormatFlags |= StringFormatFlags.NoWrap;
                    //sf.FormatFlags=StringFormatFlags.NoFontFallback | StringFormatFlags.NoClip;

                    foreach (GridColumnStyle c in grid.Columns)
                    {
                        if ((c.Width > 0) && (c.Visible) && (remain > 0))
                        {
                            int colWidth = (c.Width > remain) ? remain : c.Width;
                            remain = width - c.Width;
                            Rectangle r = new Rectangle(rect.X + lft, rect.Top, colWidth, rect.Height);
                            //Rectangle strRect=new Rectangle(rect.X+lft,rect.Top+2,colWidth,rect.Height-2);
                            if (rtl)
                            {
                                r = new Rectangle(rect.X + rect.Width - lft - colWidth, rect.Top, colWidth, rect.Height);
                            }
                            c.DesignRect = r;
                            pe.Graphics.DrawRectangle(p, r);
                            Rectangle strRect = new Rectangle(r.X, r.Top + 2, colWidth, r.Height - 2);
                            pe.Graphics.DrawString(c.HeaderText, grid.HeaderFont, grid.HeaderForeBrush, (RectangleF)strRect, sf);

                            lft += colWidth;
                        }
                    }
                }
            }
            //}

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
					designerVerbs.Add(new DesignerVerb("Preview", new EventHandler(DisplayGridPreview)));
					designerVerbs.Add(new DesignerVerb("Add DataSet", new EventHandler(AddDataSet)));
					designerVerbs.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
					designerVerbs.Add(new DesignerVerb("Adjust Columns", new EventHandler(AdjustColumns)));

				}
				return designerVerbs;
			}
		}

		void AddPainter(object sender, EventArgs e)
		{
			StyleGrid painter=new  StyleGrid (Control.Container);
			Control.Container.Add (painter);
			((ILayout)Control).StylePainter=painter as IStyle;
		}

		void AdjustColumns(object sender, EventArgs e)
		{
            /*bound*/
			this.grid.OnDesignAdjustColumns(true);//.AdjustColumns(false,false);
		}

		#endregion

		#region EventHandlers

		void DisplayGridPreview(object sender, EventArgs e)
		{
			try
			{
				if(grid.Columns.Count ==0 )//&& grid.TableStyles.Count == 0 )
				{
					MsgBox.ShowInfo  ("No columns found to preview");
					return;
				}
	
				GridView.GridPreview gp=new GridView.GridPreview ();
				gp.Preview ((Grid)Control);
				DialogResult dr= gp.ShowDialog ();
				if(dr==DialogResult.OK)
				{
					int width=gp.GridWidth;
		
					if(grid.Width !=width)
					{
          	
						if( MsgBox.ShowQuestion ("Save width grid changes ?")==DialogResult.Yes )
						{
							grid.Width =width;
							grid.Update();   
						}
					}
				}
			}
			catch(Exception ex)
			{
				MsgBox.ShowError (ex.Message );
			}
 
		}

		void AddDataSet(object sender, EventArgs e)
		{
			Control.Container.Add (new System.Data.DataSet());//  GridView.GridDataSet (Control.Container));
		}

		#endregion

        #region columnsProperty

        protected override bool GetHitTest(Point point)
        {
            GridColumnStyle col = GetColumnAtPoint(this.grid.PointToClient(point));
            return (col != null);
        }

        protected GridColumnStyle GetColumnAtPoint(Point p)
        {
            foreach (GridColumnStyle col in grid.Columns)
            {
                Rectangle rect = col.DesignRect;
                if (rect.IsEmpty)
                    continue;
                else if (rect.Contains(p))
                {
                    return col;
                }
            }
            return null;
        }
        /// <summary>
        /// Processes Windows messages
        /// </summary>
        protected override void WndProc(ref Message msg)
        {

            // Test for the right mouse down windows message
            if (msg.Msg == (int)Win32.Msgs.WM_RBUTTONUP)
            {
                HitColumnProperty(msg);
            }
            if (msg.Msg == (int)Win32.Msgs.WM_LBUTTONUP)
            {
                if (propertyColumns != null && PropertyDlg.IsOpen)
                {
                    HitColumnProperty(msg);
                }
            }
            base.WndProc(ref msg);
        }

        private void HitColumnProperty(Message msg)
        {
            // Check we have a valid object reference
            if (grid.Columns.Count > 0)
            {
                // Extract the mouse position
                int xPos = (short)((uint)msg.LParam & 0x0000FFFFU);
                int yPos = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

                Point screenCoord = grid.PointToScreen(new Point(xPos, yPos));
                Point clientCoord = grid.PointToClient(screenCoord);
                GridColumnStyle col = GetColumnAtPoint(clientCoord);
                if (col != null)
                {
                    PropertyDlgHandle(col);
                }
            }
        }

        private void PropertyDlgHandle(GridColumnStyle col)
        {
            try
            {
                if (propertyColumns == null || propertyColumns.IsDisposed)
                {
                    propertyColumns = new PropertyDlg(col,"GridView Columns properties");
                    propertyColumns.Caption.SubText = col.Site.Name;
                    propertyColumns.Show();
                }
                else if (PropertyDlg.IsOpen)
                {
                    propertyColumns.SelectObject(col,col.Site.Name);
                    propertyColumns.Show();
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region base


        //		public void OnAutoFormat(object sender, EventArgs e)
		//		{
		//			Grid grid1 = (Grid) base.Component;
		//			GridAutoFormatDialog dialog1 = new GridAutoFormatDialog(grid1);
		//			if (dialog1.ShowDialog() == DialogResult.OK)
		//			{
		//				DataRow row1 = dialog1.SelectedData;
		//				if (row1 != null)
		//				{
		//					PropertyDescriptorCollection collection1 = TypeDescriptor.GetProperties(typeof(Grid));
		//					foreach (DataColumn column1 in row1.Table.Columns)
		//					{
		//						object obj2 = row1[column1];
		//						PropertyDescriptor descriptor1 = collection1[column1.ColumnName];
		//						if (descriptor1 != null)
		//						{
		//							if (Convert.IsDBNull(obj2) || (obj2.ToString().Length == 0))
		//							{
		//								descriptor1.ResetValue(grid1);
		//							}
		//							else
		//							{
		//								try
		//								{
		//									object obj3 = descriptor1.Converter.ConvertFromString(obj2.ToString());
		//									descriptor1.SetValue(grid1, obj3);
		//									continue;
		//								}
		//								catch (Exception)
		//								{
		//									continue;
		//								}
		//							}
		//						}
		//					}
		//				}
		//				grid1.Invalidate();
		//			}
		//		}
		//


		#endregion
	}

}
