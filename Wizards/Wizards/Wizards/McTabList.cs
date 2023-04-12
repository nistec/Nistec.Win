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
    [ToolboxItem(true), ToolboxBitmap(typeof(McTabList), "Toolbox.McTabList.bmp")]
    [Designer(typeof(Design.McTabListDesigner))]
    public class McTabList : Nistec.WinForms.Controls.McContainer
    {

        #region Members

        // Instance fields
        //private int _imageIndex;
        //private ImageList _imageList;

        protected McTabPage _selectedPage;
        protected Nistec.WinForms.McLabel _lbl;
        protected Nistec.WinForms.McTabControl _tabControl;
        private Nistec.WinForms.McPanel panelTabs;
        internal Nistec.WinForms.McSplitter splitter1;
        private Nistec.WinForms.McPanel panelList;
        private Nistec.WinForms.McListBox listBox1;

        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Events

        // Instance events
        public event EventHandler SelectedPageChanged;
        public event EventHandler SelectionItemChanged;

        #endregion

        #region Constructor

        public McTabList()
        {
 
            // No page currently selected
            _selectedPage = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McTabList));
            this._tabControl = new Nistec.WinForms.McTabControl();
            this.panelTabs = new Nistec.WinForms.McPanel();
            this.splitter1 = new Nistec.WinForms.McSplitter();
            this.panelList = new Nistec.WinForms.McPanel();
            this.listBox1 = new Nistec.WinForms.McListBox();
            this._lbl = new Nistec.WinForms.McLabel();
            this.panelTabs.SuspendLayout();
            this.panelList.SuspendLayout();
            this.SuspendLayout();
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
            this._tabControl.PageTextChanged += new EventHandler(_tabControl_PageTextChanged);
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
            // McTabList
            // 
            this.Controls.Add(this.panelTabs);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelList);
            this.Name = "McTabList";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(264, 224);
            this.panelTabs.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void _tabControl_PageTextChanged(object sender, EventArgs e)
        {
            for(int i=0;i<this.TabPages.Count;i++)
            {
                McTabPage wp = TabPages[i] as McTabPage;
                if (wp.Text != this.listBox1.ListItems[i].Text)
                {
                    this.listBox1.ListItems[i] = new ListItem(wp.Text, wp.ImageIndex);
                }
            }
            this.listBox1.Invalidate();
        }

        void TabPages_Inserted(int index, object value)
        {
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
            this.TabPages.Add(new McTabPage(text, imageIndex));
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
                    if (value == ControlLayout.Flat || value == ControlLayout.System)
                    {
                        this._lbl.Visible = false;
                    }
                    else
                    {
                        this._lbl.Visible = true; 
                        this._lbl.ControlLayout = value;
                    }
                    this._tabControl.ControlLayout = value;

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
        [Description("Collection of Tab pages")]
        [Browsable(false), EditorBrowsable( EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TabPageCollection TabPages
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
            if (this.DesignMode) return;
            
            //this.listBox1.Items.Clear();
            this.listBox1.ListItems.Clear();
            this.listBox1.ImageList = this.ImageList;

            foreach (McTabPage wp in this.TabPages)
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
                this.listBox1.SelectedIndex = 0;
            }
        }

        protected void OnSelectionItemChanged(EventArgs e)
        {
            if (this.SelectionItemChanged != null)
                this.SelectionItemChanged(this, e);
        }


        protected void OnTabSelectionChanged(EventArgs e)
        {

              // Remember which is the newly seleced page
            if (_tabControl.SelectedIndex != -1)
                _selectedPage = TabPages[_tabControl.SelectedIndex] as McTabPage;

            if (SelectedPageChanged != null)
                SelectedPageChanged(this, e);
            // Generate page enter event is now on a valid page
            //			if (_selectedPage != null)
            //				OnWizardPageEnter(_selectedPage);
        }


        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this._tabControl.SelectedIndex = this.listBox1.SelectedIndex;
            OnSelectionItemChanged(e);

        }

        private void _tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTabSelectionChanged(e);
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
            this.panelList.SetStyleLayout(this.LayoutManager.Layout);

            this.Invalidate(true);
        }

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            this.panelTabs.StylePainter = this.m_StylePainter;
            this._tabControl.StylePainter = this.m_StylePainter;
            this.panelList.StylePainter = this.m_StylePainter;
            Invalidate();

        }

        #endregion

 
    }
}
