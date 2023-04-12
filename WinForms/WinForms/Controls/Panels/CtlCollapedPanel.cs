using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//using mControl.Util;


namespace mControl.WinCtl.Controls
{

 
	/// <summary>
	/// An extended <see cref="System.Windows.Forms.Panel">CtlPanel</see> that provides collapsible panels like those provided in Windows XP.
	/// </summary>
	[ToolboxBitmap (typeof(CtlCollapedPanel),"Toolbox.TaskPanel.bmp")]
	[ToolboxItem(false)]
	[Designer(typeof(Design.TaskPanelDesigner))]
	public class CtlCollapedPanel : CtlPanel
	{

		#region Members
		private const int minTitleHeight = 24;
		private const int iconBorder = 2;
		private const int expandBorder = 4;

		private int offsetX;
		private int offsetY;


		private System.Drawing.Imaging.ColorMatrix grayMatrix;
		private System.Drawing.Imaging.ImageAttributes grayAttributes;
		private PanelState state;
		private bool hookPanel;
		private int panelHeight;
		//private int imageIndex;
		private int imageBarIndex;
		private int controlSpace;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label labelTitle;
		private System.Drawing.Image image;
		private System.Drawing.Image lblImage;
		private System.Windows.Forms.ImageList imageList;
        internal bool showStateImage = true;
        internal CtlTaskBar owner;
        private Size expandedSize;
        private Size collapsedSize;

        //private Image imageCollapsed;
        //private Image imageExpanded;
 
		#endregion

		#region Constructors
		/// <summary>
		/// Initialises a new instance of <a cref="mContril.WinCtl.Controls.CtlCollapedPanel">CtlCollapedPanel</a>.
		/// </summary>
        public CtlCollapedPanel()
            : base()
		{
			state = PanelState.Expanded;
    		hookPanel = false;
			//imageIndex = 0;
			imageBarIndex = -1;
			controlSpace=8;
			image=null;

			this.components = new System.ComponentModel.Container();
			InitializeComponent();
			base.m_BorderStyle=BorderStyle.FixedSingle;

			// Set the background TaskPanelColors to ControlLightLight
			//this.BackColor   = Color.AliceBlue;

			// Store the current panelHeight
			this.panelHeight = this.Height;

			// Setup the ColorMatrix and ImageAttributes for grayscale images.
			this.grayMatrix = new ColorMatrix();
			this.grayMatrix.Matrix00 = 1/3f;
			this.grayMatrix.Matrix01 = 1/3f;
			this.grayMatrix.Matrix02 = 1/3f;
			this.grayMatrix.Matrix10 = 1/3f;
			this.grayMatrix.Matrix11 = 1/3f;
			this.grayMatrix.Matrix12 = 1/3f;
			this.grayMatrix.Matrix20 = 1/3f;
			this.grayMatrix.Matrix21 = 1/3f;
			this.grayMatrix.Matrix22 = 1/3f;
			this.grayAttributes = new ImageAttributes();
			this.grayAttributes.SetColorMatrix(this.grayMatrix, ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);
		}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CtlCollapedPanel));
			this.labelTitle = new System.Windows.Forms.Label();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// labelTitle
			// 
			this.labelTitle.BorderStyle = BorderStyle.None;
			this.labelTitle.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelTitle.ForeColor = System.Drawing.Color.Navy;
			this.labelTitle.Location = new System.Drawing.Point(0, 0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(200, 24);
			this.labelTitle.TabIndex = 0;
			this.labelTitle.Text = "Title";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
			this.labelTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseUp);
			this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
			// 
			// CtlCollapedPanel
			// 
			this.Controls.Add(this.labelTitle);
		    this.Controls.SetChildIndex(this.labelTitle,0);
			this.ResumeLayout(false);
		}

		#endregion

		#region Events
		/// <summary>
		/// A <see cref="PanelState">PanelState</see> changed event.
		/// </summary>
        [Category("State"),Description("Raised when panel state has changed.")]
        public event PanelStateChangedEventHandler PanelStateChanged;

        #endregion

		#region Public Properties

 		[Category("Style"),Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set{base.AutoChildrenStyle=value;}
		}

		[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override GradientStyle GradientStyle
		{
			get{return base.GradientStyle;}
			set{base.GradientStyle=value;}
		}


        /// <summary>
        /// Gets/sets the <see cref="PanelState">PanelState</see>.
        /// </summary>
        [Browsable(true), DefaultValue(PanelState.Expanded)]
        public PanelState PanelState
        {
            get
            {
                return this.state;
            }
            set
            {
                if (this.hookPanel)
                {
                    if ((DesignMode) && this.Height <= minTitleHeight) //(this.state == PanelState.Collapsed))
                    {
                        this.state = PanelState.Expanded;
                        SetPanelHeight();
                        this.UpdatePositions();
                        UpdateDisplayedState();
                    }
                    return;
                }
                PanelState oldState = this.state;
                this.state = value;
                if (oldState != this.state)
                {
                    if (DesignMode && value == PanelState.Expanded)
                    {
                        SetPanelHeight();// GetPanelHeight();
                        this.UpdatePositions();
                        //this.SetPanelControls ();
                    }

                    // State has changed to update the display
                    UpdateDisplayedState();
                }
            }
        }

  
		[Browsable(true),DefaultValue(false)]
		public bool PanelHook
		{
			get
			{
				return this.hookPanel;
			}
			set
			{
				this.hookPanel = value;
				if((DesignMode) && (this.state==PanelState.Collapsed) )
				{
					this.PanelState=PanelState.Expanded;
				}
				
			}
		}

		[Browsable(true)]
		public int PanelHeight
		{
			get
			{
				return this.panelHeight;
			}
//			set
//			{
//				this.panelHeight = value;
//			}
		}

		/// <summary>
		/// Gets/sets the text displayed as the panel title.
		/// </summary>
		[Category("Title"),Browsable(true),
		Description("The text contained in the title bar.")]
		public override string Text
		{
			get{return this.labelTitle.Text;}
			set{this.labelTitle.Text = value;}
		}

//		/// <summary>
//		/// Gets/sets the foreground TaskPanelColors used for the title bar.
//		/// </summary>
//		[Category("Title"),DefaultValue(typeof(Color),"Black"),
//		Description("The foreground TaskPanelColors used to display the title text.")]
//		public Color TitleForeColor
//		{
//			get
//			{
//				return this.labelTitle.ForeColor;
//			}
//			set
//			{
//				this.labelTitle.ForeColor = value;
//			}
//		}

		/// <summary>
		/// Gets/sets the font used for the title bar text.
		/// </summary>
		[Category("Title"),
		Description("The font used to display the title text.")]
		public Font TitleFont
		{
			get
			{
				return this.labelTitle.Font;
			}
			set
			{
				this.labelTitle.Font = value;
			}
		}

//		/// <summary>
//		/// Gets/sets the image list used for the expand/collapse image.
//		/// </summary>
//		[Category("Title"),Browsable(false),
//		Description("The image list to get the images displayed for expanding/collapsing the panel.")]
//		private ImageList ImageList
//		{
//			get
//			{
//				return this.imageList;
//			}
//			set
//			{
//				this.imageList = value;
//				if(null != this.imageList)
//				{
//					if(this.imageList.Images.Count > 0)
//					{
//						this.imageIndex = 0;
//					}
//				}
//				else
//				{
//					this.imageIndex = -1;
//				}
//			}
//		}

		/// <summary>
		/// Gets/sets the image displayed in the header of the title bar.
		/// </summary>
		[Category("Title"),DefaultValue(null),
		Description("The image that will be displayed on the left hand side of the title bar.")]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
				if(null != value)
				{
					// Update the height of the title label
					this.labelTitle.Height = this.image.Height + (2 * CtlCollapedPanel.iconBorder);
					if(this.labelTitle.Height < minTitleHeight)
					{
						this.labelTitle.Height = minTitleHeight;
					}
				}
				this.labelTitle.Invalidate();
			}
		}

		//[Category("Title"),DefaultValue(-1),
		//Description("The image index  that will be displayed on the left hand side of the title bar.")]
		[Description("ImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int ImageBarIndex
		{
			get{return this.imageBarIndex;}
			set
			{
				if(value < 0)
				{
					this.imageBarIndex  =-1; 
					this.Image =null;
				}
				else if(Parent is CtlTaskBar) 
				{
					if(value <= ((CtlTaskBar)Parent).ImageList.Images.Count -1)
					{
						this.Image =((CtlTaskBar)Parent).ImageList.Images [value]; 
						this.imageBarIndex  =value; 
					}
				}

				this.labelTitle.Invalidate();
			}
		}

        public ControlLayout TaskBarStyle
        {
            get
            {
                if (owner != null)
                    return owner.ControlLayout;
                else
                    return ControlLayout.Visual;

            }
        }

 		#endregion

		#region Private Helper functions
		// <feature>Expand/Collapse functionality updated as per Windows XP. Whole of title bar is active
		// </feature>
		/// <summary>
		/// Helper function to determine if the mouse is currently over the title bar.
		/// </summary>
		/// <param name="xPos">The x-coordinate of the mouse position.</param>
		/// <param name="yPos">The y-coordinate of the mouse position.</param>
		/// <returns></returns>
		private bool IsOverTitle(int xPos, int yPos)
		{
			// Get the dimensions of the title label
			Rectangle rectTitle = this.labelTitle.Bounds;
			// Check if the supplied coordinates are over the title label
			if(rectTitle.Contains(xPos, yPos))
			{
				return true;
			}
			return false;
		}

        		/// <summary>
		/// Helper function to update the displayed state of the panel.
		/// </summary>
        public void UpdateDisplayedState(bool Collaps)
        {
            this.PanelState = Collaps ? PanelState.Collapsed : PanelState.Expanded;
        }

		/// <summary>
		/// Helper function to update the displayed state of the panel.
		/// </summary>
		private void UpdateDisplayedState()
		{
			switch(this.state)
			{
				case PanelState.Collapsed :
					// Entering collapsed state, so store the current height.
					this.panelHeight = this.Height;
					// Collapse the panel
					//this.Height = labelTitle.Height;
                    SlideShow(labelTitle.Height, panelHeight, false, false);

					// Update the image.
					this.lblImage=CtlTaskBar.imageCollapse;// .imageIndex = 1;
					break;
				case PanelState.Expanded :
                    SetPanelHeight();
					// Entering expanded state, so expand the panel.
					//this.Height = this.panelHeight;
                    SlideShow(labelTitle.Height, panelHeight, true, false);
					// Update the image.
					this.lblImage=CtlTaskBar.imageExpand;//this.imageIndex = 0;
					break;
				default :
					// Ignore
					break;
			}
            //this.labelTitle.Invalidate();
            //OnPanelStateChanged(new PanelStateChangedEventArgs(this));
            //if (Parent is CtlTaskBar)
            //{
            //    ((CtlTaskBar)Parent).UpdatePositions(this);
            //}

		}

        private void SlideShow(int start, int end, bool expanded,bool useTimer)
        {
            if (useTimer)
            {
                if (timer == null)
                {
                    timer = new mControl.Threading.ThreadTimer(10);
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                }
                if (expanded)
                {
                    slideValue = start;
                    slideLimit = end;
                }
                else
                {
                    slideValue = end;
                    slideLimit = start;
                }
                timer.Start();
            }
            else
            {
                if (expanded)
                {
                    if (this.owner !=null && this.owner.ControlLayout == ControlLayout.XpLayout)
                    {
                        for (int i = start; i <= end; i++)
                        {
                            this.Height = i;
                            i++;
                        }
                    }
                    this.Height = end;
                }
                else
                {
                    if (this.owner != null && this.owner.ControlLayout == ControlLayout.XpLayout)
                    {
                        for (int i = end; i >= start; i--)
                        {
                            this.Height = i;
                            i--;
                        }
                    }
                    this.Height = start;
                }
                this.labelTitle.Invalidate();
                OnPanelStateChanged(new PanelStateChangedEventArgs(this));
            }
        }

        private int slideValue;
        private int slideLimit;
        mControl.Threading.ThreadTimer timer;
        delegate void SlideHeightCallBack(int value);

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
          
            switch (this.state)
            {
                case PanelState.Collapsed:
                    if (slideValue < slideLimit)
                    {
                        timer.Stop();
                        SlideEnd(labelTitle.Height);
                        return;
                    }
                    slideValue-=4;
                    break;
                case PanelState.Expanded:
                    if (slideValue > slideLimit)
                    {
                        timer.Stop();
                        SlideEnd(this.panelHeight);
                        return;
                    }
                    slideValue+=4;
                    break;
            }
            //this.Height = slideValue;
            SlideHeight(slideValue);
        }

        private void SlideHeight(int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SlideHeightCallBack(SlideHeight), value);
            }
            else
            {
                this.Height = value;
            }
        }

        private void SlideEnd(int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SlideHeightCallBack(SlideEnd), value);
            }
            else
            {
                this.Height = value;
                this.labelTitle.Invalidate();
                OnPanelStateChanged(new PanelStateChangedEventArgs(this));
            }
        }
        internal void UpdateDisplay(bool imageState)
        {
            if (this.state == PanelState.Collapsed)
                this.lblImage = CtlTaskBar.imageCollapse;
            else      // PanelState.Expanded:
                this.lblImage = CtlTaskBar.imageExpand;
  
            this.showStateImage = imageState;
            this.labelTitle.Invalidate();
        }



//		private void SetPanelControls()
//		{
//			int h=this.labelTitle.Height+4;
//			//Control c;
//
//			//for(int i=Controls.Count-1;i>=0 ;i--)
//			foreach(Control c in this.Controls )
//			{
//				//c=this.Controls[i]; 
//				c.Left =8;
//				c.Top  =h+ mControlSpace;
//				c.Width   =this.Width -16;
//				if(c.Visible )
//					h+=c.Height; 
//			}
//		}

        private void SetPanelHeight()
        {
            this.panelHeight = GetPanelHeight();
           
        }
		private int GetPanelHeight()
		{
            return GetButtomControls()+4;// +6;
		}

		private int GetButtomControls()
		{
			int h=this.labelTitle.Height;
			foreach(Control c in this.Controls )
			{
				if(c.Top+c.Height  > h && c.Visible ) 
					h=c.Top+c.Height ;
			}
			return Math.Min(this.Parent.Height-4, h);
		}

		private void UpdatePositions()
		{
			UpdatePositions(0,items.Count-1 );
		}

		private void UpdatePositions(int index)
		{
			UpdatePositions(index,index );
		}

		private void UpdatePositions(int iStart,int iEnd)
		{
			
			int top=this.labelTitle.Height +4;

			if(iStart>0)
			{
				for(int i = iStart-1; i>0; i--)
				{
					if(this.items[i].Visible)
					{
						top = this.items[i].Bottom + this.ControlSpace;
						break;
					}
				}
			}

//			if(iStart>0)
//			{
//				top = this.items[iStart-1].Bottom + this.ControlSpace;
//			}
			
			for(int i = iStart; i<= iEnd; i++)
			{
				if(!this.items[i].Visible)
					continue;
			    if (this.items[i].Dock != DockStyle.None)
                    continue;

				this.items[i].Top = top;

				top = this.items[i].Bottom + this.ControlSpace;
				
//				if(i == 0)
//					this.items[i].Top = this.labelTitle.Height +4 ;
//				else
//					this.items[i].Top = this.items[i - 1].Bottom + this.ControlSpace;
			
				// Update the panel widths.
				this.items[i].Left = controlSpace;
				this.items[i].Width = this.Width - (controlSpace*2);
				// CtlPanel width adjusted when vertical scroll bars are present.
				if(true == this.VScroll)
				{
					this.items[i].Width -= SystemInformation.VerticalScrollBarWidth;
				}
				
			}
            //if(!this.Controls.Contains (this.labelTitle ))
            //    this.Controls.Add (this.labelTitle );  
		}

		private void UpdateWidthPositions()
		{
			foreach(Control c in this.Controls)
			{
                if (c.Dock == DockStyle.None)
                {
                    c.Left = controlSpace;
                    c.Width = this.Width - (controlSpace * 2);
                    // CtlPanel width adjusted when vertical scroll bars are present.
                    if (true == this.VScroll)
                        c.Width -= SystemInformation.VerticalScrollBarWidth;
                }
			}
		}
		private void ChangeState()
		{
            if (this.PanelHook)
                return;

			if(this.state==PanelState.Collapsed)
				this.state = PanelState.Expanded;
			else
				this.state = PanelState.Collapsed;

			UpdateDisplayedState();
		}


		#endregion

		#region Protected Methods
		/// <summary>
		/// Event handler for the <see cref="Control.ControlAdded">ControlAdded</see> event.
		/// </summary>
		/// <param name="e">A <see cref="System.Windows.Forms.ControlEventArgs">ControlEventArgs</see> that contains the event data.</param>
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);

			if(e.Control is mControl.WinCtl.Controls.LinkLabelItem)
			{
				// Adjust the docking property to Left | Right | Top
				e.Control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
				// Update the size and position of the panels
				if(this.items.Count>0)
					UpdatePositions(this.items.Count - 1);
	
			}
		}

		/// <summary>
		/// Event handler for the <see cref="Control.ControlRemoved">ControlRemoved</see> event.
		/// </summary>
		/// <param name="e">A <see cref="System.Windows.Forms.ControlEventArgs">ControlEventArgs</see> that contains the event data.</param>
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			
			if(e.Control is mControl.WinCtl.Controls.LinkLabelItem)
			{
				// Get the index of the panel within the collection.
				int index = this.items.IndexOf((mControl.WinCtl.Controls.LinkLabelItem)e.Control);
				if(-1 != index && index < items.Count)
				{
					// Remove this panel from the collection.
					this.items.Remove(index);
					// Update the position of any remaining panels.
					UpdatePositions();//this.panels.Count - 1);
				}
			}
		}
		#endregion

		#region Event handlers

		protected override void OnSizeChanged(EventArgs e)
		{
			if (this.Height<minTitleHeight)
			{
				this.Height=minTitleHeight; 
			}
			if (this.Width < minTitleHeight)
			{
				this.Width =minTitleHeight; 
			}

			base.OnSizeChanged (e);
            if (this.Controls.Count > 0)
                UpdateWidthPositions(); 
		
		}

		/// <summary>
		/// Event handler for the <see cref="CtlCollapedPanel.PanelStateChanged">PanelStateChanged</see> event.
		/// </summary>
		/// <param name="e">A <see cref="mContril.WinCtl.Controls.PanelEventArgs">PanelEventArgs</see> that contains the event data.</param>
        protected virtual void OnPanelStateChanged(PanelStateChangedEventArgs e)
		{
            if (this.DesignMode)
            {
                if (Parent is CtlTaskBar)
                {
                    ((CtlTaskBar)Parent).PanelStateChangedInternal(e);
                    ((CtlTaskBar)Parent).UpdatePositions();
                }
            }
            else
            {

                if (Parent is CtlTaskBar)
                {
                    ((CtlTaskBar)Parent).PanelStateChangedInternal(e);
                    ((CtlTaskBar)Parent).UpdatePositions();
                }

                if (PanelStateChanged != null)
                {
                    PanelStateChanged(this, e);
                }

            }
		}

		private void labelTitle_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
            ControlLayout cl = TaskBarStyle;
            switch (cl)
            {
                case ControlLayout.XpLayout:
                    DrawPanelRounded(e);
                    break;
                case ControlLayout.Flat:
                    DrawPanelRect(cl,e); 
                    break;
                case ControlLayout.Visual:
                    DrawPanelRect(cl,e);
                    break;
                case ControlLayout.System:
                    DrawPanelRect(cl,e);
                    break;
                  
            }
            //if(TaskBarStyle==TaskBarStyles.Outlook) 
            //    DrawPanelRect(e);
            //else 
            //    DrawPanelRounded(e);
		}



        #region Paint background

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            PaintBackground(pevent, this.ClientRectangle);

        }

        internal protected void PaintButtonBackground(PaintEventArgs e, Rectangle bounds, Brush background)
        {
            if (background == null)
            {
                this.PaintBackground(e, bounds);
            }
            else
            {
                e.Graphics.FillRectangle(background, bounds);
            }
        }

        internal protected void PaintBackground(PaintEventArgs e, Rectangle rectangle)
        {
            if (this.RenderTransparent)
            {
                this.PaintTransparentBackground(e, rectangle);
            }
            if ((this.BackgroundImage != null) && !SystemInformation.HighContrast)
            {
                TextureBrush brush1 = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
                try
                {
                    Matrix matrix1 = brush1.Transform;
                    matrix1.Translate((float)this.DisplayRectangle.X, (float)this.DisplayRectangle.Y);
                    brush1.Transform = matrix1;
                    e.Graphics.FillRectangle(brush1, rectangle);
                    return;
                }
                finally
                {
                    brush1.Dispose();
                }
            }
            Color color1 = this.BackColor;
            bool flag1 = false;
            if ((color1.A == 0xff))//&& (e.Graphics.GetHdc() != IntPtr.Zero)) //&& (this.BitsPerPixel > 8))
            {
                Win32.RECT rect1 = new Win32.RECT(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
                //Win32.WinAPI.FillRect (new HandleRef(e, e.HDC), ref rect1, new HandleRef(this, this.BackBrush));
                Win32.WinAPI.FillRect((IntPtr)new HandleRef(e, this.Handle), ref rect1, (IntPtr)new HandleRef(this, this.BackBrush));
                flag1 = true;
            }
            if (!flag1 && (color1.A > 0))
            {
                if (color1.A == 0xff)
                {
                    color1 = e.Graphics.GetNearestColor(color1);
                }
                using (Brush brush2 = new SolidBrush(color1))
                {
                    e.Graphics.FillRectangle(brush2, rectangle);
                }
            }
        }

        internal protected void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
        {
            Graphics graphics1 = e.Graphics;
            Control control1 = this.Parent;
            if (control1 != null)
            {
                int num1;
                mControl.Win32.WinMethods.POINT point1 = new mControl.Win32.WinMethods.POINT();
                point1.y = num1 = 0;
                point1.x = num1;
                mControl.Win32.WinMethods.MapWindowPoints(new HandleRef(this, this.Handle), new HandleRef(control1, control1.Handle), point1, 1);
                rectangle.Offset(point1.x, point1.y);
                PaintEventArgs args1 = new PaintEventArgs(graphics1, rectangle);
                GraphicsState state1 = graphics1.Save();
                try
                {
                    graphics1.TranslateTransform((float)-point1.x, (float)-point1.y);
                    this.InvokePaintBackground(control1, args1);
                    graphics1.Restore(state1);
                    state1 = graphics1.Save();
                    graphics1.TranslateTransform((float)-point1.x, (float)-point1.y);
                    this.InvokePaint(control1, args1);
                    return;
                }
                finally
                {
                    graphics1.Restore(state1);
                }
            }
            //graphics1.FillRectangle(SystemBrushes.Control, rectangle);
            graphics1.FillRectangle(new SolidBrush (this.BackColor), rectangle);
        }

        internal bool RenderTransparent
        {
            get
            {
                if (this.GetStyle(ControlStyles.SupportsTransparentBackColor))
                {
                    return true;//(this.BackColor.A < 0xff);
                }
                return false;
            }
        }

        private IntPtr BackBrush
        {
            get
            {
                IntPtr ptr1;
                //				object obj1 = this.Properties.GetObject(Control.PropBackBrush);
                //				if (obj1 != null)
                //				{
                //					return (IntPtr) obj1;
                //				}
                Color color1 = this.BackColor;
                if ((this.Parent != null) && (this.Parent.BackColor == this.BackColor))
                {
                    ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
                    //return this.Parent.BackBrush;
                }
                else if (ColorTranslator.ToOle(color1) < 0)
                {
                    ptr1 = Win32.WinAPI.GetSysColorBrush(ColorTranslator.ToOle(color1) & 0xff);
                    //this.SetState(0x200000, false);
                }
                else
                {
                    ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
                    //this.SetState(0x200000, true);
                }
                //this.Properties.SetObject(Control.PropBackBrush, ptr1);
                return ptr1;
            }
        }
        #endregion

		private void DrawPanelRounded(System.Windows.Forms.PaintEventArgs e)
		{

			const int diameter = 14;
			int radius = diameter / 2;
			Rectangle bounds =new Rectangle (labelTitle.Bounds.X ,labelTitle.Bounds.Y , labelTitle.Bounds.Width -1,labelTitle.Bounds.Height );
			SetBoundOffset();  
			if(offsetY != 0)
			{
				bounds.Offset(0, offsetY);
				bounds.Height -= offsetY;
			}
            GraphicsPath path = mControl.Drawing.DrawUtils.GettRoundedTopRect(bounds, radius);

            if (Parent is IPanel)
                e.Graphics.Clear(((IStyleCtl)this.Parent).CtlStyleLayout.Layout.FlatColorInternal);
            else
                e.Graphics.Clear(this.Parent.BackColor);

			Pen   penBorder=CtlStyleLayout.GetPenBorder();

			Brush brush;

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			if(true == this.Enabled)
			{
				brush=CtlStyleLayout.GetBrushCaptionGradient(bounds,180f,true);
			}
			else
			{
				// Paint the grayscale gradient into the title label.
				brush=mControl.Drawing.CtlBrushes.GetControlBrush(bounds,90f);
			}
            
            //e.Graphics.Clip = new Region(path);

            //// Paint the TaskPanelColors gradient into the title label.
            e.Graphics.FillPath(brush, path);
           

			// Draw the CtlLabel Border.
			e.Graphics.DrawPath (penBorder, path);

			// Draw  line at the bottom:
			const int lineWidth = 1;
			e.Graphics.DrawLine(penBorder, bounds.Left, bounds.Bottom - lineWidth, bounds.Right, 
				bounds.Bottom - lineWidth);

            brush.Dispose(); 
			penBorder.Dispose();

			DrawLabelImage(e.Graphics);
			DrawLabelString(ControlLayout.XpLayout,  e.Graphics);
            if(showStateImage)
			DrawLabelImageState(e.Graphics,bounds);

		}


		private void DrawPanelRect(ControlLayout cl, System.Windows.Forms.PaintEventArgs e)
		{
			Rectangle bounds =new Rectangle (labelTitle.Bounds.X ,labelTitle.Bounds.Y , labelTitle.Bounds.Width -1,labelTitle.Bounds.Height );
	        SetBoundOffset(); 
 			if(offsetY != 0)
			{
				bounds.Offset(0, offsetY);
				bounds.Height -= offsetY;
			}

			Brush brush;

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			if(true == this.Enabled)
			{
                if(cl==ControlLayout.Visual)
					brush=CtlStyleLayout.GetBrushCaptionGradient(bounds,90f,true);
                else if (cl == ControlLayout.Flat)
                    brush = CtlStyleLayout.GetBrushCaption();
                else //if (cl == ControlLayout.System)
                    brush = CtlStyleLayout.GetBrushGradient(bounds, 270f);//, true);
            }
			else
			{
				// Paint the grayscale gradient into the title label.
				brush=mControl.Drawing.CtlBrushes.GetControlBrush(bounds,90f);
			}

			// Paint the TaskPanelColors gradient into the title label.
			e.Graphics.FillRectangle(brush, bounds);
	
			// Draw the CtlLabel Border.
			using(Pen penBorder=CtlStyleLayout.GetPenBorder())
			{
				e.Graphics.DrawRectangle (penBorder, bounds);
				// Draw  line at the bottom:
				//const int lineWidth = 1;
				//e.Graphics.DrawLine(penBorder, bounds.Left, bounds.Bottom - lineWidth, bounds.Right, 
				//	bounds.Bottom - lineWidth);
			}

			brush.Dispose(); 


			DrawLabelImage(e.Graphics);
			DrawLabelString(cl,e.Graphics);
            if(showStateImage)
			DrawLabelImageState(e.Graphics,bounds);
		}

		private void SetBoundOffset()
		{
			offsetY = 0;
			if(null != this.image)
			{
				offsetY = this.labelTitle.Height - CtlCollapedPanel.minTitleHeight;
				if(offsetY < 0)
				{
					offsetY = 0;
				}
				//bounds.Offset(0, offsetY);
				//bounds.Height -= offsetY;
			}
		}

		private void DrawLabelImage(Graphics g)
		{
			// Draw the header icon, if there is one
			System.Drawing.GraphicsUnit graphicsUnit = System.Drawing.GraphicsUnit.Display;
			offsetX = CtlCollapedPanel.iconBorder;
			if(null != this.image)
			{
				offsetX += this.image.Width + CtlCollapedPanel.iconBorder;
				// Draws the title icon grayscale when the panel is disabled.
				RectangleF srcRectF = this.image.GetBounds(ref graphicsUnit);
				Rectangle destRect = new Rectangle(CtlCollapedPanel.iconBorder,
					CtlCollapedPanel.iconBorder, this.image.Width, this.image.Height);
				if(true == this.Enabled)
				{
					g.DrawImage(this.image, destRect, (int)srcRectF.Left, (int)srcRectF.Top,
						(int)srcRectF.Width, (int)srcRectF.Height, graphicsUnit);
				}
				else
				{
					g.DrawImage(this.image, destRect, (int)srcRectF.Left, (int)srcRectF.Top,
						(int)srcRectF.Width, (int)srcRectF.Height, graphicsUnit, this.grayAttributes);
				}
				
			}

		}


		private void DrawLabelString(ControlLayout cl, Graphics g)
		{
			// Draw the title text.
			using(Brush textBrush= cl== ControlLayout.System ? CtlStyleLayout.GetBrushCaption() : CtlStyleLayout.GetBrushContent())//GetBrushText())
			{
				float left = (float)offsetX;
				float top = (float)offsetY + (float)CtlCollapedPanel.expandBorder;
				float width = (float)this.labelTitle.Width - left - this.imageList.ImageSize.Width - 
					CtlCollapedPanel.expandBorder;
				float height = (float)CtlCollapedPanel.minTitleHeight - (2f * (float)CtlCollapedPanel.expandBorder);
				RectangleF textRectF = new RectangleF(left, top, width, height);
				StringFormat format = new StringFormat();
				format.Trimming = StringTrimming.EllipsisWord;
				if(true == this.Enabled)
				{
					g.DrawString(labelTitle.Text, labelTitle.Font, textBrush, 
						textRectF, format);
				}
				else
				{
					Color disabled = SystemColors.GrayText;
					ControlPaint.DrawStringDisabled(g, labelTitle.Text, labelTitle.Font,
						disabled, textRectF, format);
				}
				format.Dispose();
			}

		}

		private void DrawLabelImageState(Graphics g,Rectangle bounds)
		{
			if(!this.hookPanel)
			{

				System.Drawing.GraphicsUnit graphicsUnit = System.Drawing.GraphicsUnit.Display;
				// Draw the expand/collapse image
				int xPos = bounds.Right - this.imageList.ImageSize.Width - CtlCollapedPanel.expandBorder;
				int yPos = bounds.Top + CtlCollapedPanel.expandBorder;
//				RectangleF srcIconRectF = this.ImageList.Images[(int)this.state].GetBounds(ref graphicsUnit);
//				Rectangle destIconRect = new Rectangle(xPos, yPos,this.imageList.ImageSize.Width, this.imageList.ImageSize.Height);
				RectangleF srcIconRectF = this.lblImage.GetBounds(ref graphicsUnit);
				Rectangle destIconRect = new Rectangle(xPos, yPos,this.lblImage.Size.Width, this.lblImage.Size.Height);

				if(true == this.Enabled)
				{
//					g.DrawImage(this.ImageList.Images[(int)this.state], destIconRect,
//						(int)srcIconRectF.Left, (int)srcIconRectF.Top, (int)srcIconRectF.Width,
//						(int)srcIconRectF.Height, graphicsUnit);
					g.DrawImage(this.lblImage, destIconRect,
						(int)srcIconRectF.Left, (int)srcIconRectF.Top, (int)srcIconRectF.Width,
						(int)srcIconRectF.Height, graphicsUnit);
				}
				else
				{
//					g.DrawImage(this.ImageList.Images[(int)this.state], destIconRect,
//						(int)srcIconRectF.Left, (int)srcIconRectF.Top, (int)srcIconRectF.Width,
//						(int)srcIconRectF.Height, graphicsUnit, this.grayAttributes);
					g.DrawImage(this.lblImage, destIconRect,
						(int)srcIconRectF.Left, (int)srcIconRectF.Top, (int)srcIconRectF.Width,
						(int)srcIconRectF.Height, graphicsUnit, this.grayAttributes);
				}
			}

		}

		private void labelTitle_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if((e.Button == MouseButtons.Left) && (true == IsOverTitle(e.X, e.Y)))
			{
				ChangeState();
			}
		}

		private void labelTitle_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if((e.Button == MouseButtons.None) && (true == IsOverTitle(e.X, e.Y)))
			{
				this.labelTitle.Cursor = Cursors.Hand;
			}
			else
			{
				this.labelTitle.Cursor = Cursors.Default;
			}
		}

	    #endregion

		#region IStyleCtl

		public override IStyleLayout CtlStyleLayout
		{
			get
			{
				if(this.m_StylePainter!=null)
					return base.m_StylePainter.Layout as IStyleLayout;
				else if(owner!=null)
					return owner.CtlStyleLayout as IStyleLayout;
				else
					return base.CtlStyleLayout;
			}
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			this.Invalidate ();
            if(this.labelTitle!=null)
			this.labelTitle.Invalidate (); 
		}

        //[Category("Style"), DefaultValue(ControlLayout.System)]
        //public override ControlLayout ControlLayout
        //{
        //    get 
        //    {
        //        if (owner != null)
        //            return owner.ControlLayout;
        //        else
        //            return base.ControlLayout;
        //    }
        //    set
        //    {
        //        if (base.ControlLayout != value)
        //        {
        //            if (owner != null)
        //            {
        //                base.ControlLayout = owner.ControlLayout;
        //            }
        //            else
        //            {
        //                base.ControlLayout = value;
        //            }
        //            UpdateDisplay(true);
        //            //this.labelTitle.Invalidate();
        //            //OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        //            this.Invalidate();
        //        }
        //    }

        //}
		#endregion

    
    }
}
