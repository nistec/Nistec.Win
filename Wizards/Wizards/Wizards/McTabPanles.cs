using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

using Nistec.Collections;
using Nistec.Drawing;
using Nistec.WinForms;
using Nistec.WinForms.Design;
using Nistec.WinForms.Controls;

namespace Nistec.Wizards
{

 
    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(McTabPanels), "Toolbox.McTabPanels.bmp")]
    [DefaultProperty("WizardPanelType")]
    [Designer(typeof(Design.McTabPanelDesigner))]//.McPanelDesigner))]
    public class McTabPanels : Nistec.WinForms.Controls.McContainer//, IMcWizard
    {

        #region Members
        // Class wide constants
        protected const int _panelGap = 10;
        protected const int _buttonGap = 4;// 10;

        // Instance fields
        //private int _imageIndex;
        //private ImageList _imageList;

        private WizardPanelType wizardType;
        private GradientStyle gardientStyle;
        //private ControlLayout m_ControlLayout;

        protected McTabPage _selectedPage;
        //protected WizardPageCollection _wizardPages;

        protected ButtonMode _UpdateMode;
        protected ButtonMode _CancelMode;
        protected ButtonMode _HelpMode;

        protected Nistec.WinForms.McButtonMenu _buttonUpdate;
        protected Nistec.WinForms.McButton _buttonCancel;
        protected Nistec.WinForms.McButton _buttonHelp;
        protected Nistec.WinForms.McLabel _lbl;


        // Instance designer fields
        protected Nistec.WinForms.McCaption _panelTop;
        protected Nistec.WinForms.McPanel _panelBottom;
        protected Nistec.WinForms.McTabControl _tabControl;

        private Nistec.WinForms.McPanel panelBase;
        private Nistec.WinForms.McPanel panelTabs;
        internal Nistec.WinForms.McSplitter splitter1;
        private Nistec.WinForms.McPanel panelList;
        private Nistec.WinForms.McListBox listBox1;

        private System.ComponentModel.IContainer components = null;
        private Form form;
        #endregion

        #region Events

        // Instance events
        public event EventHandler SelectedPageChanged;
        public event SelectedPopUpItemEventHandler SelectedButtonMenuClicked;
        //public event McPanelHandler WizardPageEnter;
        //public event McPanelHandler WizardPageLeave;
        //public event EventHandler McCaptionTitleChanged;
        public event EventHandler SelectionItemChanged;
        public event EventHandler UpdateClick;
        public event EventHandler CancelClick;
        public event EventHandler HelpClick;

        #endregion

        #region Constructor

        public McTabPanels()
        {
 
            this.wizardType = WizardPanelType.Configure;


            // No page currently selected
            _selectedPage = null;

            // Hook into tab control events
            //_tabControl.SelectedIndexChanged += new EventHandler(OnTabSelectionChanged);


            _UpdateMode = ButtonMode.Default;
            _CancelMode = ButtonMode.Default;
            _HelpMode = ButtonMode.Default;
            gardientStyle = GradientStyle.BottomToTop;
            //m_ControlLayout = ControlLayout.Visual;

            //			_showPicture=true;
            //_imageIndex = -1;
            //_imageList = null;
            InitializeComponent();
            this.tabControl.TabPages.Inserted += new CollectionChange(TabPages_Inserted);
            this.tabControl.TabPages.Removed += new CollectionChange(TabPages_Removed);
            this.tabControl.TabPages.Cleared += new CollectionClear(TabPages_Cleared);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McTabPanels));
            this._panelBottom = new Nistec.WinForms.McPanel();
            this._buttonUpdate = new Nistec.WinForms.McButtonMenu();
            this._buttonCancel = new Nistec.WinForms.McButton();
            this._buttonHelp = new Nistec.WinForms.McButton();
            this._panelTop = new Nistec.WinForms.McCaption();
            this._tabControl = new Nistec.WinForms.McTabControl();
            this.panelBase = new Nistec.WinForms.McPanel();
            this.panelTabs = new Nistec.WinForms.McPanel();
            this.splitter1 = new Nistec.WinForms.McSplitter();
            this.panelList = new Nistec.WinForms.McPanel();
            this.listBox1 = new Nistec.WinForms.McListBox();
            this._lbl = new Nistec.WinForms.McLabel();
            this._panelBottom.SuspendLayout();
            this.panelBase.SuspendLayout();
            this.panelTabs.SuspendLayout();
            this.panelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelBottom
            // 
            this._panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelBottom.Controls.Add(this._buttonUpdate);
            this._panelBottom.Controls.Add(this._buttonCancel);
            this._panelBottom.Controls.Add(this._buttonHelp);
            this._panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelBottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panelBottom.Location = new System.Drawing.Point(2, 178);
            this._panelBottom.Name = "_panelBottom";
            this._panelBottom.Size = new System.Drawing.Size(260, 44);
            this._panelBottom.TabIndex = 3;
            this._panelBottom.Text = "statusBar1";
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonUpdate.ButtonMenuStyle = Nistec.WinForms.ButtonMenuStyles.Media;
            this._buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
            this._buttonUpdate.FixSize = false;
            this._buttonUpdate.Location = new System.Drawing.Point(31, 6);
            this._buttonUpdate.Name = "_buttonUpdate";
            this._buttonUpdate.Size = new System.Drawing.Size(70, 28);
            this._buttonUpdate.TabIndex = 4;
            this._buttonUpdate.Text = "Update";
            this._buttonUpdate.ToolTipItems = null;
            this._buttonUpdate.ToolTipText = "Update";
            this._buttonUpdate.ButtonClick += new System.EventHandler(this.OnButtonUpdate);
            this._buttonUpdate.SelectedItemClick += new Nistec.WinForms.SelectedPopUpItemEventHandler(this._buttonUpdate_SelectedItemClick);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this._buttonCancel.Location = new System.Drawing.Point(111, 6);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(70, 28);
            this._buttonCancel.TabIndex = 1;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.ToolTipText = "Cancel";
            this._buttonCancel.Click += new System.EventHandler(this.OnButtonCancel);
            // 
            // _buttonHelp
            // 
            this._buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonHelp.DialogResult = System.Windows.Forms.DialogResult.None;
            this._buttonHelp.Location = new System.Drawing.Point(187, 6);
            this._buttonHelp.Name = "_buttonHelp";
            this._buttonHelp.Size = new System.Drawing.Size(70, 28);
            this._buttonHelp.TabIndex = 0;
            this._buttonHelp.Text = "Help";
            this._buttonHelp.ToolTipText = "Help";
            this._buttonHelp.Click += new System.EventHandler(this.OnButtonHelp);
            // 
            // _panelTop
            // 
            this._panelTop.BackColor = System.Drawing.Color.Silver;
            this._panelTop.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this._panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panelTop.Image = ((System.Drawing.Image)(resources.GetObject("_panelTop.Image")));
            //this._panelTop.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._panelTop.Location = new System.Drawing.Point(2, 2);
            this._panelTop.Name = "_panelTop";
            //this._panelTop.SubText = "";
            //this._panelTop.Text = "Caption Control";
            // 
            // _tabControl
            // 
            //this._tabControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.Size = new System.Drawing.Size(136, 112);
            this._tabControl.TabIndex = 0;
            this._tabControl.HideTabs=true;//.TabPosition = Nistec.WinForms.TabPosition.Hide;
            this._tabControl.SelectedIndexChanged += new System.EventHandler(this._tabControl_SelectedIndexChanged);
            // 
            // panelBase
            // 
            this.panelBase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelBase.Controls.Add(this.panelTabs);
            this.panelBase.Controls.Add(this.splitter1);
            this.panelBase.Controls.Add(this.panelList);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelBase.Location = new System.Drawing.Point(2, 62);
            this.panelBase.Name = "panelBase";
            this.panelBase.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelBase.Size = new System.Drawing.Size(260, 116);
            this.panelBase.TabIndex = 5;
            // 
            // panelTabs
            // 
            this.panelTabs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelTabs.Controls.Add(this._tabControl);
            this.panelTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelTabs.Location = new System.Drawing.Point(124, 2);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(136, 112);
            this.panelTabs.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.splitter1.Location = new System.Drawing.Point(120, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 112);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panelList
            // 
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelList.Controls.Add(this.listBox1);
            this.panelList.Controls.Add(this._lbl);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelList.Location = new System.Drawing.Point(0, 2);
            this.panelList.Name = "panelList";
            this.panelList.Padding = new System.Windows.Forms.Padding(2);
            this.panelList.Size = new System.Drawing.Size(120, 112);
            this.panelList.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(2, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.ReadOnly = false;
            this.listBox1.Size = new System.Drawing.Size(116, 88);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // _lbl
            // 
            this._lbl.BackColor = System.Drawing.SystemColors.Control;
            this._lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this._lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lbl.Location = new System.Drawing.Point(2, 2);
            this._lbl.Name = "_lbl";
            this._lbl.Size = new System.Drawing.Size(116, 20);
            // 
            // McTabPanels
            // 
            this.Controls.Add(this.panelBase);
            this.Controls.Add(this._panelBottom);
            this.Controls.Add(this._panelTop);
            this.Name = "McTabPanels";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(264, 224);
            this._panelBottom.ResumeLayout(false);
            this.panelBase.ResumeLayout(false);
            this.panelTabs.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void TabPages_Inserted(int index, object value)
        {
            //OnPageCollectionChanged();
            if (!DesignMode)
                return;
            McTabPage wp = value as McTabPage;
            if (index > this.listBox1.ListItems.Count - 1)
                this.listBox1.ListItems.Add(wp.Text, wp.ImageIndex);
            else
                this.listBox1.ListItems.Insert(index, new ListItem(wp.Text, wp.ImageIndex));
            //if (!this.listBox1.Items.Contains(wp.Text))
            //    this.listBox1.Items.Add(wp.Text);

            //if (!this.Controls.Contains(wp))
            //    this.Controls.Add(wp);
        }
        void TabPages_Removed(int index, object value)
        {
            //OnPageCollectionChanged();
            if (!DesignMode)
                return;

            McTabPage wp = value as McTabPage;

            this.listBox1.ListItems.RemoveAt(index);

            //if (this.listBox1.Items.Contains(wp.Text))
            //    this.listBox1.Items.Remove(wp.Text);
 
            //if (this.Controls.Contains(wp))
            //    this.Controls.Remove(wp);
        }

        void TabPages_Cleared()
        {
            if (!DesignMode)
                return;
            this.listBox1.ListItems.Clear();
            //this.listBox1.Items.Clear();
        }

        public void AddPage(string text,int imageIndex)
        {
            this.WizardPages.Add(new McTabPage(text, imageIndex));
            this.listBox1.ListItems.Add(text, imageIndex);
            //OnPageCollectionChanged();
        }

        #endregion

        #region Hide Properties

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new BorderStyle BorderStyle
        //{
        //    get { return base.BorderStyle; }
        //    set { base.BorderStyle = value; }
        //}

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new string DefaultValue
        //{
        //    get { return base.DefaultValue; }
        //    set { base.DefaultValue = value; }
        //}

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new bool ReadOnly
        //{
        //    get { return base.ReadOnly; }
        //    set { base.ReadOnly = value; }
        //}

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new bool FixSize
        //{
        //    get { return base.FixSize; }
        //    set { base.FixSize = value; }
        //}

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new BindingFormat BindFormat
        //{
        //    get { return base.BindFormat; }
        //    set { base.BindFormat = value; }
        //}

        #endregion

        #region Properties

        [Category("Wizard"), DefaultValue(WizardPanelType.Configure)]
        public WizardPanelType WizardPanelType
        {
            get { return wizardType; }
            set
            {
                if (wizardType != value)
                {
                    wizardType = value;
                    if (value == WizardPanelType.Configure)
                    {
                        this.listBox1.ListItems.Clear();
                    }
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
                    this._panelTop.GradientStyle = value;
                    //this._panelBottom.GradientStyle = value;
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
                    this._panelTop.ControlLayout = value;
                    this._panelBottom.ControlLayout = value;
                    this._lbl.ControlLayout = value;

                    ControlLayout val = value;
                    if (value == ControlLayout.Flat || value == ControlLayout.System)
                    {
                        val = ControlLayout.XpLayout;
                    }
                    this._buttonCancel.ControlLayout = val;
                    this._buttonHelp.ControlLayout = val;
                    this._buttonUpdate.ControlLayout = val;

                    this.Invalidate(true);
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
        //public int ImageIndex
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
        //    }
        //}

        protected override void OnImageIndexChanged(EventArgs e)
        {
            base.OnImageIndexChanged(e);
            this.Invalidate();
        }

        [Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        internal McListBox List
        {
            get { return this.listBox1; }
        }

        [Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Access to underlying McTabControl instance")]
        internal McTabControl tabControl
        {
            get { return _tabControl; }
        }

        [Category("Wizard")]
        [Description("Access to underlying header panel")]
        internal Nistec.WinForms.McCaption Caption//HeaderPanel
        {
            get { return _panelTop; }
        }

        [Category("Wizard")]
        [Description("Access to underlying trailer panel")]
        internal Nistec.WinForms.McPanel ButtonsPanel//TrailerPanel
        {
            get { return _panelBottom; }
        }

        [Category("Wizard")]
        [Description("Collection of wizard pages")]
        [Browsable(false), EditorBrowsable( EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TabPageCollection WizardPages
        {
            get { return this._tabControl.TabPages; }
        }

        //[Category("Wizard"), Description("ListBoxItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        //public ListBox.ObjectCollection Items
        //{
        //    get
        //    {
        //        return this.listBox1.Items;
        //    }
        //}

        [Category("Wizard"), DefaultValue(16)]
        [Description("List Item Height")]
        public int ItemHeight
        {
            get { return this.listBox1.ItemHeight; }

            set
            {
                this.listBox1.ItemHeight = value;
                this.listBox1.Invalidate();
            }
        }

        [Category("Wizard"), DefaultValue(true)]
        [Description("Show Main title Picture")]
        public bool ShowImage
        {
            get { return _panelTop.ShowImage; }

            set
            {
                _panelTop.ShowImage = value;
                //_panelTop.Invalidate();
            }
        }

        [Category("Wizard"), DefaultValue(true)]
        [Description("Show Caption")]
        public bool ShowCaption
        {
            get { return _panelTop.Visible; }

            set
            {
                _panelTop.Visible = value;
                this.Invalidate();
            }
        }

        [Category("Wizard"), DefaultValue(true)]
        [Description("Show Bottom panel")]
        public bool ShowBottom
        {
            get { return _panelBottom.Visible; }

            set
            {
                _panelBottom.Visible = value;
                this.Invalidate();
            }
        }

        [Category("Wizard"),DefaultValue("")]
        [Description("Main title Sub Text")]
        public string CaptionSubText
        {
            get { return _panelTop.SubText; }

            set
            {
                _panelTop.SubText = value;
               // _panelTop.Invalidate();
            }
        }

        [Category("Wizard")]
        [Description("Main title Picture")]
        public Image CaptionImage
        {
            get { return _panelTop.Image; }

            set
            {
                _panelTop.Image = value;
                //_panelTop.Invalidate();
            }
        }

        [Category("Wizard")]
        [Description("Font for drawing main title text")]
        public Font CaptionFont
        {
            get
            {

                return _panelTop.TitleFont;
            }

            set
            {
                if (value != null)
                {
                    _panelTop.TitleFont = value;
                    //_panelTop.Invalidate();
                }
            }
        }

        [Category("Wizard")]
        [Description("List width")]
        public int ListWidth
        {
            get { return this.panelList.Width; }

            set
            {
                this.panelList.Width = value;
            }
        }

        [Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("currently selected WizardPage")]
        public McTabPage SelectedPage
        {
            get { return _tabControl.SelectedTab; }
            set { _tabControl.SelectedTab = value; }
        }

        [Category("Wizard")]
        [Description("Index of currently selected WizardPage")]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return _tabControl.SelectedIndex; }
            set { _tabControl.SelectedIndex = value; }
        }

        [Category("Wizard"), DefaultValue("Update")]
        [Description("Button update text")]
        public string ButttonUpdateText
        {
            get { return this._buttonUpdate.Text; }
            set { this._buttonUpdate.Text = value; }
        }

        [Category("Wizard"), DefaultValue("Cancel")]
        [Description("Button Cancel text")]
        public string ButttonCancelText
        {
            get { return this._buttonCancel.Text; }
            set { this._buttonCancel.Text = value; }
        }

        [Category("Wizard"), DefaultValue("Help")]
        [Description("Button Help text")]
        public string ButttonHelpText
        {
            get { return this._buttonHelp.Text; }
            set { this._buttonHelp.Text = value; }
        }

        [Category("Wizard"), DefaultValue(true)]
        [Description("Button update Visible")]
        public bool ButttonUpdateVisible
        {
            get { return this._buttonUpdate.Visible; }
            set 
            { 
                this._buttonUpdate.Visible = value;
                UpdateControlButtons();
            }
        }

        [Category("Wizard"), DefaultValue(true)]
        [Description("Button Cancel Visible")]
        public bool ButttonCancelVisible
        {
            get { return this._buttonCancel.Visible; }
            set 
            { 
                this._buttonCancel.Visible = value;
                UpdateControlButtons();
            }
        }

        [Category("Wizard"), DefaultValue(true)]
        [Description("Button Help Visible")]
        public bool ButttonHelpVisible
        {
            get { return this._buttonHelp.Visible; }
            set 
            {
                this._buttonHelp.Visible = value;
                UpdateControlButtons();
            }
        }

        [Category("Wizard"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public PopUpItemsCollection ButtonMenuItems
        {
            get
            {
                return this._buttonUpdate.MenuItems;
            }
        }

        [Category("Wizard"),DefaultValue(null)]
        public ImageList ButtonImageList
        {
            get
            {
                return _buttonUpdate.ImageList;
            }
            set
            {
                _buttonUpdate.ImageList = value;
            }
        }

        [Category("Wizard"), DefaultValue(ButtonComboType.Combo), Localizable(true)]
        public ButtonComboType ButtonComboType
        {
            get { return _buttonUpdate.ButtonComboType; }
            set
            {
                _buttonUpdate.ButtonComboType = value;
                this.Invalidate();
            }
        }

        [Category("Wizard"), Browsable(true), DefaultValue(ButtonMenuStyles.Media)]
        public ButtonMenuStyles ButtonMenuStyle
        {
            get { return _buttonUpdate.ButtonMenuStyle; }
            set
            {
                _buttonUpdate.ButtonMenuStyle = value;
            }
        }

        [Category("Wizard"),DefaultValue("")]
        [Description("Caption List Text")]
        public string ListCaption
        {
            get { return this._lbl.Text; }
            set { this._lbl.Text = value; }
        }
        #endregion

        #region Override

        protected override void OnRightToLeftChanged(System.EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            if (RightToLeft == RightToLeft.Yes)
            {
                this.panelList.Dock = DockStyle.Right;
                this.splitter1.Dock = DockStyle.Right;
                this.splitter1.Width = 4;

            }
            else
            {
                this.panelList.Dock = DockStyle.Left;
                this.splitter1.Dock = DockStyle.Left;
                this.splitter1.Width = 4;
            }
        }

        protected override void OnHandleCreated(System.EventArgs e)
        {
            base.OnHandleCreated(e);
            //this.panelList.BackColor=this.listBox1.BackColor;
            OnPageCollectionChanged();
         }


        protected void OnPageCollectionChanged()
        {
            if (this.wizardType == WizardPanelType.Controller)
            {
                return;
            }
            if (this.DesignMode) return;
            
            this.form = this.FindForm();
            //this.listBox1.Items.Clear();
            this.listBox1.ListItems.Clear();
            this.listBox1.ImageList = this.ImageList;

            foreach (McTabPage wp in this.WizardPages)
            {
                if (wp.PageVisible)
                {
                    //this.listBox1.Items.Add(wp.Text);
                    this.listBox1.ListItems.Add(wp.Text, wp.ImageIndex);
                }
            }

            if (this.listBox1.Items.Count > 0)
            {
                this._tabControl.SelectedIndex = 0;
                if (_tabControl.SelectedIndex != -1)
                {
                    // Get the selected wizard page
                    McTabPage wp = WizardPages[_tabControl.SelectedIndex];
                    this._panelTop.SetCaptionText(wp.Text, (string)wp.Tag, true);

                }
                else
                {
                    this._panelTop.SetCaptionText("", "", true);
                }
                this.listBox1.SelectedIndex = 0;
            }
        }

        private void OnButtonHelp(object sender, EventArgs e)
        {
            OnHelpClick(EventArgs.Empty);
            if (HelpClick != null)
                HelpClick(this, e);
        }

        private void OnButtonCancel(object sender, EventArgs e)
        {
            OnCancelClick(EventArgs.Empty);
            if (CancelClick != null)
                CancelClick(this, e);
        }

        private void OnButtonUpdate(object sender, EventArgs e)
        {
            OnUpdateClick(EventArgs.Empty);
            if (UpdateClick != null)
                UpdateClick(this, e);
        }

        protected virtual void OnCancelClick(EventArgs e)
        {
            if (this.form != null)
            {
                this.form.Close();
            }
        }

        protected virtual void OnUpdateClick(EventArgs e)
        {
        }

        protected virtual void OnHelpClick(EventArgs e)
        {
        }

        protected void OnSelectedButtonMenuClicked(SelectedPopUpItemEvent e)
        {

        }

        protected void OnSelectionItemChanged(EventArgs e)
        {
            if (this.SelectionItemChanged != null)
                this.SelectionItemChanged(this, e);
        }


        protected void OnTabSelectionChanged(EventArgs e)
        {

            if (_tabControl.SelectedIndex != -1)
            {
                // Get the selected wizard page
                McTabPage wp = WizardPages[_tabControl.SelectedIndex];
                this._panelTop.SetCaptionText(wp.Text, (string)wp.Tag, true);

            }
            else
            {
                this._panelTop.SetCaptionText("", "", true);
            }

            // Update manual drawn text
            //_panelTop.Invalidate();

            // Generate raw selection changed event
            //			OnSelectionChanged(EventArgs.Empty);
            //            
            //			// Generate page leave event if currently on a valid page
            //			if (_selectedPage != null)
            //			{
            //				OnWizardPageLeave(_selectedPage);
            //				_selectedPage = null;
            //			}

            // Remember which is the newly seleced page
            if (_tabControl.SelectedIndex != -1)
                _selectedPage = WizardPages[_tabControl.SelectedIndex] as McTabPage;

            if (SelectedPageChanged != null)
                SelectedPageChanged(this, e);
            // Generate page enter event is now on a valid page
            //			if (_selectedPage != null)
            //				OnWizardPageEnter(_selectedPage);
        }


        //protected override void OnResize(EventArgs e)
        //{
        //    this.PerformLayout();
        //}

        protected void OnRepaintPanels(object sender, EventArgs e)
        {
            //_panelTop.Invalidate();
            _panelBottom.Invalidate();
        }


        protected void OnPaintBottomPanel(object sender, PaintEventArgs pe)
        {
            using (Pen lightPen = LayoutManager.GetPenBorder())
            {
                pe.Graphics.DrawRectangle(lightPen, 0, _panelBottom.Top, _panelBottom.Width - 1, _panelBottom.Height - 1);

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.wizardType == WizardPanelType.Configure)
            {
                this._tabControl.SelectedIndex = this.listBox1.SelectedIndex;
            }
            //else
            //{
            OnSelectionItemChanged(e);
            //}

        }

        private void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTabSelectionChanged(e);
        }


        private void _buttonUpdate_SelectedItemClick(object sender, SelectedPopUpItemEvent e)
        {
            this.OnSelectedButtonMenuClicked(e);
            if (this.SelectedButtonMenuClicked != null)
                this.SelectedButtonMenuClicked(this, e);
        }
        #endregion

        #region IStyleCtl

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if ((DesignMode || IsHandleCreated))
            {
                this.Invalidate(true);
            }
        }

        protected override void SetChildrenStyle(bool clear)
        {
            base.SetChildrenStyle(clear);
            this.panelTabs.SetStyleLayout(this.LayoutManager.Layout);
            this._tabControl.SetStyleLayout(this.LayoutManager.Layout);
            this._panelBottom.SetStyleLayout(this.LayoutManager.Layout);
            this._panelTop.SetStyleLayout(this.LayoutManager.Layout);
            this.panelList.SetStyleLayout(this.LayoutManager.Layout);

            this.Invalidate(true);
        }

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            this.panelTabs.StylePainter = this.m_StylePainter;
            this._tabControl.StylePainter = this.m_StylePainter;
            this._panelTop.StylePainter = this.m_StylePainter;
            this._panelBottom.StylePainter = this.m_StylePainter;
            this.panelList.StylePainter = this.m_StylePainter;
            Invalidate();

        }

        #endregion

        protected void UpdateControlButtons()
        {
            // Track next button inserted position
            int x = this.Width - 10 - _buttonHelp.Width;

            bool showHelp = this.ButttonHelpVisible;
            bool showCancel = this.ButttonCancelVisible;
            bool showUpdate = this.ButttonUpdateVisible;

            if (showHelp)
            {
                _buttonHelp.Left = x;
                x -= _buttonHelp.Width + _buttonGap;
                //_buttonHelp.Enabled = ShouldEnableHelp();
                _buttonHelp.Show();
            }
            else
                _buttonHelp.Hide();

            if (showCancel)
            {
                _buttonCancel.Left = x;
                x -= _buttonCancel.Width + _buttonGap;
                //_buttonCancel.Enabled = ShouldEnableCancel();
                _buttonCancel.Show();
            }
            else
                _buttonCancel.Hide();

            if (showUpdate)
            {
                _buttonUpdate.Left = x;
                x -= _buttonUpdate.Width + _buttonGap;
                //_buttonUpdate.Enabled = ShouldEnableUpdate();
                _buttonUpdate.Show();
            }
            else
                _buttonUpdate.Hide();

            //AutoAssignDefaultButton();
        }
 
    }
}
