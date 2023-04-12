using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System;

namespace mControl.GridStyle.Columns 
{
 	public class GridIconColumn : GridColumnStyle 
	{
        
		#region Members

     	private ImageList m_IconList;
		#endregion
   
		#region Constructor
		public GridIconColumn() 
		{
			m_IconList = new ImageList();
			//this.Padding.SetPadding(0 );
			this.Width = 50;//this.GetPreferredSize(null, null).Width;
			m_ColumnType = ColumnTypes.IconColumn;
			
		}
       
		#endregion

		#region Property
	
		public ImageList IconList 
		{
			get{return m_IconList;}
			set{m_IconList = value;}
		}
        
		#endregion
	
		#region overrides

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight) 
		{
			try 
			{
				g.FillRectangle(backBrush, bounds);
				Rectangle controlBounds = this.GetCellBounds(bounds);
				Rectangle fillRect = new Rectangle((controlBounds.X + 2), (controlBounds.Y + 2), (controlBounds.Width - 2), (controlBounds.Height - 2));
				int val = ((int)(this.GetColumnValueAtRow(source, rowNum)));
	
				/*using(Pen p = new Pen(new SolidBrush(Color.Transparent)))
				{
					g.DrawRectangle(p, controlBounds);
				}*/
				
				if(val >=0 && val < m_IconList.Images.Count )
				{
					g.DrawImage(m_IconList.Images[val], fillRect);
					// g.DrawIcon(mIcon, fillRect)
				}
			}
			catch //(System.Exception Throw) 
			{
				new Exception("Error In Icon List");
			}
		}
		#endregion
	}
}

