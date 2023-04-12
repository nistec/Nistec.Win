using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;

using mControl.Drawing;

namespace mControl.WinForms
{
    [Designer(typeof(Design.McPageDesigner)), ToolboxItem(false)]
    public class McPage : McPanel
    {
        // Fields
        //private bool drawBorder;
        private Image image;
        protected int _imageIndex;
        protected ImageList _imageList;
        private string toolTipText = "";
        //internal bool AutoToolTip = false;

        private bool pageVisible;// invisible;
        private ContextMenu titleContextMenu;
        private object tag;

        public McPage()
            : this("McPage")
        {
            base.BorderStyle = BorderStyle.FixedSingle;
        }

        public McPage(string text)
        {
            base.BorderStyle = BorderStyle.FixedSingle;
            base.autoChildrenStyle = true;
            
            this.toolTipText = "";
            //this.AutoToolTip = false;

            int num1;
            this.image = null;
            this._imageIndex = -1;
            //this.drawBorder = true;
            this.pageVisible = true;
            this.Text = text;
            this.tag = null;
            this.DockPadding.Right = num1 = 4;
            this.DockPadding.Bottom = num1;
            this.DockPadding.Left = num1;
            this.DockPadding.Top = num1;
            this.Visible = false;
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
        }


        protected override void OnPaint(PaintEventArgs p)
        {
            base.OnPaint(p);
            //if(!(this.Parent is McTabPages))
            //	return;
            //			Graphics graphics1 = p.Graphics;
            //			Rectangle rectangle1 = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            //			if (this.DrawBorder && !this.Invisible)
            //			{
            //				if (!((McTabPages) base.Parent).PositionAtBottom)
            //				{
            //					graphics1.DrawLine(SystemPens.ControlLightLight, rectangle1.X, rectangle1.Y, rectangle1.X, rectangle1.Bottom);
            //					graphics1.DrawLine(SystemPens.ControlDark, rectangle1.X, rectangle1.Bottom, rectangle1.Right, rectangle1.Bottom);
            //					graphics1.DrawLine(SystemPens.ControlDark, rectangle1.Right, rectangle1.Top, rectangle1.Right, rectangle1.Bottom);
            //					graphics1.DrawLine(SystemPens.ControlDark, rectangle1.X, rectangle1.Top, rectangle1.Left, rectangle1.Bottom);
            //				}
            //				else
            //				{
            //					graphics1.DrawLine(SystemPens.ControlLightLight, rectangle1.X, rectangle1.Y, rectangle1.X, rectangle1.Bottom);
            //					graphics1.DrawLine(SystemPens.ControlLightLight, rectangle1.X, rectangle1.Top, rectangle1.Right, rectangle1.Top);
            //					graphics1.DrawLine(SystemPens.ControlDark, rectangle1.Right, rectangle1.Top, rectangle1.Right, rectangle1.Bottom);
            //				}
            //			}
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            McColors.InitColors();
        }

        //		[Browsable(false)]
        //		public BorderStyle BorderStyle
        //		{
        //			get
        //			{
        //				return base.BorderStyle;
        //			}
        //			set
        //			{
        //				base.BorderStyle = value;
        //			}
        //		}

        [Browsable(false)]
        public new DockStyle Dock
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new ScrollableControl.DockPaddingEdges DockPadding
        {
            get
            {
                return base.DockPadding;
            }
        }

        //		[Category("Appearance"), DefaultValue(true)]
        //		public bool DrawBorder
        //		{
        //			get
        //			{
        //				return this.drawBorder;
        //			}
        //			set
        //			{
        //				this.drawBorder = value;
        //				//if(value)
        //				//	base.BorderStyle=BorderStyle.FixedSingle;
        //				base.Invalidate();
        //				if (base.Parent != null)
        //				{
        //					base.Parent.Invalidate();
        //				}
        //			}
        //		}

        //		[DefaultValue(null)]
        //		public ImageList ImageList
        //		{
        //			get { return _imageList; }
        //		
        //			set 
        //			{ 
        //				if (_imageList != value)
        //				{
        //					_imageList = value; 
        //				}
        //			}
        //		}

        private McTabPages Owner()
        {
            McTabPages owner = null;
            if (this.Parent != null)
            {
                owner = (McTabPages)this.Parent as McTabPages;
            }
            return owner;
        }

        [Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        public int ImageIndex
        {
            get
            {

                McTabPages owner = Owner();
                if (owner == null) return -1;
                if (((this._imageIndex != -1) && (owner.ImageList != null)) && (this._imageIndex >= owner.ImageList.Images.Count))
                {
                    return (owner.ImageList.Images.Count - 1);
                }
                return this._imageIndex;
            }
            set
            {
                McTabPages owner = Owner();
                if (owner == null) return;
                if (value < -1)
                {
                    throw new ArgumentException("InvalidLowBoundArgumentEx");
                }
                if (this._imageIndex != value)
                {
                    if (value != -1 && owner.ImageList != null)
                    {
                        this.Image = owner.ImageList.Images[value];// null;
                    }
                    this._imageIndex = value;
                    base.Invalidate();
                }
            }
        }

        [Category("Appearance"), DefaultValue((string)null)]
        public virtual Image Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                base.Invalidate();
                if (base.Parent != null)
                {
                    base.Parent.Invalidate();
                }
            }
        }

        [DefaultValue(true), Category("Appearance")]
        public bool PageVisible// Invisible
        {
            get
            {
                return this.pageVisible;
            }
            set
            {
                this.pageVisible = value;
                if (!base.DesignMode)
                {
                    McTabPages control1 = base.Parent as McTabPages;
                    if (control1 != null)
                    {
                        if ((control1.SelectedTab == null) && this.pageVisible)
                        {
                            control1.SelectedTab = this;
                        }
                        else if (control1.SelectedTab == this)
                        {
                            foreach (McPage page1 in control1.Controls)
                            {
                                if (!page1.PageVisible)
                                {
                                    control1.SelectedTab = page1;
                                    break;
                                }
                            }
                        }
                        if ((control1.SelectedTab == this) && !this.pageVisible)
                        {
                            control1.SelectedTab = null;
                        }
                    }
                }
                base.Invalidate();
                if (base.Parent != null)
                {
                    base.Parent.Invalidate();
                }
            }
        }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                base.Invalidate();
                if (base.Parent != null)
                {
                    base.Parent.Invalidate();
                }
            }
        }

        [DefaultValue(null)]
        public new object Tag
        {
            get
            {
                return this.tag;
            }
            set
            {
                this.tag = value;
            }
        }

        [Category("Behavior")]
        public ContextMenu TitleContextMenu
        {
            get
            {
                return this.titleContextMenu;
            }
            set
            {
                this.titleContextMenu = value;
            }
        }

        [Browsable(false)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if ((base.Parent != null) && (this == ((McTabPages)base.Parent).SelectedTab))
                {
                    base.Visible = true;
                    this.Dock = DockStyle.Fill;
                }
                else
                {
                    base.Visible = value;
                }
            }
        }

 
        [Category("Behavior"), DefaultValue(null), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Localizable(true), Description("ToolTipText")]
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

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnStylePropertyChanged(e);
            this.Invalidate();
        }

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            this.Invalidate();
        }

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoChildrenStyle
        {
            get { return base.AutoChildrenStyle; }
            set { base.AutoChildrenStyle = value; }
        }

        //[Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get { return base.GradientStyle; }
        //    set { base.GradientStyle = value; }
        //}

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]//,DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set { base.ControlLayout = value; }
        }
    }

}
