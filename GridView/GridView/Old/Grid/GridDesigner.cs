using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using mControl.Util;
using mControl.Collections; 
using mControl.Win32;
using mControl.WinCtl.Controls;

namespace mControl.GridStyle
{

	/// <summary>
	/// 
	/// </summary>
	#region GridDesigner

	//[Designer(typeof(CtlBaseDesigner))]

	internal class GridDesigner :ControlDesigner
	{

		#region Memmbers
        private Grid grid;
		private DesignerVerbCollection actions;

		#endregion

		public GridDesigner(){}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this.grid=component as Grid;
		}

		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			base.OnPaintAdornments (pe);
			if(!grid.ColumnHeadersVisible)
				return;
			//if(grid.isVirtual)
			//	return;
		
			grid.DataGrid.Visible=false;
			int top=grid.CaptionVisible? Grid.DefaultCaptionHeight+1:1;
			int width=grid.Width-2;  
			Rectangle rect=new Rectangle(0,top,width,Grid.DefaultColumnHeaderHeight);
			int lft=1;  
			int remain=width;  

			using(System.Drawing.Brush bb =new System.Drawing.SolidBrush(grid.HeaderBackColor),bf =new System.Drawing.SolidBrush(grid.HeaderForeColor))
			{
				pe.Graphics.FillRectangle(bb,rect);

				using(System.Drawing.Pen p =grid.CtlStyleLayout.GetPenBorder())//  new System.Drawing.Pen(grid.BorderColor,1))
				{
					pe.Graphics.DrawRectangle (p,rect );
					if(grid.RowHeadersVisible)
					{
						remain= width - grid.RowHeaderWidth;
						Rectangle r=new Rectangle(rect.X+lft,rect.Top,grid.RowHeaderWidth,rect.Height);  
						if(grid.RightToLeft==RightToLeft.Yes)
						{
							r=new Rectangle(rect.X +rect.Width-lft-grid.RowHeaderWidth,rect.Top,grid.RowHeaderWidth,rect.Height);   
						}
		
						pe.Graphics.DrawRectangle (p,r);
						lft+=grid.RowHeaderWidth;
					}
				
					foreach(GridColumnStyle c in grid.Columns)
					{
						if((c.Width>0) && (c.Visible) && (remain>0))
						{
							int colWidth= (c.Width > remain)? remain:c.Width;
							remain= width - c.Width;
							Rectangle r=new Rectangle(rect.X+lft,rect.Top,colWidth,rect.Height);
							//Rectangle strRect=new Rectangle(rect.X+lft,rect.Top+2,colWidth,rect.Height-2);
							if(grid.RightToLeft==RightToLeft.Yes)
							{
								r=new Rectangle(rect.X + rect.Width-lft-colWidth,rect.Top,colWidth,rect.Height);
							}
		
							pe.Graphics.DrawRectangle (p,r);
							Rectangle strRect=new Rectangle(r.X,r.Top+2,colWidth,r.Height-2);
							using(StringFormat sf=new StringFormat())
							{
								sf.Alignment=StringAlignment.Center;
								pe.Graphics.DrawString(c.HeaderText,grid.HeaderFont,bf,(RectangleF)strRect,sf);
							}
							lft+=colWidth;
						}
					}
				}
			}
			
		}

		
		#region UserDefinedVariables

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if(actions == null)
				{
					actions = new DesignerVerbCollection();
					actions.Add(new DesignerVerb("Preview", new EventHandler(DisplayGridPreview)));
					actions.Add(new DesignerVerb("AddDataSet", new EventHandler(AddDataSet)));
				    actions.Add(new DesignerVerb("AddPainter", new EventHandler(AddPainter)));

				}
				return actions;
			}
		}

		void AddPainter(object sender, EventArgs e)
		{
			StyleGrid painter=new  StyleGrid (Control.Container);
			Control.Container.Add (painter);
			((IStyleCtl)Control).StylePainter=painter;
		}
		#endregion

		#region EventHandlers

		void DisplayGridPreview(object sender, EventArgs e)
		{
 			try
			{
				if(((Grid)Control).GridTableStyle==null)
				{
					MsgBox.ShowInfo  ("No tables found to preview");
					return;
				}
				if(((Grid)Control).Columns.Count ==0)
				{
					MsgBox.ShowInfo  ("No columns found to preview");
					return;
				}
	
				GridStyle.GridPreview gp=new GridStyle.GridPreview ();
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
			Control.Container.Add (new System.Data.DataSet());//  GridStyle.GridDataSet (Control.Container));
		}

		#endregion

		protected override void PostFilterProperties(IDictionary Properties)
		{
			Properties.Remove("BackgroundImage");
			Properties.Remove("ForeColor");
			Properties.Remove("BackColor");
			Properties.Remove("AutoScroll");
			Properties.Remove("AutoScrollMargin");
			Properties.Remove("AutoScrollMinSize");
			Properties.Remove("Image");
			Properties.Remove("ImageAlign");
			Properties.Remove("ImageIndex");
			Properties.Remove("DockPadding");

		}
	}
	#endregion
}
