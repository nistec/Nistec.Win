
using System;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using Nistec.Collections;
using Nistec.Win;

namespace Nistec.WinForms
{
    [ToolboxItem(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("PropertyChanged")]
	[Designer(typeof(Design.TabPageDesigner))]
    public class McTabPage : Nistec.WinForms.McPanel
    {

		#region Propety Enum and events
        // Enumeration of property change events
        public enum Property
        {
            Text,
            Control,
            ImageIndex,
            ImageList,
            Icon,
            Selected,
			PageVisible
		}

        // Declare the property change event signature
        public delegate void PropChangeHandler(McTabPage page, Property prop, object oldValue);

        // Public events
        public event PropChangeHandler PropertyChanged;

		#endregion

		#region Members
        //private string _title;
        private int _imageIndex;
        private ImageList _imageList;
        private Image _icon;
        private bool _selected;
        private Control _startFocus;
        private bool _shown;
        private string toolTipText;
        //internal bool AutoToolTip;

        private bool _pageVisible;

		internal McTabControl owner;

		#endregion

		#region Constructor

        public McTabPage()
        {
            InternalConstruct("Page", null, -1, null);
        }

        public McTabPage(string title)
        {
            InternalConstruct(title, null, -1, null);
        }
			
        public McTabPage(string title, int imageIndex)
        {
            InternalConstruct(title, null, imageIndex, null);
        }

        public McTabPage(string title, ImageList imageList, int imageIndex)
        {
            InternalConstruct(title, imageList, imageIndex, null);
        }

        public McTabPage(string title, Image icon)
        {
            InternalConstruct(title, null, -1, icon);
        }

        protected void InternalConstruct(string title, 
                                         ImageList imageList, 
                                         int imageIndex,
                                         Image icon)
        {
			//base.m_netFram=true;
            // Assign parameters to internal fields
            //_title = title;
			this.Text=title;
            //_control = control;
            _imageIndex = imageIndex;
            _imageList = imageList;
            _icon = icon;
			toolTipText="";
            //AutoToolTip = false;

            // Appropriate defaults
            _selected = false;
			_startFocus = null;
			_pageVisible=true;
			base.BorderStyle=BorderStyle.None;
            base.ControlLayout=ControlLayout.Flat;
            base.BackColor = StyleLayout.DefaultControlColor;

//			if(Parent is McTabControl )
//			{
//                this.owner=(McTabControl)Parent;  
//				//Styles style=((ILayout)Parent).LayoutManager.StylePlan;
//                //if(style!=Styles.Custom )  
//				// m_Style.StylePlan =style;  
//				//m_StyleGuide  =((ILayout)Parent).StyleGuide ;  
//			}
        }

        //internal void Initialize()
        //{
        //    bool oldValue=this.Visible;
        //    this.Visible=true;
        //    this.Visible=oldValue;
        //    //			bool oldValue = _selected;
        //    //			_selected = true;
        //    //			OnPropertyChanged(Property.Selected, oldValue);
        //}
		#endregion

		#region Methods
		protected override void Dispose(bool disposing)
		{
			if(this.DesignMode && this.owner !=null)
			{
				if(this.owner.TabPages.Count==0)
					return;
				if(this.owner.TabPages.Contains(this))
				{
					this.owner.TabPages.Remove(this); 
					this.owner.Invalidate();
				}
			}

			base.Dispose (disposing);
		}

        public virtual void OnPropertyChanged(Property prop, object oldValue)
        {
            // Any attached event handlers?
            if (PropertyChanged != null)
                PropertyChanged(this, prop, oldValue);
        }

		public override IStyleLayout LayoutManager
		{
			get
			{
				 if(this.m_StylePainter!=null)
					return base.m_StylePainter.Layout as IStyleLayout;
				 else if(owner!=null)
					return owner.LayoutManager as IStyleLayout;
				 else
				    return base.LayoutManager;
			}
		}

		#endregion
 
		#region Internal Properties

//		[DefaultValue(null)]
//		internal Control Control
//		{
//			get { return _control; }
//
//			set 
//			{ 
//				if (_control != value)
//				{
//					Control oldValue = _control;
//					_control = value; 
//
//					OnPropertyChanged(Property.Control, oldValue);
//				}
//			}
//		}

		//Fix_Visible
		[DefaultValue(true), Browsable(true),Localizable(true)]
		public bool PageVisible
		{
			get { return _pageVisible; }
			set 
			{
				if(_pageVisible!=value)
				{
					bool oldValue=_pageVisible;
					_pageVisible = value; 
					int index=this.owner.TabPages.IndexOf(this);
					if(index==0 && !value)
					{
						_pageVisible = true; 
						MsgBox.ShowWarning("Unable To change this Property for the first TabPage"); 
					}
					else if(!DesignMode && this._selected)
					{
						_pageVisible = true; 
						MsgBox.ShowWarning("Unable To change this Property for Selected TabPage"); 
						//this.owner.SelectedIndex=0;
					}
					else
					{
						OnPropertyChanged(Property.PageVisible, oldValue);
					}
				}
			}
		}

		internal bool Shown
		{
			get { return _shown; }
			set { _shown = value; }
		}

        [DefaultValue(null)]
        internal Control StartFocus
        {
            get { return _startFocus; }
            set { _startFocus = value; }
        }

		#endregion

		#region public Properties

		[Browsable(true),DefaultValue("Page")]
		[Localizable(true)]
		public override string Text
		{
			get { return base.Text; }
		   
			set 
			{ 
				if (base.Text != value)
				{
					string oldValue = base.Text;
					base.Text = value; 

					OnPropertyChanged(Property.Text, oldValue);
                    if (this.owner != null)
                    {
                        this.owner.OnPageTextChanged(EventArgs.Empty);
                    }
				}
			}
		}

		[DefaultValue(null)]
		public Image Image
		{
			get { return _icon; }
		
			set 
			{ 
				if (_icon != value)
				{
					Image oldValue = _icon;
					_icon = value; 

					OnPropertyChanged(Property.Icon, oldValue);
				}
			}
		}

		//[Browsable(false),DefaultValue(true)]
        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected
		{
			get { return _selected; }

			set
			{
				if (_selected != value)
				{
					//Fix_Visible
					if(!DesignMode && !_pageVisible)
						return;
					bool oldValue = _selected;
					_selected = value;

					OnPropertyChanged(Property.Selected, oldValue);
				}
			}
		}

		[DefaultValue(null)]
		public ImageList ImageList
		{
			get { return _imageList; }
		
			set 
			{ 
				if (_imageList != value)
				{
					ImageList oldValue = _imageList;
					_imageList = value; 

					OnPropertyChanged(Property.ImageList, oldValue);
				}
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
				if (this._imageIndex != value)
				{
					if (value != -1)
					{
						this.Image = null;
					}
					int oldValue=this._imageIndex;
					this._imageIndex = value;
					base.Invalidate();
					OnPropertyChanged(Property.ImageIndex, oldValue);
				}
			}
		}

        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}
        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}
 
        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}
//		[Browsable(true), Localizable(true)]
//		public override string Text
//		{
//			get
//			{
//				return base.Text;
//			}
//			set
//			{
//				base.Text = value;
//				this.UpdateParent();
//			}
//		}
        //[Localizable(true), DefaultValue(""), Description("TabItemToolTipText")]
        //public string ToolTipText
        //{
        //    get
        //    {
        //        return this.toolTipText;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            value = "";
        //        }
        //        if (value != this.toolTipText)
        //        {
        //            this.toolTipText = value;
        //            //this.UpdateParent();
        //        }
        //    }
        //}

 
        [Category("Behavior"), DefaultValue(typeof(String),""), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Localizable(true), Description("ToolTipText")]
        public string ToolTipText
        {
            get
            {

                //if (!this.AutoToolTip || !string.IsNullOrEmpty(this.toolTipText))
                //{
                    return this.toolTipText;
                //}
                //string text = this.Text;
                //if (McToolTip.ContainsMnemonic(text))
                //{
                //    text = string.Join("", text.Split(new char[] { '&' }));
                //}
                //return text;
            }
            set
            {
                this.toolTipText = value;

            }
        }

        [Category("Behavior"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
		[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set{base.AutoChildrenStyle=value;}
		}

        //[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get{return base.GradientStyle;}
        //    set{base.GradientStyle=value;}
        //}
		[Category("Style"),Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ControlLayout ControlLayout
		{
			get{return base.ControlLayout;}
			set{base.ControlLayout=value;}
		}

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = BorderStyle.None; }
        }

		#endregion



    }
}
