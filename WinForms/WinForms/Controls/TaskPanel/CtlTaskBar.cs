using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace mControl.WinCtl.Controls
{

	/// <summary>
	/// An ExplorerBar-type extended CtlPanel for containing <see cref="CtlTaskPanel">CtlTaskPanel</see> objects.
	/// </summary>
	[Designer(typeof(Design.TaskBarDesigner))]
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(CtlTaskBar),"Toolbox.TaskPanelBar.bmp")]
	public class CtlTaskBar : CtlPanel, ISupportInitialize
	{
		#region Members
		private const int minTitleHeight = 24;
		private TaskPanelCollection panels = new TaskPanelCollection();
		private System.Windows.Forms.ImageList imageList;
		private int border =0;// 8;
		private int spacing =0;// 8;
		private bool initialising = false;
		private bool singleActive;
        internal bool showStateImage = true;

        //internal CtlTaskPanel selectedPanel =null;


		internal static Image imageCollapse;
		internal static Image imageExpand;


        /// <summary>
        /// A <see cref="PanelState">PanelState</see> changed event.
        /// </summary>
        [Category("State"), Description("Raised when panel state has changed.")]
        public event PanelStateChangedEventHandler PanelStateChanged;

		#endregion

		#region Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="mContril.WinCtl.Controls.CtlTaskBar">CtlTaskBar</see>.
		/// </summary>
		public CtlTaskBar() //: base(Styles.Custom,true,true)
		{
			InitializeComponent();
			base.m_BorderStyle=BorderStyle.FixedSingle;
            //base.allowGradient = false;
		}

		static CtlTaskBar()
		{
			CtlTaskBar.imageCollapse =ResourceUtil.LoadImage (Global.ImagesPath + "xpCollapse.gif");
			CtlTaskBar.imageExpand =ResourceUtil.LoadImage (Global.ImagesPath + "xpExpand.gif");
		}

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //Resettings();
        }
 		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.imageList = new System.Windows.Forms.ImageList();//this.components);
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CtlTaskBar
			// 
			this.panels.Inserted +=new mControl.Collections.CollectionChange(panels_Inserted); 
			this.panels.Removed +=new mControl.Collections.CollectionChange(panels_Removed);
			this.panels.Cleared +=new mControl.Collections.CollectionClear(panels_Cleared);
			this.ResumeLayout(false);
		}

		private void panels_Inserted(int index, object value)
		{
            this.Controls.Add (value as CtlTaskPanel );
            //this.panels[index].PanelStateChanged +=
            //    new PanelStateChangedEventHandler(this.panel_StateChanged);

		}

		private void panels_Removed(int index, object value)
		{
			if(index > 0 && index < panels.Count)
			{
                //this.panels[index].PanelStateChanged -=
                //    new PanelStateChangedEventHandler(this.panel_StateChanged);
				this.Controls.Remove (value as CtlTaskPanel );   
			}
		}

		private void panels_Cleared()
		{
			this.Controls.Clear  ();   
		}

		#endregion

		#region Public Properties

        private void Resettings()
        {
            SerializeImages();
            UpdatePositions();
            UpdateDisplayAll();
            this.Invalidate();
        }

        private void SerializeImages()
        {
 
            switch (base.ControlLayout)
            {
                case ControlLayout.XpLayout:
                    this.border = 8;
                    this.spacing = 8;
                    CtlTaskBar.imageCollapse = ResourceUtil.LoadImage(Global.ImagesPath + "xpCollapse.gif");
                    CtlTaskBar.imageExpand = ResourceUtil.LoadImage(Global.ImagesPath + "xpExpand.gif");
                    break;
                case ControlLayout.Visual:
                    this.border = 0;
                    this.spacing = 0;
                    CtlTaskBar.imageCollapse = ResourceUtil.LoadImage(Global.ImagesPath + "collapsed.gif");
                    CtlTaskBar.imageExpand = ResourceUtil.LoadImage(Global.ImagesPath + "expaned.gif");
                    break;
                case ControlLayout.Flat:
                    this.border = 0;
                    this.spacing = 0;
                    CtlTaskBar.imageCollapse = ResourceUtil.LoadImage(Global.ImagesPath + "collapsed.gif");
                    CtlTaskBar.imageExpand = ResourceUtil.LoadImage(Global.ImagesPath + "expaned.gif");
                    break;
                case ControlLayout.System:
                    this.border = 0;
                    this.spacing = 0;
                    CtlTaskBar.imageCollapse = ResourceUtil.LoadImage(Global.ImagesPath + "collapsed.gif");
                    CtlTaskBar.imageExpand = ResourceUtil.LoadImage(Global.ImagesPath + "expaned.gif");
                    break;
            }
         }

        [Category("Appearance"), RefreshProperties(RefreshProperties.Repaint)]
        public override ControlLayout ControlLayout
        {
            get
            {
                return base.ControlLayout;
            }
            set
            {
                if (base.ControlLayout != value)
                {
                    base.ControlLayout = value;
                    SerializeImages();
                    UpdatePositions();
                    UpdateDisplayAll();
                    this.Invalidate();
                }
            }
        }

		[Category("Appearance")]
		public bool SingleActive
		{
			get{return this.singleActive;}
			set
			{
				if(SingleActive!=value)
				{
					this.singleActive=value;
					//UpdatePositions();
					this.Invalidate();
				}
			}
		}


		/// <summary>
		/// Gets the <see cref="TaskPanelCollection">TaskPanelCollection</see> collection.
		/// </summary>
		
		//[Editor("System.Windows.Forms.Design.ComponentEditorForm,System.Design", 
		//	 "System.Drawing.Design.UITypeEditor,System.Drawing"),

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content )]
		public TaskPanelCollection Panels
		{
			get
			{
				return this.panels;
			}
		}

		/// <summary>
		/// Gets/sets the border around the panels. 
		/// </summary>
		internal int Border
		{
			get{return this.border;}
			set
			{
				this.border = value;
				UpdatePositions();//this.panels.Count - 1);
			}
		}

		/// <summary>
		/// Gets/sets the vertical spacing between adjacent panels.
		/// </summary>
		internal int Spacing
		{
			get{return this.spacing;}
			set
			{
				this.spacing = value;
				UpdatePositions();//this.panels.Count - 1);
			}
		}

		/// <summary>
		/// Gets/sets the image list used for the expand/collapse image.
		/// </summary>
		[Category("Title"),
		Description("The image list to get the images displayed for expanding/collapsing the panel.")]
		public ImageList ImageList
		{
			get{return this.imageList;}
			set{this.imageList = value;}
		}

        /// <summary>
        /// Gets/sets Show State Image visible.
        /// </summary>
        public bool ShowStateImage
        {
            get { return this.showStateImage; }
            set
            {
                this.showStateImage = value;
                UpdateDisplayAll();
            }
        }


		#endregion

		#region Public Methods
		/// <summary>
		/// Signals the object that initialization is starting.
		/// </summary>
		public void BeginInit()
		{
			this.initialising = true;
		}

		/// <summary>
		/// Signals the object that initialization is complete.
		/// </summary>
		public void EndInit()
		{
			this.initialising = false;
		}
		#endregion

        #region Private Helper Functions

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetPositions();
        }
        public void SetPositions()
        {

            int countExpanded = 0;
            int totalExpanded = 0;
            int totalCollapsed = 0;
            int totalHoked = 0;
            int totalHeight = 0;
            int totalDiff = 0;
            int diff = 0;
            foreach (CtlTaskPanel p in this.panels)
            {
                if (p.Visible)
                {
                             if (p.PanelHook)
                        totalHoked += p.Height;
            else if (p.PanelState == PanelState.Expanded)
                    {
                        totalExpanded += p.Height;
                        countExpanded++;
                    }
                    else
                        totalCollapsed += p.Height;
                }
            }
            if (countExpanded == 0)
                return;
            totalHeight = totalExpanded + totalCollapsed + totalHoked;
            totalDiff = this.Height - totalHeight;
            diff = totalDiff / countExpanded;
            foreach (CtlTaskPanel p in this.panels)
            {
                if (p.Visible && p.PanelState == PanelState.Expanded && !p.PanelHook)
                    p.Height = Math.Max(minTitleHeight + border, p.Height + diff);
            }
            UpdatePositions();

        }

        public void UpdatePositions()
        {
            UpdatePositions(0, panels.Count - 1);
        }

        private void UpdatePositions(int index)
        {
            UpdatePositions(index, index);
        }

        private void UpdatePositions(int iStart, int iEnd)
        {
            int top = this.border;
            if (iStart > 0)
            {
                for (int i = iStart - 1; i > 0; i--)
                {
                    if (this.panels[i].Visible)
                    {
                        top = this.panels[i].Bottom + this.border;
                        break;
                    }
                }
            }

            for (int i = iStart; i <= iEnd; i++)
            {
                if (!this.panels[i].Visible)
                    continue;

                this.panels[i].Top = top;

                top = this.panels[i].Bottom + this.border;

                // Update the panel widths.
                this.panels[i].Left = this.spacing;
                this.panels[i].Width = this.Width - (2 * this.spacing);
                // CtlPanel width adjusted when vertical scroll bars are present.
                if (true == this.VScroll)
                {
                    this.panels[i].Width -= SystemInformation.VerticalScrollBarWidth;
                }

            }
        }

        public void UpdateDisplayAll()
        {
            for (int i = 0; i < this.panels.Count; i++)
            {
                this.panels[i].UpdateDisplay(this.showStateImage);
            }
        }


        //public void UpdatePositions(CtlTaskPanel panel)
        //{
        //    // Get the index of the control that just changed state.
        //    int index = this.panels.IndexOf(panel);
        //    if (-1 != index)
        //    {
        //        if (this.singleActive && !DesignMode)
        //        {
        //            if (panel.PanelState == PanelState.Collapsed)
        //                return;
        //            foreach (CtlTaskPanel c in this.Panels)
        //            {
        //                if (c.PanelState == PanelState.Expanded && c != panel && !c.PanelHook)
        //                {
        //                    c.PanelState = PanelState.Collapsed;
        //                }
        //            }
        //            //if (index == 0)
        //            //    this.panels[index].Top = this.border;
        //            //else
        //            //    this.panels[index].Top = this.panels[index - 1].Bottom + this.border;


        //            //this.panels[index].Height = this.Height - border - ((this.panels.Count - 1) * (minTitleHeight + border));

        //            //index++;


        //            this.panels[index].Height = this.Height - border - CalcPanelsHieght();
        //            UpdatePositions();
        //            return;
        //        }
        //        // Now update the position of all subsequent panels.
        //        UpdatePositions(index, panels.Count - 1);//--index);
        //    }
        //}
        public void UpdatePositions(CtlTaskPanel panel)
        {
            //this.selectedPanel = panel;
            int indx = panels.IndexOf(panel);
            UpdatePositions(indx, panels.Count - 1);
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

     		if(e.Control is CtlTaskPanel)
     		{
				// Adjust the docking property to Left | Right | Top
				e.Control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
				((CtlTaskPanel)e.Control).owner=this;
	
	
			if(true == initialising)
				{
//					// In the middle of InitializeComponent call.
//					// Generated code adds panels in reverse order, so add to end
//					if(!this.panels.Contains ((CtlTaskPanel)e.Control))
//					this.panels.Add((CtlTaskPanel)e.Control);
//
//					this.panels[this.panels.Count - 1].PanelStateChanged +=
//						new PanelStateChangedEventHandler(this.panel_StateChanged);
				}
				else
				{
//					// Add the panel to the beginning of the internal collection.
//					panels.Insert(0, (CtlTaskPanel)e.Control);
//
//					panels[0].PanelStateChanged += 
//						new PanelStateChangedEventHandler(this.panel_StateChanged);
				}
		
              	// Update the size and position of the panels
				if(this.panels.Count >0)
				UpdatePositions(this.panels.Count - 1);
	
			}
		}

		/// <summary>
		/// Event handler for the <see cref="Control.ControlRemoved">ControlRemoved</see> event.
		/// </summary>
		/// <param name="e">A <see cref="System.Windows.Forms.ControlEventArgs">ControlEventArgs</see> that contains the event data.</param>
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);

            if(e.Control is CtlTaskPanel)
            {
				// Get the index of the panel within the collection.
				int index = this.panels.IndexOf((CtlTaskPanel)e.Control);
				if(-1 != index && index < panels.Count)
				{
					// Remove this panel from the collection.
					this.panels.Remove(index);
					// Update the position of any remaining panels.
					UpdatePositions();//this.panels.Count - 1);
				}
			}
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged (e);
//			foreach(CtlTaskPanel c in this.panels)
//			{
//				c.StylePainter=this.StylePainter;
//			}
			this.Invalidate(true);

		}

		#endregion

		#region Event handlers
        //private void panel_StateChanged(object sender, PanelStateChangedEventArgs e)
        //{
        //    // Get the index of the control that just changed state.
        //    int index = this.panels.IndexOf(e.CtlTaskPanel);
        //    if (-1 != index)
        //    {
        //        if (this.singleActive && !DesignMode)
        //        {
        //            if (e.PanelState == PanelState.Collapsed)
        //                return;
        //            foreach (CtlTaskPanel c in this.Panels)
        //            {
        //                if (c.PanelState == PanelState.Expanded && c != e.CtlTaskPanel && !c.PanelHook)
        //                {
        //                    c.PanelState = PanelState.Collapsed;
        //                }
        //            }
        //            this.panels[index].Height = this.Height - border - CalcPanelsHieght();
        //            UpdatePositions();
        //            return;
        //        }
        //        // Now update the position of all subsequent panels.
        //        UpdatePositions(index, panels.Count - 1);//--index);
        //    }
        //}

		private int CalcPanelsHieght()
		{
			int hieght=0;


			foreach(CtlTaskPanel c in this.Panels)
			{
                if (c.PanelHook)
                    hieght += c.Height + border;
                else if (c.Visible)
				    hieght+=minTitleHeight+border;
			}
			return hieght-(minTitleHeight+border);
		}

        internal void PanelStateChangedInternal(PanelStateChangedEventArgs e)
        {
            OnPanelStateChanged(e);
        }

        protected virtual void OnPanelStateChanged(PanelStateChangedEventArgs e)
        {
            // Get the index of the control that just changed state.
            int index = this.panels.IndexOf(e.CtlTaskPanel);
            if (-1 != index)
            {
                if (this.singleActive && !DesignMode)
                {
                    if (e.PanelState == PanelState.Collapsed)
                    {
                        UpdatePositions();
                        goto label_01;// return;
                    }
                    foreach (CtlTaskPanel c in this.Panels)
                    {
                        if (c.PanelState == PanelState.Expanded && c != e.CtlTaskPanel && !c.PanelHook)
                        {
                            c.PanelState = PanelState.Collapsed;
                        }
                    }
                    if (this.ControlLayout != ControlLayout.XpLayout)
                    {
                        this.panels[index].Height = this.Height - border - CalcPanelsHieght();
                    }

                    UpdatePositions();
                    goto label_01;// return;
                }
                // Now update the position of all subsequent panels.
                UpdatePositions(index, panels.Count - 1);//--index);
            }
            label_01:
            if (this.PanelStateChanged != null)
            {
                PanelStateChanged(this,e);
            }

        }
		#endregion

	}
}
