using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nistec.Drawing;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for McMove.
	/// </summary>
	[ToolboxItem(false)]
	public class McMove : Nistec.WinForms.McPictureBox
	{
		#region Ctor
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


//		private static Image _image;
//
//		static McMove()
//		{
//			McMove._image = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McMove"),
//				"Nistec.WinForms.Images.movetoMc.gif");
//
//		}

		public McMove()
		{
			base.SetStyle(ControlStyles.FixedHeight,true);
			base.SetStyle(ControlStyles.FixedWidth,true);

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

//			this.Image = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McMove"),
//				"Nistec.WinForms.Images.movetoMc.gif");

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

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McMove));
			// 
			// McMove
			// 
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
			this.Size = new System.Drawing.Size(20, 20);
			this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;

		}
		#endregion

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.Size = new System.Drawing.Size(20, 20);
		}

		#region Property

		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = System.Windows.Forms.Cursors.SizeAll;
			}
		}

		#endregion

		#region Move

		private Form form;
		private bool isMouseDown=false;
		private int x;
		private int y;

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			form=this.FindForm();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			isMouseDown=true;
			x=e.X;
			y=e.Y;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			isMouseDown=false;
			x=0;
			y=0;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			try
			{
				if(isMouseDown)
				{
					Point p=  new Point(e.X-this.x,e.Y-this.y);
					Point pform=  new Point(p.X-this.Left ,p.Y-this.Top);
					form.Location=PointToScreen(pform);
				}
			}
			catch{}
		}
		
		#endregion
	}
}
