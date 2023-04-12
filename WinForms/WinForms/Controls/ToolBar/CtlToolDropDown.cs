using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;

using System.Windows.Forms;
using mControl.Drawing;
using mControl.Util;
using mControl.Win32;

namespace mControl.WinCtl.Controls
{

	public interface ICtlToolButton
	{
		System.Windows.Forms.Control Parent{get;set;}
	}

	[ToolboxItem(false) , Designer(typeof(Design.ToolButtonDesigner))]
	public class CtlToolDropDown :Control,ICtlToolButton// mControl.WinCtl.Controls.CtlBase//,IStyleCtl,System.Windows.Forms.IButtonControl
	{		

		#region Members
		// Events
		public event EventHandler StartDragDrop;

		// Fields
		private bool allowAllUp;
		private bool draw3DButton;
		//private Menu dropDownMenu;
		private bool hotDragDrop;
		protected bool IsMouseOver;
		protected bool IsPressed;
		//private bool lockClick;
		private bool pushed;
		//private ToolBarButtonStyle buttonStyle;
		private bool wordWrap;
		private CtlToolBar parentBar;

		private CtlToolButton			BtnCmd;
		private CtlToolButton     	    BtnItmes;
		private const int btnDropWidth=11;
		private System.ComponentModel.IContainer components = null;
		private ToolTip	toolTip;


		#endregion

		#region Constructors
		
		public CtlToolDropDown():base()
		{

			//this.lockClick = false;
			this.IsMouseOver = false;
			this.IsPressed = false;
			this.wordWrap = false;
			this.hotDragDrop = false;
			this.draw3DButton = false;
			this.allowAllUp = false;
			//this.buttonStyle = ToolBarButtonStyle.ToggleButton;
			this.pushed = false;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.Opaque, false);
			this.TabStop = false;
			this.Height = 0x16;
			this.Width = 0x22;
			this.BackColor = Color.Transparent;


			InitializeComponent();


			this.BtnItmes.Image =ResourceUtil.LoadImage (Global.ImagesPath + "menuarrow.gif");
	
			}

		#endregion

		#region Component Designer generated code
	
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip = new ToolTip();
			//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CtlButtonMenu));
			this.BtnCmd = new CtlToolButton();
			this.BtnItmes = new CtlToolButton();
			this.BtnCmd.owner = this;
			this.BtnItmes.owner = this;
			this.BtnItmes.IsDropButton = true;
			//this.BtnCmd.IsDropButton = true;

			this.SuspendLayout();
			// 
			// BtnCmd
			// 
			this.BtnCmd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.BtnCmd.Location = new System.Drawing.Point(0, 0);
			this.BtnCmd.Name = "BtnCmd";
			this.BtnCmd.Size = new System.Drawing.Size(96, 20);
			this.BtnCmd.TabIndex = 0;
			//this.BtnCmd.Text = "";
			this.BtnCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.BtnCmd.MouseEnter+=new EventHandler(BtnCmd_MouseEnter);
			this.BtnCmd.MouseLeave+=new EventHandler(BtnCmd_MouseLeave);
			// 
			// BtnItmes
			// 
			this.BtnItmes.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnItmes.Location = new System.Drawing.Point(96, 0);
			this.BtnItmes.Name = "BtnItmes";
			this.BtnItmes.ImageAlign=ContentAlignment.MiddleCenter;
			this.BtnItmes.IsDropButton=true;
			this.BtnItmes.Size = new System.Drawing.Size(btnDropWidth, 20);
			this.BtnItmes.TabIndex = 1;
			this.BtnItmes.MouseEnter+=new EventHandler(BtnCmd_MouseEnter);
			this.BtnItmes.MouseLeave+=new EventHandler(BtnCmd_MouseLeave);
 			// 
			// CtlButtonMenu
			// 
			this.Controls.Add(this.BtnCmd);
			this.Controls.Add(this.BtnItmes);
			this.Name = "CtlToolDropDown";
			this.Text = "";
		  	this.ResumeLayout(false);

		}
		#endregion

    	#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(this.BtnCmd!=null)
				{
                   this.BtnCmd.Dispose();
				}
				if(this.BtnItmes!=null)
				{
					this.BtnItmes.Dispose();
				}
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Properties

		[Category("Behavior")]
		[DefaultValue("")]
		public virtual String ToolTip
		{
			get { return toolTip.GetToolTip(this); }
			set
			{
				toolTip.RemoveAll();
				toolTip.SetToolTip(this, value);
			}
		}

		[Category("Behavior"), DefaultValue(false)]
		public bool AllowAllUp
		{
			get
			{
				return this.allowAllUp;
			}
			set
			{
				this.allowAllUp = value;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		public new string Text
		{
			get
			{
				return base.Text;
			}
			set
			{   
				base.Text=value;
				if(this.BtnCmd!=null)
				this.BtnCmd.Text = value;
			}
		}

		public Image Image
		{
			get
			{
				return this.BtnCmd.Image;
			}
			set
			{
				if(this.BtnCmd!=null)
					this.BtnCmd.Image = value;
			}
		}

		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				return this.BtnCmd.ImageIndex;
			}
			set
			{
				if(this.BtnCmd!=null)
					this.BtnCmd.ImageIndex = value;
			}
		}

		public ImageList ImageList
		{
			get
			{
				return this.BtnCmd.ImageList;
			}
			set
			{
				this.BtnCmd.ImageList = value;
			}
		}

		[Category("Appearance")]
		//[DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment ImageAlign
		{
			get{ return this.BtnCmd.ImageAlign; }

			set
			{
				BtnCmd.ImageAlign = value;
				//this.Invalidate();
			}
		}

		[Category("Appearance")]
		//[DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment TextAlign
		{
			get{ return BtnCmd.TextAlign; }

			set
			{
				BtnCmd.TextAlign = value;
				//this.Invalidate();
			}
		}

		[Browsable(false)]
		public bool Draw3DButton
		{
			get
			{
				return this.draw3DButton;
			}
			set
			{
				this.draw3DButton = value;
			}
		}
 
		[Category("Behavior")]
		public Menu DropDownMenu
		{
			get
			{
				return this.BtnItmes.DropDownMenu;// dropDownMenu;
			}
			set
			{
				this.BtnItmes.DropDownMenu = value;
			}
		}

		//private FlatStyle flatStyle;

//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
//		public new FlatStyle FlatStyle
//		{
//			get
//			{
//				return base.FlatStyle;
//			}
//			set
//			{
//				base.FlatStyle = value;
//			}
//		}
		[Browsable(false)]
		public bool HotDragDrop
		{
			get
			{
				return this.hotDragDrop;
			}
			set
			{
				this.hotDragDrop = value;
			}
		}
 
		[DefaultValue(false), Category("Behavior")]
		public bool Pushed
		{
			get
			{
				return this.pushed;
			}
			set
			{
				this.pushed = false;
				base.Invalidate();
			}
		}
 
//		[DefaultValue(2), Category("Behavior")]
//		public ToolBarButtonStyle ButtonStyle
//		{
//			get
//			{
//				return this.buttonStyle;
//			}
//			set
//			{
//				this.buttonStyle = value;
//				base.Invalidate();
//			}
//		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}
 
		[Category("Behavior"), DefaultValue(false)]
		public bool WordWrap
		{
			get
			{
				return this.wordWrap;
			}
			set
			{
				this.wordWrap = value;
				base.Invalidate();
			}
		}

		[Category("Behavior")]
		internal CtlToolBar ParentBar
		{
			get
			{
				return this.parentBar;
			}
			set
			{
				this.parentBar = value;
				//base.Invalidate();
			}
		}
		
		#endregion

		#region Overrides

		protected override void OnPaint(PaintEventArgs p)
		{
			base.OnPaint(p);
			//Rectangle rectangle2;
			//Rectangle rectangle3;
			Graphics graphics1 = p.Graphics;
			Rectangle rectangle1 = new Rectangle(0, 0, base.Width, base.Height);
			if (!base.Enabled || (!this.pushed && !this.IsPressed))
			{
				goto Label_01B0;
			}
			rectangle1.X++;
			rectangle1.Y++;
			rectangle1.Width -= 2;
			rectangle1.Height -= 2;
			if (this.IsPressed)
			{
				using (SolidBrush brush1 = new SolidBrush(CtlPaint.Dark(CtlColors.Selected, 50)))
				{
					graphics1.FillRectangle(brush1, rectangle1);
					goto Label_014A;
				}
			}
			graphics1.FillRectangle(CtlBrushes.Selected, rectangle1);
			Label_014A:
				rectangle1.Width--;
			rectangle1.Height--;
			graphics1.DrawRectangle(CtlPens.SelectedText, rectangle1);
			graphics1.DrawLine( CtlPens.SelectedText,rectangle1.Right-btnDropWidth,rectangle1.Top,rectangle1.Right-btnDropWidth,rectangle1.Bottom);
			rectangle1.X--;
			rectangle1.Y--;
			rectangle1.Width += 3;
			rectangle1.Height += 3;
			Label_01B0:
				if (!this.IsPressed)
				{
					if (this.IsMouseOver && !base.DesignMode)
					{
						graphics1.FillRectangle(CtlBrushes.Focus, rectangle1);
						rectangle1.Width--;
						rectangle1.Height--;
						graphics1.DrawRectangle(CtlPens.SelectedText, rectangle1);
						graphics1.DrawLine( CtlPens.SelectedText,rectangle1.Right-btnDropWidth,rectangle1.Top,rectangle1.Right-btnDropWidth,rectangle1.Bottom);
					}
					else if (base.DesignMode || this.Draw3DButton)
					{
						ControlPaint.DrawBorder3D(graphics1, rectangle1, Border3DStyle.RaisedInner, Border3DSide.All);
					}
				}
			}

		private void BtnCmd_MouseEnter(object sender, EventArgs e)
		{
            OnMouseEnter(e);    
		}

		private void BtnCmd_MouseLeave(object sender, EventArgs e)
		{
           OnMouseLeave(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.IsMouseOver = true;
			base.Invalidate();
		}

 
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.IsMouseOver =false;
			base.Invalidate();
		}

 
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			Point point1 = base.PointToClient(Cursor.Position);
			if ((this.IsPressed && this.HotDragDrop) && !base.ClientRectangle.Contains(point1))
			{
				if (this.StartDragDrop != null)
				{
					this.StartDragDrop(this, EventArgs.Empty);
				}
				this.IsPressed = false;
				base.Invalidate();
			}
		}



		#endregion

	}
}