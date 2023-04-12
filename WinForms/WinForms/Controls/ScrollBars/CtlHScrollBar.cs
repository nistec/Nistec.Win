using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;


namespace Nistec.WinForms
{
	[ToolboxBitmap(typeof(McHScrollBar), "Toolbox.HScrollBar.bmp"), ToolboxItem(false)]
	public class McHScrollBar : HScrollBar
	{
		public McHScrollBar()
		{
		}

		public void DoLeft()
		{
			if ((base.Value - base.SmallChange) < 0)
			{
				base.Value = 0;
			}
			else
			{
				base.Value -= base.SmallChange;
			}
		}

 
		public void DoLeftEdge()
		{
			base.Value = 0;
			base.Invalidate();
		}

 
		public void DoPageLeft()
		{
			if ((base.Value - base.SmallChange) < 0)
			{
				base.Value = 0;
			}
			else
			{
				base.Value -= base.SmallChange;
			}
		}

 
		public void DoPageRight()
		{
			if ((base.Value + base.SmallChange) > (base.Maximum - base.LargeChange))
			{
				base.Value = base.Maximum - base.LargeChange;
			}
			else
			{
				base.Value += base.SmallChange;
			}
		}

 
		public void DoRight()
		{
			if ((base.Value + base.SmallChange) > (base.Maximum - base.LargeChange))
			{
				base.Value = Math.Max(base.Maximum - base.LargeChange, base.Minimum);
			}
			else
			{
				base.Value += base.SmallChange;
			}
		}

		public void DoRightEdge()
		{
			base.Value = base.Maximum - base.LargeChange;
		}

		public bool IsEnd
		{
			get
			{
				return (base.Value == (base.Maximum - base.LargeChange));
			}
		}
		public bool IsStart
		{
			get
			{
				return (base.Value == 0);
			}
		}
 


	}
 

}
