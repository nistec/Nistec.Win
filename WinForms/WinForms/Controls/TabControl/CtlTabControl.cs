using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Microsoft.Win32;

using Nistec.Win32;

using Nistec.Collections;
using Nistec.Drawing;
using Nistec.WinForms.Controls;
using Nistec.Win;

namespace Nistec.WinForms
{

	
    /// <summary>
    /// Manages a related set of tab pages.
    /// </summary>
	[ToolboxBitmap (typeof(McTabControl),"Toolbox.TabControl.bmp")]
	[ToolboxItem(true)]
    [DefaultProperty("ControlLayout")]
    [DefaultEvent("SelectedIndexChanged")]
	[Designer(typeof(Design.TabControlDesigner))]
	public class McTabControl : McContainer
	{

		#region Enum


		// Enumeration of modes that control display of the tabs area


		// Indexes into the menu images strip
		private enum ImageStrip
		{
			LeftEnabled = 0,
			LeftDisabled = 1,
			RightEnabled = 2,
			RightDisabled = 3,
			Close = 4,
			Error = 5
		}

		// Enumeration of Indexes into positioning constants array
        //private enum PositionIndex
        //{
        //    BorderTop			= 0,
        //    BorderLeft			= 1,
        //    BorderBottom		= 2, 
        //    BorderRight			= 3,
        //    ImageGapTop			= 4,
        //    ImageGapLeft		= 5,
        //    ImageGapBottom		= 6,
        //    ImageGapRight		= 7,
        //    TextOffset			= 8,
        //    TextGapLeft			= 9,
        //    TabsBottomGap		= 10,
        //    ButtonOffset		= 11,
        //}

		#endregion

		#region MultiRect

		// Helper class for handling multiline calculations
        private class MultiRect
		{
			protected Rectangle _rect;
			protected int _index;

			public MultiRect(Rectangle rect, int index)
			{
				_rect = rect;
				_index = index;
			}

			public int Index
			{
				get { return _index; }
			}            
            
			public Rectangle Rect
			{
				get { return _rect; }
				set { _rect = value; }
			}
            
			public int X
			{
				get { return _rect.X; }
				set { _rect.X = value; }
			}

			public int Y
			{
				get { return _rect.Y; }
				set { _rect.Y = value; }
			}

			public int Width
			{
				get { return _rect.Width; }
				set { _rect.Width = value; }
			}

			public int Height
			{
				get { return _rect.Height; }
				set { _rect.Height = value; }
			}
		}
        
		#endregion

		#region HostPanel

        private class HostPanel : System.Windows.Forms.Panel
		{
			public HostPanel()
			{
				// Prevent flicker with double buffering and all painting inside WM_PAINT
				SetStyle(ControlStyles.DoubleBuffer, true);
				SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				SetStyle(ControlStyles.UserPaint, true);
			}
            
			protected override void OnResize(EventArgs e)
			{
				// Update size of each child to match ourself
				foreach(Control c in this.Controls)
					c.Size = this.Size;
            
				base.OnResize(e);
			}
		}

		#endregion

		#region static Members
      
        	const int PositionBorderTop			= 6;//0;
			const int PositionBorderLeft		= 2;//1;
			const int PositionBorderBottom		= 2;//2;
			const int PositionBorderRight		= 3;//3;
			const int PositionImageGapTop		= 3;//4;
			const int PositionImageGapLeft		= 1;//5;
			const int PositionImageGapBottom	= 1;//6;
			const int PositionImageGapRight		= 0;//7;
			const int PositionTextOffset		= 1;//8;
			const int PositionTextGapLeft		= 1;//9;
			const int PositionTabsBottomGap		= 2;//10;
            const int PositionButtonOffset      = 0;//11;
            
            const int PositionShadowWidth       = 2;//12;
            const int PositionPlainBorder       = 4;//13;

		// Class constants for sizing/positioning each style
        //private static int[] _position = { 6, 2, 2, 3, 3, 1, 1, 0, 1, 1, 2, 0 };

        //{6, 2, 2, 3, 3, 1, 1, 0, 1, 1, 2, 0}/*{2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 2, 2}*/,	// IDE
        //{6, 2, 2, 3, 3, 1, 1, 0, 1, 1, 2, 0}	// Plain {6, 2, 2, 3, 3, 1, 1, 0, 1, 1, 2, 0}
        //};
        //{3, 1, 1, 1, 1, 2, 1, 1, 2, 1, 3, 2},	// IDE
        //{6, 2, 2, 3, 3, 1, 1, 0, 1, 1, 2, 0}	// Plain
	
		// Class constants
        //private static int _plainBorder = 3;
       // private static int PositionPlainBorder = 4;// 6;
        private static int _tabsAreaStartInset = 0;// 5;
        private static int _tabsAreaEndInset = 5;
        //private static float _alphaIDE = 1.5F;
        private static int _buttonGap = 3;
        private static int _buttonWidth = 14;
        private static int _buttonHeight = 14;
        private static int _imageButtonWidth = 12;
        private static int _imageButtonHeight = 12;
        //private static int _multiBoxAdjust = 2;
        private readonly Rectangle _nullPosition = new Rectangle(-999, -999, 0, 0);//Rectangle.Empty;//

		// Class state
        private static ImageList _internalImages;

		#endregion

		#region protected Members

		// Instance fields - size/positioning
		private int _textHeight;
        private int _imageWidth;
        private int _imageHeight;
        private int _imageGapTopExtra;
        private int _imageGapBottomExtra;
        private Rectangle _pageRect;
        private Rectangle _pageAreaRect;
        private Rectangle _tabsAreaRect;

        //private int _styleIndex;				// Index into position array
        private int _pageSelected;			// index of currently selected page (-1 is none)
        private int _startPage;				// index of first page to draw, used when scrolling pages
        private int _hotTrackPage;			// which page is currently displayed as being tracked
        private int _topYPos;                 // Y position of first line in multiline mode
        private int _bottomYPos;              // Y position of last line in multiline mode

        //private bool _mouseOver;              // Mouse currently over the control (or child pages)
        private bool _multiline;              // should tabs that cannot fit on a line create new lines
        private bool _changed;				// Flag for use when updating contents of collection
        private bool _positionAtTop;			// display tabs at top or bottom of the control
        private bool _showClose;				// should the close button be displayed
        private bool _showArrows;				// should then scroll arrow be displayed
        private bool _rightScroll;			// Should the right scroll button be enabled
        private bool _leftScroll;				// Should the left scroll button be enabled
        //private bool _hotTrack;				// should mouve moving over text hot track it
        private bool _recalculate;			// flag to indicate recalculation is needed before painting
        private bool _recordFocus;			// Record the control with focus when leaving a page

		//Colors
        private Color _backIDE;				// background drawing color when in IDE appearance
        //private Color _ActiveColor;		    // color for drawing when active
        //private Color _InactiveColor;			// color for drawing  when inactive
        //private Color _backLight;				// light variation of the back color
        private Color _backLightLight;		// lightlight variation of the back color
        private Color _backDark;				// dark variation of the back color
        private Color _backDarkDark;			// darkdark variation of the back color

        private bool _hideTabs;               // Decide when to hide/show tabs area
        //private HideTabsModes _hideTabsMode;  // Decide when to hide/show tabs area
        private HostPanel _hostPanel;         // Hosts the page instance control/form
        private ImageList _imageList;			// collection of images for use is tabs
        private ArrayList _tabRects;			// display rectangles for associated page
        private TabPageCollection _tabPages;	// collection of pages

		// Instance fields - buttons
        private ImageButton _closeButton;
        private ImageButton _leftArrow;
        private ImageButton _rightArrow;

        private AlignmentOptions _AlignmentOption;
        //private ControlLayout _ControlLayout;
        //private BackgroundIDE drawBackgroung;
		private Size itemSize;
    
        internal bool hideButtons;
		internal Control owner=null;
        //private bool autoToolTip = false;
        /*toolTip*/ 
        private McToolTip toolTip;
        private bool showToolTip=true;
        private McTabPage _hoverPage;

		const int DefaultTitleHeight =20;// 0x16;

		private Size DefaultItemSize
		{
			get{return new Size(0,DefaultTitleHeight);} 
		}

        public void HideButtons(bool value)
        {
            hideButtons = value;
        }

		#endregion

		#region delegate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="page"></param>
		public delegate void DoubleClickTabHandler(McTabControl sender, McTabPage page);

		/// <summary>
        /// Occurs when Close Button Pressed
		/// </summary>
		public event EventHandler ClosePressed;
        /// <summary>
        /// Occurs when Selection Changing
        /// </summary>
		public event EventHandler SelectionChanging;
        /// <summary>
        /// Occurs when Page Got Focus
        /// </summary>
		public event EventHandler PageGotFocus;
        /// <summary>
        /// Occurs when Page Lost Focus
        /// </summary>
		public event EventHandler PageLostFocus;
        /// <summary>
        /// Occurs when Page Text Changed
        /// </summary>
        public event EventHandler PageTextChanged;
        /// <summary>
        /// Occurs when Selected Index Changed
        /// </summary>
        [Description("selectedIndexChangedEvent"), Category("Behavior")]
		public event EventHandler SelectedIndexChanged;
        /// <summary>
        /// Occurs when Draw Item
        /// </summary>
		[Category("Behavior"), Description("drawItemEvent")]
		public event DrawItemEventHandler DrawItem;
		#endregion

		#region Constructor

		static McTabControl()
		{
			// Create a strip of images by loading an embedded bitmap resource
			_internalImages = DrawUtils.LoadBitmapStrip(Type.GetType("Nistec.WinForms.McTabControl"),
				"Nistec.WinForms.Images.ImagesTabControl.bmp",
				new Size(_imageButtonWidth, _imageButtonHeight),
				new Point(0,0));
		}

        //internal McTabControl(bool net,Control c):this()
        //{
        //    this.m_netFram=net;
        //    this.owner=c;
        //    if(this.owner!=null)
        //    {
        //        InitMcTabControl(true);
        //    }
        //}
        /// <summary>
        /// Initializes a new instance of the McTabControl class
        /// </summary>
        /// <param name="c"></param>
        public McTabControl(Control c)
            : this()
        {
            this.owner = c;
            if (this.owner != null)
            {
                InitMcTabControl(true);
            }
        }
        /// <summary>
        /// Initializes a new instance of the McTabControl class
        /// </summary>
        /// <param name="useHost"></param>
		public McTabControl(bool useHost)
		{
			InitMcTabControl(true);
		}
        /// <summary>
        /// Initializes a new instance of the McTabControl class
        /// </summary>
		public McTabControl()
		{
			InitMcTabControl(false);
		}

		private void InitMcTabControl(bool useHost)
		{
			hideButtons=false;

			// Prevent flicker with double buffering and all painting inside WM_PAINT
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
            base.shouldSetAutoStyle = false;
         
			// Create collections
			_tabRects = new ArrayList();
			_tabPages = new TabPageCollection();

			// Hookup to collection events
			_tabPages.Clearing += new CollectionClear(OnClearingPages);
			_tabPages.Cleared += new CollectionClear(OnClearedPages);
			_tabPages.Inserting += new CollectionChange(OnInsertingPage);
			_tabPages.Inserted += new CollectionChange(OnInsertedPage);
			_tabPages.Removing += new CollectionChange(OnRemovingPage);
			_tabPages.Removed += new CollectionChange(OnRemovedPage);

			// Define the default state of the control
			_positionAtTop = true;
			_startPage = -1;						
			_pageSelected = -1;						
			_hotTrackPage = -1;					
			//_hotTrack = true;					
			_imageList = null;						
			_multiline = false;
			//_mouseOver = false;
			_leftScroll = false;
			_rightScroll = false;
			//drawBackgroung=BackgroundIDE.None;
            _hideTabs = false;
			//_hideTabsMode = HideTabsModes.ShowAlways;
			_recordFocus = true;
			//_styleIndex = 0;
			
			// Create hover buttons
			_closeButton = new ImageButton(_internalImages, (int)ImageStrip.Close);
			_leftArrow = new ImageButton(_internalImages, (int)ImageStrip.LeftEnabled) ;//(int)ImageStrip.LeftDisabled);
			_rightArrow = new ImageButton(_internalImages, (int)ImageStrip.RightEnabled);// (int)ImageStrip.RightDisabled);
   
			// We want our buttons to have very thin borders
			_closeButton.BorderWidth = _leftArrow.BorderWidth = _rightArrow.BorderWidth = 1;

			// Hookup to the button events
			_closeButton.Click += new EventHandler(OnCloseButton);
			_leftArrow.Click += new EventHandler(OnLeftArrow);
			_rightArrow.Click += new EventHandler(OnRightArrow);

			// Set their fixed sizes
			_leftArrow.Size = _rightArrow.Size = _closeButton.Size = new Size(_buttonWidth, _buttonHeight);
			_leftArrow.PopupStyle = _rightArrow.PopupStyle = _closeButton.PopupStyle = false;

			// Add child controls

			if(useHost)
			{

				// Create the panel that hosts each page control. This is done to prevent the problem where a 
				// hosted Control/Form has 'AutoScaleBaseSize' defined. In which case our attempt to size it the
				// first time is ignored and the control sizes itself to big and would overlap the tabs area.
				_hostPanel = new HostPanel();
				_hostPanel.Location = new Point(0,0);
				_hostPanel.Size = new Size(0,0);

				//_hostPanel.MouseEnter += new EventHandler(OnPageMouseEnter);
				//_hostPanel.MouseLeave += new EventHandler(OnPageMouseLeave);
				this._closeButton.Visible=true;
				this._showClose=true;
				this.Controls.AddRange(new Control[]{_closeButton, _leftArrow, _rightArrow, _hostPanel});
			}
			else
			{
				this.Controls.AddRange(new Control[]{_leftArrow, _rightArrow});
				_rightArrow.Hide();
				_leftArrow.Hide();
			}
				//InitComponent();

			// Grab some contant values
			_imageWidth = 16;
			_imageHeight = 16;
            this.Size = new Size(80, 80);
			// Default to having a MultiForm usage
			_AlignmentOption=AlignmentOptions.Top ;
            //_ControlLayout = ControlLayout.XpLayout;
			this.itemSize=DefaultItemSize;

			// Define the default Font, BackColor and McButton images
			//DefineFont(SystemInformation.MenuFont);
            SerializeBackColor(StyleLayout.DefaultControlColor, true);
			//DefineButtonImages();

			//ReSetting();
		}

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			if(disposing)
			{
				// Remove notifications
                /*toolTip*/
                if (this.toolTip != null)
                {
                    this.toolTip.Dispose();
                    this.toolTip = null;
                }
			}
			base.Dispose(disposing);
		}
        /// <summary>
        /// OnHandleCreated
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            /*toolTip*/
            if (showToolTip && (this.toolTip == null))
            {
                this.toolTip = new McToolTip();
            }
            Initialize();
        }
		/// <summary>
		/// Initialize all TabPages ,Recommended when controls on TabPages are Binding
		/// </summary>
        public void Initialize()
        {
            if (this._tabPages.Count > 1)
            {
                //Fix_Visible
                //for(int i=_tabPages.Count-1;i>=0;i--)
                //    this.TabPages[i].Initialize();//.Selected=true;

                StyleSettings(false);
                this.TabPages[0].Selected = true;
                //this.SelectedTab.Visible=true;
            }
        }

		#endregion

		#region Appearance options
        /// <summary>
        /// Gets or sets a value indicating whether more than one row of tabs can be displayed
        /// </summary>
		[Category("Appearance"),DefaultValue(false)]  
		public bool Multiline
		{
			get{return _multiline;} 
			set
			{
				if(_multiline !=value)
				{
					_multiline =value;
					ShouldShowArrows();
					_recalculate = true;
					Invalidate();
				}
			}
		}

		internal void ShouldShowArrows()
		{
			if( _multiline || hideButtons)
			{
				_showArrows=false;
                return ;
			}

			_showArrows=this._rightScroll || this._leftScroll;
		}

			[Category("Appearance"),DefaultValue(false)]  
			internal bool ShowClose
			{
				get{return _showClose;} 
				set
				{
					_showClose=value;
					_recalculate = true;
					Invalidate();
				}
			}
        /// <summary>
        /// Gets a value indicating is Arrows buttons should show
        /// </summary>
		[Category("Appearance"),DefaultValue(false)]  
		internal bool ShowArrows
		{
			get{return _showArrows;} 
			set
			{
				if(_multiline)
				{
					_showArrows=false;
				}
				else
				{
					_showArrows=value;
				}
				_recalculate = true;
				Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the size of the control's tabs
        /// </summary>
		[Category("Behavior"), Localizable(true), Description("TabBaseItemSize")]
		public Size ItemSize
		{
			get
			{
				if (!this.itemSize.IsEmpty)
				{
					return this.itemSize;
				}
				if (base.IsHandleCreated)
				{
					//this.getTabRectfromItemSize = true;
					//Rectangle rectangle1 = this.GetTabRect(0);
					//return rectangle1.Size;
				}
				return DefaultItemSize;
			}
			set
			{
				if ((value.Width < 0) || (value.Height < 0))
				{
					throw new ArgumentException("InvalidArgument", "ItemSize " + value.ToString() );
				}
				this.itemSize = value;
				_recalculate = true;
				Invalidate();
			}
		}
        /// <summary>
        /// Gets or sets the area of the control where the tabs are aligned
        /// </summary>
		[Category("Appearance"),RefreshProperties(RefreshProperties.All ),DefaultValue(AlignmentOptions.Top)]  
		public AlignmentOptions Alignment
		{
			get{return _AlignmentOption;} 
			set
			{
				if(_AlignmentOption !=value)
				{
					_AlignmentOption=value;
					switch(value)
					{
						case AlignmentOptions.Top  :
							_positionAtTop = true;
							break;
						case AlignmentOptions.Bottom   :
							_positionAtTop = false;
							break;
					}
					_recalculate = true;
					Invalidate();
				}
			}
		}
        /// <summary>
        /// Gets or sets the Control Layout
        /// </summary>
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                if (base.ControlLayout != value)
                {
                    base.ControlLayout = value;

                    ShouldShowArrows();
                    //ReSetting();
                    _recalculate = true;
                    //Invalidate();

                    //foreach (McTabPage tp in this._tabPages)
                    //    tp.ControlLayout = _ControlLayout;

                    //OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));

                     this.Invalidate();
                }

            }
        }


		#endregion

        #region colors settings 

        /// <summary>
        /// Gets or sets the font of the text displayed by the control
        /// </summary>
        [Category("Appearance")]
        public override Font Font
        {
            get { return base.Font; }

            set
            {
                if (value == null)
                {
                    value = StyleLayout.DefaultFont;
                }
                if (value != base.Font)
                {
                    SerializeFont(value, true);
                    //_recalculate = true;
                    //Invalidate();
                }
            }
        }
        ///// <summary>
        ///// Gets or sets the control Fore Color
        ///// </summary>
        //[Category("Color Style"),Browsable(false),DefaultValue(typeof(Color),"Black")]
        [Browsable(false), DefaultValue(typeof(Color), "Black"), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                if (base.ForeColor != value)
                {
                    SerializeForeColor(value, true);
                }
            }
        }
        /// <summary>
        /// Gets or sets the control Back Color
        /// </summary>
        //[Category("Color Style"),/*Browsable(false),*/DefaultValue(typeof(Color),"Control")]
        //[Browsable(false), DefaultValue(typeof(Color), StyleLayout.DefaultBackColorString), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get { return base.BackColor; }

            set
            {
                if (base.BackColor != value)
                {
                    SerializeBackColor(value, true);
                    _recalculate = true;
                    Invalidate();
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            else if (force)
                base.ForeColor = value;


        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
                base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
            else if (force)
                base.BackColor = value;
            else
                base.BackColor = StyleLayout.DefaultControlColor;

            Color backColor = BackColor;

            _backLightLight = ControlPaint.LightLight(backColor);
            _backDark = ControlPaint.Dark(backColor);
            _backDarkDark = ControlPaint.DarkDark(backColor);
            //_InactiveColor = Color.Gray;
            _backIDE = TabColorUtils.TabBackgroundFromBaseColor(backColor);

        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;

        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            if(!( IsHandleCreated && StylePainter != null))
                return false;
            Font font = LayoutManager.Layout.TextFontInternal;
            return font != this.Font;

        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                base.Font = LayoutManager.Layout.TextFontInternal;
            else if (force && value != this.Font)
                base.Font = value;
            else
                return;

            // Update internal height value using Font
            _textHeight = Font.Height;

            // Is the font height bigger than the image height?
            if (_imageHeight >= _textHeight)
            {
                // No, do not need extra spacing around the image to fit in text
                _imageGapTopExtra = 0;
                _imageGapBottomExtra = 0;
            }
            else
            {
                // Yes, need to make the image area bigger so that its height calculation
                // matchs that height of the text
                int extraHeight = _textHeight - _imageHeight;

                // Split the extra height between the top and bottom of image
                _imageGapTopExtra = extraHeight / 2;
                _imageGapBottomExtra = extraHeight - _imageGapTopExtra;
            }
        }
        #endregion
 

		#region ILayout

        private void StyleSettings(bool force)
        {
            bool auto = this.AutoChildrenStyle;
            IStyle style = this.StylePainter;

            foreach (McTabPage tp in this.TabPages)
            {
                if (force)
                {
                    tp.autoChildrenStyle = auto;
                }
                else
                {
                    tp.StylePainter = style;
                }
                if (auto && style == null)
                {
                    tp.BackColor = this.BackColor;
                }
                tp.Invalidate(true);
            }

            this.Invalidate(true);
        }


        /// <summary>
        /// On Style Painter Changed
        /// </summary>
        /// <param name="e"></param>
		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged(e);
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
            StyleSettings(false);
		}
        /// <summary>
        /// On Style Property Changed
        /// </summary>
        /// <param name="e"></param>
		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ColorBrush1") || e.PropertyName.Equals("BackgroundColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
			{
				this.Invalidate(true);
			}
		}
 
		#endregion

		#region Property
        /// <summary>
        /// Gets the collection of tab pages in this tab control.
        /// </summary>
        [Browsable(false), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual TabPageCollection TabPages
        {
            get { return _tabPages; }
        }

        //[Category("Behavior"), DefaultValue(false)]
        //public bool AutoToolTip
        //{
        //    get { return autoToolTip; }
        //    set
        //    {
        //        if (autoToolTip != value)
        //        {
        //            autoToolTip = value;
        //            foreach (McTabPage tp in this.TabPages)
        //            {
        //                tp.AutoToolTip = value;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets a value indicating whether the Tool Tip is displayed
        /// </summary>
        [Category("Behavior"), DefaultValue(true)]
        public virtual bool ShowToolTip
        {
            get { return showToolTip; }
            set
            {
                if (showToolTip != value)
                {
                    showToolTip = value;
                    /*toolTip*/
                    if (value && (this.toolTip == null))
                    {
                        this.toolTip = new McToolTip();
                    }
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the children control style Inherited from Control
        /// </summary>
        [Category("Style"), Browsable(true), DefaultValue(true)]//, EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoChildrenStyle
        {
            get { return base.AutoChildrenStyle; }
            set
            {
                if (base.AutoChildrenStyle != value)
                {
                    base.AutoChildrenStyle = value;

                    if (value)
                    {
                        StyleSettings(true);
                    }
                    else
                    {
                        foreach (McTabPage tp in this.TabPages)
                        {
                            tp.autoChildrenStyle = value;
                            tp.Invalidate(true);
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the Background drowing style
        ///// </summary>
        //[DefaultValue(BackgroundIDE.None),Category("Style"),RefreshProperties(RefreshProperties.Repaint)]
        //public BackgroundIDE DrawBackground
        //{
        //    get
        //    {
        //        return this.drawBackgroung;
        //    }
        //    set
        //    {
        //        if(this.drawBackgroung!=value)
        //        {
        //            this.drawBackgroung=value;
        //            this.ReSettingButtons(); 
        //            //this.OnParentBackColorChanged(EventArgs.Empty);
        //            this.Invalidate();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets or sets a value indicating whether the children control style Inherited from Control
        ///// </summary>
        //[DefaultValue(PagesStyleSetting.None), Category("Style"), RefreshProperties(RefreshProperties.Repaint)]
        //public PagesStyleSetting PagesStyleSetting
        //{
        //    get
        //    {
        //        return this.pagesStyleSetting;
        //    }
        //    set
        //    {
        //        if (this.pagesStyleSetting != value)
        //        {
        //            this.pagesStyleSetting = value;
        //            StyleSettingChanged();
        //            //this.Invalidate();
        //        }
        //    }
        //}
  
		internal void SetVisualStyle(VisualStyle value)
		{
			if (value==VisualStyle.Plain)
                ControlLayout = ControlLayout.System;  
			else
                ControlLayout = ControlLayout.XpLayout;  
		}
        ///// <summary>
        ///// Gets or sets the control Fore Color
        ///// </summary>
        //[Category("Appearance")]
        ////[DefaultValue(false)]
        //public virtual bool HotTrack
        //{
        //    get { return _hotTrack; }
			
        //    set 
        //    {
        //        if (_hotTrack != value)
        //        {
        //            _hotTrack = value;

        //            if (!_hotTrack)
        //                _hotTrackPage = -1;

        //            _recalculate = true;
        //            Invalidate();
        //        }
        //    }
        //}

		[Browsable(false)]
		internal protected virtual Rectangle TabsAreaRect
		{
			get { return _tabsAreaRect; }
		}

//		[Category("Appearance")]
//		[DefaultValue(null)]
//		public virtual ImageList ImageList
//		{
//			get { return _imageList; }
//
//			set 
//			{
//				if (_imageList != value)
//				{
//					_imageList = value;
//
//					_recalculate = true;
//					Invalidate();
//				}
//			}
//		}
		
        /// <summary>
        /// Gets or sets a value indicating whether the Tabs is displayed 
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public virtual bool HideTabs
        {
            get { return _hideTabs; }

            set
            {
                if (_hideTabs != value)
                {
                    _hideTabs = value;

                    ReSetting();
                    Invalidate();
                }
            }
        }

		#endregion

		#region Hide Properties
        /// <summary>
        /// Gets or sets the selected tab index
        /// </summary>
		[Browsable(false),DefaultValue(-1),EditorBrowsable( EditorBrowsableState.Never),DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
		public virtual int SelectedIndex
		{
			get { return _pageSelected; }

			set
			{
				if ((value >= 0) && (value < _tabPages.Count))
				{
					if (_pageSelected != value)
					{
						//Fix_Visible
						if(!DesignMode && !_tabPages[value].PageVisible)
							return;

						// Raise selection changing event
						OnSelectionChanging(EventArgs.Empty);

						// Any page currently selected?
						if (_pageSelected != -1)
							DeselectPage(_tabPages[_pageSelected]);

						_pageSelected = value;

						if (_pageSelected != -1)
						{
							SelectPage(_tabPages[_pageSelected]);

							// If newly selected page is scrolled off the left hand side
							if (_pageSelected < _startPage)
								_startPage = _pageSelected;  // then bring it into view
						}
						// Raise selection change event
						OnSelectedIndexChanged(EventArgs.Empty);

						Invalidate();
					}
				}
			}
		}
        /// <summary>
        /// Gets or sets the selected tab 
        /// </summary>
		[Browsable(false)]
        [DefaultValue(null), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual McTabPage SelectedTab
		{
			get 
			{
				// If nothing is selected we return null
				if (_pageSelected == -1)
					return null;
				else
					return _tabPages[_pageSelected];
			}

			set
			{
				// Cannot change selection to be none of the tabs
				if (value != null)
				{
					// Get the requested page from the collection
					int index = _tabPages.IndexOf(value);

					// If a valid known page then using existing property to perform switch
					if (index != -1)
						this.SelectedIndex = index;
				}
			}
		}

		private void MakePageVisible(McTabPage page)
		{
			MakePageVisible(_tabPages.IndexOf(page));
		}

		private void MakePageVisible(int index)
		{
			// Only relevant if we do not shrink all pages to fit and not in multiline
			if (!_multiline)
			{
				// RangeType check the request page
				if ((index >= 0) && (index < _tabPages.Count))
				{
                    //Fix_Visible
					if(!DesignMode && !_tabPages[index].PageVisible)
						 return;

					// Is requested page before those shown?
					if (index < _startPage)
					{
						// Define it as the new start page
						_startPage = index;

						_recalculate = true;
						Invalidate();
					}
					else
					{
						// Find the last visible position
						int xMax = GetMaximumDrawPos();

						Rectangle rect = (Rectangle)_tabRects[index];

						// Is the page drawn off over the maximum position?
						if (rect.Right >= xMax)
						{
							// Need to find the new start page to bring this one into view
							int newStart = index;

							// Space left over for other tabs to be drawn inside
							int spaceLeft = xMax - rect.Width - _tabsAreaRect.Left - _tabsAreaStartInset;

							do
							{
								// Is there a previous tab to check?
								if (newStart == 0)
									break;

								Rectangle rectStart = (Rectangle)_tabRects[newStart - 1];
		
								// Is there enough space to draw it?
								if (rectStart.Width > spaceLeft)
									break;

								// Move to new tab and reduce available space left
								newStart--;
								spaceLeft -= rectStart.Width;

							} while(true);

							// Define the new starting page
							_startPage = newStart;

							_recalculate = true;
							Invalidate();
						}
					}
				}
			}
		}
        /// <summary>
        /// Processes a mnemonic character. (Inherited from Control.)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		protected override bool ProcessMnemonic(char key)
		{
			int total = _tabPages.Count;
			int index = this.SelectedIndex + 1;
            
			for(int count=0; count<total; count++, index++)
			{
				// RangeType check the index
				if (index >= total)
					index = 0;

				//Fix_Visible
				if(!DesignMode && !_tabPages[index].PageVisible)
					return false;

				McTabPage page = _tabPages[index];
        
				// Find position of first mnemonic character
				int position = page.Text.IndexOf('&');

				// Did we find a mnemonic indicator?
				if (IsMnemonic(key, page.Text))
				{
					// Select this page
					this.SelectedTab = page;
                
					return true;
				}
			}
                        
			return false;
		}

		#endregion

		#region overrides
        /// <summary>
        /// Raises the Resize event
        /// </summary>
        /// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{

			ReSetting();
			if(_hostPanel!=null)
			{
				_hostPanel.Size = _pageRect.Size;
			}
			else
			{
				SetPagesPosition();
			}
//			foreach(Control c in this.Controls)
//			{
//				if(c is McTabPage)
//				c.Size = _pageRect.Size;// this.Size;
//			}
			Invalidate();
            
			base.OnResize(e);
		}

		private void SetPagesPosition()
		{
			foreach(Control c in this.Controls)
			{
				if(c is McTabPage)
				{
					c.Location = _pageRect.Location;
					c.Size = _pageRect.Size;
				}
			}
		}
//		protected override void OnSizeChanged(EventArgs e)
//		{
//			ReSetting();
//			Invalidate();
//
//			base.OnSizeChanged(e);
//		}
        /// <summary>
        /// Raises the SelectionChanging event
        /// </summary>
        /// <param name="e"></param>
		public virtual void OnSelectionChanging(EventArgs e)
		{
			// Has anyone registered for the event?
			if (SelectionChanging != null)
				SelectionChanging(this, e);
		}
        /// <summary>
        /// Raises the SelectedIndexChanged event
        /// </summary>
        /// <param name="e"></param>
		public virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}

        /// <summary>
        /// Raises the ClosePressed event
        /// </summary>
        /// <param name="e"></param>
		public virtual void OnClosePressed(EventArgs e)
		{
			// Has anyone registered for the event?
			if (ClosePressed != null)
				ClosePressed(this, e);
		}
        /// <summary>
        /// Raises the Page GotFocus event
        /// </summary>
        /// <param name="e"></param>
		public virtual void OnPageGotFocus(EventArgs e)
		{
			// Has anyone registered for the event?
			if (PageGotFocus != null)
				PageGotFocus(this, e);
		}
		/// <summary>
        /// Raises the Page LostFocus event
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPageLostFocus(EventArgs e)
		{
			// Has anyone registered for the event?
			if (PageLostFocus != null)
				PageLostFocus(this, e);
		}
        /// <summary>
        /// Raises the Page LostFocus event
        /// </summary>
        /// <param name="e"></param>
        internal protected virtual void OnPageTextChanged(EventArgs e)
        {
            // Has anyone registered for the event?
            if (PageTextChanged != null)
                PageTextChanged(this, e);
        }
        /// <summary>
        /// Raises the ClosePressed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected virtual void OnCloseButton(object sender, EventArgs e)
		{
			OnClosePressed(EventArgs.Empty);
		}
        /// <summary>
        /// Raises the LeftArrow click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected virtual void OnLeftArrow(object sender, EventArgs e)
		{
			// Set starting page back one
			_startPage--;

            //if (SelectedIndex >= _tabRects.Count-1)
            //{
            //    SelectedIndex --;
            //}

			_recalculate = true;
			Invalidate();
		}
	
        /// <summary>
        /// Raises the Right Arrow click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected virtual void OnRightArrow(object sender, EventArgs e)
		{
			// Set starting page forward one
			_startPage++;
            //if (SelectedIndex < _startPage)
            //{
            //    SelectedIndex = _startPage;
            //}
			_recalculate = true;
			Invalidate();
		}

		#endregion

		#region Define Methods

		internal void SetTabStyle(bool ColorSetting,bool ButtonSetting)
		{
			if(ColorSetting)
				_recalculate = true;
			Invalidate();

			//if(ButtonSetting)
			//	DefineButtonImages();
		}

        //private void Recalculate(bool value)
        //{
        //    _recalculate=value;
        //}

 
        //private void DefineBackColor(Color newColor)
        //{

        //    //base.BackColor = newColor;
        //    //base.ForeColor =_textColor;//m_Style.ForeColor;
        //    //_textColor=m_Style.ForeColor ;

        //    //if (/*IsHandleCreated &&*/ StylePainter != null)
        //    //    newColor = LayoutManager.Layout.BackgroundColorInternal;


        //    // base.BackColor = newColor;
	
        //    //Color newColor=this.BackColor;
        //    // Calculate the modified colors from this base
        //    //_backLight = ControlPaint.Light(newColor);
        //    _backLightLight = ControlPaint.LightLight(newColor);
        //    _backDark = ControlPaint.Dark(newColor);
        //    _backDarkDark = ControlPaint.DarkDark(newColor);
        //    _InactiveColor=Color.Gray;
        //    _backIDE =TabColorUtils.TabBackgroundFromBaseColor(newColor);
        //}
		
        //public virtual void DefineButtonImages()
        //{

        //    ImageAttributes ia = new ImageAttributes();

        //    ColorMap activeMap = new ColorMap();
        //    ColorMap inactiveMap = new ColorMap();

        //    // Define the color transformations needed
        //    activeMap.OldColor = Color.Black;
        //    activeMap.NewColor = Color.Transparent;// _ActiveColor;
        //    inactiveMap.OldColor = Color.White;
        //    inactiveMap.NewColor = _InactiveColor ;

        //    // Create remap attributes for use by button
        //    ia.SetRemapTable(new ColorMap[]{activeMap, inactiveMap}, ColorAdjustType.Bitmap);

        //    // Pass attributes to the buttons
        //    _leftArrow.ImageAttributes = ia;
        //    _rightArrow.ImageAttributes = ia;
        //    _closeButton.ImageAttributes = ia;
        //}

 
		#endregion

		#region ReSetting Methods

        private void ReSetting()
        {
            if (!(DesignMode || IsHandleCreated))
                return;

            // Reset the need for a recalculation
            _recalculate = false;

            ShouldShowArrows();

            // The height of a tab button is...
            //			int tabButtonHeight = _position[ (int)PositionIndex.ImageGapTop] + 
            //				_imageGapTopExtra +
            //				_imageHeight + 
            //				_imageGapBottomExtra + 
            //				_position[ (int)PositionIndex.ImageGapBottom] +
            //				_position[ (int)PositionIndex.BorderBottom]; 

            int tabButtonHeight = this.itemSize.Height + 1;


            // The height of the tabs area is...
            int tabsAreaHeight = /*PositionBorderTop*/4 +
                tabButtonHeight + PositionTabsBottomGap;

            //int tabsAreaHeight = tabButtonHeight + PositionTabsBottomGap+PositionBorderTop;
            //bool hideTabsArea = HideTabsCalculation();

            // Should the tabs area be hidden?
            if (_hideTabs)
            {
                // ... then do not show the tabs or button controls
                _pageAreaRect = new Rectangle(0, 0, this.Width, this.Height);
                _tabsAreaRect = new Rectangle(0, 0, 0, 0);
            }
            else
            {
                if (_positionAtTop)
                {
                    // Create rectangle that represents the entire tabs area
                    _pageAreaRect = new Rectangle(0, tabsAreaHeight, this.Width - PositionShadowWidth, this.Height - tabsAreaHeight - PositionShadowWidth);

                    // Create rectangle that represents the entire area for pages
                    _tabsAreaRect = new Rectangle(0, 0, this.Width, tabsAreaHeight);
                }
                else
                {
                    // Create rectangle that represents the entire tabs area
                    _tabsAreaRect = new Rectangle(0, this.Height - tabsAreaHeight, this.Width, tabsAreaHeight);

                    // Create rectangle that represents the entire area for pages
                    _pageAreaRect = new Rectangle(0, PositionShadowWidth, this.Width - PositionShadowWidth, this.Height - tabsAreaHeight - PositionShadowWidth);
                }
            }

            int xEndPos = 0;

            if (!_hideTabs && _tabPages.Count > 0)
            {
                // The minimum size of a button includes its left and right borders for width,
                // and then fixed height which is based on the size of the image and font
                Rectangle tabPosition;

                if (_positionAtTop)
                    tabPosition = new Rectangle(0,
                        _tabsAreaRect.Bottom - tabButtonHeight - PositionBorderTop,
                         PositionBorderLeft +
                        PositionBorderRight,
                        tabButtonHeight);
                else
                    tabPosition = new Rectangle(0,
                        _tabsAreaRect.Top +
                        PositionBorderTop,
                        PositionBorderLeft +
                        PositionBorderRight,
                        tabButtonHeight);

                // Find starting and ending positons for drawing tabs
                int xStartPos = _tabsAreaRect.Left + _tabsAreaStartInset;
                xEndPos = GetMaximumDrawPos();

                // Available width for tabs is size between start and end positions
                int xWidth = xEndPos - xStartPos;

                if (_multiline)
                    ReSettingMultilineTabs(xStartPos, xEndPos, tabPosition, tabButtonHeight);
                else
                    ReSettingTabs(xWidth, xStartPos, tabPosition);
            }

            // Position of Controls defaults to the entire page area
            _pageRect = _pageAreaRect;

            // Shrink by having a border on left,top and right borders
            _pageRect.X += PositionPlainBorder;
            _pageRect.Width -= (PositionPlainBorder * 2) - 1;

            if (!_positionAtTop)
                _pageRect.Y += PositionPlainBorder;

            _pageRect.Height -= PositionPlainBorder - 1;

            // If hiding the tabs then need to adjust the controls positioning
            if (_hideTabs)
            {
                _pageRect.Height -= PositionPlainBorder;

                if (_positionAtTop)
                    _pageRect.Y += PositionPlainBorder;
            }

            // Calcualte positioning of the child controls/forms
            int leftOffset = 0;// _ctrlLeftOffset;
            int rightOffset = 0;// _ctrlRightOffset;
            int topOffset = 0;// _ctrlTopOffset;
            int bottomOffset = 0;// _ctrlBottomOffset;

            Point pageLoc = new Point(_pageRect.Left + leftOffset,
                _pageRect.Top + topOffset);

            Size pageSize = new Size(_pageRect.Width - leftOffset - rightOffset,
                _pageRect.Height - topOffset - bottomOffset);

            // Position the host panel appropriately
            if (_hostPanel != null)
            {
                _hostPanel.Size = pageSize;
                _hostPanel.Location = pageLoc;
                //_hostPanel.Location = new Point(0,0);
            }
            else
            {
                SetPagesPosition();
            }
            // If we have any tabs at all
            if (_tabPages.Count > 0)
            {
                Rectangle rect = (Rectangle)_tabRects[_tabPages.Count - 1];

                // Determine is the right scrolling button should be enabled
                _rightScroll = (rect.Right > xEndPos);
            }
            else
            {
                // No pages means there can be no right scrolling
                _rightScroll = false;
            }

            // Determine if left scrolling is possible
            _leftScroll = (_startPage > 0);

            // Handle then display and positioning of buttons
            ReSettingButtons();
        }

        private void ReSettingMultilineTabs(int xStartPos, int xEndPos, 
			Rectangle tabPosition, int tabButtonHeight)
		{
			using (Graphics g = this.CreateGraphics())
			{
				// McMultiBox style needs a pixel extra drawing room on right
				//if (AppearanceOption == AppearanceOptions.Buttons )
				//	xEndPos-=2;
                        
				// How many tabs on this line
				int lineCount = 0;
                            
				// Remember which line is the first displayed
				_topYPos = tabPosition.Y;
                            
				// Next tab starting position
				int xPos = xStartPos;
				int yPos = tabPosition.Y;
                            
				// How many full lines were there
				int fullLines = 0;
                            
				// Line increment value
				int lineIncrement = tabButtonHeight+ 4;

				// Track which line has the selection on it                                
				int selectedLine = 0;

				// Vertical adjustment
				int yAdjust = 0;
                        
				// Create array for holding lines of tabs
				ArrayList lineList = new ArrayList();
                            
				// Add the initial line
				lineList.Add(new ArrayList());
                        
				// Process each tag page in turn
				for(int i=0; i<_tabPages.Count; i++)
				{
					//Fix_Visible
					if(!DesignMode && !_tabPages[i].PageVisible)
					{
						//Continue
					}
					else
					{
						// Get the tab instance for this position
						McTabPage page = _tabPages[i];
                            
						// Find out the tabs total width
						int tabWidth = GetTabPageSpace(g, page);
                                
						// If not the first on the line, then check if newline should be started
						if (lineCount > 0)
						{
							// Does this tab extend pass end of the lines
							if ((xPos + tabWidth) > xEndPos)
							{
								// Next tab position is down a line and back to the start
								xPos = xStartPos;
								yPos += lineIncrement;
                                        
								// Remember which line is the last displayed
								_bottomYPos = tabPosition.Y;

								// Increase height of the tabs area
								_tabsAreaRect.Height += lineIncrement;
                                        
								// Decrease height of the control area
								_pageAreaRect.Height -= lineIncrement;
                                        
								// Moving areas depends on drawing at top or bottom
								if (_positionAtTop)
									_pageAreaRect.Y += lineIncrement;
								else
								{
									yAdjust -= lineIncrement;
									_tabsAreaRect.Y -= lineIncrement;
								}
                                        
								// Start a new line 
								lineList.Add(new ArrayList());
                                        
								// Make sure the entries are aligned to fill entire line
								fullLines++;
							}
						}


						// Limit the width of a tab to the whole line
						if (tabWidth > (xEndPos - xStartPos))
							tabWidth = xEndPos - xStartPos;
					                                
						// Construct rectangle for representing this tab
						Rectangle tabRect = new Rectangle(xPos, yPos, tabWidth, tabButtonHeight);
                                
						// Add this tab to the current line array
						ArrayList thisLine = lineList[lineList.Count - 1] as ArrayList;
                                
						// Create entry to represent the sizing of the given page index
						MultiRect tabEntry = new MultiRect(tabRect, i);
                                
						thisLine.Add(tabEntry);
                                
						// Track which line has the selection on it
						if (i == _pageSelected)
							selectedLine = fullLines;
                                
						// Move position of next tab along
						xPos += tabWidth + 1;
                                
						// Increment number of tabs on this line
						lineCount++;
					}
				}

				int line = 0;

				// Do we need all lines to extend full width
				//if (_multilineFullWidth)
				//	fullLines++;
                            
				// Make each full line stretch the whole line width
				foreach(ArrayList lineArray in lineList)
				{
					// Only right fill the full lines
					if (line < fullLines)
					{
						// Number of items on this line
						int numLines = lineArray.Count;
                                
						// Find ending position of last entry
						MultiRect itemEntry = (MultiRect)lineArray[numLines - 1];

						// Is there spare room between last entry and end of line?                            
						if (itemEntry.Rect.Right < (xEndPos - 1))
						{
							// Work out how much extra to give each one
							int extra = (int)((xEndPos - itemEntry.Rect.Right - 1) / numLines);
                                        
							// Keep track of how much items need moving across
							int totalMove = 0;
                                        
							// Add into each entry
							for(int i=0; i<numLines; i++)
							{
								// Get the entry class instance
								MultiRect expandEntry = (MultiRect)lineArray[i];
                                        
								// Move across requried amount
								expandEntry.X += totalMove;
                                            
								// Add extra width
								expandEntry.Width += (int)extra;

								// All items after this needing moving
								totalMove += extra;
							}
                                        
							// Extend the last position, in case rounding errors means its short
							itemEntry.Width += (xEndPos - itemEntry.Rect.Right - 1);
						}
					}
                                
					line++;
				}

				if (_positionAtTop)
				{
					// If the selected line is not the bottom line
					if (selectedLine != (lineList.Count - 1))
					{
						ArrayList lastLine = (ArrayList)(lineList[lineList.Count - 1]);
                                    
						// Find y offset of last line
						int lastOffset = ((MultiRect)lastLine[0]).Rect.Y;
                                
						// Move all lines below it up one
						for(int lineIndex=selectedLine+1; lineIndex<lineList.Count; lineIndex++)
						{
							ArrayList al = (ArrayList)lineList[lineIndex];
                                    
							for(int item=0; item<al.Count; item++)
							{
								MultiRect itemEntry = (MultiRect)al[item];
								itemEntry.Y -= lineIncrement;
							}
						}
                                    
						// Move selected line to the bottom
						ArrayList sl = (ArrayList)lineList[selectedLine];
                                    
						for(int item=0; item<sl.Count; item++)
						{
							MultiRect itemEntry = (MultiRect)sl[item];
							itemEntry.Y = lastOffset;
						}
					}
				}
				else
				{
					// If the selected line is not the top line
					if (selectedLine != 0)
					{
						ArrayList topLine = (ArrayList)(lineList[0]);
                                    
						// Find y offset of top line
						int topOffset = ((MultiRect)topLine[0]).Rect.Y;
                                
						// Move all lines above it down one
						for(int lineIndex=0; lineIndex<selectedLine; lineIndex++)
						{
							ArrayList al = (ArrayList)lineList[lineIndex];
                                    
							for(int item=0; item<al.Count; item++)
							{
								MultiRect itemEntry = (MultiRect)al[item];
								itemEntry.Y += lineIncrement;
							}
						}
                                    
						// Move selected line to the top
						ArrayList sl = (ArrayList)lineList[selectedLine];
                                    
						for(int item=0; item<sl.Count; item++)
						{
							MultiRect itemEntry = (MultiRect)sl[item];
							itemEntry.Y = topOffset;
						}
					}
				}

				// Now assignt each lines rectangle to the corresponding structure
				foreach(ArrayList al in lineList)
				{
					foreach(MultiRect multiEntry in al)
					{
						Rectangle newRect = multiEntry.Rect;
                                    
						// Make the vertical adjustment
						newRect.Y += yAdjust;
                                    
						_tabRects[multiEntry.Index] = newRect;
					}
				}
			}
		}

        private void ReSettingTabs(int xWidth, int xStartPos, Rectangle tabPosition)
		{
			using (Graphics g = this.CreateGraphics())
			{
				// Remember which lines are then first and last displayed
				_topYPos = tabPosition.Y;
				_bottomYPos = _topYPos;
            
				// Set the minimum size for each tab page
                for (int i = 0; i < _tabPages.Count; i++)
                {
                    // Is this page before those displayed?
                    if (i < _startPage)
                        _tabRects[i] = (object)_nullPosition;  // Yes, position off screen
                    else
                        _tabRects[i] = (object)tabPosition;	 // No, create minimum size
                }

				// Subtract the minimum tab sizes already allocated
				xWidth -= _tabPages.Count * (tabPosition.Width + 1);

				// Is there any more space left to allocate
				if (xWidth > 0)
				{
					ArrayList listNew = new ArrayList();
					ArrayList listOld = new ArrayList();

					// Add all pages to those that need space allocating
					for(int i=_startPage; i<_tabPages.Count; i++)
						listNew.Add(_tabPages[i]);
            		
          		
					do 
					{
						// The list generated in the last iteration becomes 
						// the to be processed in this iteration
						listOld = listNew;
            	
						// List of pages that still need more space allocating
						listNew = new ArrayList();

						// Assign space to each page that is requesting space
						foreach(McTabPage page in listOld)
						{
							int index = _tabPages.IndexOf(page);
                            if (index < 0)
                                return;

							Rectangle rectPos = (Rectangle)_tabRects[index];

							// Find out how much extra space this page is requesting
							int xSpace = GetTabPageSpace(g, page) - rectPos.Width;

							if(this.itemSize.Width>0)
							{
								rectPos.Width=this.itemSize.Width;
							}
							else
							{
								// Give space to tab
								rectPos.Width += xSpace;
							}
							_tabRects[index] = (object)rectPos;

							// Reduce extra left for remaining tabs
							xWidth -= xSpace;
						}
					} while ((listNew.Count > 0) && (xWidth > 0));
				}

				// Assign the final positions to each tab now we known their sizes
				for(int i=_startPage; i<_tabPages.Count; i++)
				{
					//Fix_Visible
					if(!DesignMode && !_tabPages[i].PageVisible)
					{
						//Continue
					}
					else
					{
						Rectangle rectPos = (Rectangle)_tabRects[i];

						// Define position of tab page
						rectPos.X = xStartPos;
			
						_tabRects[i] = (object)rectPos;

						// Next button must be the width of this one across
						xStartPos += rectPos.Width + 1;
					}
				}
			}
		}

        private void ReSettingButtons()
		{
			int buttonTopGap = 0;
 
			if (_multiline)
			{
				// The height of a tab row is
				int tabButtonHeight = PositionImageGapTop + 
					_imageGapTopExtra +
					_imageHeight + 
					_imageGapBottomExtra + 
					PositionImageGapBottom +
					PositionBorderBottom; 

				// The height of the tabs area is...
				int tabsAreaHeight = PositionBorderTop + 
					tabButtonHeight + PositionTabsBottomGap;
                
				// Find offset to place button halfway down the tabs area rectangle
				buttonTopGap = PositionButtonOffset	+ 
					(tabsAreaHeight - _buttonHeight) / 2;

				// Invert gap position when at bottom
				if (!_positionAtTop)
					buttonTopGap = _tabsAreaRect.Height - buttonTopGap - _buttonHeight;
			}
			else
			{
				// Find offset to place button halfway down the tabs area rectangle
				buttonTopGap = PositionButtonOffset	+ 
					(_tabsAreaRect.Height - _buttonHeight) / 2;
			}
        
			// Position to place next button
			int xStart = _tabsAreaRect.Right - _buttonWidth - _buttonGap;

			// Close button should be shown?
			if (_showClose)
			{
				// Define the location
				_closeButton.Location = new Point(xStart, _tabsAreaRect.Top + buttonTopGap);

				if (xStart < 1)
					_closeButton.Hide();
				else
					_closeButton.Show();

				xStart -= _buttonWidth;
			}
			else
				_closeButton.Hide();

				ShouldShowArrows();

			// Arrows should be shown?
			if (_showArrows)
			{
				// Position the right arrow first as its more the right hand side
				_rightArrow.Location = new Point(xStart, _tabsAreaRect.Top + buttonTopGap);

				if (xStart < 1)
					_rightArrow.Hide();
				else
					_rightArrow.Show();
                    
				xStart -= _buttonWidth;

				_leftArrow.Location = new Point(xStart, _tabsAreaRect.Top + buttonTopGap);

				if (xStart < 1)
					_leftArrow.Hide();
				else
					_leftArrow.Show();
                    
				xStart -= _buttonWidth;

				// Define then enabled state of buttons
				_leftArrow.Enabled = _leftScroll;
				_rightArrow.Enabled = _rightScroll;
			}
			else
			{
				_leftArrow.Hide();
				_rightArrow.Hide();
			}

            //if (ControlLayout == ControlLayout.System)
            //    _closeButton.BackColor = _leftArrow.BackColor = _rightArrow.BackColor = this.BackColor;
            //else
            //{
				Color color1=this.BackColor;;
                //switch(this.drawBackgroung)
                //{
                //    case BackgroundIDE.Content:
                //        color1=this._backIDE;
                //        break;
                //    case BackgroundIDE.Light:
                //        color1=LayoutManager.Layout.GetFlatColor(FlatLayout.Light);
                //        break;
                //    case BackgroundIDE.None:
                //        color1=this.BackColor;
                //        break;
                //}

				this._leftArrow.BackColor=this._rightArrow.BackColor=color1=this._closeButton.BackColor=color1; 
				//_closeButton.BackColor = _leftArrow.BackColor = _rightArrow.BackColor = _backIDE;
			//}
		}

        private int GetMaximumDrawPos()
		{
			int xEndPos = _tabsAreaRect.Right - _tabsAreaEndInset;

			// Showing the close button reduces available space
			if (_showClose)
				xEndPos -= _buttonWidth + _buttonGap;

			// If showing arrows then reduce space for both
			if (_showArrows)
				xEndPos -= _buttonWidth * 2;
				
			return xEndPos;
		}

        private int GetTabPageSpace(Graphics g, McTabPage page)
		{

			if(this.itemSize.Width>0)
			{
				return this.itemSize.Width;
			}

			// Find the fixed elements of required space
			int width = PositionBorderLeft + 
				PositionBorderRight;

			// Any icon or image provided?
			if ((page.Image != null) || (((_imageList != null) || (page.ImageList != null)) && (page.ImageIndex != -1)))
			{
				width += PositionImageGapLeft +
					_imageWidth + 
					PositionImageGapRight;
			}

			// Any text provided?
			if (page.Text.Length > 0)
			{
				//if (!_selectedTextOnly || (_selectedTextOnly && (_pageSelected == _tabPages.IndexOf(page))))
				//{
					Font drawFont = base.Font;

					// Find width of the requested text
					SizeF dimension = g.MeasureString(page.Text, drawFont);			

					// Convert to integral
					width += PositionTextGapLeft +
						(int)dimension.Width + 1; 
				//}						 
			}

			return width;
		}

		#endregion

		#region Draw Methods

        /// <summary>
        /// OnPaintBackground
        /// </summary>
        /// <param name="e"></param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
            //base.OnPaintBackground(e);
		}

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            _recalculate = true;
            Invalidate();
        }
        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Does the state need recalculating before paint can occur?
            if (_recalculate)
                ReSetting();
            int pageHeight = _pageAreaRect.Height;
            int yDraw = _pageAreaRect.Top;
            //Rectangle rectGap = new Rectangle(_tabsAreaRect.X, _tabsAreaRect.Bottom - 2, 2, _tabsAreaRect.Width);
            Rectangle rectTabs = _tabsAreaRect;
            rectTabs.Height -= 4;
            
            using (Brush pageAreaBrush = new SolidBrush(this.BackColor),
                tabsAreaBrush = new SolidBrush(this.Parent.BackColor))
            {
                // Fill backgrounds of the page and tabs areas
                e.Graphics.FillRectangle(pageAreaBrush, ClientRectangle);
                //e.Graphics.FillRectangle(pageAreaBrush, _pageAreaRect);
                e.Graphics.FillRectangle(tabsAreaBrush,rectTabs);// _tabsAreaRect);
                //e.Graphics.FillRectangle(pageAreaBrush, rectGap);


                //if (DrawBackground == BackgroundIDE.Content)
                //{
                //    using (Brush tabsAreaBrush = new SolidBrush(_backIDE))
                //        e.Graphics.FillRectangle(tabsAreaBrush, _tabsAreaRect);
                //}
                //else if (this.drawBackgroung == BackgroundIDE.Light)
                //{
                //    using (Brush tabsAreaBrush = LayoutManager.GetBrushFlat(FlatLayout.Light))
                //        e.Graphics.FillRectangle(tabsAreaBrush, _tabsAreaRect);
                //}
                //else
                //{
                //    using (Brush tabsAreaBrush = new SolidBrush(this.Parent.BackColor))
                //        e.Graphics.FillRectangle(tabsAreaBrush, _tabsAreaRect);
                //}
            }
            pageHeight += PositionPlainBorder;

            // Should the tabs area be hidden?
            if (_hideTabs)
            {
                // Then need to readjust pageHeight
                pageHeight -= PositionPlainBorder;
            }
            else
            {
                // If drawing at top then overdraw upwards and not down
                if (_positionAtTop)
                    yDraw -=  PositionPlainBorder;

            }

            //Rectangle rectShadow = new Rectangle(_pageAreaRect.X, _pageAreaRect.Y - 4, _pageAreaRect.Width, _pageAreaRect.Height + 4); ;
            if (_positionAtTop)
                LayoutManager.DrawShadow(e.Graphics, new Rectangle(_pageAreaRect.X, _pageAreaRect.Y - 4, _pageAreaRect.Width, _pageAreaRect.Height + 4), PositionShadowWidth * 2, _positionAtTop);
            else
                LayoutManager.DrawShadow(e.Graphics, new Rectangle(_pageAreaRect.X, _pageAreaRect.Y , _pageAreaRect.Width, _pageAreaRect.Height ), PositionShadowWidth * 2, _positionAtTop);

            using (Pen penBorder = LayoutManager.GetPenBorder())
                e.Graphics.DrawRectangle(penBorder, new Rectangle(_pageAreaRect.Left, yDraw, _pageAreaRect.Width - 1, pageHeight - 1));

            //// Clip the drawing to prevent drawing in unwanted areas
            ClipDrawingTabs(e.Graphics);

            if (!_hideTabs)
            {
                // Paint each tab page
                foreach (McTabPage page in _tabPages)
                    DrawTab(page, e.Graphics, false);
            }

        }

        private Rectangle ClippingRectangle()
		{
			// Calculate how much to reduce width by for clipping rectangle
			int xReduce = _tabsAreaRect.Width - GetMaximumDrawPos();

			// Create clipping rect
			return new Rectangle(_tabsAreaRect.Left, 
				_tabsAreaRect.Top,
				_tabsAreaRect.Width - xReduce,
				_tabsAreaRect.Height);
		}

        private void ClipDrawingTabs(Graphics g)
		{
			Rectangle clipRect = ClippingRectangle();

			// Restrict drawing to this clipping rectangle
			g.Clip = new Region(clipRect);
		}

        private void DrawTab(McTabPage page, Graphics g, bool hot)
		{
			//Fix_Visible
			if(this.itemSize.Height<=0 || (!DesignMode && !page.PageVisible))
				return;

            if (/*toolTip*/ toolTip != null && this.showToolTip && !string.IsNullOrEmpty(page.ToolTipText))
            {
                //McTabPage pg = this.GetTabPageAtPoint(base.PointToClient(Cursor.Position));
                if(!hot || page != _hoverPage)
                {
                    toolTip.Hide(this);
                    //McToolTip.Instance.Hide(this);
                }

                if (hot && page != _hoverPage)
                {
                    //McToolTip.Instance.Show(page.ToolTipText, this);
                    // /*toolTip*/ 
                    toolTip.Show(page.ToolTipText, this);//, base.PointToClient(Cursor.Position));
                    _hoverPage = page;
                }
            }

			Rectangle rectTab = (Rectangle)_tabRects[_tabPages.IndexOf(page)];

			DrawTabBorder(ref rectTab, g,page.Selected, hot);

			int xDraw = rectTab.Left + PositionBorderLeft;
			int xMax = rectTab.Right - PositionBorderRight;

			DrawTabImage(rectTab, page, g, xMax, ref xDraw);            
			DrawTabText(rectTab, page, g, false, xMax, xDraw);

			if(DrawItem!=null)
			{
				DrawItemState stat= this.SelectedTab==page ? DrawItemState.Selected: DrawItemState.None;
				int i=this._tabPages.IndexOf(page);
				this.DrawItem(this,new DrawItemEventArgs(g,this.Font,rectTab,i,stat));
			}
		}

        private void DrawTabImage(Rectangle rectTab, 
			McTabPage page, 
			Graphics g, 
			int xMax, 
			ref int xDraw)
		{
			// Default to using the Icon from the page
			Image drawIcon = page.Image;
			Image drawImage = null;
			
			// If there is no valid Icon and the page is requested an image list index...
			if ((drawIcon == null) && (page.ImageIndex != -1))
			{
				try
				{
					// Default to using an image from the McTabPage
					ImageList imageList = page.ImageList;

					// If page does not have an ImageList...
					if (imageList == null)
						imageList = _imageList;   // ...then use the McTabControl one

					// Do we have an ImageList to select from?
					if (imageList != null)
					{
						// Grab the requested image
						drawImage = imageList.Images[page.ImageIndex];
					}
				}
				catch(Exception)
				{
					// User supplied ImageList/ImageIndex are invalid, use an error image instead
					drawImage = _internalImages.Images[(int)ImageStrip.Error];
				}
			}

			// Draw any image required
			if ((drawImage != null) || (drawIcon != null))
			{
				// Enough room to draw any of the image?
				if ((xDraw + PositionImageGapLeft) <= xMax)
				{
					// Move past the left image gap
					xDraw += PositionImageGapLeft;

					// Find down position for drawing the image
					int yDraw = rectTab.Top + 
						PositionImageGapTop + 
						_imageGapTopExtra;

					// If there is enough room for all of the image?
					if ((xDraw + _imageWidth) <= xMax)
					{
						if (drawImage != null)
							g.DrawImage(drawImage, new Rectangle(xDraw, yDraw, _imageWidth, _imageHeight));
						else
							g.DrawImage(drawIcon, new Rectangle(xDraw, yDraw, _imageWidth, _imageHeight));

						// Move past the image and the image gap to the right
						xDraw += _imageWidth + PositionImageGapRight;
					}
					else
					{
						// Calculate how much room there is
						int xSpace = xMax - xDraw;

						// Any room at all?
						if (xSpace > 0)
						{
							if (drawImage != null)
							{
								// Draw only part of the image
								g.DrawImage(drawImage, 
									new Point[]{new Point(xDraw, yDraw), 
												   new Point(xDraw + xSpace, yDraw), 
												   new Point(xDraw, yDraw + _imageHeight)},
									new Rectangle(0, 0, xSpace, 
									_imageHeight),
									GraphicsUnit.Pixel);
							}
                            
							// All space has been used up, nothing left for text
							xDraw = xMax;
						}
					}
				}
			}
		}

        private void DrawTabText(Rectangle rectTab, 
			McTabPage page, 
			Graphics g, 
			bool highlightText,
			int xMax, 
			int xDraw)
		{
			//if (!_selectedTextOnly || (_selectedTextOnly && page.Selected))
			//{
				// Any space for drawing text?
				if (xDraw < xMax)
				{
					//Color drawColor;
					Font drawFont = base.Font;
					Brush drawBrush;
				

					// Decide which base color to use
					if (highlightText)
					{
						//drawColor =_hotTextColor ;
						drawBrush =LayoutManager.GetBrushHot();// HotTextColor ;
					}
					else
					{
						// Do we modify base color depending on selection?
						if (!page.Selected)//_dimUnselected && 
						{
							// Reduce the intensity of the color
                            drawBrush = new SolidBrush(StyleLayout.DefaultInactiveColor);
						}
						else
						{
							//drawColor = ForeColor;
							drawBrush = LayoutManager.GetBrushText();// TextInactiveColor;
						}
					}


					// Now the color is determined, create solid brush
					//drawBrush = new SolidBrush(drawColor);

					// Ensure only a single line is draw from then left hand side of the
					// rectangle and if to large for line to shows ellipsis for us
					StringFormat drawFormat = new StringFormat();
					drawFormat.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
					drawFormat.Trimming = StringTrimming.EllipsisCharacter;
					drawFormat.Alignment = StringAlignment.Center;
					drawFormat.HotkeyPrefix = HotkeyPrefix.Show;

					// Find the vertical drawing limits for text
					int yStart = rectTab.Top + PositionImageGapTop;

					int yEnd = rectTab.Bottom - 
						PositionImageGapBottom - 
						PositionBorderBottom;

					// Use text offset to adjust position of text
					yStart += PositionTextOffset;

					// Across the text left gap
					xDraw += PositionTextGapLeft;

					// Need at least 1 pixel width before trying to draw
					if (xDraw < xMax)
					{
						// Find drawing rectangle
						Rectangle drawRect = new Rectangle(xDraw, yStart, xMax - xDraw, yEnd - yStart);

						// Finally....draw the string!
						g.DrawString(page.Text, drawFont, drawBrush, drawRect, drawFormat);
					}
                        	
					// Cleanup resources!
					drawFormat.Dispose();
					drawBrush.Dispose();
					//drawFont.Dispose();
				}
			//}
		}

        private void DrawTabBorder(ref Rectangle rectTab, Graphics g, bool selected, bool hot)
		{
            //added for replace the tab area rect
            rectTab.Height+=2;
            if (!_positionAtTop)
            {
                rectTab.Y -= 3;
            }
            // Drawing the border is style specific
            switch (ControlLayout)
			{
                case ControlLayout.System:
					//DrawPlainTabBorder(page, g, rectTab);
                    DrawSystemTabBorder(g, rectTab,selected, hot);
                    break;
                case ControlLayout.XpLayout:
                    DrawXpTabBorder(g, rectTab, selected, hot);
					break;
                case ControlLayout.Flat:
                    DrawButtonTabBorder(g, rectTab, selected, hot);
					break;
                case ControlLayout.Visual:
                    DrawPolygonTabBorder(g, rectTab, selected, hot);
					break;
                case ControlLayout.VistaLayout:
                    DrawVistaTabBorder(g, rectTab, selected, hot);
                    break;
            }
		}

        private void DrawMultiBoxBorder(McTabPage page, Graphics g, Rectangle rectPage)
		{
			if (page.Selected)
			{
				using (SolidBrush lightlight = new SolidBrush(_backLightLight))
					g.FillRectangle(lightlight, rectPage);

				using (Pen darkdark = new Pen(_backDarkDark))
					g.DrawRectangle(darkdark, rectPage);
			}
			else
			{
				using (SolidBrush backBrush = new SolidBrush(this.BackColor))
					g.FillRectangle(backBrush, rectPage.X + 1, rectPage.Y, rectPage.Width - 1, rectPage.Height);
            
				// Find the index into McTabPage collection for this page
				int index = _tabPages.IndexOf(page);

				// Decide if the separator should be drawn
				bool drawSeparator = (index == _tabPages.Count - 1) ||
					(index < (_tabPages.Count - 1)) && 
					(_tabPages[index+1].Selected != true);
                                         
				// Multiline mode is slighty more complex
				if (_multiline && !drawSeparator)
				{
					// By default always draw separator
					drawSeparator = true;
                    
					// If we are not the last item
					if (index < (_tabPages.Count - 1))
					{
						// If the next item is selected
						if (_tabPages[index+1].Selected == true)
						{
							Rectangle thisRect = (Rectangle)_tabRects[index];
							Rectangle nextRect = (Rectangle)_tabRects[index+1];

							// If we are on the same drawing line then do not draw separator
							if (thisRect.Y == nextRect.Y)
								drawSeparator = false;
						}
					}
				}

				// Draw tab separator unless the next page after us is selected
				if (drawSeparator)
				{
					using(Pen lightlight = new Pen(_backLightLight),
							  dark = new Pen(_backDark))
					{
						g.DrawLine(dark, rectPage.Right, rectPage.Top + 2, rectPage.Right, 
							rectPage.Bottom - PositionTabsBottomGap - 1);
						g.DrawLine(lightlight, rectPage.Right+1, rectPage.Top + 2, rectPage.Right+1, 
							rectPage.Bottom - PositionTabsBottomGap - 1);
					}
				}
			}
		}

        private void DrawPlainTabBorder(McTabPage page, Graphics g, Rectangle rectPage)
		{
		
			using(Pen light = new Pen(_backLightLight),
					  dark = new Pen(_backDark),
					  darkdark = new Pen(_backDarkDark))
			{
				int yLeftOffset = 0;
				int yRightOffset = 0;

				using (Brush backBrush =  new SolidBrush(SystemColors.Control))// base.BackColor))
				{
					if (page.Selected)
					{
						// Calculate the rectangle that covers half the top border area
						int yBorder;
						
						if (_positionAtTop)
							yBorder = rectPage.Top + (PositionBorderTop / 2);
						else
							yBorder = rectPage.Top - (PositionBorderTop / 2);

						// Construct rectangle that covers the outer part of the border
						Rectangle rectBorder = new Rectangle(rectPage.Left, yBorder, rectPage.Width - 1, rectPage.Height);

						// Blank out area 
						g.FillRectangle(backBrush, rectBorder);

						// Make the left and right border lines extend higher up
						yLeftOffset = -2;
						yRightOffset = -1;
					}
				}

				if (_positionAtTop)
				{
					// Draw the left border
					g.DrawLine(light, rectPage.Left, rectPage.Bottom, rectPage.Left, rectPage.Top + 2); 
					g.DrawLine(light, rectPage.Left + 1 , rectPage.Top + 1, rectPage.Left + 1, rectPage.Top + 2); 

					// Draw the top border
					g.DrawLine(light, rectPage.Left + 2, rectPage.Top + 1, rectPage.Right - 2, rectPage.Top + 1); 

					// Draw the right border
					g.DrawLine(light, rectPage.Right, rectPage.Bottom - yRightOffset, rectPage.Right, rectPage.Top + 2); 
					g.DrawLine(dark, rectPage.Right - 1, rectPage.Bottom - yRightOffset, rectPage.Right - 1, rectPage.Top + 2); 
					g.DrawLine(dark, rectPage.Right - 2, rectPage.Top + 1, rectPage.Right - 2, rectPage.Top + 2); 
					g.DrawLine(light, rectPage.Right - 2, rectPage.Top, rectPage.Right, rectPage.Top + 2);
				}
				else
				{
					// Draw the left border
					g.DrawLine(light, rectPage.Left, rectPage.Top + yLeftOffset, rectPage.Left, rectPage.Bottom - 2); 
					g.DrawLine(dark, rectPage.Left + 1 , rectPage.Bottom - 1, rectPage.Left + 1, rectPage.Bottom - 2); 

					// Draw the bottom border
					g.DrawLine(dark, rectPage.Left + 2, rectPage.Bottom - 1, rectPage.Right - 2, rectPage.Bottom - 1); 
					g.DrawLine(darkdark, rectPage.Left + 2, rectPage.Bottom, rectPage.Right - 2, rectPage.Bottom); 

					// Draw the right border
					g.DrawLine(light, rectPage.Right, rectPage.Top, rectPage.Right, rectPage.Bottom - 2); 
					g.DrawLine(dark, rectPage.Right - 1, rectPage.Top + yRightOffset, rectPage.Right - 1, rectPage.Bottom - 2); 
					g.DrawLine(dark, rectPage.Right - 2, rectPage.Bottom - 1, rectPage.Right - 2, rectPage.Bottom - 2); 
					g.DrawLine(light, rectPage.Right - 2, rectPage.Bottom, rectPage.Right, rectPage.Bottom - 2);
				}
			}
		}

		private void DrawPolygonTabBorder(Graphics g, Rectangle rectPage,bool selected,bool hot)
		{
			
			Point[] pointArray1;
			Point[] pointArray2;

			if (this._positionAtTop)
			{
                pointArray2 = new Point[] { new Point(rectPage.X, rectPage.Y ), new Point(rectPage.X, rectPage.Bottom ), new Point(rectPage.Right + 5, rectPage.Bottom ), new Point(rectPage.Right - 5, rectPage.Y ) };
				pointArray1 = pointArray2;
			}
			else
			{
                rectPage.Height--;
                rectPage.Y++;
                pointArray2 = new Point[] { new Point(rectPage.X, rectPage.Y), new Point(rectPage.X, rectPage.Bottom), new Point(rectPage.Right - 5, rectPage.Bottom), new Point(rectPage.Right + 5, rectPage.Y) };
				pointArray1 = pointArray2;
			}

			using (GraphicsPath path1 = new GraphicsPath(FillMode.Alternate))
			{
				path1.AddPolygon(pointArray1);

				if (selected)
				{
					// Draw background in selected color
					//using (SolidBrush pageAreaBrush = new SolidBrush(_backTabSelected))//this.BackColor))
					//	g.FillRectangle(pageAreaBrush, rectPage);
                    if (rectPage == _nullPosition || rectPage.Height == 0 || rectPage.Width == 0)
						return;
					using (Brush pageAreaBrush = LayoutManager.GetBrushGradient(rectPage,270f,_positionAtTop))
						g.FillPath(pageAreaBrush, path1);



                    using (Pen penBorder = LayoutManager.GetPenBorder())
                    {
                        if (!_positionAtTop)
                        {
                            g.DrawLine(penBorder, pointArray1[0], pointArray1[1]);
                            g.DrawLine(penBorder, pointArray1[1], pointArray1[2]);
                            g.DrawLine(penBorder, pointArray1[2], pointArray1[3]);
                        }
                        else
                        {
                            g.DrawLine(penBorder, pointArray1[0], pointArray1[1]);
                            g.DrawLine(penBorder, pointArray1[0], pointArray1[3]);
                            g.DrawLine(penBorder, pointArray1[2], pointArray1[3]);
                        }
                    }

                    using (Pen penLight =LayoutManager.GetPenLight())
                    {
                        if (this._positionAtTop)
                            g.DrawLine(penLight, rectPage.X+1, rectPage.Bottom, rectPage.Right+5, rectPage.Bottom);
                        else
                            g.DrawLine(penLight, rectPage.X+1, rectPage.Top-1, rectPage.Right+5, rectPage.Top-1);
                    }
				}
				else
				{

					using(Brush pageAreaBrush = new SolidBrush(_backIDE))
						g.FillPath(pageAreaBrush, path1);


                    using (Pen penBorder = new Pen(McPaint.Dark(SystemColors.Control, 40)))
					{
						if (!_positionAtTop)
						{
                            g.DrawLine(penBorder, pointArray1[0], pointArray1[1]);
                            g.DrawLine(penBorder, pointArray1[1], pointArray1[2]);
                            g.DrawLine(penBorder, pointArray1[2], pointArray1[3]);
						}
						else
						{
                            g.DrawLine(penBorder, pointArray1[0], pointArray1[1]);
                            g.DrawLine(penBorder, pointArray1[0], pointArray1[3]);
                            g.DrawLine(penBorder, pointArray1[2], pointArray1[3]);
						}
					}
				}
			}
		}

        private void DrawXpTabBorder(Graphics g, Rectangle rectPage, bool selected, bool hot)
		{

			GraphicsPath path1,pathHot;
			int radius =3;
            Rectangle rectHot =new Rectangle( rectPage.X+1,rectPage.Y,rectPage.Width-2,2);
            //rectHot.Height = 2;
            if (this._positionAtTop)
            {
                path1 = DrawUtils.GettRoundedTopRect((RectangleF)rectPage, radius);
                pathHot = DrawUtils.GettRoundedTopRect((RectangleF)rectHot, radius);
            }
            else
            {
                path1 = DrawUtils.GettRoundedBottomRect((RectangleF)rectPage, radius);
                rectHot.Y = rectPage.Bottom - 2;
                pathHot = DrawUtils.GettRoundedBottomRect((RectangleF)rectHot, radius);

            }
			g.SmoothingMode = SmoothingMode.AntiAlias;

            if (selected)
            {
                // Draw background in selected color
                if (rectPage == _nullPosition || rectPage.Height == 0 || rectPage.Width == 0)
                    return;

                using (Brush pageAreaBrush = LayoutManager.GetBrushGradient(rectPage, 270f, _positionAtTop),
                    selectedAreaBrush = LayoutManager.GetBrushHot())
                {
                    g.FillPath(pageAreaBrush, path1);
                    g.FillPath(selectedAreaBrush, pathHot);
                }
                using (Pen penBorder = LayoutManager.GetPenBorder(),
                    penLight = LayoutManager.GetPenLight())
                {
                    g.DrawPath(penBorder, path1);
                    if (this._positionAtTop)
                        g.DrawLine(penLight, rectPage.X+1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penLight, rectPage.X+1, rectPage.Top, rectPage.Right, rectPage.Top);

                }

            }
            else
            {
                using (Brush pageAreaBrush = new SolidBrush(_backIDE))
                    g.FillPath(pageAreaBrush, path1);

                if (hot)
                {
                    using (Brush selectedAreaBrush = LayoutManager.GetBrushHot())//this.BackColor))
                        g.FillPath(selectedAreaBrush, pathHot);
                }

                using (Pen InActive = LayoutManager.GetPenInActive(), penBorder = LayoutManager.GetPenBorder())
                {
                    g.DrawPath(InActive, path1);
                    if (this._positionAtTop)
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Top, rectPage.Right, rectPage.Top);
                }
               
            }
            path1.Dispose();
            pathHot.Dispose();
		}

        private void DrawVistaTabBorder(Graphics g, Rectangle rectPage, bool selected, bool hot)
        {

            GraphicsPath path1, pathHot;
            int radius = 3;
            Rectangle rectHot = new Rectangle(rectPage.X + 1, rectPage.Y, rectPage.Width - 2, 2);
            //rectHot.Height = 2;
            if (this._positionAtTop)
            {
                path1 = DrawUtils.GettRoundedTopRect((RectangleF)rectPage, radius);
                pathHot = DrawUtils.GettRoundedTopRect((RectangleF)rectHot, radius);
            }
            else
            {
                path1 = DrawUtils.GettRoundedBottomRect((RectangleF)rectPage, radius);
                rectHot.Y = rectPage.Bottom - 2;
                pathHot = DrawUtils.GettRoundedBottomRect((RectangleF)rectHot, radius);

            }
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (selected)
            {
                // Draw background in selected color
                if (rectPage == _nullPosition || rectPage.Height == 0 || rectPage.Width == 0)
                    return;

                using (Brush pageAreaBrush = LayoutManager.GetBrushGradientDark(rectPage, 270f, !_positionAtTop),
                    selectedAreaBrush = LayoutManager.GetBrushHot())
                {
                    g.FillPath(pageAreaBrush, path1);
                    g.FillPath(selectedAreaBrush, pathHot);
                }
                using (Pen penBorder = LayoutManager.GetPenBorder(),
                    penLight = LayoutManager.GetPenLight())
                {
                    g.DrawPath(penBorder, path1);
                    if (this._positionAtTop)
                        g.DrawLine(penLight, rectPage.X + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penLight, rectPage.X + 1, rectPage.Top, rectPage.Right, rectPage.Top);

                }

            }
            else
            {
                using (Brush pageAreaBrush = new SolidBrush(_backIDE))
                    g.FillPath(pageAreaBrush, path1);

                if (hot)
                {
                    using (Brush selectedAreaBrush = LayoutManager.GetBrushHot())//this.BackColor))
                        g.FillPath(selectedAreaBrush, pathHot);
                }

                using (Pen InActive = LayoutManager.GetPenInActive(), penBorder = LayoutManager.GetPenBorder())
                {
                    g.DrawPath(InActive, path1);
                    if (this._positionAtTop)
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Top, rectPage.Right, rectPage.Top);
                }

            }
            path1.Dispose();
            pathHot.Dispose();
        }

        private void DrawButtonTabBorder(Graphics g, Rectangle rectPage, bool selected, bool hot)
		{

            Rectangle rectHot = rectPage;
            rectHot.Height = 2;
            if (!_positionAtTop)
            {
                rectHot.Y = rectPage.Bottom - 2;
            }
			if (selected)
			{
                if (rectPage == _nullPosition || rectPage.Height == 0 || rectPage.Width == 0)
					return;

                using (Brush pageAreaBrush = LayoutManager.GetBrushGradient(rectPage, 270f, _positionAtTop),
                    selectedAreaBrush = LayoutManager.GetBrushHot())
                {
                    g.FillRectangle(pageAreaBrush, rectPage);
                    g.FillRectangle(selectedAreaBrush, rectHot);
                }

                using (Pen penBorder = LayoutManager.GetPenBorder(),
                    penLight = LayoutManager.GetPenLight())
                {
                    g.DrawRectangle(penBorder, rectPage);
                    if (this._positionAtTop)
                        g.DrawLine(penLight, rectPage.X+1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penLight, rectPage.X+1, rectPage.Top, rectPage.Right, rectPage.Top);
                }
			}
			else
			{
				using(Brush pageAreaBrush = new SolidBrush(_backIDE))
					g.FillRectangle(pageAreaBrush, rectPage);

                if (hot)
                {
                    using (Brush selectedAreaBrush = LayoutManager.GetBrushHot())// Brushes.OrangeRed)//this.BackColor))
                        g.FillRectangle(selectedAreaBrush, rectHot);
                }

                using (Pen penInActive = LayoutManager.GetPenInActive(), penBorder = LayoutManager.GetPenBorder())
                {
                    g.DrawRectangle(penInActive, rectPage);
                    if (this._positionAtTop)
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Top, rectPage.Right, rectPage.Top);

                }
            }
		}

        private void DrawSystemTabBorder(Graphics g, Rectangle rectPage, bool selected, bool hot)
        {

            GraphicsPath path1, pathHot;
            int radius = 3;
            Rectangle rectHot = new Rectangle(rectPage.X + 1, rectPage.Y, rectPage.Width - 2, 2);
            //rectHot.Height = 2;
            if (this._positionAtTop)
            {
                path1 = DrawUtils.GettRoundedTopRect((RectangleF)rectPage, radius);
                pathHot = DrawUtils.GettRoundedTopRect((RectangleF)rectHot, 2);
            }
            else
            {
                path1 = DrawUtils.GettRoundedBottomRect((RectangleF)rectPage, radius);
                rectHot.Y = rectPage.Bottom - 2;
                pathHot = DrawUtils.GettRoundedBottomRect((RectangleF)rectHot, 2);

            }
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (selected)
            {
                // Draw background in selected color
                if (rectPage == _nullPosition || rectPage.Height == 0 || rectPage.Width == 0)
                    return;

                using (Brush pageAreaBrush = LayoutManager.GetBrushBar())
                    g.FillPath(pageAreaBrush, path1);

                using (Brush selectedAreaBrush = new SolidBrush(StyleLayout.DefaultButtonHotColor))
                    g.FillPath(selectedAreaBrush, pathHot);

                using (Pen penBorder = LayoutManager.GetPenBorder(), penLight = LayoutManager.GetPenLight())
                {
                    g.DrawPath(penBorder, path1);

                    if (this._positionAtTop)
                        g.DrawLine(penLight, rectPage.X + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penLight, rectPage.X + 1, rectPage.Top, rectPage.Right, rectPage.Top);
                }
            }
            else
            {
                using (Brush pageAreaBrush = new SolidBrush(_backIDE))
                    g.FillPath(pageAreaBrush, path1);

                if (hot)
                {
                    using (Brush selectedAreaBrush = new SolidBrush(StyleLayout.DefaultButtonHotColor))
                        g.FillPath(selectedAreaBrush, pathHot);
                }

                using (Pen penInActive = LayoutManager.GetPenInActive(), penBorder = LayoutManager.GetPenBorder())
                {
                    g.DrawPath(penInActive, path1);
                    if (this._positionAtTop)
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
                    else
                        g.DrawLine(penBorder, rectPage.X + 1, rectPage.Top, rectPage.Right, rectPage.Top);

                }
            }
            path1.Dispose();
            pathHot.Dispose();
        }


		#endregion
		
		#region Collection Methods
        /// <summary>
        /// OnClearingPages
        /// </summary>
      	protected virtual void OnClearingPages()
		{
			// Is a page currently selected?
			if (_pageSelected != -1)
			{
				// Deselect the page
				DeselectPage(_tabPages[_pageSelected]);

				// Remember that nothing is selected
				_pageSelected = -1;
				_startPage = -1;
			}

			// Remove all the user controls 
			foreach(McTabPage page in _tabPages)
				RemoveTabPage(page);

			// Remove all rectangles associated with McTabPage's
			_tabRects.Clear();
		}
        /// <summary>
        /// OnClearedPages
        /// </summary>
		protected virtual void OnClearedPages()
		{
			// Must recalculate after the pages have been removed and
			// not before as that would calculate based on pages still
			// being present in the list
			ReSetting();
            
			// Raise selection changing event
			OnSelectionChanging(EventArgs.Empty);

			// Must notify a change in selection
			OnSelectedIndexChanged(EventArgs.Empty);
            
			Invalidate();
		}
        /// <summary>
        /// OnInsertingPage
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		protected virtual void OnInsertingPage(int index, object value)
		{
			// If a page currently selected?
			if (_pageSelected != -1)
			{
				// Is the selected page going to be after this new one in the list
				if (_pageSelected >= index)
					_pageSelected++;  // then need to update selection index to reflect this
			}
		}
        /// <summary>
        /// OnInsertedPage
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		protected virtual void OnInsertedPage(int index, object value)
		{
			bool selectPage = false;

			McTabPage page = value as McTabPage;


			// Hookup to receive McTabPage property changes
			page.PropertyChanged += new McTabPage.PropChangeHandler(OnPagePropertyChanged);

			// Add the appropriate Control/Form/McTabPage to the control
			AddTabPage(page);
			
			// Do we want to select this page?
			if ((_pageSelected == -1) || (page.Selected))
			{
				// Raise selection changing event
				OnSelectionChanging(EventArgs.Empty);

				// Any page currently selected
				if (_pageSelected != -1)
					DeselectPage(_tabPages[_pageSelected]);

				// This becomes the newly selected page
				_pageSelected = _tabPages.IndexOf(page);

				// If no page is currently defined as the start page
				if (_startPage == -1)
					_startPage = 0;	 // then must be added then first page

				// Request the page be selected
				selectPage = true;
    
			}

			// Add new rectangle to match new number of pages, this must be done before
			// the 'SelectPage' or 'OnSelectionChanged' to ensure the number of _tabRects 
			// entries matches the number of _tabPages entries.
			_tabRects.Add((object)new Rectangle(0,0,1,1));

			// Cause the new page to be the selected one
			if (selectPage)
			{
				// Must recalculate to ensure the new _tabRects entry above it correctly
				// filled in before the new page is selected, as a change in page selection
				// may cause the _tabRects values ot be interrogated.
				ReSetting();

				SelectPage(page);

				// Raise selection change event
				OnSelectedIndexChanged(EventArgs.Empty);
			}

			ReSetting();
			Invalidate();
		}
        /// <summary>
        /// OnRemovingPage
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		protected virtual void OnRemovingPage(int index, object value)
		{
			McTabPage page = value as McTabPage;

			page.PropertyChanged -= new McTabPage.PropChangeHandler(OnPagePropertyChanged);

			// Remove the appropriate Control/Form/McTabPage to the control
			RemoveTabPage(page);

			// Notice a change in selected page
			_changed = false;

			// Is this the currently selected page
			if (_pageSelected == index)
			{
				// Raise selection changing event
				OnSelectionChanging(EventArgs.Empty);

				_changed = true;
				DeselectPage(page);
			}
		}
        /// <summary>
        /// OnRemovedPage
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		protected virtual void OnRemovedPage(int index, object value)
		{
			// Is first displayed page then one being removed?
			if (_startPage >= index)
			{
				// Decrement to use start displaying previous page
				_startPage--;

				// Have we tried to select off the left hand side?
				if (_startPage == -1)
				{
					// Are there still some pages left?
					if (_tabPages.Count > 0)
						_startPage = 0;
				}
			}

			// Is the selected page equal to or after this new one in the list
			if (_pageSelected >= index)
			{
				// Decrement index to reflect this change
				_pageSelected--;

				// Have we tried to select off the left hand side?
				if (_pageSelected == -1)
				{
					// Are there still some pages left?
					if (_tabPages.Count > 0)
						_pageSelected = 0;
				}

				// Is the new selection valid?
				if (_pageSelected != -1)
					SelectPage(_tabPages[_pageSelected]);  // Select it
			}

			// Change in selection causes event generation
			if (_changed)
			{
				// Reset changed flag
				_changed = false;

				// Raise selection change event
				OnSelectedIndexChanged(EventArgs.Empty);
			}

			// Remove a rectangle to match number of pages
			_tabRects.RemoveAt(0);

			//this.Controls.Remove(value as McTabPage);

			ReSetting();
			Invalidate();
		}
		/// <summary>
        /// AddTabPage
		/// </summary>
		/// <param name="page"></param>
		internal protected virtual void AddTabPage(McTabPage page)
		{

			// Has not been shown for the first time yet
			page.Shown = false;
        
			page.Hide();

			// Must fill the entire hosting panel it is on
			page.Dock = DockStyle.None;

			// Set correct size
//			page.Location = new Point(0,0);
//			page.Size = _hostPanel.Size;
//			page.Location = _pageRect.Location;
//			page.Size = _pageRect.Size;

			// Add the McTabPage itself instead
			//_hostPanel.Controls.Add(page);
	
			//SettingNewPage(page); 
			page.owner=this;
			//this._tabPages.Add(page);
			if(this._hostPanel!=null)
			{
				page.Location = new Point(0,0);
				page.Size = _hostPanel.Size;
				this._hostPanel.Controls.Add(page);
			}
			else
			{
				page.Location = _pageRect.Location;
				page.Size = _pageRect.Size;
				if(!this.Controls.Contains(page))
					this.Controls.Add(page);
			}

		}
        /// <summary>
        /// RemoveTabPage
        /// </summary>
        /// <param name="page"></param>
		internal protected virtual void RemoveTabPage(McTabPage page)
		{
			// Use helper method to circumvent form Close bug
			//ControlUtils.Remove(_hostPanel.Controls, page);
			//ControlUtils.Remove(this.Controls, page);
			if(this._hostPanel!=null)
			{
				ControlUtils.Remove(_hostPanel.Controls, page);
			}
			else
			{
				ControlUtils.Remove(this.Controls, page);
			}

	
		}

		#endregion

		#region Events
        /// <summary>
        /// OnPagePropertyChanged
        /// </summary>
        /// <param name="page"></param>
        /// <param name="prop"></param>
        /// <param name="oldValue"></param>
		protected virtual void OnPagePropertyChanged(McTabPage page, McTabPage.Property prop, object oldValue)
		{
			switch(prop)
			{
				case McTabPage.Property.Control:
//					Control pageControl = oldValue as Control;
//
//					// Is there a Control to be removed?
//					if (pageControl != null)
//					{
//						// Use helper method to circumvent form Close bug
//						ControlUtils.Remove(this.Controls, pageControl);
//					}
//					else
//					{
//						// Use helper method to circumvent form Close bug
//						ControlUtils.Remove(this.Controls, page); // remove the whole McTabPage instead
//					}
//
//					// Add the appropriate Control/Form/McTabPage to the control
//					AddTabPage(page);
//
//					// Is a page currently selected?
//					if (_pageSelected != -1)
//					{
//						// Is the change in Control for this page?
//						if (page == _tabPages[_pageSelected])
//							SelectPage(page);   // make Control visible
//					}
//
//					ReSetting();
//					Invalidate();
					break;
				case McTabPage.Property.Text:
				case McTabPage.Property.ImageIndex:
				case McTabPage.Property.ImageList:
				case McTabPage.Property.Icon:
				case McTabPage.Property.PageVisible:
					_recalculate = true;
					Invalidate();
					break;
				case McTabPage.Property.Selected:
					// Becoming selected?
					if (page.Selected)
					{
						// Move selection to the new page and update page properties
						MovePageSelection(page);
						MakePageVisible(page);
					}
					break;
			}
		}
        /// <summary>
        /// OnMouseDown
        /// </summary>
        /// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			// Only select a button or page when using left mouse button
			InternalMouseDown(new Point(e.X, e.Y));

			base.OnMouseDown(e);
		}
        /// <summary>
        /// InternalMouseDown
        /// </summary>
        /// <param name="mousePos"></param>
		private void InternalMouseDown(Point mousePos)
		{
			// Clicked on a tab page?
			for(int i=0; i<_tabPages.Count; i++)
			{
				Rectangle rect = (Rectangle)_tabRects[i];

				if (rect.Contains(mousePos))
				{
					// Are the scroll buttons being shown?
					if (_leftArrow.Visible)
					{
						// Ignore mouse down over then buttons area
						if (mousePos.X >= _leftArrow.Left)
							break;
					}
					else
					{
						// No, is the close button visible?
						if (_closeButton.Visible)
						{
							// Ignore mouse down over then close button area
							if (mousePos.X >= _closeButton.Left)
								break;
						}
					}

					// Remember where the left mouse was initially pressed
					if (Control.MouseButtons == MouseButtons.Left)
					{
						//_leftMouseDown = true;
						//_ignoreDownDrag = false;
						//_leftMouseDownDrag = false;
						//_leftMouseDownPos = mousePos;
					}
                    
					MovePageSelection(_tabPages[i]);
					MakePageVisible(_tabPages[i]);
					break;
				}
			}
		}		
        /// <summary>
        /// OnMouseMove
        /// </summary>
        /// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{

            int mousePage = -1;

            //if (_hotTrack )
            //{
				//int mousePage = -1;
				bool pageChanged = false;

				// Create a point representing current mouse position
                Point mousePos = new Point(e.X, e.Y);

                // Find the page this mouse point is inside
                for (int pos = 0; pos < _tabPages.Count; pos++)
                {
                    Rectangle rect = (Rectangle)_tabRects[pos];

                    if (rect.Contains(mousePos))
                    {
                        mousePage = pos;
                        break;
                    }
                }
                //if (mousePage != _hotTrackPage && _hotTrackPage > -1 && _hotTrackPage < _tabPages.Count)
                //{
                //    _hoverPage = _tabPages[_hotTrackPage];
                //    //McTabPage pg = _tabPages[_hotTrackPage];
                //    //if (toolTip != null && !string.IsNullOrEmpty(pg.ToolTipText))
                //    //{
                //    //    toolTip.Hide(pg);
                //    //}
                //}

				if (/*_hotTrack &&*/ !pageChanged && (mousePage != _hotTrackPage))
				{
					Graphics g = this.CreateGraphics();

					// Clip the drawing to prevent drawing in unwanted areas
					ClipDrawingTabs(g);

					// Remove highlight of old page
                    if (_hotTrackPage != -1 && (_hotTrackPage < _tabPages.Count))
                    {
                        DrawTab(_tabPages[_hotTrackPage], g, false);

                        //ToolTipProvider.Hide(_tabPages[_hotTrackPage]);
                    }
					_hotTrackPage = mousePage;

					// Add highlight to new page
                    if (_hotTrackPage != -1 && (_hotTrackPage < _tabPages.Count))
                    {
                        DrawTab(_tabPages[_hotTrackPage], g, true);

                        //if (toolTip != null && !string.IsNullOrEmpty(_tabPages[_hotTrackPage].ToolTipText))
                        //{
                        //    toolTip.Show(_tabPages[_hotTrackPage].ToolTipText, _tabPages[_hotTrackPage], mousePos);
                        //}

                    }

					// Must correctly release resource
					g.Dispose();
				}
            //}
            
			base.OnMouseMove(e);
		}
        /// <summary>
        /// OnMouseEnter
        /// </summary>
        /// <param name="e"></param>
		protected override void OnMouseEnter(EventArgs e)
		{
			//_mouseOver = true;
            //if (toolTip != null && !DesignMode && _trackPage!=null && !string.IsNullOrEmpty(_trackPage.ToolTipText))
            //{
            //    toolTip.Show(_trackPage.ToolTipText, this);
            //}
			base.OnMouseEnter(e);
		}
        /// <summary>
        /// OnMouseLeave
        /// </summary>
        /// <param name="e"></param>
		protected override void OnMouseLeave(EventArgs e)
		{

            //if (_hotTrack)
            //{
				int newTrackPage = -1;

				if (newTrackPage != _hotTrackPage)
				{
					Graphics g = this.CreateGraphics();

					// Clip the drawing to prevent drawing in unwanted areas
					ClipDrawingTabs(g);

					// Remove highlight of old page
                    if (_hotTrackPage != -1 && _hotTrackPage < _tabPages.Count)
                    {
                        DrawTab(_tabPages[_hotTrackPage], g, false);
                        //toolTip.Hide(_tabPages[_hotTrackPage]);
                    }
					_hotTrackPage = newTrackPage;

					// Must correctly release resource
					g.Dispose();
				}
            //}
            //if (toolTip != null && !DesignMode && _trackPage!=null)
            //{
            //    toolTip.Hide(this);
            //}

			base.OnMouseLeave(e);
		}		

        /// <summary>
        /// OnSystemColorsChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            // If still using the Default color when we were created
            SerializeBackColor(McTabControl.DefaultBackColor, true);
            ReSetting();
            Invalidate();

            base.OnSystemColorsChanged(e);
        }

		#endregion

		#region virtual Helper

		internal McTabPage GetTabPageAtPoint(Point p)
		{
			using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
			{
				foreach (McTabPage page1 in _tabPages)
				{
					if (!page1.Visible && !base.DesignMode)
					{
						continue;
					}
					//Rectangle rectangle1 = this.GetPageRectangle(graphics1, page1);
					Rectangle rectangle1 =(Rectangle) this._tabRects[_tabPages.IndexOf(page1)];// .GetPageRectangle(graphics1, page1);

					if (rectangle1.Contains(p))
					{
						return page1;
					}
				}
			}
			return null;
		}
        /// <summary>
        ///Check for focus at child controldFocus
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
  		private Control FindFocus(Control root)
		{
			// Does the root control has focus?
			if (root.Focused)
				return root;

			// Check for focus at each child control
			foreach(Control c in root.Controls)
			{
				Control child = FindFocus(c);

				if (child != null)
					return child;
			}

			return null;
		}
        /// <summary>
        /// DeselectPage
        /// </summary>
        /// <param name="page"></param>
		protected virtual void DeselectPage(McTabPage page)
		{
			page.Selected = false;

//			// Hide any associated control
//			if (page.Control != null)
//			{
//				// Should we remember which control had focus when leaving?
//				if (_recordFocus)
//				{
//					// Record current focus location on Control
//					if (page.Control.ContainsFocus)
//						page.StartFocus = FindFocus(page.Control);
//					else
//						page.StartFocus = null;
//				}
//
//				page.Control.Hide();
//			}
//			else
//			{
				// Should we remember which control had focus when leaving?
				if (_recordFocus)
				{
					// Record current focus location on Control
					if (page.ContainsFocus)
						page.StartFocus = FindFocus(page);
					else
						page.StartFocus = null;
				}

				page.Hide();
			//}
		}
        /// <summary>
        /// SelectPage
        /// </summary>
        /// <param name="page"></param>
		protected virtual void SelectPage(McTabPage page)
		{
			//Fix_Visible
			if(!DesignMode && !page.PageVisible)
				return;

			page.Selected = true;
			//this.SelectedTab=page;

//			// Bring the control for this page to the front
//			if (page.Control != null)
//				HandleShowingTabPage(page, page.Control);
//			else
				HandleShowingTabPage(page, page);
		}

		private void HandleShowingTabPage(McTabPage page, Control c)
		{
			// First time this page has been displayed?
			if (!page.Shown)
			{
				// Special testing needed for Forms
				Form f = c as Form;
                
				// AutoScaling can cause the Control/Form to be
                if ((f != null) && (f.AutoScaleMode == AutoScaleMode.Dpi))//.AutoScale))
				{
					// Workaround the problem where a form has a defined 'AutoScaleBaseSize' value. The 
					// first time it is shown it calculates the size of each contained control and scales 
					// as needed. But if the contained control is Dock=DockStyle.Fill it scales up/down so 
					// its not actually filling the space! Get around by hiding and showing to force correct 
					// calculation.
					c.Show();
					c.Hide();
				}
                    
				// Only need extra logic first time around
				page.Shown = true;
			}

			// Finally, show it!
			c.Show();

			// Restore focus to last know control to have it
			if (page.StartFocus != null)
				page.StartFocus.Focus();
			else
				c.Focus();
		}
		/// <summary>
        /// MovePageSelection
		/// </summary>
		/// <param name="page"></param>
		private void MovePageSelection(McTabPage page)
		{
			//Fix_Visible
			if(!DesignMode && !page.PageVisible)
				return;

			int pageIndex = _tabPages.IndexOf(page);

			if (pageIndex != _pageSelected)
			{
				// Raise selection changing event
				OnSelectionChanging(EventArgs.Empty);

				// Any page currently selected?
				if (_pageSelected != -1)
					DeselectPage(_tabPages[_pageSelected]);

				_pageSelected = pageIndex;

				if (_pageSelected != -1)
					SelectPage(_tabPages[_pageSelected]);

				// Change in selection causes tab pages sizes to change
				if (_multiline)//(_selectedTextOnly ||  _multiline)
				{
					ReSetting();
					//Invalidate();
				}

                Invalidate();

				// Raise selection change event
				OnSelectedIndexChanged(EventArgs.Empty);

			}
		}

		// Used by the TabControlDesigner
		internal bool ExternalMouseTest(IntPtr hWnd, Point mousePos)
		{
			bool res= ((ControlMouseTest(hWnd, mousePos, _leftArrow) ||
				ControlMouseTest(hWnd, mousePos, _rightArrow) ||
				ControlMouseTest(hWnd, mousePos, _closeButton)));
			
				//if(!res)
				  // InternalMouseDown(mousePos);
			return res;
			
		}

		private bool ControlMouseTest(IntPtr hWnd, Point mousePos, Control check)
		{
			// Is the mouse down for the left arrow window and is it valid to click?
			if ((hWnd == check.Handle) && check.Visible && check.Enabled)
			{
				// Check if the mouse click is over the left arrow
				if (check.ClientRectangle.Contains(mousePos))
				{
					if (check == _leftArrow)
						OnLeftArrow(null, EventArgs.Empty);
	
					if (check == _rightArrow)
						OnRightArrow(null, EventArgs.Empty);

					return true;
				}
			}

			return false;
		}

		#endregion


	}
}


