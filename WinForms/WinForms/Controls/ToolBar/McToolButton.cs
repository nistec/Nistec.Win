using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;

using Nistec.Drawing;

namespace Nistec.WinForms
{

    // Summary:
    // Specifies the button style within a toolbar.
    public enum ToolButtonStyle
    {
        // Summary:
        //     A standard, three-dimensional button.
        CheckButton = 1,
        //
        // Summary:
        //     A toggle button that appears sunken when clicked and retains the sunken appearance
        //     until clicked again.
        Button = 2,
        //
        // Summary:
        //     A space or line between toolbar buttons. The appearance depends on the value
        //     of the System.Windows.Forms.ToolBar.Appearance property.
        Separator = 3,
        //
        // Summary:
        //     A drop-down control that displays a menu or other window when clicked.
        DropDownButton = 4,
    }

    //[ToolboxItem(false),ToolboxBitmap(typeof(McToolButton), "Toolbox.McToolButton.bmp")]
    [ToolboxItem(false), Designer(typeof(Design.ToolButtonDesigner))]
    public class McToolButton : McButtonBase, IMcToolButton, IButton
    {

        #region membrs

        internal bool IsDropButton;
        //internal McToolDropDown owner;
        //private ToolTip toolTip;
        private const int btnDropWidth = 11;
        private readonly static Image dropDownImage;

        // Events
        public event EventHandler StartDragDrop;

        // Fields
        private bool allowAllUp;
        private bool draw3DButton;
        private Menu dropDownMenu;
        private bool hotDragDrop;
        protected bool IsMouseOver;
        protected bool IsPressed;
        private bool lockClick;
        private bool pushed;
        private ToolButtonStyle buttonStyle;
        private bool wordWrap;
        //private IToolBar parentBar;
        private PopUpItem selectedPopUpItem;
        private int groupIndex;
        //private string toolTipText;
        //private bool autoToolTip;
        private string optionGroup;

        #endregion

        #region Ctor

        static McToolButton()
        {
            McToolButton.dropDownImage = ResourceUtil.LoadImage(Global.ImagesPath + "menuarrow.gif");
        }

        public McToolButton()
        {
            this.IsDropButton = false;
            //this.owner=null;
            //this.toolTipText=null;
            //this.autoToolTip=false;
            this.groupIndex = -1;
            this.lockClick = false;
            this.IsMouseOver = false;
            this.IsPressed = false;
            this.wordWrap = false;
            this.hotDragDrop = false;
            this.draw3DButton = false;
            this.allowAllUp = false;
            this.dropDownMenu = null;
            this.buttonStyle = ToolButtonStyle.Button;
            this.pushed = false;
            this.selectedPopUpItem = null;
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, false);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Opaque, false);
            this.TabStop = false;
            base.Height = 0x16;
            base.Width = 0x16;
            this.BackColor = Color.Transparent;
        }

        protected override void Dispose(bool disposing)
        {
            McPopUpDispose();
            base.Dispose(disposing);
        }
        #endregion

        #region Properties

    

        [Category("Behavior"), DefaultValue(-1)]
        public int GroupIndex
        {
            get
            {
                return this.groupIndex;
            }
            set
            {
                this.groupIndex = value;
            }
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowAllUp
        {
            get
            {
                return this.allowAllUp;
            }
            set
            {
                this.allowAllUp = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false), DefaultValue(false)]
        internal bool Draw3DButton
        {
            get
            {
                return this.draw3DButton;
            }
            set
            {
                this.draw3DButton = value;
            }
        }

        [Category("Behavior"), DefaultValue(null)]
        public Menu DropDownMenu
        {
            get
            {
                return this.dropDownMenu;
            }
            set
            {
                this.dropDownMenu = value;
            }
        }

        //private FlatStyle flatStyle;

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        //public new FlatStyle FlatStyle
        //{
        //    get
        //    {
        //        return base.FlatStyle;
        //    }
        //    set
        //    {
        //        base.FlatStyle = value;
        //    }
        //}

        [Browsable(false), DefaultValue(false)]
        public bool HotDragDrop
        {
            get
            {
                return this.hotDragDrop;
            }
            set
            {
                this.hotDragDrop = value;
            }
        }

        [DefaultValue(false), Category("Behavior")]
        public bool Checked
        {
            get
            {
                return this.pushed;
            }
            set
            {
                if (this.buttonStyle == ToolButtonStyle.CheckButton)
                {
                    if (value && this.AllowAllUp)
                    {
                        this.ResetPushed();
                    }
                    this.pushed = value;
                }
                else
                {
                    this.pushed = false;
                }
                base.Invalidate();
            }
        }

        [DefaultValue(2), Category("Behavior")]
        public ToolButtonStyle ButtonStyle
        {
            get
            {
                return this.buttonStyle;
            }
            set
            {
                this.buttonStyle = value;
                this.IsDropButton = (value == ToolButtonStyle.DropDownButton);
                base.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Category("Behavior"), DefaultValue(false)]
        public bool WordWrap
        {
            get
            {
                return this.wordWrap;
            }
            set
            {
                this.wordWrap = value;
                base.Invalidate();
            }
        }

        [Category("Behavior"), DefaultValue((string)null), Description("Select OptionGroup in CheckButton style when you need one button only checked in group at the time")]
        public string OptionGroup
        {
            get
            {
                return this.optionGroup;
            }
            set
            {
                if (this.buttonStyle == ToolButtonStyle.CheckButton)
                {
                    this.optionGroup = value;
                }
            }
        }

        //[Category("Behavior")]
        //public IToolBar ParentBar
        //{
        //    get
        //    {
        //        if (this.parentBar == null)
        //        {
        //            if (this.Parent != null && this.Parent is IToolBar)
        //            {
        //                this.parentBar = (IToolBar)this.Parent;
        //            }
        //        }
        //        return this.parentBar;
        //    }
        //    set
        //    {
        //        this.parentBar = value;
        //        //base.Invalidate();
        //    }
        //}


        //[Category("Behavior"), DefaultValue("")]
        //public virtual String ToolTip
        //{
        //    get { return toolTip.GetToolTip(this); }
        //    set
        //    {
        //        toolTip.RemoveAll();
        //        toolTip.SetToolTip(this, value);
        //    }
        //}

        //[Category("Behavior"), DefaultValue(false)]
        //public virtual bool AutoToolTip
        //{
        //    get { return autoToolTip; }
        //    set
        //    {
        //        autoToolTip = value;
        //    }
        //}

        //[Category("Behavior"), DefaultValue(null), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Localizable(true), Description("ToolTipText")]
        //public string ToolTipText
        //{
        //    get
        //    {

        //        if (!this.AutoToolTip || !string.IsNullOrEmpty(this.toolTipText))
        //        {
        //            return this.toolTipText;
        //        }
        //        string text = this.Text;
        //        if (McToolTip.ContainsMnemonic(text))
        //        {
        //            text = string.Join("", text.Split(new char[] { '&' }));
        //        }
        //        return text;
        //    }
        //    set
        //    {
        //        this.toolTipText = value;

        //    }
        //}

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    if (!string.IsNullOrEmpty(this.ToolTipText))
        //    {
        //        McToolTip.Instance.SetToolTip(this, ToolTipText);
        //    }
        //}

        #endregion

        #region methods

        private void CalcRectangles(out Rectangle textRect, out Rectangle imageRect, Rectangle rect, Graphics g)
        {

            SizeF ef1;
            Size size1 = Size.Empty;
            if (((base.ImageList != null) && (base.ImageIndex >= 0)) && (base.ImageIndex < base.ImageList.Images.Count))
            {
                size1 = base.ImageList.ImageSize;
            }
            try
            {
                if (size1.IsEmpty && (base.Image != null))
                {
                    size1 = new Size(base.Image.Width, base.Image.Height);
                }
            }
            catch
            {
                size1 = new Size(0x10, 0x10);
            }
            using (StringFormat format1 = this.GetStringFormat())
            {
                ef1 = g.MeasureString(this.Text, this.Font, new SizeF((float)rect.Width, (float)rect.Height), format1);
            }
            textRect = rect;
            imageRect = new Rectangle(rect.X, rect.Y, size1.Width, size1.Height);
            //if(this.IsDropButton)
            //{
            //    int numf = (int) ((rect.Width - size1.Width) / 2f);
            //    imageRect.X = rect.X + numf+1;
            //    imageRect.Y = rect.Y + ((rect.Height - size1.Height) / 2);
            //}
            if ((this.TextAlign == ContentAlignment.MiddleCenter) && (base.ImageAlign == ContentAlignment.MiddleCenter))
            {
                textRect.X += size1.Width;
                textRect.Width -= size1.Width;
                int num1 = (int)(((rect.Width - size1.Width) - ef1.Width) / 2f);
                imageRect.X = rect.X + num1;
                imageRect.Y = rect.Y + ((rect.Height - size1.Height) / 2);
            }
            else
            {
                ContentAlignment alignment1 = base.ImageAlign;
                if (alignment1 <= ContentAlignment.MiddleCenter)
                {
                    switch (alignment1)
                    {
                        case ContentAlignment.TopLeft:
                            {
                                imageRect.X = rect.X;
                                imageRect.Y = rect.Y;
                                return;
                            }
                        case ContentAlignment.TopCenter:
                            {
                                imageRect.X = rect.X + ((rect.Width - size1.Width) / 2);
                                imageRect.Y = rect.Y;
                                return;
                            }
                        case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                            {
                                return;
                            }
                        case ContentAlignment.TopRight:
                            {
                                imageRect.X = rect.Right - size1.Width;
                                imageRect.Y = rect.Y;
                                return;
                            }
                        case ContentAlignment.MiddleLeft:
                            {
                                imageRect.X = rect.X;
                                imageRect.Y = rect.Y + ((rect.Height - size1.Height) / 2);
                                return;
                            }
                        case ContentAlignment.MiddleCenter:
                            {
                                imageRect.X = rect.X + ((rect.Width - size1.Width) / 2);
                                imageRect.Y = rect.Y + ((rect.Height - size1.Height) / 2);
                                return;
                            }
                    }
                }
                else if (alignment1 <= ContentAlignment.BottomLeft)
                {
                    if (alignment1 == ContentAlignment.MiddleRight)
                    {
                        imageRect.X = rect.Right - size1.Width;
                        imageRect.Y = rect.Y + ((rect.Height - size1.Height) / 2);
                    }
                    else if (alignment1 == ContentAlignment.BottomLeft)
                    {
                        imageRect.X = rect.X;
                        imageRect.Y = rect.Bottom - size1.Height;
                    }
                }
                else if (alignment1 == ContentAlignment.BottomCenter)
                {
                    imageRect.X = rect.X + ((rect.Width - size1.Width) / 2);
                    imageRect.Y = rect.Bottom - size1.Height;
                }
                else if (alignment1 == ContentAlignment.BottomRight)
                {
                    imageRect.X = rect.Right - size1.Width;
                    imageRect.Y = rect.Bottom - size1.Height;
                }
            }
        }


        private void DrawImage(Graphics g, Rectangle imageRect)
        {
            if ((((base.ImageList != null) && (base.ImageIndex >= 0)) && (base.ImageIndex < base.ImageList.Images.Count)) || (base.Image != null))
            {
                if (((base.ImageList != null) && (base.ImageIndex >= 0)) && (base.ImageIndex < base.ImageList.Images.Count))
                {
                    if (base.Enabled)
                    {
                        base.ImageList.Draw(g, imageRect.X, imageRect.Y, imageRect.Width, imageRect.Height, base.ImageIndex);
                    }
                    else
                    {
                        McPaint.DrawImageDisabled(g, base.ImageList.Images[base.ImageIndex], imageRect.Left, imageRect.Top);
                    }
                }
                else if (base.Enabled)
                {
                    g.DrawImage(base.Image, imageRect.X, imageRect.Y, imageRect.Width, imageRect.Height);
                }
                else
                {
                    McPaint.DrawImageDisabled(g, base.Image, imageRect.Left, imageRect.Top);
                }
            }
        }

        private void DrawText(Graphics g, Rectangle textRect)
        {
            if ((this.Text != null) && (this.Text.Length > 0))
            {
                using (StringFormat format1 = this.GetStringFormat())
                {
                    if (base.Enabled)
                    {
                        using (SolidBrush brush1 = new SolidBrush(this.ForeColor))
                        {
                            g.DrawString(this.Text, this.Font, brush1, (RectangleF)textRect, format1);
                            return;
                        }
                    }
                    ControlPaint.DrawStringDisabled(g, this.Text, this.Font, SystemColors.ControlLight, (RectangleF)textRect, format1);
                }
            }
        }


        private StringFormat GetStringFormat()
        {
            StringFormat format1 = new StringFormat();
            format1.Trimming = StringTrimming.EllipsisCharacter;
            format1.FormatFlags = 0;
            if (!this.wordWrap)
            {
                format1.FormatFlags = StringFormatFlags.NoWrap;
            }
            format1.HotkeyPrefix = HotkeyPrefix.Show;
            if (this.RightToLeft == RightToLeft.Yes)
            {
                format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            if ((this.TextAlign == ContentAlignment.MiddleCenter) && (base.ImageAlign == ContentAlignment.MiddleCenter))
            {
                format1.Alignment = StringAlignment.Center;
                format1.LineAlignment = StringAlignment.Center;
                return format1;
            }
            ContentAlignment alignment1 = this.TextAlign;
            if (alignment1 <= ContentAlignment.MiddleCenter)
            {
                switch (alignment1)
                {
                    case ContentAlignment.TopLeft:
                        {
                            format1.Alignment = StringAlignment.Near;
                            format1.LineAlignment = StringAlignment.Near;
                            return format1;
                        }
                    case ContentAlignment.TopCenter:
                        {
                            format1.Alignment = StringAlignment.Center;
                            format1.LineAlignment = StringAlignment.Near;
                            return format1;
                        }
                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        {
                            return format1;
                        }
                    case ContentAlignment.TopRight:
                        {
                            format1.Alignment = StringAlignment.Far;
                            format1.LineAlignment = StringAlignment.Near;
                            return format1;
                        }
                    case ContentAlignment.MiddleLeft:
                        {
                            format1.Alignment = StringAlignment.Near;
                            format1.LineAlignment = StringAlignment.Center;
                            return format1;
                        }
                    case ContentAlignment.MiddleCenter:
                        {
                            format1.Alignment = StringAlignment.Center;
                            format1.LineAlignment = StringAlignment.Center;
                            return format1;
                        }
                }
                return format1;
            }
            if (alignment1 <= ContentAlignment.BottomLeft)
            {
                if (alignment1 == ContentAlignment.MiddleRight)
                {
                    format1.Alignment = StringAlignment.Far;
                    format1.LineAlignment = StringAlignment.Center;
                    return format1;
                }
                if (alignment1 == ContentAlignment.BottomLeft)
                {
                    format1.Alignment = StringAlignment.Near;
                    format1.LineAlignment = StringAlignment.Far;
                }
                return format1;
            }
            if (alignment1 == ContentAlignment.BottomCenter)
            {
                format1.Alignment = StringAlignment.Center;
                format1.LineAlignment = StringAlignment.Far;
                return format1;
            }
            if (alignment1 == ContentAlignment.BottomRight)
            {
                format1.Alignment = StringAlignment.Far;
                format1.LineAlignment = StringAlignment.Far;
            }
            return format1;
        }

        private void ResetPushed()
        {
            foreach (Control control1 in base.Parent.Controls)
            {
                if (((control1 != this) && (control1 is McToolButton)) && ((McToolButton)control1).Checked)
                {
                    ((McToolButton)control1).Checked = false;
                }
            }
        }

        #endregion

        #region override

        protected override void OnClick(EventArgs e)
        {
            if (!this.lockClick)
            {
                this.lockClick = true;
                if (this.ctlPopUp != null && this.ctlPopUp.MenuItems.Count > 0)
                {
                    //this.ctlPopUp.ShowPopUp();
                    this.ctlPopUp.DrawItemStyle = this.DrawItemStyle;
                    this.ctlPopUp.CalcDropDownWidth();
                    this.ctlPopUp.ShowPopUp(this.PointToScreen(new Point(0, this.Height + 1)));
                }
                else if (this.DropDownMenu != null)
                {
                    //if(this.owner!=null)
                    //	this.DropDownMenu.GetContextMenu().Show(this.owner, new Point(0, base.Height));
                    //else
                    this.DropDownMenu.GetContextMenu().Show(this, new Point(0, base.Height));
                }
                else if (this.buttonStyle == ToolButtonStyle.CheckButton)
                {
                    //this.Checked = !this.Checked;
                    OnParentBarButtonCheck();
                }
                else
                {
                    OnParentBarButtonClick();
                }
                base.OnClick(e);
                //if (!string.IsNullOrEmpty(this.ToolTipText))
                //{
                //    McToolTip.Instance.SetToolTip(this, ToolTipText);
                //}

            }
        }

        private void OnParentBarButtonClick()
        {
            if (this.Parent!=null && this.Parent is McToolBar)//  this.ParentBar != null)
               ((McToolBar) this.Parent).InvokeButtonClick(this);
        }

        private void OnParentBarButtonCheck()
        {
            if (!string.IsNullOrEmpty(this.optionGroup))
            {
                this.Checked = true;
                if (this.Parent != null && this.Parent is McToolBar)
                    ((McToolBar)this.Parent).InvokeButtonChecked(this);
            }
            else
            {
                this.Checked = !this.Checked;
                if (this.Parent != null && this.Parent is McToolBar)
                    ((McToolBar)this.Parent).InvokeButtonClick(this);
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.DropDownMenu != null)
            {
                //if(this.owner!=null)
                //this.DropDownMenu.GetContextMenu().Show(this.owner, new Point(0, base.Height));
                //else
                this.DropDownMenu.GetContextMenu().Show(this, new Point(0, base.Height));
            }
            else
            {
                this.IsPressed = true;
            }
            base.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.IsMouseOver = true;
            base.Invalidate();
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.IsMouseOver = false;
            base.Invalidate();
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point point1 = base.PointToClient(Cursor.Position);
            if ((this.IsPressed && this.HotDragDrop) && !base.ClientRectangle.Contains(point1))
            {
                if (this.StartDragDrop != null)
                {
                    this.StartDragDrop(this, EventArgs.Empty);
                }
                this.IsPressed = false;
                base.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.lockClick = false;
            this.IsPressed = false;
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs p)
        {
            //base.OnPaint(p);
            Rectangle rectangle2;
            Rectangle rectangle3;
            Graphics graphics1 = p.Graphics;
            Rectangle rectangle1 = new Rectangle(0, 0, base.Width, base.Height);

            if (this.buttonStyle == ToolButtonStyle.DropDownButton)
            {
                this.DrawButtonDropDown(graphics1, rectangle1);
                goto Label_01C0;
            }
            if (this.ButtonStyle == ToolButtonStyle.Separator)
            {
                if ((this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom))
                {
                    int num1 = (base.Height / 2) - 1;
                    graphics1.DrawLine(SystemPens.ControlDark, 2, num1, base.Width - 4, num1);
                    graphics1.DrawLine(SystemPens.ControlLightLight, 2, num1 + 1, base.Width - 4, num1 + 1);
                    return;
                }
                int num2 = (base.Width / 2) - 1;
                graphics1.DrawLine(SystemPens.ControlDark, num2, 2, num2, base.Height - 4);
                graphics1.DrawLine(SystemPens.ControlLightLight, num2 + 1, 2, num2 + 1, base.Height - 4);
                return;
            }
            if (!base.Enabled || (!this.Checked && !this.IsPressed))
            {
                goto Label_01B0;
            }
            rectangle1.X++;
            rectangle1.Y++;
            rectangle1.Width -= 2;
            rectangle1.Height -= 2;
            if (this.IsPressed)
            {
                using (SolidBrush brush1 = new SolidBrush(McPaint.Dark(McColors.Selected, 50)))
                {
                    graphics1.FillRectangle(brush1, rectangle1);
                    goto Label_014A;
                }
            }
            if (!this.IsDropButton)
                graphics1.FillRectangle(McBrushes.Selected, rectangle1);
        Label_014A:
            rectangle1.Width--;
            rectangle1.Height--;
            if (!this.IsDropButton)
                graphics1.DrawRectangle(McPens.SelectedText, rectangle1);
            rectangle1.X--;
            rectangle1.Y--;
            rectangle1.Width += 3;
            rectangle1.Height += 3;
        Label_01B0:
            if (!this.IsPressed)
            {
                if (this.IsMouseOver && !base.DesignMode)
                {
                    if (!this.IsDropButton)
                        graphics1.FillRectangle(McBrushes.Focus, rectangle1);
                    rectangle1.Width--;
                    rectangle1.Height--;
                    if (!this.IsDropButton)
                        graphics1.DrawRectangle(McPens.SelectedText, rectangle1);

                }
                else if (base.DesignMode || this.Draw3DButton)
                {
                    ControlPaint.DrawBorder(graphics1, rectangle1, Color.LightGray /*McPens.SelectedText.Color*/, ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder3D(graphics1, rectangle1, Border3DStyle.RaisedInner, Border3DSide.All);
                }
            }

        Label_01C0:
            rectangle1 = new Rectangle(0, 0, this.IsDropButton ? base.Width - btnDropWidth : base.Width, base.Height);
            this.CalcRectangles(out rectangle3, out rectangle2, new Rectangle(rectangle1.X + 4, rectangle1.Y + 4, rectangle1.Width - 8, rectangle1.Height - 8), graphics1);
            try
            {
                this.DrawImage(graphics1, rectangle2);
                this.DrawText(graphics1, rectangle3);
            }
            catch
            {
                return;
            }

        }

        protected virtual void DrawButtonDropDown(Graphics g, Rectangle rectangle1)
        {
            //base.OnPaint(p);
            //Rectangle rectangle2;
            //Rectangle rectangle3;
            //Rectangle rectangle1 = new Rectangle(0, 0, base.Width, base.Height);
            if (!base.Enabled || (!this.pushed && !this.IsPressed))
            {
                goto Label_01B0;
            }
            rectangle1.X++;
            rectangle1.Y++;
            rectangle1.Width -= 2;
            rectangle1.Height -= 2;
            if (this.IsPressed)
            {
                using (SolidBrush brush1 = new SolidBrush(McPaint.Dark(McColors.Selected, 50)))
                {
                    g.FillRectangle(brush1, rectangle1);
                    goto Label_014A;
                }
            }
            g.FillRectangle(McBrushes.Selected, rectangle1);
        Label_014A:
            rectangle1.Width--;
            rectangle1.Height--;
            g.DrawRectangle(McPens.SelectedText, rectangle1);
            g.DrawLine(McPens.SelectedText, rectangle1.Right - btnDropWidth, rectangle1.Top, rectangle1.Right - btnDropWidth, rectangle1.Bottom);
            rectangle1.X--;
            rectangle1.Y--;
            rectangle1.Width += 3;
            rectangle1.Height += 3;

        Label_01B0:
            if (!this.IsPressed)
            {
                if (this.IsMouseOver && !base.DesignMode)
                {
                    g.FillRectangle(McBrushes.Focus, rectangle1);
                    rectangle1.Width--;
                    rectangle1.Height--;
                    g.DrawRectangle(McPens.SelectedText, rectangle1);
                    g.DrawLine(McPens.SelectedText, rectangle1.Right - btnDropWidth, rectangle1.Top, rectangle1.Right - btnDropWidth, rectangle1.Bottom);
                }
                else if (base.DesignMode || this.Draw3DButton)
                {
                    ControlPaint.DrawBorder(g, rectangle1,Color.LightGray /*McPens.SelectedText.Color*/ , ButtonBorderStyle.Solid);
                    //ControlPaint.DrawBorder3D(g, rectangle1, Border3DStyle.RaisedInner, Border3DSide.All);
                }
            }
            int imageWidth = McToolButton.dropDownImage.Width;
            g.DrawImage(McToolButton.dropDownImage, (rectangle1.Right - btnDropWidth) + (imageWidth / 2), (rectangle1.Height - imageWidth) / 2, imageWidth, imageWidth);
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            McColors.InitColors();
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

        private void McPopUpDispose()
        {
            if (this.ctlPopUp == null) return;
            this.ctlPopUp.SelectedValueChanged += new EventHandler(ctlPopUp_SelectedValueChanged);
            this.ctlPopUp.Dispose();
            this.ctlPopUp = null;
        }

        private void ctlPopUp_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.ctlPopUp == null) return;
            this.selectedPopUpItem = this.ctlPopUp.SelectedItem;
            OnSelectedItemClick(new SelectedPopUpItemEvent(this.ctlPopUp.SelectedItem, this.ctlPopUp.SelectedIndex));
        }

        protected virtual void OnSelectedItemClick(SelectedPopUpItemEvent e)
        {
            if (SelectedItemClick != null)
            {
                SelectedItemClick(this, e);
            }
            OnParentBarButtonClick();
        }

        //		[Browsable(false), Category("Items"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //		public McPopUp PopUp
        //		{
        //			get
        //			{
        //				//if (this.ctlPopUp == null)
        //				//{
        //				//    InitMcPopUp();
        //				//}
        //				return this.ctlPopUp;
        //			}
        //		}

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
                if (this.ctlPopUp == null)
                {
                    InitMcPopUp();
                    //this.menuItems=new Nistec.WinForms.McPopUp.PopUpItemsCollection();
                }
                return this.ctlPopUp.MenuItems;
            }
        }

        public new ImageList ImageList
        {
            get
            {
                return base.ImageList;
            }
            set
            {
                base.ImageList = value;
                if (this.ctlPopUp != null)
                {
                    this.ctlPopUp.ImageList = value;
                }
            }
        }
        [Browsable(false)]
        public PopUpItem SelectedPopUpItem
        {
            get { return this.selectedPopUpItem; }
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

    }

}
