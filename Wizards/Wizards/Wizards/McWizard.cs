
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

using Nistec.Collections;
using Nistec.Drawing;
using Nistec.WinForms;
using Nistec.WinForms.Design;
using Nistec.Win;

namespace Nistec.Wizards
{

 
	[ToolboxItem(true),	ToolboxBitmap(typeof(McWizard),"Toolbox.McWizard.bmp")]
    [DefaultProperty("WizardPanelType")]
    [Designer(typeof(Design.McWizardDesigner))]
    public class McWizard : Nistec.WinForms.Controls.McContainer//, IMcWizard// System.Windows.Forms.UserControl,IStyleCtl
	{
        
        private System.Collections.Generic.List<int> _PagesFlow;
        private int _CurrentFlow;

        [Category("Wizard")]
        [Description("wizard pages flow")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        protected System.Collections.Generic.List<int> PagesFlow
        {
            get 
            {
                if (_PagesFlow == null)
                {
                    _PagesFlow = new System.Collections.Generic.List<int>();
                    
                }
                return _PagesFlow;
            }
        }
        /// <summary>
        /// Set the pages flow index zero based by next,back,finished and close buttons
        /// and define which page to see and which page to ignore, 
        /// for example if the control contains 5 pages, you can set the PagesFlow 
        /// Likw 0,1,3,4 when the 3th page is hide, 4th page is the finished page and 5th page is the Close Page
        /// </summary>
        /// <param name="flow"></param>
        public void SetPagesFlow(params int[] flow)
        {
            if (flow == null)
                return;
            foreach (int i in flow)
            {
                if (i < 0 || i > WizardPages.Count - 1)
                {
                    throw new ArgumentOutOfRangeException("element:" + i.ToString());
                }
            }
            PagesFlow.Clear();
            PagesFlow.AddRange(flow);
        }

        private int GetNextFlowStep()
        {
            if (_CurrentFlow < _PagesFlow.Count - 1)
            {
                _CurrentFlow++; 
            }
            return _PagesFlow[_CurrentFlow];


            //int index = _tabControl.SelectedIndex;
            //for (int i = 0; i < _PagesFlow.Count; i++)
            //{
            //    if (_PagesFlow[i] == index)
            //    {
            //        if(_PagesFlow[i] == _PagesFlow.Count-1)
            //            return _PagesFlow[i];
            //        return _PagesFlow[i + 1];
            //    }
            //}
        }
        private int GetBackFlowStep()
        {
            if (_CurrentFlow > 0)
            {
                _CurrentFlow --;
            }
            return _PagesFlow[_CurrentFlow];
        }

        private void MoveByFolow(bool next)
        {
            //MsgBox.ShowInfo(_CurrentFlow.ToString());

            if (_PagesFlow != null && _PagesFlow.Count > 0)
            {
                int index = _tabControl.SelectedIndex;
                try
                {
                    index = next ? GetNextFlowStep() : GetBackFlowStep();
                    _tabControl.SelectedIndex = index;
                }
                catch (Exception)
                {
                    MsgBox.ShowError("Incorrect PagesFlow");
                }
            }
            else if (next)
            {
                if (_tabControl.SelectedIndex < _tabControl.TabPages.Count-1)
                    _tabControl.SelectedIndex++;
            }
            else 
            {
                if (_tabControl.SelectedIndex > 0) 
                    _tabControl.SelectedIndex--;
            }
        }

		#region NetReflectedFram
//		internal bool m_netFram=false;
//
//		public void NetReflectedFram(string pk)
//		{
//			try
//			{
//				// this is done because this method can be called explicitly from code.
//				System.Reflection.MethodBase method = (System.Reflection.MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
//				m_netFram=Nistec.Util.Net.nf_1.nf_2(method,pk);
//			}
//			catch{}
//		}

//		protected override void OnHandleCreated(EventArgs e)
//		{
//			base.OnHandleCreated (e);
//			if(!DesignMode && !m_netFram)
//			{
//				Nistec.Util.Net.netWinCtl.NetFram(this.Name); 
//			}
//		}

		#endregion

		#region Members
	    // Class wide constants
	    protected const int _panelGap = 10;
	    protected const int _buttonGap =4;// 10;
	    protected static Image _standardPicture;
	
	    // Instance fields
		private bool _showPicture;
        private Image _image;
        //private int _imageIndex;
        //private ImageList _imageList;
        private int _totalPages;

 
        private string _title;
        private Font _fontTitle;
        private Font _fontSubTitle;
       // protected Color _colorTitle;
		//protected Color _colorBackTitle1;
		//protected Color _colorBackTitle2;
        private Color _colorSubTitle;
        private WizardType wizardType;
        private bool _assignDefault;
		//protected float _titleAngle;
		private GradientStyle gardientStyle;
		//private ControlLayout		m_ControlLayout;


  
        protected WizardPage _selectedPage;
//        protected McStatus _showUpdate, _enableUpdate;
//        protected McStatus _showCancel, _enableCancel;
//        protected McStatus _showBack, _enableBack;
//        protected McStatus _showNext, _enableNext;
//        protected McStatus _showFinish, _enableFinish;
//        protected McStatus _showClose, _enableClose;
//        protected McStatus _showHelp, _enableHelp;
        protected WizardPageCollection _wizardPages;

		protected ButtonMode _UpdateMode;
		protected ButtonMode _CancelMode;
		protected ButtonMode _BackMode;
		protected ButtonMode _NextMode;
		protected ButtonMode _FinishMode;
		protected ButtonMode _CloseMode;
		protected ButtonMode _HelpMode;

	    
	    // Instance designer fields
        protected System.Windows.Forms.Control _panelTop;
        protected Nistec.WinForms.McPanel _panelBottom;
        protected Nistec.WinForms.McButton _buttonUpdate;
        protected Nistec.WinForms.McButton _buttonCancel;
        protected Nistec.WinForms.McButton _buttonBack;
        protected Nistec.WinForms.McButton _buttonNext;
        protected Nistec.WinForms.McButton _buttonFinish;
        protected Nistec.WinForms.McButton _buttonClose;
        protected Nistec.WinForms.McButton _buttonHelp;
        protected Nistec.WinForms.McTabControl _tabControl;
		//private Nistec.WinForms.Styles _StylePlan;
	
		private System.ComponentModel.IContainer components=null;

		#endregion
 
		#region Events
        // Delegate definitions
        public delegate void WizardPageHandler(WizardPage wp, McWizard wc);

        // Instance events
        public event WizardPageHandler WizardPageEnter;
        public event WizardPageHandler WizardPageLeave;
        public event EventHandler McCaptionTitleChanged;
        public event EventHandler SelectionChanged;
        public event EventHandler UpdateClick;
        public event EventHandler CancelClick;
        public event EventHandler FinishClick;
        public event EventHandler CloseClick;
        public event EventHandler HelpClick;
        public event CancelEventHandler NextClick;
        public event CancelEventHandler BackClick;

		#endregion

		#region Constructor

        static McWizard()
        {
            // Create a strip of images by loading an embedded bitmap resource
            _standardPicture = DrawUtils.LoadBitmap(Type.GetType("Nistec.Wizards.McWizard"),
                                                        "Nistec.Wizards.Images.mCtlIcon32.bmp");
  
		}

		public McWizard()
		{
            _totalPages = -1;

            _UpdateMode = ButtonMode.Default;
            _CancelMode = ButtonMode.Default;
            _BackMode = ButtonMode.Default;
            _NextMode = ButtonMode.Default;
            _FinishMode = ButtonMode.Default;
            _CloseMode = ButtonMode.Default;
            _HelpMode = ButtonMode.Default;
            gardientStyle = GradientStyle.BottomToTop;
            //m_ControlLayout = ControlLayout.Visual;

            _showPicture = true;
            //_colorBackTitle1=Color.White;
            //_colorBackTitle2=Color.White;
            //_titleAngle=90f;
            //_imageIndex = -1;
            //_imageList = null;

			InitializeComponent();

            //this._tabControl.hideButtons = true;
            this._tabControl.HideButtons(true);
			//this._tabControl.m_netFram=true;
			
			// No page currently selected
			_selectedPage = null;
			
	        // Hook into tab control events
	        _tabControl.SelectedIndexChanged += new EventHandler(OnTabSelectionChanged);

            // Create our collection of wizard pages
            _wizardPages = new WizardPageCollection();
			_wizardPages.wizOwner=this;
            
            // Hook into collection events
            _wizardPages.Cleared += new CollectionClear(OnWizardCleared);
            _wizardPages.Inserted += new CollectionChange(OnWizardInserted);
            _wizardPages.Removed += new CollectionChange(OnWizardRemoved);

            // Hook into drawing events
            _panelTop.Resize += new EventHandler(OnRepaintPanels);
            _panelTop.Paint += new PaintEventHandler(OnPaintTopPanel);
            _panelBottom.Resize += new EventHandler(OnRepaintPanels);
            _panelBottom.Paint += new PaintEventHandler(OnPaintBottomPanel);

            // Initialize state
//            _showUpdate = _enableUpdate = McStatus.Default;
//            _showCancel = _enableUpdate = McStatus.Default;
//            _showBack = _enableBack = McStatus.Default;
//            _showNext = _enableNext = McStatus.Default;
//            _showFinish = _enableFinish = McStatus.Default;
//            _showClose = _enableClose = McStatus.Default;
//            _showHelp = _enableHelp = McStatus.Default;
	
            
            // Default properties
            ResetWizardType();
            ResetTitle();
            ResetTitleFont();
            //ResetTitleColor();
            ResetSubTitleFont();
            ResetSubTitleColor();
            ResetImage();
            ResetAssignDefaultButton();
            
            // Position and enable/disable control button state
            UpdateControlButtons();
			//Nistec.Util.Net.netWinCtl.NetFram(this.Name); 
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
			//this._tabControl.owner=this;
            
    	}

		private void InitComponent()
		{
            this._tabControl = new Nistec.WinForms.McTabControl(this);
			this._panelTop = new System.Windows.Forms.Control();
			this._panelBottom = new Nistec.WinForms.McPanel();
			this._buttonUpdate = new Nistec.WinForms.McButton();
			this._buttonBack = new Nistec.WinForms.McButton();
			this._buttonNext = new Nistec.WinForms.McButton();
			this._buttonCancel = new Nistec.WinForms.McButton();
			this._buttonFinish = new Nistec.WinForms.McButton();
			this._buttonClose = new Nistec.WinForms.McButton();
			this._buttonHelp = new Nistec.WinForms.McButton();
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
			this._panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._panelBottom.Controls.Add(this._buttonUpdate);
			this._panelBottom.Controls.Add(this._buttonBack);
			this._panelBottom.Controls.Add(this._buttonNext);
			this._panelBottom.Controls.Add(this._buttonCancel);
			this._panelBottom.Controls.Add(this._buttonFinish);
			this._panelBottom.Controls.Add(this._buttonClose);
			this._panelBottom.Controls.Add(this._buttonHelp);
			this._panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			//this._panelBottom.InheritanceStyle = true;
			this._panelBottom.Location = new System.Drawing.Point(0, 276);
			this._panelBottom.Name = "_panelBottom";
			this._panelBottom.Size = new System.Drawing.Size(424, 48);
			this._panelBottom.TabIndex = 2;
			// 
			// _tabControl
			// 
//			this._tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//				| System.Windows.Forms.AnchorStyles.Left) 
//				| System.Windows.Forms.AnchorStyles.Right)));
			this._tabControl.Dock=DockStyle.Fill;
			this._tabControl.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this._tabControl.ForeColor = System.Drawing.SystemColors.ControlText;
			this._tabControl.HideTabs= true;
			this._tabControl.ItemSize = new System.Drawing.Size(0, 22);
			this._tabControl.Location = new System.Drawing.Point(0, 60);
			this._tabControl.Name = "_tabControl";
			this._tabControl.SelectedIndex = 0;
			this._tabControl.Size = new System.Drawing.Size(424, 196);
			//this._tabControl.StyleCtl.BorderColor = System.Drawing.Color.SteelBlue;
			//this._tabControl.StyleCtl.StylePlan = Nistec.WinForms.Styles.Custom;
			this._tabControl.TabIndex = 0;

			// 
			// _buttonUpdate
			// 
			this._buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonUpdate.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this._buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonUpdate.FixSize = false;
			this._buttonUpdate.Location = new System.Drawing.Point(8, 14);
			this._buttonUpdate.Name = "_buttonUpdate";
			this._buttonUpdate.Size = new System.Drawing.Size(70, 20);
			this._buttonUpdate.TabIndex = 4;
			this._buttonUpdate.Text = "Update";
			this._buttonUpdate.Click += new System.EventHandler(this.OnButtonUpdate);
			// 
			// _buttonBack
			// 
			this._buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonBack.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this._buttonBack.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonBack.FixSize = false;
			this._buttonBack.Location = new System.Drawing.Point(56, 14);
			this._buttonBack.Name = "_buttonBack";
			this._buttonBack.Size = new System.Drawing.Size(70, 20);
			this._buttonBack.TabIndex = 3;
			this._buttonBack.Text = "< Back";
			this._buttonBack.Click += new System.EventHandler(this.OnButtonBack);
			// 
			// _buttonNext
			// 
			this._buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonNext.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this._buttonNext.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonNext.FixSize = false;
			this._buttonNext.Location = new System.Drawing.Point(120, 14);
			this._buttonNext.Name = "_buttonNext";
			this._buttonNext.Size = new System.Drawing.Size(70, 20);
			this._buttonNext.TabIndex = 2;
			this._buttonNext.Text = "Next >";
			this._buttonNext.Click += new System.EventHandler(this.OnButtonNext);
			// 
			// _buttonCancel
			// 
			this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonCancel.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonCancel.FixSize = false;
			this._buttonCancel.Location = new System.Drawing.Point(184, 14);
			this._buttonCancel.Name = "_buttonCancel";
			this._buttonCancel.Size = new System.Drawing.Size(70, 20);
			this._buttonCancel.TabIndex = 1;
			this._buttonCancel.Text = "Cancel";
			this._buttonCancel.Click += new System.EventHandler(this.OnButtonCancel);
			// 
			// _buttonFinish
			// 
			this._buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonFinish.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this._buttonFinish.DialogResult = System.Windows.Forms.DialogResult.None;
			this._buttonFinish.FixSize = false;
			this._buttonFinish.Location = new System.Drawing.Point(248, 14);
			this._buttonFinish.Name = "_buttonFinish";
			this._buttonFinish.Size = new System.Drawing.Size(70, 20);
			this._buttonFinish.TabIndex = 0;
			this._buttonFinish.Text = "Finish";
			this._buttonFinish.Click += new System.EventHandler(this.OnButtonFinish);
			// 
			// _buttonClose
			// 
			this._buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._buttonClose.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
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
			this._buttonHelp.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
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
			this.Controls.Add(this._tabControl);
			this.Controls.Add(this._panelTop);
			this.Controls.Add(this._panelBottom);
            this.Controls.SetChildIndex(this._panelTop, 0);
			this.Controls.SetChildIndex(this._panelBottom,0);
            this.Controls.SetChildIndex(this._tabControl, 0);
            this.Name = "McWizard";
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
		[Description("Define visibility of Back button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonBackMode
		{
			get { return _BackMode; }
            
			set 
			{ 
				if (_BackMode != value)
				{
					_BackMode = value;
					UpdateControlButtons();
				}
			}
		}

		[Category("Control Buttons")]
		[Description("Define visibility of Next button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonNextMode
		{
			get { return _NextMode; }
            
			set 
			{ 
				if (_NextMode != value)
				{
					_NextMode = value;
					UpdateControlButtons();
				}
			}
		}

		[Category("Control Buttons")]
		[Description("Define visibility of Finish button")]
		[DefaultValue(typeof(ButtonMode), "Default")]
		public ButtonMode ButtonFinishMode
		{
			get { return _FinishMode; }
            
			set 
			{ 
				if (_FinishMode != value)
				{
					_FinishMode = value;
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
		
        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new BorderStyle BorderStyle
        //{
        //    get{return base.BorderStyle;}
        //    set{base.BorderStyle=value;}
        //}

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new string DefaultValue
        //{
        //    get{return base.DefaultValue;}
        //    set{base.DefaultValue=value;}
        //}

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new bool ReadOnly
        //{
        //    get{return base.ReadOnly;}
        //    set{base.ReadOnly=value;}
        //}

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new bool FixSize
        //{
        //    get{return base.FixSize;}
        //    set{base.FixSize=value;}
        //}

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new BindingFormat BindFormat
        //{
        //    get{return base.BindFormat;}
        //    set{base.BindFormat=value;}
        //}

		#endregion

		#region Properties

        [DefaultValue(-1)]
        [Category("Wizard"),Browsable(false),EditorBrowsable( EditorBrowsableState.Advanced)]
        public int ConfigureTotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

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
            get { return base.ControlLayout; }
			set
			{
                if (base.ControlLayout != value)
				{
                    base.ControlLayout = value;

                    this._tabControl.ControlLayout = value;

                    ControlLayout val = value;
                    if(value== ControlLayout.Flat|| value==ControlLayout.System)
                    {
                        val = ControlLayout.XpLayout;
                    }
                    this._buttonBack.ControlLayout = val;
                    this._buttonCancel.ControlLayout = val;
                    this._buttonClose.ControlLayout = val;
                    this._buttonFinish.ControlLayout = val;
                    this._buttonHelp.ControlLayout = val;
                    this._buttonNext.ControlLayout = val;
                    this._buttonUpdate.ControlLayout = val;


                    //switch(value)
                    //{
                    //    case ControlLayout.Flat:
                    //        this._tabControl.ControlLayout = ControlLayout.Visual;
                    //        break;
                    //    case ControlLayout.Visual:
                    //        this._tabControl.ControlLayout = ControlLayout.Flat;
                    //        break;
                    //    case ControlLayout.XpLayout:
                    //        this._tabControl.ControlLayout = ControlLayout.XpLayout;
                    //        break;
                    //    case ControlLayout.System:
                    //        this._tabControl.ControlLayout = ControlLayout.System;
                    //        break;
                    //}
					this._panelTop.Invalidate();
					this.Invalidate (true);
				}
			}

		}


        //[DefaultValue(null)]
        //public ImageList ImageList
        //{
        //    get { return _imageList; }
		
        //    set 
        //    { 
        //        _imageList = value; 
        //    }
        //}

        //[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        //public new int ImageIndex
        //{
        //    get
        //    {
        //        if (((this._imageIndex != -1) && (this._imageList != null)) && (this._imageIndex >= this._imageList.Images.Count))
        //        {
        //            return (this._imageList.Images.Count - 1);
        //        }
        //        return this._imageIndex;
        //    }
        //    set
        //    {
        //        if (value < -1)
        //        {
        //            throw new ArgumentException("InvalidLowBoundArgumentEx");
        //        }
        //        if (value != -1)
        //        {
        //            this.Image = null;
        //        }
        //        this._imageIndex = value;
        //        base.Invalidate();
        //        _panelTop.Invalidate();
        //    }
        //}

        [Category("Wizard"),Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Access to underlying McTabControl instance")]
        internal McTabControl TabControl
        {
            get { return _tabControl; }
        }

        [Category("Wizard")]
        [Description("Access to underlying header panel")]
        internal System.Windows.Forms.Control HeaderPanel
        {
            get { return _panelTop; }
        }

        [Category("Wizard")]
        [Description("Access to underlying trailer panel")]
        internal Nistec.WinForms.McPanel ButtonsPanel
        {
            get { return _panelBottom; }
        }

        [Category("Wizard")]
        [Description("Collection of wizard pages")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public WizardPageCollection WizardPages
		{
		    get { return _wizardPages; }
		}
		
        [Category("Wizard")]
        [Description("Determine default operation of buttons")]
        [DefaultValue(typeof(WizardType), "Configure")]
        public WizardType WizardType
        {
            get { return wizardType; }
		    
            set 
            {
                if (wizardType != value)
                {
                    wizardType = value;
		            
                    switch(wizardType)
                    {
                        case WizardType.Install:
						case WizardType.Configure:
                            // Current page selection determines if full page is needed
                            if (_tabControl.SelectedIndex != -1)
                            {
                                // Get the selected wizard page
                                WizardPage wp = _wizardPages[_tabControl.SelectedIndex];
                
                                // Should we be presented in full page?
                                if (wp.FullPage)
                                    EnterFullPage();
                                else
                                {
                                    // Controller profile is not allowed to be outside of FullMode
                                    if (wizardType != WizardType.Controller)
                                        LeaveFullPage();
                                }
                            }
                            else
                                LeaveFullPage();

                            _tabControl.HideTabs = true; 
                            break;
                        case WizardType.Controller:
                            // Controller is always full page
                            EnterFullPage();

                            _tabControl.HideTabs = false;
                            break;
                    }
		            
                    // Position and enable/disable control button state
                    UpdateControlButtons();
                }
            }
        }

        public void ResetWizardType()
        {
            WizardType = WizardType.Configure;
        }
 
//		[Category("Dialog")]
//		[Description("Color for drawing main title Start Back Color")]
//		public Color TitleBackColor1
//		{
//			get { return _colorBackTitle1; }
//		    
//			set
//			{
//				_colorBackTitle1 = value;
//				_panelTop.Invalidate();
//			}
//		}
//
//		[Category("Dialog")]
//		[Description("Color for drawing main End Back Color")]
//		public Color TitleBackColor2
//		{
//			get { return _colorBackTitle2; }
//		    
//			set
//			{
//				_colorBackTitle2 = value;
//				_panelTop.Invalidate();
//			}
//		}

//		[Category("Dialog"),DefaultValue(90f)]
//		[Description("The LinearGradientBrush angle between 0 and 270")]
//		public float TitleColorAngle
//		{
//			get { return _titleAngle; }
//            
//			set
//			{
//				if(value >=0f && value < 270f)
//				{
//					_titleAngle = value;
//					_panelTop.Invalidate();
//				}
//			}
//		}

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
        public Image Image//Picture
        {
			get { return _image;}// _picture; }
            
            set
            {
                _image = value;// _picture = value;
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
            Title = "Welcome to the Mc Control";
        }

        protected bool ShouldSerializeTitle()
        {
            return !_title.Equals("Welcome to the Mc Control");
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

//        [Category("Wizard")]
//        [Description("Color for drawing main title text")]
//        public Color TitleColor
//		{
//		    get { return _colorTitle; }
//		    
//		    set
//		    {
//		        _colorTitle = value;
//		        _panelTop.Invalidate();
//		    }
//		}

//		public void ResetTitleColor()
//		{
//		    TitleColor = base.ForeColor;
//		}

//        protected bool ShouldSerializeTitleColor()
//        {
//            return !_colorTitle.Equals(base.ForeColor);
//        }
		
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

//        [Category("Control Buttons")]
//        [Description("Define visibility of Update button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowUpdateButton
//        {
//            get { return _showUpdate; }
//            
//            set 
//            { 
//                if (_showUpdate != value)
//                {
//                    _showUpdate = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Update button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableUpdateButton
//        {
//            get { return _enableUpdate; }
//            
//            set 
//            { 
//                if (_enableUpdate != value)
//                {
//                    _enableUpdate = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Update control button")]
        [DefaultValue("Update")]
        [Localizable(true)]
        public string ButtonUpdateText
        {
            get { return _buttonUpdate.Text; }
            set { _buttonUpdate.Text = value; }
        }

//        [Category("Control Buttons")]
//        [Description("Define visibility of Cancel button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowCancelButton
//        {
//            get { return _showCancel; }
//            
//            set 
//            { 
//                if (_showCancel != value)
//                {
//                    _showCancel = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Cancel button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableCancelButton
//        {
//            get { return _enableCancel; }
//            
//            set 
//            { 
//                if (_enableCancel != value)
//                {
//                    _enableCancel = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Cancel control button")]
        [DefaultValue("Cancel")]
        [Localizable(true)]
        public string ButtonCancelText
        {
            get { return _buttonCancel.Text; }
            set { _buttonCancel.Text = value; }
        }

//        [Category("Control Buttons")]
//        [Description("Define visibility of Back button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowBackButton
//        {
//            get { return _showBack; }
//            
//            set 
//            { 
//                if (_showBack != value)
//                {
//                    _showBack = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Back button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableBackButton
//        {
//            get { return _enableBack; }
//            
//            set 
//            { 
//                if (_enableBack != value)
//                {
//                    _enableBack = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Back control button")]
        [DefaultValue("< Back")]
        [Localizable(true)]
        public string ButtonBackText
        {
            get { return _buttonBack.Text; }
            set { _buttonBack.Text = value; }
        }

//        [Category("Control Buttons")]
//        [Description("Define visibility of Next button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowNextButton
//        {
//            get { return _showNext; }
//            
//            set 
//            { 
//                if (_showNext != value)
//                {
//                    _showNext = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Next button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableNextButton
//        {
//            get { return _enableBack; }
//            
//            set 
//            { 
//                if (_enableNext != value)
//                {
//                    _enableNext = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Next control button")]
        [DefaultValue("Next >")]
        [Localizable(true)]
        public string ButtonNextText
        {
            get { return _buttonNext.Text; }
            set { _buttonNext.Text = value; }
        }

//        [Category("Control Buttons")]
//        [Description("Define visibility of Finish button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowFinishButton
//        {
//            get { return _showFinish; }
//            
//            set 
//            { 
//                if (_showFinish != value)
//                {
//                    _showFinish = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Finish button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableFinishButton
//        {
//            get { return _enableFinish; }
//            
//            set 
//            { 
//                if (_enableFinish != value)
//                {
//                    _enableFinish = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Finish control button")]
        [DefaultValue("Finish")]
        [Localizable(true)]
        public string ButtonFinishText
        {
            get { return _buttonFinish.Text; }
            set { _buttonFinish.Text = value; }
        }
        
//        [Category("Control Buttons")]
//        [Description("Define visibility of Close button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowCloseButton
//        {
//            get { return _showClose; }
//            
//            set 
//            { 
//                if (_showClose != value)
//                {
//                    _showClose = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Close button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableCloseButton
//        {
//            get { return _enableClose; }
//            
//            set 
//            { 
//                if (_enableClose != value)
//                {
//                    _enableClose = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Close control button")]
        [DefaultValue("Close")]
        [Localizable(true)]
        public string ButtonCloseText
        {
            get { return _buttonClose.Text; }
            set { _buttonClose.Text = value; }
        }

//        [Category("Control Buttons")]
//        [Description("Define visibility of Help button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus ShowHelpButton
//        {
//            get { return _showHelp; }
//            
//            set 
//            { 
//                if (_showHelp != value)
//                {
//                    _showHelp = value;
//                    UpdateControlButtons();
//                }
//            }
//        }
//
//        [Category("Control Buttons")]
//        [Description("Define selectability of Help button")]
//        [DefaultValue(typeof(McStatus), "Default")]
//        public McStatus EnableHelpButton
//        {
//            get { return _enableHelp; }
//            
//            set 
//            { 
//                if (_enableHelp != value)
//                {
//                    _enableHelp = value;
//                    UpdateControlButtons();
//                }
//            }
//        }

        [Category("Control Buttons")]
        [Description("Modify the text for the Help control button")]
        [DefaultValue("Help")]
        [Localizable(true)]
        public string ButtonHelpText
        {
            get { return _buttonHelp.Text; }
            set { _buttonHelp.Text = value; }
        }

        [Category("Wizard")]
        [Description("Index of currently selected WizardPage")]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return _tabControl.SelectedIndex; }
            set { _tabControl.SelectedIndex = value; }
        }

        [Category("Wizard"),Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("currently selected WizardPage")]
        public McTabPage SelectedPage
        {
            get { return _tabControl.SelectedTab; }
            set { _tabControl.SelectedTab = value; }
        }
		#endregion

		#region Override
        
        public virtual void OnWizardPageEnter(WizardPage wp)
        {
            if (WizardPageEnter != null)
                WizardPageEnter(wp, this);
        }

        public virtual void OnWizardPageLeave(WizardPage wp)
        {
            if (WizardPageLeave != null)
                WizardPageLeave(wp, this);
        }

        public virtual void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }
                
        public virtual void OnCloseClick(EventArgs e)
        {
            if (CloseClick != null)
                CloseClick(this, e);
        }

        public virtual void OnFinishClick(EventArgs e)
        {
            if (FinishClick != null)
                FinishClick(this, e);
        }
    
        public virtual void OnNextClick(CancelEventArgs e)
        {
            if (NextClick != null)
                NextClick(this, e);
        }
    
        public virtual void OnBackClick(CancelEventArgs e)
        {
            if (BackClick != null)
                BackClick(this, e);
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


        public void UpdateControlButtons()
        {
            // Track next button inserted position
            int x = this.Width - _buttonGap - _buttonFinish.Width;
            
            bool showHelp = ShouldShowHelp();
            bool showClose = ShouldShowClose();
            bool showFinish = ShouldShowFinish();
            bool showNext = ShouldShowNext();
            bool showBack = ShouldShowBack();
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

            if (showFinish) 
            {
                _buttonFinish.Left = x;
                x -= _buttonFinish.Width + _buttonGap;
                _buttonFinish.Enabled = ShouldEnableFinish();
                _buttonFinish.Show();
            }
            else
                _buttonFinish.Hide();

            if (showNext) 
            {
                _buttonNext.Left = x;
                x -= _buttonNext.Width + _buttonGap;
                _buttonNext.Enabled = ShouldEnableNext();
                _buttonNext.Show();
            }
            else
                _buttonNext.Hide();

            if (showBack) 
            {
                _buttonBack.Left = x;
                x -= _buttonBack.Width + _buttonGap;
                _buttonBack.Enabled = ShouldEnableBack();
                _buttonBack.Show();
            }
            else
                _buttonBack.Hide();

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
                if (_assignDefault && (_tabControl.SelectedIndex >= 0))
                {
                    // McButton default depends on the profile mode
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            // Is this the last page?
                            if (GetIsEqualStep(1))//(_tabControl.SelectedIndex == (_tabControl.TabPages.Count - 1))
                            {
                                // Then use the Close button
                                parentForm.AcceptButton = _buttonClose;
                            }
                            else
                            {
                                // Is this the second from last page?
                                if (GetIsEqualStep(2))//(_tabControl.SelectedIndex == (_tabControl.TabPages.Count - 2))
                                {
                                    // Then use the Cancel button
                                    parentForm.AcceptButton = _buttonCancel;
                                }
                                else
                                {
                                    // Then use the Next button
                                    parentForm.AcceptButton = _buttonNext;
                                }
                            }
                            break;
                        case WizardType.Configure:
                            // Is this the last page?
                            if (GetIsEqualStep(1))//(_tabControl.SelectedIndex == (_tabControl.TabPages.Count - 1))
                            {
                                // Then always use the Finish button
                                parentForm.AcceptButton = _buttonFinish;
                            }
                            else
                            {
                                // Else we are not on last page, use the Next button
                                parentForm.AcceptButton = _buttonNext;
                            }
                            break;
                        case WizardType.Controller:
                            // Always use the Update button
                            parentForm.AcceptButton = _buttonUpdate;
                            break;
                    }
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

        //private bool ShouldUseTotalPages()
        //{
        //    return (_totalPages > -1 && _totalPages <= _tabControl.TabPages.Count);
        //}
        private bool GetIsLastStep(int offset)
        {
            if (_totalPages > -1 && _totalPages <= _tabControl.TabPages.Count)
                return this.SelectedIndex < _totalPages - offset;
            else
                return this.SelectedIndex < (_tabControl.TabPages.Count - offset);
        }
        private bool GetIsLastEqualStep(int offset)
        {
            if (_totalPages > -1 && _totalPages <= _tabControl.TabPages.Count)
                return this.SelectedIndex <= _totalPages - offset;
            else
                return this.SelectedIndex <= (_tabControl.TabPages.Count - offset);
        }
        private bool GetIsEqualStep(int offset)
        {
            if (_totalPages > -1 && _totalPages <= _tabControl.TabPages.Count)
                return this.SelectedIndex == _totalPages - offset;
            else
                return this.SelectedIndex == (_tabControl.TabPages.Count - offset);
        }
        private bool GetIsGreatEqualStep(int offset)
        {
            if (_totalPages > -1 && _totalPages <= _tabControl.TabPages.Count)
                return this.SelectedIndex >= _totalPages - offset;
            else
                return this.SelectedIndex >= (_tabControl.TabPages.Count - offset);
        }


        protected virtual bool ShouldShowClose()
        {
            bool ret = false;
        
            switch(_CloseMode)// _showClose)
            {
				case ButtonMode.Hide:// McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                switch(wizardType)
                {
                    case WizardType.Install:
                        // Must have at least one page
                        if (_tabControl.SelectedIndex != -1)
                        {
                            if (GetIsGreatEqualStep(1))
                                ret = true;

                            // Cannot 'Close' unless on the last page
                            //if (_tabControl.SelectedIndex == (_tabControl.TabPages.Count - 1))
                            //    ret = true;
                        }
                        break;
                    case WizardType.Configure:
                        break;
                    case WizardType.Controller:
                        break;
                }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldEnableClose()
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

        protected virtual bool ShouldShowFinish()
        {
            bool ret = false;
        
            switch(_FinishMode)// _showFinish)
            {
                case ButtonMode.Hide://McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            if (GetIsEqualStep(2))
                                ret = true;
                            break;
                        case WizardType.Configure:
                            ret = true;
                            break;
                        case WizardType.Controller:
                            break;
                    }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldEnableFinish()
        {
            bool ret = false;
        
            switch(_FinishMode)// _enableFinish)
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

        protected virtual bool ShouldShowNext()
        {
            bool ret = false;

            switch(_NextMode)// _showNext)
            {
                case ButtonMode.Hide://McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            // Must have at least one page
                            if (_tabControl.SelectedIndex != -1)
                            {
                                if (GetIsLastEqualStep(3))
                                    ret = true;

                                //if (ShouldUseTotalPages())
                                //{
                                //    if (_tabControl.SelectedIndex <= TotalPages - 2)
                                //        ret = true;
                                //}

                                //// Cannot 'Next' when at the last or second to last pages
                                //else if (_tabControl.SelectedIndex <= (_tabControl.TabPages.Count - 2))
                                //    ret = true;
                            }
                            break;
                        case WizardType.Configure:
                            ret = true;
                            break;
                        case WizardType.Controller:
                            break;
                    }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldEnableNext()
        {
            bool ret = false;

            switch(_NextMode)// _enableNext)
            {
                case ButtonMode.Hide://McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            // Must have at least one page
                            if (_tabControl.SelectedIndex != -1)
                            {
                                if (GetIsLastEqualStep(2))
                                    ret = true;
                                //if (ShouldUseTotalPages())
                                //{
                                //    if (_tabControl.SelectedIndex <= TotalPages - 2)
                                //        ret = true;
                                //}
                                //// Cannot 'Next' when at the last or second to last pages
                                //else if (_tabControl.SelectedIndex <= (_tabControl.TabPages.Count - 2))
                                //    ret = true;
                            }
                            break;
                        case WizardType.Configure:
                        case WizardType.Controller:
                            // Must have at least one page
                            if (_tabControl.SelectedIndex != -1)
                            {
                                if (GetIsLastStep(1))
                                    ret = true;
                                //if (ShouldUseTotalPages())
                                //{
                                //    if (_tabControl.SelectedIndex < TotalPages - 1)
                                //        ret = true;
                                //}
                                //// Cannot 'Next' when at the last or second to last pages
                                //else if (_tabControl.SelectedIndex < (_tabControl.TabPages.Count - 1))
                                //    ret = true;
                            }
                            break;
                    }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldShowBack()
        {
            bool ret = false;

            switch(_BackMode)// _showBack)
            {
                case ButtonMode.Hide:// McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            if (GetIsLastEqualStep(2))
                                ret = true;

                            //if (ShouldUseTotalPages())
                            //{
                            //    if ((_tabControl.SelectedIndex > 0) && _tabControl.SelectedIndex <= TotalPages - 2)
                            //        ret = true;
                            //}
                            //// Cannot 'Back' when one the first page or on the last two special pages
                            //else if ((_tabControl.SelectedIndex > 0) && (_tabControl.SelectedIndex <= (_tabControl.TabPages.Count - 2)))
                            //    ret = true;
                            break;
                        case WizardType.Configure:
                            ret = true;
                            break;
                        case WizardType.Controller:
                            break;
                    }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldEnableBack()
        {
            bool ret = false;

            switch(_BackMode)// _enableBack)
            {
                case ButtonMode.Disable://McStatus.No:
                    break;
                case ButtonMode.Hide://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    // Cannot 'Back' when one the first page
                    if (_tabControl.SelectedIndex > 0)
                        ret = true;
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldShowCancel()
        {
            bool ret = false;

            switch(_CancelMode)// _showCancel)
            {
                case ButtonMode.Hide://McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            // Must have at least one page
                            if (_tabControl.SelectedIndex != -1)
                            {
                                if (GetIsLastStep(1))
                                    ret = true;
                                //if (ShouldUseTotalPages())
                                //{
                                //    if ( _tabControl.SelectedIndex < TotalPages - 1)
                                //        ret = true;
                                //}
                                //// Cannot 'Cancel' on the last page of an Install
                                //else if (_tabControl.SelectedIndex < (_tabControl.TabPages.Count - 1))
                                //    ret = true;
                            }
                            break;
                        case WizardType.Configure:
                            ret = true;
                            break;
                        case WizardType.Controller:
                            ret = true;
                            break;
                    }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldEnableCancel()
        {
            bool ret = false;

            switch(_CancelMode)// _enableCancel)
            {
                case ButtonMode.Disable://McStatus.No:
                    break;
                case ButtonMode.Hide://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default:// McStatus.Default:
                    ret = true;
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldShowUpdate()
        {
            bool ret = false;

            switch(_UpdateMode)// _showUpdate)
            {
                case ButtonMode.Hide://McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
                    switch(wizardType)
                    {
                        case WizardType.Install:
                            break;
                        case WizardType.Configure:
                            break;
                        case WizardType.Controller:
                            ret = true;
                            break;
                    }
                    break;
            }

            return ret;
        }

        protected virtual bool ShouldEnableUpdate()
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

        protected virtual bool ShouldEnableHelp()
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

        protected virtual bool ShouldShowHelp()
        {
            bool ret = false;

            switch(_HelpMode)// _showHelp)
            {
                case ButtonMode.Hide://McStatus.No:
                    break;
                case ButtonMode.Disable://McStatus.Yes:
                    ret = true;
                    break;
                case ButtonMode.Default://McStatus.Default:
					ret = true;
					break;
            }

            return ret;
        }

		#endregion

		#region internal Events

        protected void LeaveFullPage()
        {
            _panelTop.Show();
            _tabControl.Top = _panelTop.Height;
            _tabControl.Height = _panelBottom.Top - _panelTop.Height - 1;
        }
        
        protected void EnterFullPage()
        {
            //_panelTop.Hide();
            //_tabControl.Top = 0;
            //_tabControl.Height = _panelBottom.Top - 1;
			//_tabControl.Height -= _tabControl.ItemSize.Height - 1;
		}

        protected void OnTabSelectionChanged(object sender, EventArgs e)
        {
            // Update buttons to reflect change
            UpdateControlButtons();
            
            if (_tabControl.SelectedIndex != -1)
            {
                // Get the selected wizard page
                WizardPage wp = _wizardPages[_tabControl.SelectedIndex];
                
                // Should we be presented in full page?
                if (wp.FullPage)
                    EnterFullPage();
                else
                {
                    // Controller profile is not allowed to be outside of FullMode
                    if (wizardType != WizardType.Controller)
                        LeaveFullPage();
                }
            }
            else
            {
                // Controller profile is not allowed to be outside of FullMode
                if (wizardType != WizardType.Controller)
                    LeaveFullPage();
            }
            
            // Update manual drawn text
            _panelTop.Invalidate();
            
            // Generate raw selection changed event
            OnSelectionChanged(EventArgs.Empty);
            
            // Generate page leave event if currently on a valid page
            if (_selectedPage != null)
            {
                OnWizardPageLeave(_selectedPage);
                _selectedPage = null;
            }
            
            // Remember which is the newly seleced page
            if (_tabControl.SelectedIndex != -1)
                _selectedPage = _wizardPages[_tabControl.SelectedIndex] as WizardPage;
            
            // Generate page enter event is now on a valid page
            if (_selectedPage != null)
                OnWizardPageEnter(_selectedPage);
        }

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

        protected void OnButtonFinish(object sender, EventArgs e)
        {
            // Fire event for interested handlers
            OnFinishClick(EventArgs.Empty);
            MoveByFolow(true);
        }

        protected void OnButtonNext(object sender, EventArgs e)
        {
            CancelEventArgs ce = new CancelEventArgs(false);

            // Give handlers chance to cancel this action
            OnNextClick(ce);

            if (!ce.Cancel)
            {
                if (GetIsLastStep(1))
                {
                    //_tabControl.SelectedIndex++;
                    MoveByFolow(true);
                }

                //if (ShouldUseTotalPages())
                //{
                //    if (_tabControl.SelectedIndex < TotalPages - 1)
                //        _tabControl.SelectedIndex++;
                //}
                //// Move to the next page if there is one
                //else if (_tabControl.SelectedIndex < _tabControl.TabPages.Count - 1)
                //    _tabControl.SelectedIndex++;
            }
        }

        protected void OnButtonBack(object sender, EventArgs e)
        {
            CancelEventArgs ce = new CancelEventArgs(false);
            
            // Give handlers chance to cancel this action
            OnBackClick(ce);
            
            if (!ce.Cancel)
            {
                // Move to the next page if there is one
                //if (_tabControl.SelectedIndex > 0)
                //    _tabControl.SelectedIndex--;

                MoveByFolow(false);

            }
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

        protected void OnWizardCleared()
        {
            // Unhook from event handlers for each page
            foreach(WizardPage wp in _tabControl.TabPages)
            {
                wp.FullPageChanged -= new EventHandler(OnWizardFullPageChanged);
                wp.PageTitleChanged -= new EventHandler(OnWizardSubTitleChanged);
                //wp.CaptionTitleChanged -= new EventHandler(OnWizardCaptionTitleChanged);
            }
        
            // Reflect change on underlying tab control
            _tabControl.TabPages.Clear();

            // Update buttons to reflect status
            UpdateControlButtons();
        }
        
        protected void OnWizardInserted(int index, object value)
        {
            WizardPage wp = value as WizardPage;
           
           // Monitor property changes
           wp.FullPageChanged += new EventHandler(OnWizardFullPageChanged);
           wp.PageTitleChanged += new EventHandler(OnWizardSubTitleChanged);
           //wp.CaptionTitleChanged += new EventHandler(OnWizardCaptionTitleChanged);
        
            // Reflect change on underlying tab control
            _tabControl.TabPages.Insert(index, wp);
            //if (!this.Controls.Contains(wp))
            //    this.Controls.Add(wp);

            // Update buttons to reflect status
            UpdateControlButtons();
        }
        
        protected void OnWizardRemoved(int index, object value)
        {
            WizardPage wp = _tabControl.TabPages[index] as WizardPage;
        
            // Unhook from event handlers
            wp.FullPageChanged -= new EventHandler(OnWizardFullPageChanged);
            wp.PageTitleChanged -= new EventHandler(OnWizardSubTitleChanged);
            //wp.CaptionTitleChanged -= new EventHandler(OnWizardCaptionTitleChanged);

            // Reflect change on underlying tab control
            _tabControl.TabPages.RemoveAt(index);
            //if (this.Controls.Contains(wp))
            //    this.Controls.Remove(wp);

            // Update buttons to reflect status
            UpdateControlButtons();
        }
        
        protected void OnWizardFullPageChanged(object sender, EventArgs e)
        {
            WizardPage wp = sender as WizardPage;
            
            // Is it the current page that has changed FullPage?
            if (_tabControl.SelectedIndex == _wizardPages.IndexOf(wp))
            {
                // Should we be presented in full page?
                if (wp.FullPage)
                    EnterFullPage();
                else
                {
                    // Controller profile is not allowed to be outside of FullMode
                    if (wizardType != WizardType.Controller)
                        LeaveFullPage();
                }
            }
        }

        protected void OnWizardSubTitleChanged(object sender, EventArgs e)
        {
            WizardPage wp = sender as WizardPage;
           
            // Is it the current page that has changed sub title?
            if (_tabControl.SelectedIndex == _wizardPages.IndexOf(wp))
            {
                // Force the sub title to be updated now
                _panelTop.Invalidate();
            }
        }        
        
        protected void OnWizardCaptionTitleChanged(object sender, EventArgs e)
        {
            // Generate event so any dialog containing use can be notify
            if (McCaptionTitleChanged != null)
                McCaptionTitleChanged(this, e);
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

        protected override void OnImageIndexChanged(EventArgs e)
        {
            base.OnImageIndexChanged(e);
            _panelTop.Invalidate();
        }

        protected void OnPaintTopPanel(object sender, PaintEventArgs pe)
        {
			Rectangle rect =this._panelTop.ClientRectangle;
			float gradiaentAngle=(float)this.gardientStyle;

			Brush brush=null;
			switch(this.ControlLayout)
			{
                //case ControlLayout.Flat:
                //    brush=base.LayoutManager.GetBrushFlat();
                //    break;
				case ControlLayout.Visual:
				case ControlLayout.XpLayout:
                case ControlLayout.VistaLayout:
                    brush = base.LayoutManager.GetBrushGradient(rect, gradiaentAngle);
					break;
				default:// case ControlLayout.System:
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
                if (this.ImageIndex > -1 && this.ImageList != null)
				{
                    if (ImageIndex < ImageList.Images.Count)
					{
                        image = ImageList.Images[ImageIndex]; 
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
            if (_tabControl.SelectedIndex != -1)
            {                
                // Adjust rectangle for rest of the drawing text space
                drawRectF.Y = drawRectF.Bottom + (_panelGap / 2);
                drawRectF.X += _panelGap;
                drawRectF.Width -= _panelGap;
                drawRectF.Height = _panelTop.Height - drawRectF.Y - (_panelGap / 2);

                // No longer want to prevent word wrap to extra lines
                drawFormat.LineAlignment = StringAlignment.Near;
                drawFormat.FormatFlags = StringFormatFlags.NoClip;

                WizardPage wp = _tabControl.TabPages[_tabControl.SelectedIndex] as WizardPage;

                using(SolidBrush subTitleBrush = new SolidBrush(_colorSubTitle))
                    pe.Graphics.DrawString(wp.Text, _fontSubTitle, subTitleBrush, drawRectF, drawFormat);
            }                          
        
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
//		public virtual IStyleLayout LayoutManager
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
//				foreach(Control ctl in this.Controls )
//				{
//					if(ctl is IStyleCtl )
//					{
//						((IStyleCtl)ctl).SetStyleLayout(value); 
//					}
//				}
//				_panelTop.Invalidate();
//				_panelBottom.Invalidate();
//				Invalidate();
//			}
//		}
//
//		public virtual void SetStyleLayout(Styles value)
//		{
//			if(this.m_StylePainter!=null)
//			{
//				m_StylePainter.Layout.SetStyleLayout(value);
//				foreach(Control ctl in this.Controls )
//				{
//					if(ctl is IStyleCtl )
//					{
//						((IStyleCtl)ctl).SetStyleLayout(value); 
//					}
//				}
//				_panelTop.Invalidate();
//				_panelBottom.Invalidate();
//				Invalidate();
//			}
//		}
//
//		protected virtual void OnStylePainterChanged(EventArgs e)
//		{
//			foreach(Control ctl in this.Controls )
//			{
//				if(ctl is IStyleCtl )
//				{
//					((IStyleCtl)ctl).StylePainter=m_StylePainter; 
//				}
//			}
//			this._panelBottom.StylePainter=this.m_StylePainter;
//			Invalidate();
//
//		}
//
		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
			{
				this.Invalidate(true);
			}
		}
//
//		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//
//			OnStylePropertyChanged(e);
//		}

		protected override void SetChildrenStyle(bool clear)
		{
			base.SetChildrenStyle(clear);
			this._tabControl.SetStyleLayout(this.LayoutManager.Layout);
			this._panelBottom.SetStyleLayout(this.LayoutManager.Layout);

			this.Invalidate(true);
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged(e);
			this._tabControl.StylePainter=this.m_StylePainter;
			this._panelBottom.StylePainter=this.m_StylePainter;
			Invalidate();

		}

		#endregion

    }
}
