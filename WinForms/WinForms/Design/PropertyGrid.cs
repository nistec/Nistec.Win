using System;
using System.Drawing;
using System.ComponentModel;

namespace Nistec.WinForms.Design
{

    [ToolboxItem(true), ToolboxBitmap(typeof(McPropertyGrid), "Toolbox.PropertyGrid.bmp")]
    public class McPropertyGrid : System.Windows.Forms.PropertyGrid
	{
		private System.ComponentModel.Container components = null;

		public McPropertyGrid()
		{
			InitializeComponent();
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		public void ShowEvents(bool show) 
		{
			ShowEventsButton(show);
		}
		public bool DrawFlat 
		{ 
			get { return DrawFlatToolbar; }
			set { DrawFlatToolbar = value; }
		}
	}
}
