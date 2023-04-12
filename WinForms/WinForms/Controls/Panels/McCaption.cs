
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;



using Nistec.Collections;
using Nistec.Drawing;

namespace Nistec.WinForms
{
 
	[Designer( typeof(WinForms.Design.CaptionDesigner))]
	//[ProvideProperty("HelpText",typeof(Control))]
    [ToolboxItem(true)]//,TypeConverter(typeof(McCaptionConverter))]
    [ToolboxBitmap(typeof(McCaption), "Toolbox.PanelCaption.bmp")]
    public class McCaption : Nistec.WinForms.Controls.McControl
	{
		#region Members

        internal ControlLayout m_ControlLayout;
        internal System.Windows.Forms.BorderStyle m_BorderStyle;
        internal bool autoChildrenStyle = false;

        private GradientStyle gardientStyle = GradientStyle.TopToBottom;

	    // Class wide constants
        private const int _panelGap = 8;// 10;
        private const int _buttonGap = 6;// 10;
        private const int _imagePadding = 6;
        private static Image _standardPicture;
	
        private int _imageIndex;
        private ImageList _imageList;
        //private int _imageSize = 22;
        private Image _image;
        private bool _showImage = true;

        private Color _colorSubTitle;
        private Point imagePoint = Point.Empty;
        
        private bool autoCaptionText = true;
  
        //private string _title="";
        private string _subText = "";
        private Font _fontTitle;
        private Font _fontSubTitle;

        // Instance events
        public event EventHandler CaptionTitleChanged;
        public event EventHandler FormLocationChanged;

		#endregion

        //private McPictureBox ctlpictureBox;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //this.ctlpictureBox = new Nistec.WinForms.McPictureBox();
            //((System.ComponentModel.ISupportInitialize)(this.ctlpictureBox)).BeginInit();
            this.SuspendLayout();
            //// 
            //// ctlpictureBox
            //// 
            //this.ctlpictureBox.BackColor = System.Drawing.Color.Transparent;
            //this.ctlpictureBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this.ctlpictureBox.ForeColor = System.Drawing.SystemColors.ControlText;
            //this.ctlpictureBox.Name = "ctlpictureBox";
            //this.ctlpictureBox.Size = new System.Drawing.Size(22, 22);
            //this.ctlpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            //this.ctlpictureBox.TabStop = false;
            // 
            // McCaption
            // 
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.Controls.Add(this.ctlpictureBox);
            this.Dock = System.Windows.Forms.DockStyle.Top;
            //this.Location = new System.Drawing.Point(0, 0);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "McCaption";
            this.Padding = new System.Windows.Forms.Padding(3);
            // ((System.ComponentModel.ISupportInitialize)(this.ctlpictureBox)).EndInit();
            this.PerformLayout();
            this.ResumeLayout(false);

        }

   
        #endregion

		#region Constructor

		static McCaption()
		{
			// Create a strip of images by loading an embedded bitmap resource
			_standardPicture = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McCaption"),
				"Nistec.WinForms.Images.mCtlIcon32.bmp");
		}

        public McCaption()
        {
            this.m_ControlLayout = ControlLayout.System;
            InitCaption(false);

        }

        public McCaption(ControlLayout layout)
        {
            this.m_ControlLayout = layout;
            InitCaption(false);

        }

        internal McCaption(ControlLayout layout,bool showFormBox)
        {
            this.m_ControlLayout = layout;
            InitCaption(showFormBox);
        }

        public McCaption(ControlLayout layout, Form form)
        {
            this.m_ControlLayout = layout;
            this.form = form;
            InitCaption(true);
        }

        private void InitCaption(bool showFormBox)
        {
            this.TabStop = false;
            //base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable, false);
            //base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //base.SetStyle(ControlStyles.ResizeRedraw, true);


            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserPaint, true);


            _imageIndex = -1;
            //base.drowCustom = true;
            InitializeComponent();
            this.Height = 48;
            //this.BorderStyle = BorderStyle.FixedSingle;
            // Default properties
            ResetTitleFont();
            //ResetTitleColor();
            ResetSubTitleFont();
            ResetSubTitleColor();
            ResetPicture();
            ResetTitle();

        }
 
         protected override void Dispose(bool disposing)
        {
            if (form != null)
            {
                form.ResizeBegin -= new EventHandler(form_ResizeBegin);
                form.ResizeEnd -= new EventHandler(form_ResizeEnd);
            }
            base.Dispose(disposing);
        }

		#endregion

		#region Properties

        [Category("Style"), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool AutoChildrenStyle
        {
            get { return this.autoChildrenStyle; }
            set
            {
                if (autoChildrenStyle != value)
                {
                    autoChildrenStyle = value;
                    SetChildrenStyle(!value);
                }
            }
        }

        [Category("Style"), DefaultValue(GradientStyle.TopToBottom), Browsable(true), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual GradientStyle GradientStyle
        {
            get { return this.gardientStyle; }
            set
            {
                if (gardientStyle != value)
                {
                    gardientStyle = value;
                    this.Invalidate();
                }
            }
        }

 
        [DefaultValue(true)]
        public bool AutoCaptionText
        {
            get { return autoCaptionText; }
            set 
            {
                autoCaptionText = value;
                Invalidate();
            }
        }

 		[Category("Style")]//,DefaultValue(ControlLayout.System)]
		public virtual ControlLayout ControlLayout
		{
			get
			{
                return m_ControlLayout;
			}
			set
			{
                m_ControlLayout = value;
                //if(value==ControlLayout.System)
                //{
                //    base.BackColor = Color.White;
                //    //this.Invalidate();
                //}
                this.Invalidate();
            }
		}

		[DefaultValue(null)]
		public ImageList ImageList
		{
			get { return _imageList; }
		
			set 
			{ 
				_imageList = value; 
			}
		}

		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
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
					this.Image = null;
				}
				this._imageIndex = value;
				this.Invalidate();
			}
		}

        [Category("Caption"),Description("Main title image")]
		public Image Image
        {
            get { return this._image; }
            
            set
            {
                if (value == null)
                {
                    value = this.FindForm().Icon.ToBitmap();
                }

                this._image = value;
                //this.Invalidate();
            }
        }

        //[Category("Caption"), Description("Image Size Mode")]
        //public PictureBoxSizeMode ImageSizeMode
        //{
        //    get { return this.ctlpictureBox.SizeMode; }

        //    set
        //    {
        //        this.ctlpictureBox.SizeMode = value;
        //        //this.Invalidate();
        //    }
        //}

		[Category("Caption"),DefaultValue(true)]
		[Description("Show Main title Picture")]
		public bool ShowImage
		{
            get { return _showImage; }
            
			set
			{
                _showImage = value;
				this.Invalidate();
			}
		}

        protected bool ShouldSerializePicture()
        {
            return Image.Equals(_standardPicture);
        }
        
        public void ResetPicture()
        {
            this.Image = _standardPicture;
        }

        [Category("Caption"), Description("Main title text"), Localizable(true), Browsable(true)]//,EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text/*lblCaption.Text*/; }

            set
            {
                base.Text = value;
                
                if (autoCaptionText && form!=null)
                {
                    form.Text = value;
                }
                //lblCaption.Text = value;
                //DrowTextInternal(); 
                this.Invalidate();
            }
        }
		
        public void ResetTitle()
        {
            Text = "Caption Control";
        }

        protected bool ShouldSerializeTitle()
        {
 
            return !Text.Equals("Caption Control");
        }
    
        [Category("Caption"),Description("Font for drawing main title text")]
		public Font TitleFont
		{
            get { return _fontTitle/*lblCaption.Font*/; }
		    
		    set
		    {
				if(value!=null)
				{
                    if (StylePainter != null)
                        value = LayoutManager.CaptionFont;

                    _fontTitle = value;
                    //lblCaption.Font = value;
					this.Invalidate();
				}
		    }
		}
		
        public void ResetTitleFont()
        {
            if (StylePainter == null)
            {
                TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                //TitleFont = new Font("Tahoma", 10, FontStyle.Bold);
            }
            else
            {
                TitleFont = LayoutManager.CaptionFont;

            }
        }

        protected bool ShouldSerializeTitleFont()
        {
            if (StylePainter == null)
                return !TitleFont.Equals(new Font("Microsoft Sans Serif", 10, FontStyle.Bold));
            return false;
        }
 
		[Category("Caption"),Description("Sub title text"),Localizable(true),Browsable(true)]
		public string SubText
		{
			get { return _subText /*lblSubText.Text*/; }
		    
			set
			{
                _subText = value;
				//lblSubText.Text = value;
                //DrowTextInternal(); 
                this.Invalidate();
			}
		}

        [Category("Caption"),Description("Font for drawing main sub-title text")]
		public Font SubTitleFont
        {
            get { return _fontSubTitle /*lblSubText.Font*/; }
		    
            set
            {
				if(value!=null)
				{
                    if (StylePainter != null)
                        value = LayoutManager.TextFont;

                    _fontSubTitle = value;
					//lblSubText.Font = value;
					this.Invalidate();
				}
            }
        }
		
        public void ResetSubTitleFont()
        {
            if (StylePainter == null)
            {
                SubTitleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
            }
            else
            {
                SubTitleFont = LayoutManager.TextFont;
            }

        }

        protected bool ShouldSerializeSubTitleFont()
        {
            if (StylePainter == null)
                return !SubTitleFont.Equals(new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular));
            return false;
        }

  
        [Category("Caption"),Description("Color for drawing main sub-title text")]
		public Color SubTitleColor
        {
            get { return _colorSubTitle/*lblSubText.ForeColor*/; }
		    
            set
            {
                _colorSubTitle = value;
                //lblSubText.ForeColor = value;
                this.Invalidate();
            }
        }

        public void ResetSubTitleColor()
        {
            SubTitleColor = base.ForeColor;
        }

        protected bool ShouldSerializeSubTitleColor()
        {
            return !SubTitleColor.Equals(base.ForeColor);
        }

  
 		#endregion

		#region Hiden Property

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BorderStyle BorderStyle
		{
            get { return m_BorderStyle; }
		    
			set
			{
                if (m_BorderStyle != value)
                {
                    m_BorderStyle = value;
                    this.Invalidate();
                }
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)]//,DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]//, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.Point Location
        {
            get { return base.Location; }

            set
            {
                base.Location = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            if (Image != null)
            {
                return Image;
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
            if (ShowImage)
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

        private void DrowImage(Graphics g)
        {
            if (!_showImage)
                return;

            Image image = GetImage();
            if (image==null)
                return;

            int imageSize = image.Size.Height;// 32;
            int imageTop = (this.Height - imageSize) / 2;

            Size size = new Size(imageSize, imageSize);
            Point p = new Point(this.Width - (imageSize + 12), imageTop);
            Rectangle rect = new Rectangle(p,size);

            g.DrawImage(image, rect);

            //int index=ImageIndex;
           
            //if (_image != null)
            //{
            //    g.DrawImage(_image, rect);
            //}
            //else if (ImageList != null && index > -1)
            //{
            //    ImageList.Draw(g, p.X,p.Y, imageSize, imageSize, index);
            //}
        }

        private void DrowTextInternal()
        {
            using (Graphics g = this.CreateGraphics())
            {
                DrowText(g);
            }
        }

        private void DrowText(Graphics g)
        {
            //if (isResize && Cursor.Current != System.Windows.Forms.Cursors.Default)
            //{
            //    isResize = !isResize;
            //    return;
            //}
            Rectangle rect = this.ClientRectangle;

            int right = this.Width;
            //if (_showPicture)
            //{
            //    Image image = GetImage();
            //    if (image != null)
            //    {
            //        pe.Graphics.DrawImage(image, imagePoint);//, image.Width, image.Height);

            //        //// Adjust right side by width of width and gaps around it
            //        right -= image.Width + _imagePadding + _panelGap;
            //    }
            //}

            int left = _panelGap;
            // Create main title drawing rectangle
            //RectangleF drawRectF = new Rectangle(_panelGap, _panelGap, right - _panelGap, _fontTitle.Height);
            //g.Clip = new Region(rect);
            //g.Clip.MakeEmpty();

            using (StringFormat drawFormat = new StringFormat())
            {
                drawFormat.Alignment = StringAlignment.Near;
                drawFormat.LineAlignment = StringAlignment.Center;
                drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                drawFormat.FormatFlags = StringFormatFlags.NoClip |
                StringFormatFlags.NoWrap;
                switch (ControlLayout)
                {
                    //case ControlLayout.XpLayout:
                    //    {
                    //        RectangleF drawRectF = new Rectangle(left, (this.Height - _fontTitle.Height) / 2, right - _panelGap, _fontTitle.Height);
                    //        using (Brush mainTitleBrush = LayoutManager.GetBrushText())
                    //        {
                    //            g.DrawString(Text, _fontTitle, mainTitleBrush, drawRectF, drawFormat);
                    //        }
                    //    } break;
                    //case ControlLayout.Flat:
                    //case ControlLayout.System:
                    //case ControlLayout.VistaLayout:
                    //    {
                    //        RectangleF drawRectF = new Rectangle(left, (this.Height - _fontTitle.Height) / 2, right - _panelGap, _fontTitle.Height);
                    //        using (Brush mainTitleBrush = new SolidBrush(Color.White))
                    //        {
                    //            g.DrawString(Text, _fontTitle, mainTitleBrush, drawRectF, drawFormat);
                    //        }
                    //    } break;
                    default:
                        {

                            RectangleF drawRectF = new Rectangle(left, _panelGap, right - _panelGap, _fontTitle.Height);
                            using (Brush mainTitleBrush = LayoutManager.GetBrushText(), subTitleBrush = new SolidBrush(_colorSubTitle))
                            {
                                g.DrawString(Text, _fontTitle, mainTitleBrush, drawRectF, drawFormat);
                                if (_subText != "")// && ControlLayout != ControlLayout.VistaLayout)
                                {
                                    // Adjust rectangle for rest of the drawing text space
                                    drawRectF.Y = drawRectF.Bottom + (_panelGap / 2);
                                    //drawRectF.X += _panelGap;
                                    //drawRectF.Width -= _panelGap;
                                    drawRectF.Height = this.SubTitleFont.Height + 5;// this.Height - drawRectF.Y - (_panelGap / 2);

                                    // No longer want to prevent word wrap to extra lines
                                    drawFormat.LineAlignment = StringAlignment.Near;
                                    drawFormat.FormatFlags = StringFormatFlags.NoClip;

                                    g.DrawString(_subText, _fontSubTitle, subTitleBrush, drawRectF, drawFormat);
                                }
                            }
                        } break;
                    //isResize = false;

                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (IsRsize)
            {
                return;
            }

            Rectangle rect = this.ClientRectangle;
            //e.Graphics.Clip = new Region(rect);
            switch (ControlLayout)
            {
                case ControlLayout.VistaLayout:
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                    DrawPanelGardientStyle(e.Graphics, rect);
                    break;
                case ControlLayout.Flat:
                case ControlLayout.System:
                    DrawPanelFlatStyle(e.Graphics, rect);
                    break;
            }
            DrowImage(e.Graphics);
            DrowText(e.Graphics);
        }

        private void DrawPanelFlatStyle(Graphics g, Rectangle bounds)
        {
            Rectangle rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

            //using (Brush sb = LayoutManager.GetBrushGradient(rect, gradiaentAngle))
            //{
            //    g.FillRectangle(sb, rect);
            //}

            using (Brush b = new SolidBrush(Color.White))
            {
                g.FillRectangle(b, rect);
            }
            if (m_BorderStyle == BorderStyle.FixedSingle)
            {
                using (Pen pen = LayoutManager.GetPenBorder())
                {
                    g.DrawRectangle(pen, rect);
                }
            }
            else if (m_BorderStyle == BorderStyle.Fixed3D)
                ControlPaint.DrawBorder3D(g, bounds, System.Windows.Forms.Border3DStyle.Sunken);
        }

        private void DrawPanelGardientStyle(Graphics g, Rectangle bounds)
        {
            Rectangle rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

            float gradiaentAngle = (float)this.gardientStyle;

            //using (SolidBrush sb = new SolidBrush(this.Parent.BackColor))
            //{
            //    g.FillRectangle(sb, bounds);
            //}
            g.Clear(this.Parent.BackColor);
            this.BackColor = Color.Transparent;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            System.Drawing.Drawing2D.GraphicsPath path = null;// Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 4);

            path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 4);
            using (Brush sb = LayoutManager.GetBrushGradient(rect, gradiaentAngle))
            {
                g.FillPath(sb, path);
            }

            if (m_BorderStyle == BorderStyle.FixedSingle)
            {
                using (Pen pen = LayoutManager.GetPenBorder())
                {
                    g.DrawPath(pen, path);
                }
            }
            else if (m_BorderStyle == BorderStyle.Fixed3D)
                ControlPaint.DrawBorder3D(g, bounds, System.Windows.Forms.Border3DStyle.Sunken);
            path.Dispose();

        }

		#endregion

        #region ILayout

        protected IStyle m_StylePainter;

        [Browsable(false)]
        public PainterTypes PainterType
        {
            get { return PainterTypes.Flat; }
        }

        [Category("Style"), DefaultValue(null), RefreshProperties(RefreshProperties.All)]
        public IStyle StylePainter
        {
            get { return m_StylePainter; }
            set
            {
                if (m_StylePainter != value)
                {
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged -= new PropertyChangedEventHandler(m_Style_PropertyChanged);
                    m_StylePainter = value;
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged += new PropertyChangedEventHandler(m_Style_PropertyChanged);
                    OnStylePainterChanged(EventArgs.Empty);
                    this.Invalidate(true);
                }
            }
        }

        [Browsable(false)]
        public virtual IStyleLayout LayoutManager
        {
            get
            {
                if (this.m_StylePainter != null)
                    return this.m_StylePainter.Layout as IStyleLayout;
                else
                    return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
            }
        }

        public virtual void SetStyleLayout(StyleLayout value)
        {
            if (this.m_StylePainter != null)
                this.m_StylePainter.Layout.SetStyleLayout(value);
        }

        public virtual void SetStyleLayout(Styles value)
        {
            if (this.m_StylePainter != null)
                m_StylePainter.Layout.SetStyleLayout(value);
        }

        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
            if (autoChildrenStyle)
            {
                SetChildrenStyle(false);
            }
        }

        protected virtual void SetChildrenStyle(bool clear)
        {
            foreach (Control c in this.Controls)
            {
                if (c is ILayout)
                {
                    ((ILayout)c).StylePainter = clear ? null : this.StylePainter;
                }
            }
            this.Invalidate(true);
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
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

        private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnStylePropertyChanged(e);
        }

        protected virtual void OnControlLayoutChanged(EventArgs e)
        {

        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
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
            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                case ControlLayout.VistaLayout:
                    base.BackColor = LayoutManager.Layout.ColorBrush1Internal;
                    break;
                default:
                    if (IsHandleCreated && StylePainter != null)
                        base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
                    else if (force)
                        base.BackColor = value;
                    break;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            if (!IsHandleCreated)
                return false;
            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                    return true;
                default:
                    return StylePainter != null;

            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

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
//			if (target is Control && !(target is McCaption)) 
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
        private bool isMouseDown = false;
        private int x;
        private int y;
        private bool IsRsize = false;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            
            //if (form == null)
            //{
            //    form = this.FindForm();
            //}
            if (autoCaptionText && form != null)
            {
                form.Text = Text;
                form.ResizeBegin += new EventHandler(form_ResizeBegin);
                form.ResizeEnd += new EventHandler(form_ResizeEnd);
            }
            //form.TransparencyKey = form.BackColor;

        }

        void form_ResizeEnd(object sender, EventArgs e)
        {
            this.Invalidate();
            IsRsize = false;
        }

        void form_ResizeBegin(object sender, EventArgs e)
        {
            IsRsize = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = true;
            x = e.X;
            y = e.Y;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
            x = 0;
            y = 0;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            try
            {
                if (isMouseDown && form !=null)
                {
                    Point p = new Point(e.X - this.x, e.Y - this.y);
                   // Point pform = new Point(p.X - this.Left, form.MdiParent == null ? p.Y /*- this.Top*/ : p.Y - (this.Bottom-this.Top));
                    //if (form.MdiParent != null)
                    //    pform.Y -= this.Bottom;
                   
                    //form.Location =  PointToScreen(pform);

                    Point pform = new Point(form.Left+p.X, form.Top + p.Y);
                    form.Location = pform;
                    if (FormLocationChanged != null)
                        FormLocationChanged(this, EventArgs.Empty);
                }
            }
            catch { }
        }

        #endregion

 
        public void SetCaptionText(string text,string subText,bool repaint)
        {
            bool shouldRepaint = false;

            if (base.Text != text)
            {
                if (autoCaptionText && form != null)
                {
                    form.Text = text;
                }
                base.Text = text;
                shouldRepaint = true;
            }
            if (_subText != subText)
            {
                _subText = subText;
                shouldRepaint=true;
            }
            if (shouldRepaint && repaint)
            {
                this.Invalidate();
            }
        }
    }


    #region McCaptionConverter
    /// <summary>
    /// Summary description for RangeConverter.
    /// </summary>
    public class McCaptionConverter : TypeConverter
    {
        public McCaptionConverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// allows us to display the + symbol near the property name
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(McCaption));
        }

    }
    #endregion


}
