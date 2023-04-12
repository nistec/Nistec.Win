using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Collections.Generic;

using Nistec.Collections;
using Nistec.Drawing;
using Nistec.WinForms;
using Nistec.WinForms.Design;

namespace Nistec.Wizards
{


	/// <summary>
    /// Summary description for McTabTree.
	/// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(McManagment), "Toolbox.McManagment.bmp")]
	[DefaultProperty("WizardPanelType")]
	[Designer(typeof(Design.McManagmentDesigner))]
	public class McManagment : Nistec.WinForms.Controls.McContainer
	{

		#region Members
		// Class wide constants
		protected const int _panelGap = 10;
		protected const int _buttonGap =4;// 10;
	
		// Instance fields
        //private int _imageIndex;
        //private ImageList _imageList;

		private GradientStyle gardientStyle;
		//private ControlLayout	m_ControlLayout;
  
		protected McTabPage selectedPage;
	
	    
		// Instance designer fields
        protected Nistec.WinForms.McCaption topCaption;
        protected McToolBar toolBar;
        //protected McToolBarContainer toolBarContainer;
        protected McStatusBar statusBar;
        private McPanel panelBase;
        private McPanel panelTabs;
        protected McTabControl tabPages;
        internal McSplitter splitter;
        private McPanel panelList;
        private McTreeView treeView;
        protected McLabel lblList;
        private StyleGuide styleGuide;
        //public McToolButton tbOK;
        //public McToolButton tbHelp;
        //public McToolButton tbClose;
        //public McToolButton tbOption;

        public event ToolButtonClickEventHandler ToolButtonClick;

		private System.ComponentModel.IContainer components=null;

		#endregion
 
		#region Events

		// Instance events
		public event TreeViewEventHandler SelectionNodeChanged;
		public event EventHandler SelectedPageChanged;

		#endregion

		#region Constructor

        public McManagment()
        {

            selectedPage = null;

            //_imageIndex = -1;
            //_imageList = null;
            gardientStyle = GradientStyle.BottomToTop;
            //m_ControlLayout = ControlLayout.Visual;

            InitializeComponent();

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
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McManagment));
            this.topCaption = new Nistec.WinForms.McCaption();
            this.styleGuide = new Nistec.WinForms.StyleGuide(this.components);
            this.toolBar = new Nistec.WinForms.McToolBar();
            //this.toolBarContainer = new Nistec.WinForms.McToolBarContainer();
            //this.tbHelp = new Nistec.WinForms.McToolButton();
            //this.tbOption = new Nistec.WinForms.McToolButton();
            //this.tbOK = new Nistec.WinForms.McToolButton();
            //this.tbClose = new Nistec.WinForms.McToolButton();
            this.statusBar = new Nistec.WinForms.McStatusBar();
            this.panelBase = new Nistec.WinForms.McPanel();
            this.panelTabs = new Nistec.WinForms.McPanel();
            this.tabPages = new Nistec.WinForms.McTabControl();
            this.splitter = new Nistec.WinForms.McSplitter();
            this.panelList = new Nistec.WinForms.McPanel();
            this.treeView = new Nistec.WinForms.McTreeView();
            this.lblList = new Nistec.WinForms.McLabel();
            this.toolBar.SuspendLayout();
            this.panelBase.SuspendLayout();
            this.panelTabs.SuspendLayout();
            this.panelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // topCaption
            // 
            this.topCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.topCaption.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.topCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.topCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.topCaption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.topCaption.Image = ((System.Drawing.Image)(resources.GetObject("topCaption.Image")));
            this.topCaption.Location = new System.Drawing.Point(0, 0);
            this.topCaption.Name = "topCaption";
            this.topCaption.SubText = "";
            this.topCaption.Text = "Nistec Explorer Wizard";
            // 
            // styleGuide
            // 
            this.styleGuide.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.styleGuide.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.styleGuide.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.styleGuide.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.styleGuide.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGuide.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.styleGuide.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.styleGuide.ColorBrushLower = System.Drawing.Color.LightSlateGray;
            this.styleGuide.ColorBrushUpper = System.Drawing.Color.WhiteSmoke;
            this.styleGuide.DisableColor = System.Drawing.Color.DarkGray;
            this.styleGuide.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.styleGuide.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.styleGuide.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.styleGuide.StylePlan = Nistec.WinForms.Styles.Desktop;
            this.styleGuide.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            //this.toolBar.Controls.Add(this.tbHelp);
            //this.toolBar.Controls.Add(this.tbOption);
            //this.toolBar.Controls.Add(this.tbOK);
            //this.toolBar.Controls.Add(this.tbClose);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBar.FixSize = false;
            this.toolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.toolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBar.Location = new System.Drawing.Point(0, 48);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.toolBar.Size = new System.Drawing.Size(253, 26);
            this.toolBar.StylePainter = this.styleGuide;
            this.toolBar.TabIndex = 8;
            this.toolBar.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.toolBar_ButtonClick);
            //// 
            //// tbHelp
            //// 
            //this.tbHelp.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            //this.tbHelp.DialogResult = System.Windows.Forms.DialogResult.None;
            //this.tbHelp.Dock = System.Windows.Forms.DockStyle.Left;
            //this.tbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tbHelp.Image")));
            //this.tbHelp.Location = new System.Drawing.Point(96, 3);
            //this.tbHelp.Name = "tbHelp";
            //this.tbHelp.ParentBar = this.toolBar;
            //this.tbHelp.Size = new System.Drawing.Size(22, 20);
            //this.tbHelp.StylePainter = this.styleGuide;
            //this.tbHelp.TabIndex = 4;
            //this.tbHelp.ToolTipText = "Help";
            //// 
            //// tbOption
            //// 
            //this.tbOption.ButtonStyle = Nistec.WinForms.ToolButtonStyle.DropDownButton;
            //this.tbOption.DialogResult = System.Windows.Forms.DialogResult.None;
            //this.tbOption.Dock = System.Windows.Forms.DockStyle.Left;
            //this.tbOption.Image = ((System.Drawing.Image)(resources.GetObject("tbOption.Image")));
            //this.tbOption.Location = new System.Drawing.Point(56, 3);
            //this.tbOption.Name = "tbOption";
            //this.tbOption.ParentBar = this.toolBar;
            //this.tbOption.Size = new System.Drawing.Size(40, 20);
            //this.tbOption.TabIndex = 6;
            //this.tbOption.ToolTipText = "Option";
            //// 
            //// tbOK
            //// 
            //this.tbOK.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            //this.tbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            //this.tbOK.Dock = System.Windows.Forms.DockStyle.Left;
            //this.tbOK.Image = ((System.Drawing.Image)(resources.GetObject("tbOK.Image")));
            //this.tbOK.Location = new System.Drawing.Point(34, 3);
            //this.tbOK.Name = "tbOK";
            //this.tbOK.ParentBar = this.toolBar;
            //this.tbOK.Size = new System.Drawing.Size(22, 20);
            //this.tbOK.StylePainter = this.styleGuide;
            //this.tbOK.TabIndex = 5;
            //this.tbOK.ToolTipText = "OK";
            //// 
            //// tbClose
            //// 
            //this.tbClose.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            //this.tbClose.DialogResult = System.Windows.Forms.DialogResult.None;
            //this.tbClose.Dock = System.Windows.Forms.DockStyle.Left;
            //this.tbClose.Image = ((System.Drawing.Image)(resources.GetObject("tbClose.Image")));
            //this.tbClose.Location = new System.Drawing.Point(12, 3);
            //this.tbClose.Name = "tbClose";
            //this.tbClose.ParentBar = this.toolBar;
            //this.tbClose.Size = new System.Drawing.Size(22, 20);
            //this.tbClose.StylePainter = this.styleGuide;
            //this.tbClose.TabIndex = 3;
            //this.tbClose.ToolTipText = "Close";
            // 
            // statusBar
            // 
            this.statusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.statusBar.ForeColor = System.Drawing.Color.Black;
            this.statusBar.Location = new System.Drawing.Point(0, 186);
            this.statusBar.Name = "statusBar";
            this.statusBar.ProgressValue = 0;
            this.statusBar.Size = new System.Drawing.Size(253, 24);
            this.statusBar.StartPanelPosition = 0;
            this.statusBar.StylePainter = this.styleGuide;
            this.statusBar.TabIndex = 9;
            // 
            // panelBase
            // 
            this.panelBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.panelBase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelBase.Controls.Add(this.panelTabs);
            this.panelBase.Controls.Add(this.splitter);
            this.panelBase.Controls.Add(this.panelList);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.panelBase.Location = new System.Drawing.Point(0, 74);
            this.panelBase.Name = "panelBase";
            this.panelBase.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelBase.Size = new System.Drawing.Size(253, 112);
            this.panelBase.StylePainter = this.styleGuide;
            this.panelBase.TabIndex = 10;
            // 
            // panelTabs
            // 
            this.panelTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.panelTabs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelTabs.Controls.Add(this.tabPages);
            this.panelTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.panelTabs.Location = new System.Drawing.Point(124, 2);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(129, 108);
            this.panelTabs.StylePainter = this.styleGuide;
            this.panelTabs.TabIndex = 2;
            // 
            // tabPages
            // 
            this.tabPages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabPages.HideTabs = true;
            this.tabPages.ItemSize = new System.Drawing.Size(0, 20);
            this.tabPages.Location = new System.Drawing.Point(0, 0);
            this.tabPages.Name = "tabPages";
            this.tabPages.Size = new System.Drawing.Size(129, 108);
            this.tabPages.StylePainter = this.styleGuide;
            this.tabPages.TabIndex = 0;
            this.tabPages.TabStop = false;
            this.tabPages.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabPages.PageTextChanged += new EventHandler(tabPages_PageTextChanged);
            // 
            // splitter
            // 
            this.splitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.splitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.splitter.ForeColor = System.Drawing.Color.Black;
            this.splitter.Location = new System.Drawing.Point(120, 2);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(4, 108);
            this.splitter.StylePainter = this.styleGuide;
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // panelList
            // 
            this.panelList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelList.Controls.Add(this.treeView);
            this.panelList.Controls.Add(this.lblList);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.panelList.Location = new System.Drawing.Point(0, 2);
            this.panelList.Name = "panelList";
            this.panelList.Padding = new System.Windows.Forms.Padding(2);
            this.panelList.Size = new System.Drawing.Size(120, 108);
            this.panelList.StylePainter = this.styleGuide;
            this.panelList.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.White;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.treeView.ForeColor = System.Drawing.Color.Black;
            this.treeView.Location = new System.Drawing.Point(2, 22);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(116, 84);
            this.treeView.StylePainter = this.styleGuide;
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // lblList
            // 
            this.lblList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.lblList.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblList.ForeColor = System.Drawing.Color.Black;
            this.lblList.Location = new System.Drawing.Point(2, 2);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(116, 20);
            this.lblList.StylePainter = this.styleGuide;
            // 
            // McManagment
            // 
            this.Controls.Add(this.panelBase);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.topCaption);
            this.Name = "McManagment";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(253, 210);
            this.StylePainter = this.styleGuide;
            this.toolBar.ResumeLayout(false);
            this.panelBase.ResumeLayout(false);
            this.panelTabs.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.ResumeLayout(false);

		}

     
  
        //void TabPages_Inserted(int index, object value)
        //{
        //    //WizardPage wp = value as WizardPage;
 
        //    //if (!this.Controls.Contains(wp))
        //    //    this.Controls.Add(wp);
        //}
        //void TabPages_Removed(int index, object value)
        //{
        //    //WizardPage wp = value as WizardPage;
        //    //if (this.Controls.Contains(wp))
        //    //    this.Controls.Remove(wp);
        //}
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
		

		[Category("Style"),DefaultValue(GradientStyle.TopToBottom), Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GradientStyle GradientStyle
		{
			get{return this.gardientStyle;}
			set
			{
				if(gardientStyle!=value)
				{
					gardientStyle=value;
					this.topCaption.GradientStyle=value;
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
				if(base.ControlLayout!=value)
				{
					base.ControlLayout=value;
					this.topCaption.ControlLayout=value;
					this.statusBar.ControlLayout=value;
					this.panelList.ControlLayout=value;
					this.tabPages.ControlLayout=value;
                    this.lblList.ControlLayout = value;
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

        [Category("Wizard"),Browsable(false),EditorBrowsable( EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]// Browsable(false)]//, EditorBrowsable(EditorBrowsableState.Advanced)]
        public McToolBar ToolBar
        {
            get { return this.toolBar; }
        }


        //[Category("Wizard"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]// Browsable(false)]//, EditorBrowsable(EditorBrowsableState.Advanced)]
        //public McToolBarContainer ToolBarContainer
        //{
        //    get { return this.toolBarContainer; }
        //}

        //[Category("Wizard"), Editor("Nistec.WinForms.ToolBarCollectionEditor", typeof(System.Drawing.Design.UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public ControlCollection ToolBars
        //{
        //    get { return this.ToolBarContainer.Items; }
        //}

        [Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public McTreeView TreeView
        {
            get { return this.treeView; }
        }

		[Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		[Description("Access to underlying McTabControl instance")]
        public McTabControl TabControl
		{
			get { return tabPages; }
		}

		[Category("Wizard")]
		[Description("Access to underlying header panel")]
        internal Nistec.WinForms.McCaption HeaderPanel
		{
			get { return topCaption; }
		}

		[Category("Wizard")]
        [Description("Access to underlying StatusBar ")]
		public Nistec.WinForms.McStatusBar StatusBar
		{
			get { return statusBar; }
		}

        [Category("Wizard")]
        [Description("Access to underlying StatusBar ")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StatusBar.StatusBarPanelCollection StatusBarCollection
        {
            get { return statusBar.Panels; }
        }

		[Category("Wizard")]
		[Description("Collection of wizard pages")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TabPageCollection WizardPages
		{
            get { return this.tabPages.TabPages; }
		}
		
		[Category("Wizard"),Description("TreeNodeItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		  //, Localizable(true), Category("Data"), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public TreeNodeCollection Items
		{
			get
			{
				return this.treeView.Nodes;
			}
		}

        //[Category("Wizard"), Description("MenuItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public PopUpItemsCollection MenuItems
        //{
        //    get
        //    {
        //        return this.tbOption.MenuItems;
        //    }
        //}

        [Category("Wizard"), DefaultValue(false)]
        [Description("Show Status Bar Panels")]
        public bool ShowStatusBarPanels
        {
            get { return statusBar.ShowPanels; }

            set
            {
                statusBar.ShowPanels = value;
            }
        }

		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Main title Picture")]
		public bool ShowImage
		{
			get { return topCaption.ShowImage; }
            
			set
			{
				topCaption.ShowImage = value;
				//topCaption.Invalidate();
			}
		}

		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Caption")]
		public bool ShowCaption
		{
			get { return topCaption.Visible; }
            
			set
			{
				topCaption.Visible = value;
				this.Invalidate();
			}
		}

		[Category("Wizard"),DefaultValue(true)]
		[Description("Show Bottom panel")]
		public bool ShowBottom
		{
            get { return statusBar.Visible; }
            
			set
			{
                statusBar.Visible = value;
				this.Invalidate();
			}
		}

        [Category("Wizard")]
        [Description("Main title Sub Text")]
        public string CaptionSubText
        {
            get { return topCaption.SubText; }

            set
            {
                topCaption.SubText = value;
                //topCaption.Invalidate();
            }
        }

		[Category("Wizard")]
		[Description("Main title Picture")]
		public Image CaptionImage
		{
			get { return topCaption.Image;}
            
			set
			{
				topCaption.Image=value;
				//topCaption.Invalidate();
			}
		}
    
		[Category("Wizard")]
		[Description("Font for drawing main title text")]
		public Font CaptionFont
		{
			get 
			{ 
				
				return topCaption.TitleFont; 
			}
		    
			set
			{
				if(value!=null)
				{
					topCaption.TitleFont = value;
					//topCaption.Invalidate();
				}
			}
		}

		[Category("Wizard")]
		[Description("List width")]
		public int ListWidth
		{
			get { return this.panelList.Width;}
            
			set
			{
				this.panelList.Width=value;
			}
		}

		[Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("currently selected TabPage")]
		public McTabPage SelectedPage
		{
			get { return tabPages.SelectedTab; }
			set { tabPages.SelectedTab = value; }
		}

		[Category("Wizard")]
		[Description("Index of currently selected TabPage")]
		public int SelectedIndex
		{
			get { return tabPages.SelectedIndex; }
			set { tabPages.SelectedIndex = value; }
		}

        [Category("Wizard")]
        [Description("Position of TabPages")]
        public bool HideTabs
        {
            get { return tabPages.HideTabs; }
            set { tabPages.HideTabs = value; }
        }

        [Category("Wizard")]
        [Description("Caption List Text")]
        public string ListCaption
        {
            get { return this.lblList.Text; }
            set { this.lblList.Text = value; }
        }

        [Category("Wizard")]
        [Description("Caption List Visible")]
        public bool ListCaptionVisible
        {
            get { return this.lblList.Visible; }
            set { this.lblList.Visible = value; }
        }

		#endregion

		#region Override
        
		protected override void OnRightToLeftChanged(System.EventArgs e)
		{
			base.OnRightToLeftChanged (e);

			if(RightToLeft==RightToLeft.Yes)
			{
				this.panelList.Dock=DockStyle.Right;
				this.splitter.Dock=DockStyle.Right;
				this.splitter.Width=4;
		
			}
			else
			{
				this.panelList.Dock=DockStyle.Left;
				this.splitter.Dock=DockStyle.Left;
				this.splitter.Width=4;
			}
		}


        protected virtual void OnSelectionNodeChanged(TreeViewEventArgs e)
		{
            SelectNode();
			if (SelectionNodeChanged != null)
				SelectionNodeChanged(this, e);
		}

        protected virtual void OnPageTitleChanged(EventArgs e)
        {
            if (tabPages.SelectedIndex != -1)
            {
                // Get the selected wizard page
                McTabPage wp = tabPages.SelectedTab;
                this.topCaption.Text = wp.Text;
                //this.topCaption.SubText=wp.SubTitle;

            }
        }

		protected void OnTabSelectionChanged(EventArgs e)
		{
           
			if (tabPages.SelectedIndex != -1)
			{
				// Get the selected wizard page
                McTabPage wp = tabPages.SelectedTab;// WizardPages[tabPages.SelectedIndex];
				this.topCaption.Text=wp.Text;
				//this.topCaption.SubText=wp.SubTitle;

			}
			else
			{
				this.topCaption.Text="";
				this.topCaption.SubText="";
			}
            if (SelectedPageChanged != null)
                this.SelectedPageChanged(this, e);

			//topCaption.Invalidate();
            
		}
        void tabPages_PageTextChanged(object sender, EventArgs e)
        {
           OnPageTitleChanged(e);
        }

  		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnTabSelectionChanged(e);
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
          OnSelectionNodeChanged(e);
		}

	    private void toolBar_ButtonClick(object sender, ToolButtonClickEventArgs e)
        {
            OnButtonItemClick(e);
        }

        protected virtual void OnButtonItemClick(ToolButtonClickEventArgs e)
        {
            if (ToolButtonClick != null)
                ToolButtonClick(this, e);
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
			this.panelTabs.SetStyleLayout(this.LayoutManager.Layout);
			this.tabPages.SetStyleLayout(this.LayoutManager.Layout);
            this.statusBar.SetStyleLayout(this.LayoutManager.Layout);
			this.topCaption.SetStyleLayout(this.LayoutManager.Layout);
			this.panelList.SetStyleLayout(this.LayoutManager.Layout);

			this.Invalidate(true);
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged(e);
			this.panelTabs.StylePainter=this.m_StylePainter;
			this.tabPages.StylePainter=this.m_StylePainter;
			this.topCaption.StylePainter=this.m_StylePainter;
            this.statusBar.StylePainter = this.m_StylePainter;
			this.panelList.StylePainter=this.m_StylePainter;
			Invalidate();

		}

		#endregion

        #region buttons

        //[Category("Wizard")]
        //[Description("Button update text")]
        //public string ButttonUpdateText
        //{
        //    get { return this._buttonUpdate.Text; }
        //    set { this._buttonUpdate.Text = value; }
        //}

        //[Category("Wizard")]
        //[Description("Button Cancel text")]
        //public string ButttonCancelText
        //{
        //    get { return this._buttonCancel.Text; }
        //    set { this._buttonCancel.Text = value; }
        //}

        //[Category("Wizard")]
        //[Description("Button Help text")]
        //public string ButttonHelpText
        //{
        //    get { return this._buttonHelp.Text; }
        //    set { this._buttonHelp.Text = value; }
        //}

        //[Category("Wizard"), DefaultValue(true)]
        //[Description("Button update Visible")]
        //public bool ButttonUpdateVisible
        //{
        //    get { return this._buttonUpdate.Visible; }
        //    set { this._buttonUpdate.Visible = value; }
        //}

        //[Category("Wizard"), DefaultValue(true)]
        //[Description("Button Cancel Visible")]
        //public bool ButttonCancelVisible
        //{
        //    get { return this._buttonCancel.Visible; }
        //    set { this._buttonCancel.Visible = value; }
        //}

        //[Category("Wizard"), DefaultValue(true)]
        //[Description("Button Help Visible")]
        //public bool ButttonHelpVisible
        //{
        //    get { return this._buttonHelp.Visible; }
        //    set { this._buttonHelp.Visible = value; }
        //}

        //[Category("Wizard"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        //public PopUpItemsCollection ButtonMenuItems
        //{
        //    get
        //    {
        //        return this._buttonUpdate.MenuItems;
        //    }
        //}

        //[Category("Wizard")]
        //public ImageList ButtonImageList
        //{
        //    get
        //    {
        //        return _buttonUpdate.ImageList;
        //    }
        //    set
        //    {
        //        _buttonUpdate.ImageList = value;
        //    }
        //}

        //[Category("Wizard"),DefaultValue(ButtonComboType.Combo), Localizable(true)]
        //public ButtonComboType ButtonComboType
        //{
        //    get	{ return _buttonUpdate.ButtonComboType; }
        //    set
        //    {
        //        _buttonUpdate.ButtonComboType=value;
        //        this.Invalidate();
        //    }
        //}

        //[Category("Wizard"), Browsable(true), DefaultValue(ButtonMenuStyles.Default)]
        //public ButtonMenuStyles ButtonMenuStyle
        //{
        //    get{ return _buttonUpdate.ButtonMenuStyle; }
        //    set
        //    {
        //        _buttonUpdate.ButtonMenuStyle=value;
        //    }
        //}
        #endregion

        #region nodes

        private McNodeCollection _Nodes;
        private McNode _SelectedNode;

        public McNodeCollection Nodes
        {
            get
            {
                if (_Nodes == null)
                {
                    _Nodes = new McNodeCollection();
                }
                return _Nodes;
            }
        }

        public McNode SelectedNode
        {
            get { return _SelectedNode; }
            set { _SelectedNode = value; }
        }

        public int SelectedToolBar
        {
            get { return toolBar.SelectedGroup; }
            set {toolBar.SelectedGroup=value; }
        }

        public void SelectNode()
        {
            _SelectedNode = null;
            TreeNode node = treeView.SelectedNode;
            if (node != null && _Nodes!=null)
            {
                this.SelectedNode = Nodes.Find(node.Text);
                OnSelectedNode();
            }
        }

        protected virtual void OnSelectedNode()
        {
            if (_SelectedNode != null)
            {
                if (_SelectedNode.PageIndex > -1 && _SelectedNode.PageIndex < this.WizardPages.Count)
                {
                    this.SelectedIndex = _SelectedNode.PageIndex;
                }
                if (_SelectedNode.BarIndex > -1)
                {
                    SelectedToolBar=_SelectedNode.BarIndex;
                }
            }
        }

   

        public void AddTreeNode(string text,int imageIndex,int imageSelectedIndex, McNode node)
        {
           TreeNode nodex=new TreeNode(text,imageIndex,imageSelectedIndex);
           treeView.Nodes.Add(nodex);
           node.Node = nodex;
           Nodes.Add(node);
        }

     

        #endregion

    }



}
