using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;


namespace Nistec.WinForms
{
	[ToolboxBitmap(typeof(McVScrollBar), "Toolbox.VScrollBar.bmp"), ToolboxItem(false)]
	public class McVScrollBar : VScrollBar
	{
		public McVScrollBar()
		{
		}

		public void DoBottom()
		{
			base.Value = base.Maximum - base.LargeChange;
		}

		public void DoDown()
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

		public void DoPageDown()
		{
			if ((base.Value + base.LargeChange) >= (base.Maximum - base.LargeChange))
			{
				base.Value = base.Maximum - base.LargeChange;
			}
			else
			{
				base.Value += base.LargeChange;
			}
		}

		public void DoPageUp()
		{
			if ((base.Value - base.LargeChange) < 0)
			{
				base.Value = 0;
			}
			else
			{
				base.Value -= base.LargeChange;
			}
		}

		public void DoTop()
		{
			base.Value = 0;
			base.Invalidate();
		}

		public void DoUp()
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
