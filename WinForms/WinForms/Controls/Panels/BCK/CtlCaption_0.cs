
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

using mControl.WinCtl.Controls;
using mControl.Util;
using mControl.Collections;
using mControl.Drawing;

namespace mControl.WinCtl.Controls
{

	[Designer( typeof(WinCtl.Controls.Design.CaptionDesigner))]
	//[ProvideProperty("HelpText",typeof(Control))]
    [System.ComponentModel.ToolboxItem(true)]
    [ToolboxBitmap(typeof(CtlPanel), "Toolbox.PanelCaption.bmp")]
    public class CtlCaption : CtlPanel//, System.ComponentModel.IExtenderProvider
	{
		#region Members

	    // Class wide constants
	    protected const int _panelGap = 10;
	    protected const int _buttonGap = 10;
		protected const int _imagePadding = 6;
		protected static Image _standardPicture;
	
	    // Instance fields
		protected bool _showPicture;
		protected Image _picture;
		protected int _imageIndex;
		protected ImageList _imageList;

		protected string _title;
		protected string _subText;
        protected Font _fontTitle;
        protected Font _fontSubTitle;
		//protected Color _colorBackTitle1;
		//protected Color _colorBackTitle2;
		//protected Color _colorTitle;
        protected Color _colorSubTitle;
        protected bool _assignDefault;
        //protected float _titleAngle;
        private Point imagePoint = Point.Empty;
   
        // Instance events
        public event EventHandler CaptionTitleChanged;

		#endregion

		#region Constructor

		static CtlCaption()
		{
			// Create a strip of images by loading an embedded bitmap resource
			_standardPicture = DrawUtils.LoadBitmap(Type.GetType("mControl.WinCtl.Controls.CtlCaption"),
				"mControl.WinCtl.Images.mCtlIcon32.bmp");
		}

 		public CtlCaption()
		{
            
			//_titleAngle=90f;
			_imageIndex=-1;
			_showPicture=true;
			_subText="";

			InitializeComponent();

			base.BorderStyle=BorderStyle.FixedSingle;
            
            // Default properties
            ResetTitle();
            ResetTitleFont();
            //ResetTitleColor();
            ResetSubTitleFont();
            ResetSubTitleColor();
            ResetPicture();
    		//mControl.Util.Net.netWinCtl.NetFram(this.Name); 
			//initHelpLabel();
		}

		internal CtlCaption(bool net):this()
		{
			base.m_netFram=net;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// Caption
			this.BackColor = System.Drawing.Color.White;
			this.Dock = System.Windows.Forms.DockStyle.Top;
			this.Location = new System.Drawing.Point(0, 0);
			this.Size = new System.Drawing.Size(328,56);
			this.Name = "CtlCaption";
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Properties

		[Category("Style")]//,DefaultValue(ControlLayout.System)]
		public override ControlLayout ControlLayout
		{
			get
			{
				return base.ControlLayout;
			}
			set
			{
				base.ControlLayout = value;
				if(value==ControlLayout.System)
				{
					base.BackColor = Color.White;
					this.Invalidate();
				}
			}
		}

		[DefaultValue(null)]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ImageList ImageList
		{
			get { return _imageList; }
		
			set 
			{ 
				_imageList = value; 
			}
		}

		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImageIndex
		{
			get
			{
				if (((this._imageIndex != -1) && (this._imageList != null)) && (this._imageIndex >= this._imageList.Images.Count))
				{
					return (this._imageList.Images.Count - 1);
				}
				return this._imageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");
				}
				if (value != -1)
				{
					this._picture = null;
				}
				this._imageIndex = value;
				this.Invalidate();
			}
		}

        [Category("Caption")]
        [Description("Main title image")]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image Image
        {
            get { return _picture; }
            
            set
            {
                _picture = value;
                this.Invalidate();
            }
        }

		[Category("Caption"),DefaultValue(true)]
		[Description("Show Main title Picture")]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowImage
		{
			get { return _showPicture; }
            
			set
			{
				_showPicture = value;
				this.Invalidate();
			}
		}

        protected bool ShouldSerializePicture()
        {
            return _picture.Equals(_standardPicture);
        }
        
        public void ResetPicture()
        {
            this.Image = _standardPicture;
        }
        
        [Category("Caption")]
		[Description("Main title text")]
		[Localizable(true)]
		[Browsable(true)]//,EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
		    get { return _title; }
		    
		    set
		    {
		        _title = value;
		        this.Invalidate();
		    }
		}
		
        public void ResetTitle()
        {
            Text = "Caption Control";
        }

        protected bool ShouldSerializeTitle()
        {
            return !_title.Equals("Caption Control");
        }
    
        [Category("Caption")]
		[Description("Font for drawing main title text")]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Font TitleFont
		{
		    get { return _fontTitle; }
		    
		    set
		    {
				if(value!=null)
				{
					_fontTitle = value;
					this.Invalidate();
				}
		    }
		}
		
        public void ResetTitleFont()
        {
            TitleFont = new Font("Tahoma", 10, FontStyle.Bold);
        }

        protected bool ShouldSerializeTitleFont()
        {
            return !_fontTitle.Equals(new Font("Tahoma", 10, FontStyle.Bold));
        }
 
		[Category("Caption")]
		[Description("Sub title text")]
		[Localizable(true)]
		[Browsable(true)]//,EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SubText
		{
			get { return _subText; }
		    
			set
			{
				_subText = value;
				this.Invalidate();
			}
		}

        [Category("Caption")]
        [Description("Font for drawing main sub-title text")]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Font SubTitleFont
        {
            get { return _fontSubTitle; }
		    
            set
            {
				if(value!=null)
				{
					_fontSubTitle = value;
					this.Invalidate();
				}
            }
        }
		
        public void ResetSubTitleFont()
        {
            _fontSubTitle = new Font("Tahoma", 8, FontStyle.Regular);
        }

        protected bool ShouldSerializeSubTitleFont()
        {
            return !_fontSubTitle.Equals(new Font("Tahoma", 8, FontStyle.Regular));
        }

//		[Category("Caption"),DefaultValue(90f)]
//		[Description("The LinearGradientBrush angle between 0 and 360")]
//		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//		public float GradiaentAngle
//		{
//			get { return base.gradiaentAngle;}
//            
//			set
//			{
//				if(value >=0f && value < 360f)
//				{
//					//_titleAngle = value;
//					base.gradiaentAngle=value;
//					this.Invalidate();
//				}
//			}
//		}
  
         [Category("Caption")]
        [Description("Color for drawing main sub-title text")]
		//[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SubTitleColor
        {
            get { return _colorSubTitle; }
		    
            set
            {
                _colorSubTitle = value;
                this.Invalidate();
            }
        }

        public void ResetSubTitleColor()
        {
            SubTitleColor = base.ForeColor;
        }

        protected bool ShouldSerializeSubTitleColor()
        {
            return !_colorSubTitle.Equals(base.ForeColor);
        }

 		#endregion

		#region Hiden Property

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
		    
			set
			{
				base.BorderStyle = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new System.Windows.Forms.DockStyle Dock
		{
			get { return base.Dock; }
		    
			set
			{
				base.Dock = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Enabled
		{
			get { return base.Enabled; }
		    
			set
			{
				base.Enabled = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new System.Windows.Forms.ImeMode ImeMode
		{
			get { return base.ImeMode; }
		    
			set
			{
				base.ImeMode = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new System.Drawing.Point Location
		{
			get { return base.Location; }
		    
			set
			{
				base.Location = value;
			}
		}

//		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//		public string Name
//		{
//			get { return base.Name; }
//		    
//			set
//			{
//				base.Name = value;
//			}
//		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size Size
		{
			get { return base.Size; }
		    
			set
			{
				base.Size = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex
		{
			get { return base.TabIndex; }
		    
			set
			{
				base.TabIndex = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Visible
		{
			get { return base.Visible; }
		    
			set
			{
				base.Visible = value;
			}
		}
		#endregion

		#region Events

        private Image GetImage()
        {
            if (_picture != null)
            {
                return _picture;
            }
            Image image = null;

            if (this._imageIndex > -1 && this._imageList != null)
            {
                if (_imageIndex < _imageList.Images.Count)
                {
                    image = _imageList.Images[_imageIndex];
                }
            }
             return  image;
        }

        private void ResizeImage()
        {
            if (_showPicture)
            {
                Image image = GetImage();
                if (image != null)
                {
                    // Calculate starting Y position to give equal space above and below image
                    int y = (int)((this.Height - image.Height) / 2);
                    // Adjust right side by width of width and gaps around it
                    int x = this.Width - (image.Width + _imagePadding + _panelGap);
                    imagePoint = new Point(x, y);
                }
            }
        }


        protected void OnCaptionTitleChanged(object sender, EventArgs e)
        {
            // Generate event so any dialog containing use can be notify
            if (CaptionTitleChanged != null)
                CaptionTitleChanged(this, e);
        }

        protected override void OnResize(EventArgs e)
        {
            ResizeImage();
            //this.PerformLayout();
			this.Invalidate();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            base.Location = new Point(0, 0);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Rectangle rect =this.ClientRectangle;
 
            int right = this.Width;
 			if (_showPicture)
			{
                Image image = GetImage();
				if(image!=null)
				{
                    pe.Graphics.DrawImage(image, imagePoint);//, image.Width, image.Height);
                 
                    //// Adjust right side by width of width and gaps around it
                    right -= image.Width + _imagePadding + _panelGap;
				}
			}

         
            // Create main title drawing rectangle
            RectangleF drawRectF = new Rectangle(_panelGap, _panelGap, right - _panelGap, _fontTitle.Height);

            using (StringFormat drawFormat = new StringFormat())
            {
                drawFormat.Alignment = StringAlignment.Near;
                drawFormat.LineAlignment = StringAlignment.Center;
                drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                drawFormat.FormatFlags = StringFormatFlags.NoClip |
                StringFormatFlags.NoWrap;

                using (Brush mainTitleBrush = CtlStyleLayout.GetBrushText(), subTitleBrush = new SolidBrush(_colorSubTitle))
                {
                    pe.Graphics.DrawString(_title, _fontTitle, mainTitleBrush, drawRectF, drawFormat);
                    if (_subText != "")
                    {
                        // Adjust rectangle for rest of the drawing text space
                        drawRectF.Y = drawRectF.Bottom + (_panelGap / 2);
                        //drawRectF.X += _panelGap;
                        //drawRectF.Width -= _panelGap;
                        drawRectF.Height = this.SubTitleFont.Height + 5;// this.Height - drawRectF.Y - (_panelGap / 2);

                        // No longer want to prevent word wrap to extra lines
                        drawFormat.LineAlignment = StringAlignment.Near;
                        drawFormat.FormatFlags = StringFormatFlags.NoClip;

                        pe.Graphics.DrawString(_subText, _fontSubTitle, subTitleBrush, drawRectF, drawFormat);
                    }
                }
            }
        }
        
 		
            
		#endregion

		#region IStyleCtl

//		protected IStyle			m_StylePainter;
//
//		[Browsable(false)]
//		public PainterTypes PainterType
//		{
//			get{return PainterTypes.Flat;}
//		}
//
//		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
//		public IStyle StylePainter
//		{
//			get {return m_StylePainter;}
//			set 
//			{
//				if(m_StylePainter!=value)
//				{
//					if (this.m_StylePainter != null)
//						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					m_StylePainter = value;
//					if (this.m_StylePainter != null)
//						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					OnStylePainterChanged(EventArgs.Empty);
//					this.Invalidate(true);
//				}
//			}
//		}
//
//		[Browsable(false)]
//		public virtual IStyleLayout CtlStyleLayout
//		{
//			get
//			{
//				if(this.m_StylePainter!=null)
//					return this.m_StylePainter.Layout as IStyleLayout;
//				else
//					return StyleControl.Layout as IStyleLayout;// this.m_Style as IStyleLayout;
//			}
//		}
//
//		public virtual void SetStyleLayout(StyleLayout value)
//		{
//			if(this.m_StylePainter!=null)
//			{
//				this.m_StylePainter.Layout.SetStyleLayout(value); 
//				Invalidate();
//			}
//		}
//
//		public virtual void SetStyleLayout(Styles value)
//		{
//			if(this.m_StylePainter!=null)
//			{
//				m_StylePainter.Layout.SetStyleLayout(value);
//				Invalidate();
//			}
//		}
//
//		protected virtual void OnStylePainterChanged(EventArgs e)
//		{
//			Invalidate();
//		}
//
//		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
//		{
//			if((DesignMode || IsHandleCreated))
//			{
//				this.Invalidate(true);
//			}
//		}
//
//		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//
//			OnStylePropertyChanged(e);
//		}

		#endregion

		#region HelpLabel
//		private Hashtable helpTexts;
//		internal System.Windows.Forms.Control activeControl;
//
//		private void initHelpLabel()
//		{
//			helpTexts = new Hashtable();
//		}
//
//		private bool trackSelection;
//
//		[Browsable(false)]
//		public bool TrackSelection
//		{
//			get
//			{
//				return trackSelection;
//			}
//			set
//			{
//				trackSelection = value;
//			}
//		}
//
//		bool IExtenderProvider.CanExtend(object target) 
//		{
//			if (target is Control && !(target is CtlCaption)) 
//			{
//				return true;
//			}
//			return false;
//		}
//
//		[DefaultValue("")]
//		public string GetHelpText(Control control) 
//		{
//			string text = (string)helpTexts[control];
//			if (text == null) 
//			{
//				text = string.Empty;
//			}
//			return text;
//		}
//
//		public void SetHelpText(Control control, string value) 
//		{
//			//if(!trackSelection)return;
//
//			if (value == null) 
//			{
//				value = string.Empty;
//			}
//
//			if (value.Length == 0) 
//			{
//				helpTexts.Remove(control);
//
//				control.Enter -= new EventHandler(OnControlEnter);
//				control.Leave -= new EventHandler(OnControlLeave);
//			}
//			else 
//			{
//				helpTexts[control] = value;
//
//				control.Enter += new EventHandler(OnControlEnter);
//				control.Leave += new EventHandler(OnControlLeave);
//			}
//
//			if (control == activeControl) 
//			{
//				PaintActiveHelpText();
//				//Invalidate();
//			}
//		}
//		private void OnControlEnter(object sender, EventArgs e) 
//		{
//			activeControl = (Control)sender;
//			this.SubText=GetHelpText(activeControl);
//			//Invalidate();
//		}
//
//		private void OnControlLeave(object sender, EventArgs e) 
//		{
//			if (sender == activeControl) 
//			{
//				activeControl = null;
//				this.SubText="";
//				//Invalidate();
//			}
//		}
//
//		internal void PaintActiveHelpText()
//		{
//			if (activeControl != null) 
//			{
//				this.SubText=GetHelpText(activeControl);
//				Invalidate();
//			}
//		}

		#endregion

	}




}
