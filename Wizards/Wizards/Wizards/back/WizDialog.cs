
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

using MControl.Util;
using MControl.Collections;
using MControl.Drawing;
using MControl.WinForms;
using MControl.WinForms.Design;

namespace MControl.Wizards
{

	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap(typeof(McDialog),"Toolbox.WizDialog.bmp")]
	[Designer(typeof(Design.McDialogDesigner))]
	public class McDialog : McUserControl// System.Windows.Forms.UserControl,IStyleCtl
	{

		#region Members
	    // Class wide constants
	    protected const int _panelGap = 10;
	    protected const int _buttonGap = 10;
	    protected static Image _standardPicture;
	
	    // Instance fields
		private bool _showPicture;
        //protected Image _picture;
		private int _imageIndex;
		private ImageList _imageList;

        private string _title;
        private Font _fontTitle;
        private Font _fontSubTitle;
       // protected Color _colorTitle;
		//protected Color _colorBackTitle1;
		//protected Color _colorBackTitle2;
        private Color _colorSubTitle;
       // private WizardTypes wizardType;
        private bool _assignDefault;
		//protected float _titleAngle;
		private GradientStyle gardientStyle;
		//private ControlLayout m_ControlLayout;

		protected ButtonMode _UpdateMode;
		protected ButtonMode _CancelMode;
		protected ButtonMode _CloseMode;
		protected ButtonMode _HelpMode;

	    
	    // Instance designer fields
        protected System.Windows.Forms.Panel _panelTop;
        protected MControl.WinForms.McPanel _panelBottom;
        protected MControl.WinForms.McButton _buttonUpdate;
        protected MControl.WinForms.McButton _buttonCancel;
        protected MControl.WinForms.McButton _buttonClose;
        protected MControl.WinForms.McButton _buttonHelp;
 	
		private System.ComponentModel.IContainer components=null;

		#endregion
 
		#region Events
 
        // Instance events
        public event EventHandler WizardCaptionTitleChanged;
        public event EventHandler UpdateClick;
        public event EventHandler CancelClick;
        public event EventHandler CloseClick;
        public event EventHandler HelpClick;

		#endregion

		#region Constructor

        static McDialog()
        {
            // Create a strip of images by loading an embedded bitmap resource
            _standardPicture = DrawUtils.LoadBitmap(Type.GetType("MControl.Wizards.McWizard"),
                                                        "MControl.Wizards.Images.mCtlIcon32.bmp");
  
		}

		public McDialog()
		{
            // Initialize state
            _UpdateMode = ButtonMode.Default;
            _CancelMode = ButtonMode.Default;
            _CloseMode = ButtonMode.Default;
            _HelpMode = ButtonMode.Default;
            gardientStyle = GradientStyle.BottomToTop;
            //m_ControlLayout = ControlLayout.Visual;

            _showPicture = true;
            _imageIndex = -1;
            _imageList = null;
			InitializeComponent();


            // Hook into drawing events
            _panelTop.Resize += new EventHandler(OnRepaintPanels);
            _panelTop.Paint += new PaintEventHandler(OnPaintTopPanel);
            _panelBottom.Resize += new EventHandler(OnRepaintPanels);
            _panelBottom.Paint += new PaintEventHandler(OnPaintBottomPanel);

             
            // Default properties
            ResetTitle();
            ResetTitleFont();
            //ResetTitleColor();
            ResetSubTitleFont();
            ResetSubTitleColor();
            ResetImage();
            ResetAssignDefaultButton();
            
            // Position and enable/disable control button state
            UpdateControlButtons();
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

	
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
			InitComponent();
		}

		private void InitComponent()
		{
			this._panelTop = new System.Windows.Forms.Panel();
			this._panelBottom = new MControl.WinForms.McPanel();
			this._buttonUpdate = new MControl.WinForms.McButton();
			this._buttonCancel = new MControl.WinForms.McButton();
			this._buttonClose = new MControl.WinForms.McButton();
			this._buttonHelp = new MControl.WinForms.McButton();
			this._panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// _panelTop
			// 
			this._panelTop.BackColor = System.Drawing.Color.White;
			this._panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this._panelTop.Location = new System.Drawing.Point(0, 0);
			this._panelTop.Name = "_panelTop";
			this._panelTop.Size = new System.Drawing.Size(424, 60);
			this._panelTop.TabIndex = 1;
			// 
			// _panelBottom
			// 
			this._panelBottom.AutoChildrenStyle=true;
			this._panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._panelBottom.Controls.Add(this._buttonUpdate);
			this._panelBottom.Controls.Add(this._buttonCancel);
			this._panelBottom.Controls.Add(this._buttonClose);
			this._panelBottom.Controls.Add(this._buttonHelp);
			this._panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			//this._panelBottom.InheritanceStyle = true;
			this._panelBottom.Location = new System.Drawing.Point(0, 276);
			this._panelBottom.Name = "_panelBottom";
			this._panelBottom.Size = new System.Drawing.Size(424, 48);
			this._panelBottom.TabIndex = 2;
			// 
			// _buttonUpdate
			// 
			this._buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonUpdate.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
			this._buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonUpdate.FixSize = false;
			this._buttonUpdate.Location = new System.Drawing.Point(8, 14);
			this._buttonUpdate.Name = "_buttonUpdate";
			this._buttonUpdate.Size = new System.Drawing.Size(70, 20);
			this._buttonUpdate.TabIndex = 4;
			this._buttonUpdate.Text = "Update";
			this._buttonUpdate.Click += new System.EventHandler(this.OnButtonUpdate);
			// 
			// _buttonCancel
			// 
			this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonCancel.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
			this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonCancel.FixSize = false;
			this._buttonCancel.Location = new System.Drawing.Point(184, 14);
			this._buttonCancel.Name = "_buttonCancel";
			this._buttonCancel.Size = new System.Drawing.Size(70, 20);
			this._buttonCancel.TabIndex = 1;
			this._buttonCancel.Text = "Cancel";
			this._buttonCancel.Click += new System.EventHandler(this.OnButtonCancel);
			// 
			// _buttonClose
			// 
			this._buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonClose.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
			this._buttonClose.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonClose.FixSize = false;
			this._buttonClose.Location = new System.Drawing.Point(304, 14);
			this._buttonClose.Name = "_buttonClose";
			this._buttonClose.Size = new System.Drawing.Size(70, 20);
			this._buttonClose.TabIndex = 0;
			this._buttonClose.Text = "Close";
			this._buttonClose.Click += new System.EventHandler(this.OnButtonClose);
			// 
			// _buttonHelp
			// 
			this._buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonHelp.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
			this._buttonHelp.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonHelp.FixSize = false;
			this._buttonHelp.Location = new System.Drawing.Point(360, 14);
			this._buttonHelp.Name = "_buttonHelp";
			this._buttonHelp.Size = new System.Drawing.Size(70, 20);
			this._buttonHelp.TabIndex = 0;
			this._buttonHelp.Text = "Help";
			this._buttonHelp.Click += new System.EventHandler(this.OnButtonHelp);
			// 
			// McWizard
			// 
			this.Controls.Add(this._panelTop);
			this.Controls.Add(this._panelBottom);
			this.Controls.SetChildIndex(this._panelTop,0);
			this.Controls.SetChildIndex(this._panelBottom,0);
			this.Name = "McDialog";
			this.Size = new System.Drawing.Size(424, 304);
			this._panelBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Buttons Properties

		[Category("Control Buttons")]
		[Description("Define visibility of Update button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonUpdateMode
		{
			get { return _UpdateMode; }
            
			set 
			{ 
				if (_UpdateMode != value)
				{
					_UpdateMode = value;
					UpdateControlButtons();
				}
			}
		}

		[Category("Control Buttons")]
		[Description("Define visibility of Cancel button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonCancelMode
		{
			get { return _CancelMode; }
            
			set 
			{ 
				if (_CancelMode != value)
				{
					_CancelMode = value;
					UpdateControlButtons();
				}
			}
		}

		[Category("Control Buttons")]
		[Description("Define visibility of Close button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonCloseMode
		{
			get { return _CloseMode; }
            
			set 
			{ 
				if (_CloseMode != value)
				{
					_CloseMode = value;
					UpdateControlButtons();
				}
			}
		}

		[Category("Control Buttons")]
		[Description("Define visibility of Help button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonHelpMode
		{
			get { return _HelpMode; }
            
			set 
			{ 
				if (_HelpMode != value)
				{
					_HelpMode = value;
					UpdateControlButtons();
				}
			}
		}

		#endregion

		#region Hide Properties
		
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new BorderStyle BorderStyle
		{
			get{return base.BorderStyle;}
			set{base.BorderStyle=value;}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string DefaultValue
		{
			get{return base.DefaultValue;}
			set{base.DefaultValue=value;}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool ReadOnly
		{
			get{return base.ReadOnly;}
			set{base.ReadOnly=value;}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool FixSize
		{
			get{return base.FixSize;}
			set{base.FixSize=value;}
		}
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new BindingFormat BindFormat
		{
			get{return base.BindFormat;}
			set{base.BindFormat=value;}
		}

		#endregion

		#region Properties
		
		[Category("Style"),DefaultValue(GradientStyle.TopToBottom), Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GradientStyle GradientStyle
		{
			get{return this.gardientStyle;}
			set
			{
				if(gardientStyle!=value)
				{
					gardientStyle=value;
					_panelTop.Invalidate();
					this.Invalidate();
				}
			}
		}

		[DefaultValue(ControlLayout.Visual)]    
		[Category("Style")]
		public override ControlLayout ControlLayout 
		{
			get {return base.ControlLayout;}
			set
			{
                if (base.ControlLayout != value)
				{
                    base.ControlLayout = value;
					this._panelTop.Invalidate();
					this.Invalidate (true);
				}
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
				base.Invalidate();
				_panelTop.Invalidate();
			}
		}

        [Category("Wizard")]
        [Description("Access to underlying header panel")]
        internal System.Windows.Forms.Panel HeaderPanel
        {
            get { return _panelTop; }
        }

        [Category("Wizard")]
        [Description("Access to underlying trailer panel")]
        internal MControl.WinForms.McPanel TrailerPanel
        {
            get { return _panelBottom; }
        }

		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Main title Picture")]
		public bool ShowImage//Picture
		{
			get { return _showPicture; }
            
			set
			{
				_showPicture = value;
				_panelTop.Invalidate();
			}
		}

        [Category("Wizard")]
        [Description("Main title Picture")]
        public new Image Image//Picture
        {
			get { return base.Image;}// _picture; }
            
            set
            {
               base.Image=value;// _picture = value;
                _panelTop.Invalidate();
            }
        }

        protected bool ShouldSerializeImage()//Picture()
        {
            return this.Image.Equals(_standardPicture);
        }
        
        public void ResetImage()//Picture()
        {
            this.Image = _standardPicture;
        }
        
        [Category("Wizard")]
		[Description("Main title text")]
		[Localizable(true)]
		public string Title
		{
		    get { return _title; }
		    
		    set
		    {
		        _title = value;
		        _panelTop.Invalidate();
		    }
		}
		
        public void ResetTitle()
        {
            Title = "Wizard Dialog";
        }

        protected bool ShouldSerializeTitle()
        {
            return !_title.Equals("Wizard Dialog");
        }
    
        [Category("Wizard")]
		[Description("Font for drawing main title text")]
		public Font TitleFont
		{
		    get { return _fontTitle; }
		    
		    set
		    {
		        _fontTitle = value;
		        _panelTop.Invalidate();
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
    
        [Category("Wizard")]
        [Description("Font for drawing main sub-title text")]
        public Font SubTitleFont
        {
            get { return _fontSubTitle; }
		    
            set
            {
                _fontSubTitle = value;
                _panelTop.Invalidate();
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

		[Category("Wizard")]
        [Description("Determine is a default button should be auto-assigned")]
        [DefaultValue(false)]
        public bool AssignDefaultButton
        {
            get { return _assignDefault; }
            
            set
            {
                if (_assignDefault != value)
                {
                    _assignDefault = value;
                    AutoAssignDefaultButton();
                }
            }
        }

        public void ResetAssignDefaultButton()
        {
            AssignDefaultButton = false;
        }

        [Category("Wizard")]
        [Description("Color for drawing main sub-title text")]
        public Color SubTitleColor
        {
            get { return _colorSubTitle; }
		    
            set
            {
                _colorSubTitle = value;
                _panelTop.Invalidate();
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
        [Category("Control Buttons")]
        [Description("Modify the text for the Update control button")]
        [DefaultValue("Update")]
        [Localizable(true)]
        public string ButtonUpdateText
        {
            get { return _buttonUpdate.Text; }
            set { _buttonUpdate.Text = value; }
        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Cancel control button")]
        [DefaultValue("Cancel")]
        [Localizable(true)]
        public string ButtonCancelText
        {
            get { return _buttonCancel.Text; }
            set { _buttonCancel.Text = value; }
        }

    
        [Category("Control Buttons")]
        [Description("Modify the text for the Close control button")]
        [DefaultValue("Close")]
        [Localizable(true)]
        public string ButtonCloseText
        {
            get { return _buttonClose.Text; }
            set { _buttonClose.Text = value; }
        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Help control button")]
        [DefaultValue("Help")]
        [Localizable(true)]
        public string ButtonHelpText
        {
            get { return _buttonHelp.Text; }
            set { _buttonHelp.Text = value; }
        }

 
		#endregion

		#region Override
        
        public virtual void OnCloseClick(EventArgs e)
        {
            if (CloseClick != null)
                CloseClick(this, e);
        }

        public virtual void OnCancelClick(EventArgs e)
        {
            if (CancelClick != null)
                CancelClick(this, e);
        }
        
        public virtual void OnUpdateClick(EventArgs e)
        {
            if (UpdateClick != null)
                UpdateClick(this, e);
        }
        
        public virtual void OnHelpClick(EventArgs e)
        {
            if (HelpClick != null)
                HelpClick(this, e);
        }

		#endregion

		#region Methods

        protected void UpdateControlButtons()
        {
            // Track next button inserted position
            int x = this.Width - _buttonGap - _buttonUpdate.Width;
            
            bool showHelp = ShouldShowHelp();
            bool showClose = ShouldShowClose();
            bool showCancel = ShouldShowCancel();
            bool showUpdate = ShouldShowUpdate();
            
            if (showHelp) 
            {
                _buttonHelp.Left = x;
                x -= _buttonHelp.Width + _buttonGap;
                _buttonHelp.Enabled = ShouldEnableHelp();
                _buttonHelp.Show();
            }
            else
                _buttonHelp.Hide();

            if (showClose) 
            {
                _buttonClose.Left = x;
                x -= _buttonClose.Width + _buttonGap;
                _buttonClose.Enabled = ShouldEnableClose();
                _buttonClose.Show();
            }
            else
                _buttonClose.Hide();

             if (showCancel) 
            {
                _buttonCancel.Left = x;
                x -= _buttonCancel.Width + _buttonGap;
                _buttonCancel.Enabled = ShouldEnableCancel();
                _buttonCancel.Show();
            }
            else
                _buttonCancel.Hide();

            if (showUpdate) 
            {
                _buttonUpdate.Left = x;
                x -= _buttonUpdate.Width + _buttonGap;
                _buttonUpdate.Enabled = ShouldEnableUpdate();
                _buttonUpdate.Show();
            }
            else
                _buttonUpdate.Hide();
                
            AutoAssignDefaultButton();
        }
        
        protected void AutoAssignDefaultButton()
        {
            // Get our parent Form instance
            Form parentForm = this.FindForm();
            
            // Cannot assign a default button if we are not on a Form
            if (parentForm != null)
            {
                // Can only assign a particular button if we have been requested 
                // to auto- assign and we are on a selected page
                if (_assignDefault)
                {
                    parentForm.AcceptButton = _buttonUpdate;
                }
                else
                {
                    // Remove any assigned default button
                    parentForm.AcceptButton = null;
                }
            }
        }
        
		#endregion

		#region Config methods

        protected bool ShouldShowClose()
        {
            switch(_CloseMode)// _showClose)
            {
				case ButtonMode.Hide:// McStatus.No:
                    return false;
                case ButtonMode.Disable://McStatus.Yes:
                    return true;
                default:// case ButtonMode.Default://McStatus.Default:
					return true;
            }
        }

        protected bool ShouldEnableClose()
        {
            bool ret = false;
        
            switch(_CloseMode)// _enableClose)
            {
                case ButtonMode.Disable://McStatus.No:
                    break;
                case ButtonMode.Hide://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    ret = true;
                    break;
            }

            return ret;
        }

        protected bool ShouldShowCancel()
        {
 
            switch(_CancelMode)// _showCancel)
            {
                case ButtonMode.Hide://McStatus.No:
					return false;
                case ButtonMode.Disable://McStatus.Yes:
                    return true;
                default://case ButtonMode.Default://McStatus.Default:
					return true;
            }
		}

        protected bool ShouldEnableCancel()
        {
            switch(_CancelMode)// _enableCancel)
            {
                case ButtonMode.Disable://McStatus.No:
                    return false;
                case ButtonMode.Hide://McStatus.Yes:
                    return true;
                default://case ButtonMode.Default:// McStatus.Default:
                    return true;
            }

        }

        protected bool ShouldShowUpdate()
        {
 
            switch(_UpdateMode)// _showUpdate)
            {
                case ButtonMode.Hide://McStatus.No:
                   return false;
                case ButtonMode.Disable://McStatus.Yes:
                    return  true;
                default://case ButtonMode.Default://McStatus.Default:
					return  true;
			}
	 
        }

        protected bool ShouldEnableUpdate()
        {
            bool ret = false;

            switch(_UpdateMode)// _enableUpdate)
            {
                case ButtonMode.Disable://McStatus.No:
                    break;
                case ButtonMode.Hide://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    ret = true;
                    break;
            }

            return ret;
        }

        protected bool ShouldEnableHelp()
        {
            bool ret = false;

            switch(_CancelMode)// _enableCancel)
            {
                case ButtonMode.Disable://McStatus.No:
                    break;
                case ButtonMode.Hide://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    ret = true;
                    break;
            }

            return ret;
        }

		protected bool ShouldShowHelp()
		{
  
			switch(_HelpMode)// _showHelp)
			{
				case ButtonMode.Hide://McStatus.No:
					return false;
				case ButtonMode.Disable://McStatus.Yes:
					return true;
				default://case ButtonMode.Default://McStatus.Default:
					return true;
			}
		}
		#endregion

		#region internal Events

         protected void OnButtonHelp(object sender, EventArgs e)
        {
            // Fire event for interested handlers
            OnHelpClick(EventArgs.Empty);
        }

        protected void OnButtonClose(object sender, EventArgs e)
        {
            // Fire event for interested handlers
            OnCloseClick(EventArgs.Empty);
        }

         protected void OnButtonCancel(object sender, EventArgs e)
        {
            // Fire event for interested handlers
            OnCancelClick(EventArgs.Empty);
        }

        protected void OnButtonUpdate(object sender, EventArgs e)
        {
            // Fire event for interested handlers
            OnUpdateClick(EventArgs.Empty);
        }

       
        protected void OnWizardSubTitleChanged(object sender, EventArgs e)
        {
            WizardPage wp = sender as WizardPage;
           
            _panelTop.Invalidate();
        }        
        
        protected void OnWizardCaptionTitleChanged(object sender, EventArgs e)
        {
            // Generate event so any dialog containing use can be notify
            if (WizardCaptionTitleChanged != null)
                WizardCaptionTitleChanged(this, e);
        }
    
        protected override void OnResize(EventArgs e)
        {
            this.PerformLayout();
        }

        protected void OnRepaintPanels(object sender, EventArgs e)
        {
            _panelTop.Invalidate();
            _panelBottom.Invalidate();
        }

        protected void OnPaintTopPanel(object sender, PaintEventArgs pe)
        {
			Rectangle rect =this._panelTop.ClientRectangle;
			float gradiaentAngle=(float)this.gardientStyle;

			Brush brush=null;
			switch(this.ControlLayout)
			{
				case ControlLayout.Flat:
					brush=base.LayoutManager.GetBrushFlat();
					break;
				case ControlLayout.Visual:
				case ControlLayout.XpLayout:
					brush=base.LayoutManager.GetBrushGradient(rect, gradiaentAngle);
					break;
				case ControlLayout.System:
					brush=new SolidBrush(Color.White);
					break;
			}

			pe.Graphics.FillRectangle(brush,rect);
			brush.Dispose();

//			if(this.StylePainter!=null)
//			{
//				using(Brush b=this.StylePainter.Layout.GetBrushGradient(rect, gradiaentAngle))
//				{
//					pe.Graphics.FillRectangle(b,rect);
//				}
//			}
//			else
//			{
//				using(Brush b=LayoutManager.GetBrushGradient(rect, gradiaentAngle))
//				{
//					pe.Graphics.FillRectangle(b,rect);
//				}
//			}

            int right = _panelTop.Width;
        
			Image image=null;
            // Any picture to draw?
			if (_showPicture)
			{
				if(this._imageIndex > -1 && this._imageList!=null)
				{
					if(_imageIndex < _imageList.Images.Count)
					{
                      image=_imageList.Images[_imageIndex]; 
					}
				}
                if(image==null)
					image=this.Image;
				if(image!=null)
				{
					// Calculate starting Y position to give equal space above and below image
					int Y = (int)((_panelTop.Height - image.Height) / 2);
	
					pe.Graphics.DrawImage(image, _panelTop.Width - image.Width - Y, Y, image.Width, image.Height);
                
					// Adjust right side by width of width and gaps around it
					right -= image.Width + Y + _panelGap;
          		}
			}

//            if (_picture != null && _showPicture)
//            {
//                // Calculate starting Y position to give equal space above and below image
//                int Y = (int)((_panelTop.Height - _picture.Height) / 2);
//                
//                pe.Graphics.DrawImage(_picture, _panelTop.Width - _picture.Width - Y, Y, _picture.Width, _picture.Height);
//                
//                // Adjust right side by width of width and gaps around it
//                right -= _picture.Width + Y + _panelGap;
//            }
        
            // Create main title drawing rectangle
            RectangleF drawRectF = new Rectangle(_panelGap, _panelGap, right - _panelGap, _fontTitle.Height);
                                                
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Near;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.EllipsisCharacter;
            drawFormat.FormatFlags = StringFormatFlags.NoClip |
                                     StringFormatFlags.NoWrap;
            
            using(Brush mainTitleBrush =LayoutManager.GetBrushText())// new SolidBrush(_colorTitle))
                pe.Graphics.DrawString(_title, _fontTitle, mainTitleBrush, drawRectF, drawFormat);            
             
            // Is there a selected tab for display?   
//            if (_tabControl.SelectedIndex != -1)
//            {                
//                // Adjust rectangle for rest of the drawing text space
//                drawRectF.Y = drawRectF.Bottom + (_panelGap / 2);
//                drawRectF.X += _panelGap;
//                drawRectF.Width -= _panelGap;
//                drawRectF.Height = _panelTop.Height - drawRectF.Y - (_panelGap / 2);
//
//                // No longer want to prevent word wrap to extra lines
//                drawFormat.LineAlignment = StringAlignment.Near;
//                drawFormat.FormatFlags = StringFormatFlags.NoClip;
//
//                WizardPage wp = _tabControl.TabPages[_tabControl.SelectedIndex] as WizardPage;
//
//                using(SolidBrush subTitleBrush = new SolidBrush(_colorSubTitle))
//                    pe.Graphics.DrawString(wp.Text, _fontSubTitle, subTitleBrush, drawRectF, drawFormat);
//            }                          
        
			using(Pen lightPen = LayoutManager.GetPenBorder())// new Pen(_panelTop.BackColor),
			//darkPen = new Pen(ControlPaint.Light(ControlPaint.Dark(this.BackColor))))
			{
				pe.Graphics.DrawRectangle(lightPen, 0, _panelTop.Top, _panelTop.Width-1, _panelTop.Height - 1);
				//pe.Graphics.DrawLine(darkPen, 0, _panelTop.Height - 2, _panelTop.Width, _panelTop.Height - 2);
                //pe.Graphics.DrawLine(lightPen, 0, _panelTop.Height - 1, _panelTop.Width, _panelTop.Height - 1);
            }            

			drawFormat.Dispose();
        }
        
        protected void OnPaintBottomPanel(object sender, PaintEventArgs pe)
        {
			using(Pen lightPen = LayoutManager.GetPenBorder())
			{
				pe.Graphics.DrawRectangle(lightPen, 0, _panelBottom.Top, _panelBottom.Width-1, _panelBottom.Height - 1);
            
			}
//			using(Pen lightPen = new Pen(ControlPaint.Light(this.BackColor)),
//                      darkPen = new Pen(ControlPaint.Light(ControlPaint.Dark(this.BackColor))))
//            {
//                pe.Graphics.DrawLine(darkPen, 0, 0, _panelBottom.Width, 0);
//                pe.Graphics.DrawLine(lightPen, 0, 1, _panelBottom.Width, 1);
//            }            
        }

		#endregion

		#region IStyleCtl

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
			{
				this.Invalidate(true);
			}
		}

		protected override void SetChildrenStyle(bool clear)
		{
			base.SetChildrenStyle(clear);
			this._panelBottom.SetStyleLayout(this.LayoutManager.Layout);

			this.Invalidate(true);
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged(e);
			this._panelBottom.StylePainter=this.m_StylePainter;
			Invalidate();

		}

		#endregion

    }
}
