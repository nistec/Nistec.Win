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
    [ToolboxItem(true), ToolboxBitmap(typeof(McTabTree), "Toolbox.McTabTree.bmp")]
	[DefaultProperty("WizardPanelType")]
	[Designer(typeof(Design.McTabTreeDesigner))]
    public class McTabTree : Nistec.WinForms.Controls.McContainer//, IMcWizard
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
  
		protected McTabPage _selectedPage;
		protected ButtonMode _UpdateMode;
		protected ButtonMode _CancelMode;
		protected ButtonMode _HelpMode;

		protected Nistec.WinForms.McButtonMenu _buttonUpdate;
		protected Nistec.WinForms.McButton _buttonCancel;
		protected Nistec.WinForms.McButton _buttonHelp;
        //protected Nistec.WinForms.McLabel _lbl;
	
	    
		// Instance designer fields
		protected Nistec.WinForms.McCaption _panelTop;
		protected Nistec.WinForms.McPanel _panelBottom;
        protected Nistec.WinForms.McTabControl _tabControl;
	
		private Nistec.WinForms.McPanel panelBase;
		private Nistec.WinForms.McPanel panelTabs;
		internal Nistec.WinForms.McSplitter splitter;
		private Nistec.WinForms.McPanel panelList;
		private Nistec.WinForms.McTreeView treeView1;

		private System.ComponentModel.IContainer components=null;

		#endregion
 
		#region Events

		// Instance events
        public event SelectedPopUpItemEventHandler SelectedButtonMenuClicked;
		public event TreeViewEventHandler SelectionNodeChanged;
		public event EventHandler SelectedPageChanged;
		public event EventHandler UpdateClick;
		public event EventHandler CancelClick;
		public event EventHandler HelpClick;

		#endregion

		#region Constructor

        public McTabTree()
		{

			_selectedPage = null;
			
			_UpdateMode=ButtonMode.Default;
			_CancelMode=ButtonMode.Default;
			_HelpMode=ButtonMode.Default;
 
            //_imageIndex=-1;
            //_imageList=null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McTabTree));
            this._panelBottom = new Nistec.WinForms.McPanel();
            this._buttonUpdate = new Nistec.WinForms.McButtonMenu();
            this._buttonCancel = new Nistec.WinForms.McButton();
            this._buttonHelp = new Nistec.WinForms.McButton();
            this._panelTop = new Nistec.WinForms.McCaption();
            this._tabControl = new Nistec.WinForms.McTabControl();
            this.panelBase = new Nistec.WinForms.McPanel();
            this.panelTabs = new Nistec.WinForms.McPanel();
            this.splitter = new Nistec.WinForms.McSplitter();
            this.panelList = new Nistec.WinForms.McPanel();
            this.treeView1 = new Nistec.WinForms.McTreeView();
            //this._lbl = new Nistec.WinForms.McLabel();
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
            this._panelBottom.Location = new System.Drawing.Point(2, 164);
            this._panelBottom.Name = "_panelBottom";
            this._panelBottom.Size = new System.Drawing.Size(249, 44);
            this._panelBottom.TabIndex = 3;
            this._panelBottom.Text = "statusBar1";
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonUpdate.ButtonMenuStyle = Nistec.WinForms.ButtonMenuStyles.Media;
            this._buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
            this._buttonUpdate.FixSize = false;
            this._buttonUpdate.Location = new System.Drawing.Point(20, 6);
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
            this._buttonCancel.Location = new System.Drawing.Point(100, 6);
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
            this._buttonHelp.Location = new System.Drawing.Point(176, 6);
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
            this._tabControl.Padding = new System.Windows.Forms.Padding(0, 22, 0, 0);
            this._tabControl.Size = new System.Drawing.Size(125, 98);
            this._tabControl.TabIndex = 0;
            this._tabControl.SelectedIndexChanged += new System.EventHandler(this._tabControl_SelectedIndexChanged);
            // 
            // panelBase
            // 
            this.panelBase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelBase.Controls.Add(this.panelTabs);
            this.panelBase.Controls.Add(this.splitter);
            this.panelBase.Controls.Add(this.panelList);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelBase.Location = new System.Drawing.Point(2, 62);
            this.panelBase.Name = "panelBase";
            this.panelBase.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelBase.Size = new System.Drawing.Size(249, 102);
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
            this.panelTabs.Size = new System.Drawing.Size(125, 98);
            this.panelTabs.TabIndex = 2;
            // 
            // splitter
            // 
            this.splitter.BackColor = System.Drawing.SystemColors.Control;
            this.splitter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.splitter.Location = new System.Drawing.Point(120, 2);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(4, 98);
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // panelList
            // 
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelList.Controls.Add(this.treeView1);
            //this.panelList.Controls.Add(this._lbl);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelList.Location = new System.Drawing.Point(0, 2);
            this.panelList.Name = "panelList";
            this.panelList.Padding = new System.Windows.Forms.Padding(2);
            this.panelList.Size = new System.Drawing.Size(120, 98);
            this.panelList.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(2, 22);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(116, 74);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            //// 
            //// _lbl
            //// 
            //this._lbl.BackColor = System.Drawing.SystemColors.Control;
            //this._lbl.Dock = System.Windows.Forms.DockStyle.Top;
            //this._lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this._lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            //this._lbl.Location = new System.Drawing.Point(2, 2);
            //this._lbl.Name = "_lbl";
            //this._lbl.Size = new System.Drawing.Size(116, 20);
            // 
            // McPanelTabs
            // 
            this.Controls.Add(this.panelBase);
            this.Controls.Add(this._panelBottom);
            this.Controls.Add(this._panelTop);
            this.Name = "McPanelTabs";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(253, 210);
            this._panelBottom.ResumeLayout(false);
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
					this._panelTop.GradientStyle=value;
					//this._panelBottom.GradientStyle=value;
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
					this._panelTop.ControlLayout=value;
					this._panelBottom.ControlLayout=value;
					this.panelList.ControlLayout=value;
					this._tabControl.ControlLayout=value;
                    //this._lbl.ControlLayout = value;
                    
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
        internal McTreeView TreeView
        {
            get { return this.treeView1; }
        }

        [Category("Wizard"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Access to underlying McTabControl instance")]
        internal McTabControl TabControl
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
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public TabPageCollection WizardPages
		{
			get { return  this._tabControl.TabPages; }
		}
		
		[Category("Wizard"),Description("TreeNodeItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		  //, Localizable(true), Category("Data"), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public TreeNodeCollection Items
		{
			get
			{
				return this.treeView1.Nodes;
			}
		}

		[Category("Wizard"),DefaultValue(true)]
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

		[Category("Wizard"),DefaultValue(true)]
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

		[Category("Wizard"),DefaultValue(true)]
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
                //_panelTop.Invalidate();
            }
        }

		[Category("Wizard")]
		[Description("Main title Picture")]
		public Image CaptionImage
		{
			get { return _panelTop.Image;}
            
			set
			{
				_panelTop.Image=value;
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
				if(value!=null)
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
			get { return _tabControl.SelectedTab; }
			set { _tabControl.SelectedTab = value; }
		}

		[Category("Wizard")]
		[Description("Index of currently selected TabPage")]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get { return _tabControl.SelectedIndex; }
			set { _tabControl.SelectedIndex = value; }
		}

        //[Category("Wizard")]
        //[Description("Position of TabPages")]
        //public TabPosition TabPosition
        //{
        //    get { return _tabControl.TabPosition; }
        //    set { _tabControl.TabPosition = value; }
        //}

        [Category("Wizard")]
        [Description("Position of TabPages")]
        public bool HideTabs
        {
            get { return _tabControl.HideTabs; }
            set { _tabControl.HideTabs = value; }
        }

        [Category("Wizard"), DefaultValue("Update")]
		[Description("Button update text")]
		public string ButttonUpdateText
		{
			get { return this._buttonUpdate.Text; }
			set { this._buttonUpdate.Text = value; }
		}

		[Category("Wizard"),DefaultValue("Cancel")]
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

		[Category("Wizard"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
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

		[Category("Wizard"),DefaultValue(ButtonComboType.Combo), Localizable(true)]
		public ButtonComboType ButtonComboType
		{
			get	{ return _buttonUpdate.ButtonComboType; }
			set
			{
				_buttonUpdate.ButtonComboType=value;
				this.Invalidate();
			}
		}

        [Category("Wizard"), Browsable(true), DefaultValue(ButtonMenuStyles.Media)]
		public ButtonMenuStyles ButtonMenuStyle
		{
			get{ return _buttonUpdate.ButtonMenuStyle; }
			set
			{
				_buttonUpdate.ButtonMenuStyle=value;
			}
		}

        //[Category("Wizard")]
        //[Description("Caption List Text")]
        //public string ListCaption
        //{
        //    get { return this._lbl.Text; }
        //    set { this._lbl.Text = value; }
        //}
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


		public virtual void OnSelectionNodeChanged(TreeViewEventArgs e)
		{
			if (SelectionNodeChanged != null)
				SelectionNodeChanged(this, e);
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
            Form form = this.FindForm();
            if (form != null)
            {
                form.Close();
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
 
		protected void OnTabSelectionChanged(EventArgs e)
		{
           
			if (_tabControl.SelectedIndex != -1)
			{
				// Get the selected wizard page
				McTabPage wp = WizardPages[_tabControl.SelectedIndex];
				//this._panelTop.SubText=wp.SubTitle;
                this._panelTop.SetCaptionText(wp.Text, "", true);
            }
			else
			{
                this._panelTop.SetCaptionText("","",true);
			}
            if (SelectedPageChanged != null)
                this.SelectedPageChanged(this, e);

			//_panelTop.Invalidate();
            
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
			using(Pen lightPen = LayoutManager.GetPenBorder())
			{
				pe.Graphics.DrawRectangle(lightPen, 0, _panelBottom.Top, _panelBottom.Width-1, _panelBottom.Height - 1);
            
			}
		}

		private void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnTabSelectionChanged(e);
		}


		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
          OnSelectionNodeChanged(e);
		}

        private void _buttonUpdate_SelectedItemClick(object sender, SelectedPopUpItemEvent e)
		{
			this.OnSelectedButtonMenuClicked(e);
			if(this.SelectedButtonMenuClicked!=null)
				this.SelectedButtonMenuClicked(this,e);
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
			this._tabControl.SetStyleLayout(this.LayoutManager.Layout);
			this._panelBottom.SetStyleLayout(this.LayoutManager.Layout);
			this._panelTop.SetStyleLayout(this.LayoutManager.Layout);
			this.panelList.SetStyleLayout(this.LayoutManager.Layout);

			this.Invalidate(true);
		}

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged(e);
			this.panelTabs.StylePainter=this.m_StylePainter;
			this._tabControl.StylePainter=this.m_StylePainter;
			this._panelTop.StylePainter=this.m_StylePainter;
			this._panelBottom.StylePainter=this.m_StylePainter;
			this.panelList.StylePainter=this.m_StylePainter;
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
