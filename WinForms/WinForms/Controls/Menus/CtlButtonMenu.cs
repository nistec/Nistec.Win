using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Drawing;

using Nistec.Win32;
using Nistec.WinForms.Controls;

namespace Nistec.WinForms
{

    public enum ButtonMenuStyles
    {
        Default,
        Button,
        Combo,
        Media
    }
    /// <summary>
    /// Summary description for ButtomMenu.
    /// </summary>

    [ToolboxItem(true)]
    [DefaultEvent("ButtonClick")]
    [Designer(typeof(Design.McButtonDesigner))]
    [ToolboxBitmap(typeof(McButtonMenu), "Toolbox.ButtonMenu.bmp")]
    public class McButtonMenu : Nistec.WinForms.Controls.McBase, ILayout, IButtonControl,IButton
    {

        #region Members

        private System.ComponentModel.IContainer components = null;
        private Nistec.WinForms.McButton BtnCmd;
        private Nistec.WinForms.Controls.McButtonCombo BtnItmes;
        private bool m_HoverMode;

        private DialogResult m_DialogResult;
        private bool m_IsDefault;
        private ButtonMenuStyles m_ButtonMenuStyle;
        private ButtonComboType m_ButtonComboType;
        private McContextMenu dropDownMenu;

        [Category("Property changed")]
        public event EventHandler ButtonClick = null;

        #endregion

        #region Constructors

        public McButtonMenu()
            : base()
        {
            m_ButtonMenuStyle = ButtonMenuStyles.Default;
            m_ButtonComboType = ButtonComboType.Combo;

            m_HoverMode = false;
            m_DialogResult = DialogResult.None;
            m_IsDefault = false;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
            //BtnCmd.m_netFram = true;
            //BtnItmes.m_netFram = true;
            base.BorderStyle = BorderStyle.Fixed3D;
            this.BtnCmd.owner = this;
            InitMcPopUp();
        }

        //internal McButtonMenu(bool net)
        //    : this()
        //{
        //    this.m_netFram = net;
        //}


        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McButtonMenu));
            this.BtnCmd = new Nistec.WinForms.McButton();
            this.BtnItmes = new Nistec.WinForms.Controls.McButtonCombo(this, Nistec.WinForms.Controls.ButtonComboType.Combo);//,false,true);
            this.ctlPopUp = new McPopUp(this);
            this.SuspendLayout();
            //
            //this.ctlPopUp
            //
            this.ctlPopUp.BackColor = Color.White;
            this.ctlPopUp.ForeColor = Color.Black;
            // 
            // BtnCmd
            // 
            this.BtnCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnCmd.Location = new System.Drawing.Point(0, 0);
            this.BtnCmd.Name = "BtnCmd";
            this.BtnCmd.Size = new System.Drawing.Size(96, 20);
            this.BtnCmd.TabIndex = 0;
            this.BtnCmd.Text = "button1";
            this.BtnCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnCmd.Click += new System.EventHandler(this.BtnCmd_Click);
            this.BtnCmd.MouseHover += new System.EventHandler(this.BtnCmd_MouseHover);
            // 
            // BtnItmes
            // 
            //this.BtnItmes.ControlLayout = ControlLayout.Visual;// value == ControlLayout.Visual ? ControlLayout.XpLayout : value;
            this.BtnItmes.Dock = System.Windows.Forms.DockStyle.Right;
            //this.BtnItmes.ButtonComboType = Nistec.WinForms.ButtonComboType.Combo;
            this.BtnItmes.Location = new System.Drawing.Point(96, 0);
            this.BtnItmes.Name = "BtnItmes";
            this.BtnItmes.Size = new System.Drawing.Size(16, 20);
            this.BtnItmes.TabIndex = 1;
            this.BtnItmes.Click += new System.EventHandler(this.BtnItmes_Click);
            this.BtnItmes.MouseHover += new System.EventHandler(this.BtnItmes_MouseHover);
            // 
            // McButtonMenu
            // 
            this.Controls.Add(this.BtnCmd);
            this.Controls.Add(this.BtnItmes);
            this.Name = "McButtonMenu";
            this.ResumeLayout(false);

        }
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ctlPopUp != null)
                {
                    this.ctlPopUp.SelectedValueChanged -= new EventHandler(ctlPopUp_SelectedValueChanged);
                    this.ctlPopUp.Dispose();
                }
                if (this.BtnCmd != null)
                {
                    this.BtnCmd.Dispose();
                }
                if (this.BtnItmes != null)
                {
                    this.BtnItmes.Dispose();
                }
                if (components != null)
                {
                    components.Dispose();
                }

            }
            base.Dispose(disposing);
        }

        #endregion

        #region Events handlers


        private void BtnCmd_Click(object sender, System.EventArgs e)
        {
            if (m_ButtonMenuStyle == ButtonMenuStyles.Button && this.DropDownMenu != null)
            {
                ShowPopUp();
            }
            else if (m_ButtonMenuStyle == ButtonMenuStyles.Button && this.ctlPopUp != null && this.ctlPopUp.MenuItems.Count > 0)
            {
                ShowPopUp();
            }
            else if (this.ButtonClick != null)
                this.ButtonClick(this, e);

        }

        private void BtnItmes_Click(object sender, System.EventArgs e)
        {
            ShowPopUp();
        }

        private void BtnCmd_MouseHover(object sender, System.EventArgs e)
        {
            if (m_HoverMode)
            {
                ShowPopUp();
                //if (this.ButtonMenuStyle ==ButtonMenuStyles.Flat && this.DropDownMenu != null)
                //	this.DropDownMenu.GetContextMenu().Show(this, new Point(0, base.Height));
            }
        }

        private void BtnItmes_MouseHover(object sender, System.EventArgs e)
        {
            if (m_HoverMode)
            {
                ShowPopUp();
                //if (this.DropDownMenu != null)
                //	this.DropDownMenu.GetContextMenu().Show(this, new Point(0, base.Height));
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (m_ButtonMenuStyle == ButtonMenuStyles.Media)
            {
                this.LayoutManager.DrawButtonRect(e.Graphics, bounds, this, this.ControlLayout);//, 3, false);
            }
         }


        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.dropDownMenu != null)
            {
                this.dropDownMenu.itemWidth = this.Width;
            }
            SetButoonStyle();
        }

        public void ShowPopUp()
        {
            if (this.dropDownMenu != null)
            {
                this.DropDownMenu.Show(this, new Point(0, base.Height));
            }
            else if (ctlPopUp != null)
            {
                this.PopUp.DrawItemStyle = this.drawItemStyle;
                this.PopUp.RightToLeft = this.RightToLeft;

                int w = (int)this.PopUp.CalcDropDownWidth();
                this.PopUp.DropDownWidth = Math.Max(this.Width, w);

                if (ButtonMenuStyle == ButtonMenuStyles.Combo)
                {
                    this.PopUp.ShowPopUp(this.PointToScreen(new Point(base.Width - w, base.Height + 1)));
                }
                else
                {
                    //if (this.PopUp.DropDownWidth < this.Width)
                    //{
                    //    this.PopUp.DropDownWidth = this.Width;
                    //}
                    this.PopUp.ShowPopUp(this.PointToScreen(new Point(0, base.Height + 1)));
                }
            }
        }

        #endregion

        #region Methods

        protected bool IsMouseInButtonRect()
        {
            Rectangle rectButton = this.ClientRectangle;//  GetButtonRect;
            Point mPos = Control.MousePosition;
            if (rectButton.Contains(this.PointToClient(mPos)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Properties

        [Browsable(false)]
        public ButtonStates ButtonState
        {
            get { return ButtonStates.Normal; }
        }


        [Category("Appearance"), DefaultValue(ButtonComboType.Combo)]
        public ButtonComboType ButtonComboType
        {
            get { return BtnItmes.ButtonComboType;}
            set
            {
                m_ButtonComboType = value;

                //if (value == ButtonComboType.Combo)
                //    this.BtnItmes.Image = ResourceUtil.LoadImage(Global.ImagesPath + "btnCombo.gif");
                //else if (value == ButtonComboType.Brows)
                //    this.BtnItmes.Image = ResourceUtil.LoadImage(Global.ImagesPath + "btnBrows.gif");
                //else if (value == ButtonComboType.Down)
                //    this.BtnItmes.Image = ResourceUtil.LoadImage(Global.ImagesPath + "downSmallArrow.gif");
                //else if (value == ButtonComboType.Up)
                //    this.BtnItmes.Image = ResourceUtil.LoadImage(Global.ImagesPath + "upSmallArrow.gif");
                //this.BtnItmes.ImageAlign = ContentAlignment.MiddleCenter;
                BtnItmes.ButtonComboType = value;
                this.Invalidate();
            }
        }

        [Category("Behavior"),DefaultValue(null)]
        public McContextMenu DropDownMenu
        {
            get
            {
                return this.dropDownMenu;
            }
            set
            {
                this.dropDownMenu = value;
                if (this.dropDownMenu != null)
                {
                    this.dropDownMenu.RightToLeft = this.RightToLeft;
                    this.dropDownMenu.StylePainter = this.StylePainter;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set
            {
                base.BorderStyle = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Windows.Forms.ContextMenu ContextMenu
        {
            get { return base.ContextMenu; }
            set { base.ContextMenu = value; }
        }

        [Category("Appearance")]
        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return BtnCmd.Text; }

            set
            {
                BtnCmd.Text = value;
                this.Invalidate();
            }
        }


        [Category("Appearance")]
        [DefaultValue(null),
        System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        public override Image Image
        {
            get { return BtnCmd.Image; }
            set
            {
                //if(BtnCmd.Image != value)
                //{
                BtnCmd.Image = value;
                this.Invalidate();
                //}
            }
        }

        [Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        public override int ImageIndex
        {
            get
            {
                return base.ImageIndex;
            }
            set
            {
                if (base.ImageList == null)
                    return;

                if (value > -1 && value < ImageList.Images.Count)
                {
                    base.ImageIndex = value;
                    this.BtnCmd.Image = ImageList.Images[value];
                    base.Invalidate();
                }
            }
        }

        [Category("Behavior"),DefaultValue("")]
        public new String ToolTipText
        {
            get { return BtnCmd.ToolTipText; }
            set
            {
                if (BtnCmd.ToolTipText != value)
                    BtnCmd.ToolTipText = value;
            }
        }

        [Category("Behavior"), DefaultValue("")]
        public String ToolTipItems
        {
            get { return BtnItmes.ToolTipText; }
            set
            {
                    BtnItmes.ToolTipText = value;
            }
        }

        [Category("Appearance")]
        [DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
        public override System.Drawing.ContentAlignment TextAlign
        {
            get { return this.BtnCmd.TextAlign; }
            set
            {
                if (BtnCmd.TextAlign != value)
                {
                    BtnCmd.TextAlign = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
        public override System.Drawing.ContentAlignment ImageAlign
        {
            get { return BtnCmd.ImageAlign; }
            set
            {
                if (BtnCmd.ImageAlign != value)
                {
                    BtnCmd.ImageAlign = value;
                    this.Invalidate();
                }
            }
        }

        [Browsable(true), DefaultValue(false)]
        public bool HoverMode
        {
            get { return m_HoverMode; }
            set { m_HoverMode = value; }
        }

        [Browsable(true), DefaultValue(ButtonMenuStyles.Default)]
        public ButtonMenuStyles ButtonMenuStyle
        {
            get { return m_ButtonMenuStyle; }
            set
            {
                if (m_ButtonMenuStyle != value)
                {
                    m_ButtonMenuStyle = value;
                    SetButoonStyle();
                }
            }
        }

        [Category("Style"), DefaultValue(ControlLayout.XpLayout)]
        public override ControlLayout ControlLayout
        {
            get { return BtnCmd.ControlLayout; }
            set
            {
                BtnCmd.ControlLayout = value;
                BtnItmes.ControlLayout =  value ;//== ControlLayout.Visual ? ControlLayout.XpLayout : value;
                this.Invalidate();
            }

        }


        private void SetButoonStyle()
        {
            if (m_ButtonMenuStyle == ButtonMenuStyles.Media)
            {
                this.BtnItmes.Visible = true;
                this.BtnCmd.Visible = true;
                this.BtnItmes.buttonMedia = true;
                this.BtnCmd.Size = new Size(this.Size.Width, this.Size.Height * 75 / 100);
                this.BtnItmes.Location = new Point(0, 0);
                this.BtnCmd.Dock = System.Windows.Forms.DockStyle.Top;

                this.BtnItmes.Size = new Size(this.Size.Width,this.Size.Height*25/100);
                this.BtnItmes.Location = new Point(0, BtnCmd.Bottom);
                this.BtnItmes.Dock = System.Windows.Forms.DockStyle.Fill;
                //BtnItmes.ButtonComboType = ButtonComboType.Down;

            }
            else if (m_ButtonMenuStyle == ButtonMenuStyles.Button)
            {
                this.BtnItmes.Visible = false;
                this.BtnCmd.Visible = true;
                this.BtnCmd.Dock = System.Windows.Forms.DockStyle.Fill;
                this.BtnItmes.Dock = System.Windows.Forms.DockStyle.Right;
            }
            else if (m_ButtonMenuStyle == ButtonMenuStyles.Combo)
            {
                this.BtnItmes.Visible = true;
                this.BtnCmd.Visible = false;
                this.BtnCmd.Dock = System.Windows.Forms.DockStyle.Fill;
                this.BtnItmes.Dock = System.Windows.Forms.DockStyle.Right;
            }
            else //(m_ButtonMenuStyle==ButtonMenuStyles.Default)
            {
                this.BtnItmes.Visible = true;
                this.BtnCmd.Visible = true;
                this.BtnCmd.Dock = System.Windows.Forms.DockStyle.Fill;
                this.BtnItmes.Dock = System.Windows.Forms.DockStyle.Right;
            }
            this.Invalidate();
        }

        #endregion

        #region McPopUp

        //private MenuItemsCollection menuItems;
        private McPopUp ctlPopUp;
        private DrawItemStyle drawItemStyle;
        public event SelectedPopUpItemEventHandler SelectedItemClick;

        private void InitMcPopUp()
        {
            this.ctlPopUp = new McPopUp(this);
            //
            //this.ctlPopUp
            //
            this.ctlPopUp.BackColor = Color.White;
            this.ctlPopUp.ForeColor = Color.Black;
            this.ctlPopUp.SelectedValueChanged += new EventHandler(ctlPopUp_SelectedValueChanged);

        }

        private void ctlPopUp_SelectedValueChanged(object sender, EventArgs e)
        {
            OnSelectedItemClick(new SelectedPopUpItemEvent(this.ctlPopUp.SelectedItem, this.ctlPopUp.SelectedIndex));
        }

        protected virtual void OnSelectedItemClick(SelectedPopUpItemEvent e)
        {
            if (SelectedItemClick != null)
                SelectedItemClick(this, e);
        }

        [Browsable(false), Category("Items"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public McPopUp PopUp
        {
            get
            {
                if (this.ctlPopUp == null)
                {
                    InitMcPopUp();
                }
                return this.ctlPopUp;
            }
        }

        [Category("Items"), DefaultValue(DrawItemStyle.Default)]
        public DrawItemStyle DrawItemStyle
        {
            get
            {
                return drawItemStyle;
            }
            set
            {
                drawItemStyle = value;
            }
        }


        [Category("Items"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public PopUpItemsCollection MenuItems
        {
            get
            {
                //				if(this.ctlPopUp==null)
                //				{
                //					InitMcPopUp();
                //					//this.menuItems=new Nistec.WinForms.McPopUp.PopUpItemsCollection();
                //           		}
                return this.PopUp.MenuItems;
            }
        }

        public override ImageList ImageList
        {
            get
            {
                return base.ImageList;
            }
            set
            {
                base.ImageList = value;
                this.ctlPopUp.ImageList = value;
            }
        }


        public void AddPopUpItem(string text)
        {
            MenuItems.AddItem(text);
        }

        public void AddPopUpItem(string text, int imageIndex)
        {
            MenuItems.AddItem(text, imageIndex);
        }
        #endregion

        #region ILayout

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            this.BtnCmd.StylePainter=this.StylePainter;
            this.BtnItmes.StylePainter = this.StylePainter;
        }

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnStylePropertyChanged(e);
            if (!(DesignMode || IsHandleCreated))
                return;
            if (e.PropertyName.Equals("StyleLayout"))
            {
                this.BtnCmd.SetStyleLayout(LayoutManager.Layout);
                this.BtnItmes.SetStyleLayout(LayoutManager.Layout);
                if (this.dropDownMenu != null)
                    this.dropDownMenu.SetStyleLayout(LayoutManager.Layout);

            }
            else if (e.PropertyName.Equals("StylePlan"))
            {
                this.BtnCmd.SetStyleLayout(LayoutManager.StylePlan);
                this.BtnItmes.SetStyleLayout(LayoutManager.StylePlan);
                if (this.dropDownMenu != null)
                    this.dropDownMenu.SetStyleLayout(LayoutManager.StylePlan);
            }

            this.Invalidate();
        }

        //		[Category("Style"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint),DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //		public StyleButtonDesigner StyleMc
        //		{
        //			get {return m_Style;}
        //			set {
        //				m_Style= value;
        //				this.BtnCmd.StyleMc   = value;
        //				this.BtnItmes.StyleMc = value;
        //			    }
        //		}
        //
        //		[Browsable(false)]
        //		public virtual IStyleLayout LayoutManager
        //		{
        //			get
        //			{
        //				if(this.m_StyleGuide!=null)
        //					return this.m_StyleGuide.Layout as IStyleLayout;
        //				else
        //					return this.m_Style as IStyleLayout;
        //
        //			}
        //		}
        //
        //		public virtual void SetStyleLayout(StyleLayout value)
        //		{
        //			this.m_Style.SetStyleLayout(value); 
        //			this.BtnCmd.StyleMc.SetStyleLayout (value);
        //			this.BtnItmes.StyleMc.SetStyleLayout (value);
        //			if(this.dropDownMenu!=null)
        //			this.dropDownMenu.StyleMc.SetStyleLayout(value); 
        //		}
        //
        //		public virtual void SetStyleLayout(Styles value)
        //		{
        //			m_Style.SetStyleLayout(value);//this.m_Style.StylePlan=value; 
        //			this.BtnCmd.StyleMc.SetStyleLayout(value);//.StylePlan= value;
        //			this.BtnItmes.StyleMc.SetStyleLayout(value);//StylePlan= value;
        //			if(this.dropDownMenu!=null)
        //				this.dropDownMenu.StyleMc.SetStyleLayout(value); 
        //		}
        //
        //		protected override void OnStyleGuideChanged(EventArgs e)
        //		{
        //			this.BtnCmd.StyleGuide=this.m_StyleGuide;
        //			this.BtnItmes.StyleGuide=this.m_StyleGuide;
        //			if(this.dropDownMenu!=null)
        //				this.dropDownMenu.StyleGuide=this.m_StyleGuide; 
        //		}
        //
        //		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //		{
        //
        //			if(!(DesignMode || IsHandleCreated))
        //				return;
        //			if(e.PropertyName.Equals("StyleLayout"))
        //			{
        //				this.BtnCmd.StyleMc.SetStyleLayout (StyleMc.Layout);
        //				this.BtnItmes.StyleMc.SetStyleLayout (StyleMc.Layout);
        //				if(this.dropDownMenu!=null)
        //					this.dropDownMenu.StyleMc.SetStyleLayout(StyleMc.Layout); 
        //
        //			}
        //			else if(e.PropertyName.Equals("StylePlan"))
        //			{
        //				this.BtnCmd.StyleMc.StylePlan =m_Style.StylePlan;
        //				this.BtnItmes.StyleMc.StylePlan =m_Style.StylePlan;
        //				if(this.dropDownMenu!=null)
        //					this.dropDownMenu.StyleMc.StylePlan =m_Style.StylePlan;
        //			}
        //           // m_ButtonItems.SetColors(m_Style.Layout); 
        //
        //			this.Invalidate();
        //		}

        #endregion

        #region IButtonControl Members


        protected bool IsDefault
        {
            get
            {
                return this.m_IsDefault;
            }
            set
            {
                if (this.m_IsDefault != value)
                {
                    this.m_IsDefault = value;
                    if (base.IsHandleCreated)
                    {
                        if (this.ControlLayout != ControlLayout.System)
                        {
                            base.Invalidate();
                        }
                        else
                        {
                            base.UpdateStyles();
                        }
                    }
                }
            }
        }

        // Add implementation to the IButtonControl.DialogResult property.
        public System.Windows.Forms.DialogResult DialogResult
        {
            get { return this.m_DialogResult; }
            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    this.m_DialogResult = value;
                }
            }
        }

        // Add implementation to the IButtonControl.NotifyDefault method.
        public void NotifyDefault(bool value)
        {
            if (this.m_IsDefault != value)
            {
                this.m_IsDefault = value;
            }
        }

        // Add implementation to the IButtonControl.PerformClick method.
        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.OnClick(EventArgs.Empty);
            }
        }
        #endregion

    }

    #region PopUpItems Collection

    //	public class MenuItemsCollection :System.Collections.CollectionBase// Nistec.Collections.CollectionWithEvents
    //	{
    //		internal McButtonMenu owner;
    //
    //		//public PopUpItemsCollection(){}
    //		public MenuItemsCollection(McButtonMenu ctl)
    //		{
    //			this.owner=ctl;
    //		}
    //
    //		public PopUpItem Add(PopUpItem value)
    //		{
    //			value.owner=this.owner;
    //			value.Text=value.ToString();
    //			base.List.Add(value as object);
    //			return value;
    //		}
    //
    //		public void AddRange(PopUpItem[] values)
    //		{
    //			foreach(PopUpItem itm in values)
    //			{
    //				Add(itm);
    //			}
    //		}
    //
    //		public void Remove(PopUpItem value)
    //		{
    //			base.List.Remove(value as object);
    //		}
    //
    //		public void Insert(int index, PopUpItem value)
    //		{
    //			base.List.Insert(index, value as object);
    //		}
    //
    //		public bool Contains(PopUpItem value)
    //		{
    //			return base.List.Contains(value as object);
    //		}
    //
    //		public PopUpItem this[int index]
    //		{
    //			get { return (base.List[index] as PopUpItem); }
    //		}
    //
    //		public PopUpItem this[string text]
    //		{
    //			get 
    //			{
    //				// Search for a Page with a matching title
    //				foreach(PopUpItem itm in base.List)
    //					if (itm.Text  == text)
    //						return itm;
    //				return null;
    //			}
    //		}
    //
    //		public int IndexOf(PopUpItem value)
    //		{
    //			return base.List.IndexOf(value);
    //		}
    //
    //		//			public PopUpItem Add(string text)
    //		//			{
    //		//				PopUpItem item = new PopUpItem();
    //		//				item.Text =text;
    //		//				return Add(item);
    //		//			}
    //		
    //		public void CopyTo(PopUpItem[] array, System.Int32 index)
    //		{
    //			PopUpItem[] itms=new PopUpItem[this.Count];
    //			int i=0;
    //			foreach (PopUpItem obj in base.List)
    //			{
    //				array.SetValue(obj,i);
    //				i++;
    //			}
    //		}
    //
    //		public void CopyTo(object[] array, System.Int32 index)
    //		{
    //			object[] itms=new object[this.Count];
    //			int i=0;
    //			foreach (PopUpItem obj in base.List)
    //			{
    //				array.SetValue(obj.Text,i);
    //				i++;
    //			}
    //		}
    //
    //		public void CopyTo(BaseMc.McListItems.ObjectCollection array)
    //		{
    //			foreach (PopUpItem obj in base.List)
    //			{
    //				array.Add(obj.Text);
    //			}
    //		}
    //
    //		#region AddProperty
    //
    //		public PopUpItem AddItem(string Text)
    //		{
    //			return AddItem(Text,null, -1,true,true);
    //		}
    //		public PopUpItem AddItem(string Text,bool Enabled,bool Visible)
    //		{
    //			return AddItem(Text,null, -1,Enabled,Visible);
    //		}
    //		public PopUpItem AddItem(string Text,int imageIndex)
    //		{
    //			return AddItem(Text,null, imageIndex,true,true);
    //		}
    //		public PopUpItem AddItem(string Text,object tag, int imageIndex)
    //		{
    //			return AddItem(Text,tag, imageIndex,true,true);
    //		}
    //
    //		public PopUpItem AddItem(string Text,int imageIndex,bool Enabled,bool Visible)
    //		{
    //			return AddItem(Text,null, imageIndex,Enabled,Visible);
    //		}
    //
    //		public PopUpItem AddItem(string Text,object tag, int imageIndex,bool Enabled,bool Visible)
    //		{
    //			PopUpItem item=new  PopUpItem ();
    //			int indx=this.Count;
    //			item.Text =Text;
    //			item.Tag=tag;
    //			item.ImageIndex  =imageIndex;
    //			item.ImageList=this.owner.ImageList;
    //			//item.Enabled  =Enabled;
    //			item.Visible  =Visible;
    //			item.owner=this.owner;
    //			return Add(item);
    //		}

    //		#endregion
    //	}
    #endregion

}