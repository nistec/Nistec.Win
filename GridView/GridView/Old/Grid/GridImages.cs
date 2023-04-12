using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace mControl.GridStyle
{
	/// <summary>
	/// Summary description for GridImages.
	/// </summary>
	[DesignTimeVisible(false), ToolboxItem(false)]
	internal class GridImages : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ImageList GrigImageList;
		private System.ComponentModel.IContainer components;

		public GridImages()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
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
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GridImages));
			this.GrigImageList = new System.Windows.Forms.ImageList(this.components);
			// 
			// GrigImageList
			// 
			this.GrigImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.GrigImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GrigImageList.ImageStream")));
			this.GrigImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// GridImages
			// 
			this.Name = "GridImages";

		}
		#endregion
	}
}
