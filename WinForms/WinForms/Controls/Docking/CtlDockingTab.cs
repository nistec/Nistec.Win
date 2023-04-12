using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;

namespace Nistec.WinForms
{
	[Designer(typeof(Design.McDockingTabDesigner)), ToolboxItem(false)]
	public class McDockingTab : McPanel
	{

        public McDockingTab()
		{
			base.DockPadding.All = 2;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.Text = "McDockingTab";
			Guid guid1 = System.Guid.NewGuid();
			this.guid = guid1.ToString();
		}

		[Category("Style"),Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set{base.AutoChildrenStyle=value;}
		}

        //[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get{return base.GradientStyle;}
        //    set{base.GradientStyle=value;}
        //}

 
		public void InvokeClosed(EventArgs e)
		{
			this.OnClosed(e);
		}

		public void InvokeClosing(CancelEventArgs e)
		{
			this.OnClosing(e);
		}

 
		protected virtual void OnClosed(EventArgs e)
		{
			if (this.Closed != null)
			{
				this.Closed(this, e);
			}
		}

		protected virtual void OnClosing(CancelEventArgs e)
		{
			if (this.Closing != null)
			{
				this.Closing(this, e);
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			//--((McDockingPanel) base.Parent).Invalidate();
		}

		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			McColors.InitColors();
		}

		[Category("Behavior"), Browsable(false)]
		public string Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}
 
		[Browsable(true), Category("Appearance")]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
                //if (value is Bitmap)
                //{
                //    McPaint.MakeImageBackgroundAlphaZero(value as Bitmap);
                //}
				this.image = value;
				base.Invalidate();
				if (base.Parent != null)
				{
					base.Parent.Invalidate();
				}
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
			set
			{
				this.isSelected = value;
			}
		}
 
		[Browsable(false)]
		public bool IsVisible
		{
			get
			{
				if (base.Parent != null)
				{
					return (((McDockingPanel) base.Parent).Manager.ClosedControls.IndexOf(this) == -1);
				}
				return false;
			}
		}
		[Browsable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				if (base.Parent != null)
				{
					McDockingPanel panel1 = (McDockingPanel) base.Parent;
					if (panel1.SelectedTab == this)
					{
						panel1.InvalidateTitle();
						panel1.InvalidateTabs();
						if (panel1.FloatingForm != null)
						{
							panel1.FloatingForm.Text = value;
						}
					}
				}
			}
		}
 

		// Events
		[Category("Behavior")]
		public event EventHandler Closed;
		[Category("Behavior")]
		public event CancelEventHandler Closing;


		// Fields
		private string guid;
		private Image image;
		private bool isSelected;
	}

}
