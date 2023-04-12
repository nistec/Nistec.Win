using System;
using System.Collections.Generic;
using System.Text;

namespace mControl.WinCtl.Develop
{
 [ToolboxItem(false), DesignTimeVisible(false), Designer("System.Windows.Forms.Design.TabPageDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ComVisible(true), ClassInterface(ClassInterfaceType.AutoDispatch), DefaultProperty("Text"), DefaultEvent("Click")]
public class TabPage : Panel
{
    // Fields
    private bool enterFired;
    private ImageList.Indexer imageIndexer;
    private bool leaveFired;
    private string toolTipText;
    private bool useVisualStyleBackColor;

    // Events
    [SRDescription("ControlOnAutoSizeChangedDescr"), Browsable(false), SRCategory("CatPropertyChanged"), EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler AutoSizeChanged
    {
        add
        {
            base.AutoSizeChanged += value;
        }
        remove
        {
            base.AutoSizeChanged -= value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public event EventHandler DockChanged
    {
        add
        {
            base.DockChanged += value;
        }
        remove
        {
            base.DockChanged -= value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public event EventHandler EnabledChanged
    {
        add
        {
            base.EnabledChanged += value;
        }
        remove
        {
            base.EnabledChanged -= value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public event EventHandler LocationChanged
    {
        add
        {
            base.LocationChanged += value;
        }
        remove
        {
            base.LocationChanged -= value;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler TabIndexChanged
    {
        add
        {
            base.TabIndexChanged += value;
        }
        remove
        {
            base.TabIndexChanged -= value;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler TabStopChanged
    {
        add
        {
            base.TabStopChanged += value;
        }
        remove
        {
            base.TabStopChanged -= value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
    public event EventHandler TextChanged
    {
        add
        {
            base.TextChanged += value;
        }
        remove
        {
            base.TextChanged -= value;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler VisibleChanged
    {
        add
        {
            base.VisibleChanged += value;
        }
        remove
        {
            base.VisibleChanged -= value;
        }
    }

    // Methods
    public TabPage()
    {
        this.toolTipText = "";
        base.SetStyle(ControlStyles.CacheText, true);
        this.Text = null;
    }

    public TabPage(string text) : this()
    {
        this.Text = text;
    }

    internal override void AssignParent(Control value)
    {
        if ((value != null) && !(value is TabControl))
        {
            throw new ArgumentException(SR.GetString("TABCONTROLTabPageNotOnTabControl", new object[] { value.GetType().FullName }));
        }
        base.AssignParent(value);
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
        return new TabPageControlCollection(this);
    }

    internal void FireEnter(EventArgs e)
    {
        this.enterFired = true;
        this.OnEnter(e);
    }

    internal void FireLeave(EventArgs e)
    {
        this.leaveFired = true;
        this.OnLeave(e);
    }

    public static TabPage GetTabPageOfComponent(object comp)
    {
        if (!(comp is Control))
        {
            return null;
        }
        Control parentInternal = (Control) comp;
        while ((parentInternal != null) && !(parentInternal is TabPage))
        {
            parentInternal = parentInternal.ParentInternal;
        }
        return (TabPage) parentInternal;
    }

    internal NativeMethods.TCITEM_T GetTCITEM()
    {
        NativeMethods.TCITEM_T tcitem_t = new NativeMethods.TCITEM_T();
        tcitem_t.mask = 0;
        tcitem_t.pszText = null;
        tcitem_t.cchTextMax = 0;
        tcitem_t.lParam = IntPtr.Zero;
        string text = this.Text;
        this.PrefixAmpersands(ref text);
        if (text != null)
        {
            tcitem_t.mask |= 1;
            tcitem_t.pszText = text;
            tcitem_t.cchTextMax = text.Length;
        }
        int imageIndex = this.ImageIndex;
        tcitem_t.mask |= 2;
        tcitem_t.iImage = this.ImageIndexer.ActualIndex;
        return tcitem_t;
    }

    protected override void OnEnter(EventArgs e)
    {
        if (this.ParentInternal is TabControl)
        {
            if (this.enterFired)
            {
                base.OnEnter(e);
            }
            this.enterFired = false;
        }
    }

    protected override void OnLeave(EventArgs e)
    {
        if (this.ParentInternal is TabControl)
        {
            if (this.leaveFired)
            {
                base.OnLeave(e);
            }
            this.leaveFired = false;
        }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        TabControl control = this.ParentInternal as TabControl;
        if ((Application.RenderWithVisualStyles && this.UseVisualStyleBackColor) && ((control != null) && (control.Appearance == TabAppearance.Normal)))
        {
            Color backColor = this.UseVisualStyleBackColor ? Color.Transparent : this.BackColor;
            Rectangle bounds = LayoutUtils.InflateRect(this.DisplayRectangle, base.Padding);
            Rectangle rectangle2 = new Rectangle(bounds.X - 4, bounds.Y - 2, bounds.Width + 8, bounds.Height + 6);
            TabRenderer.DrawTabPage(e.Graphics, rectangle2);
            if (this.BackgroundImage != null)
            {
                ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, backColor, this.BackgroundImageLayout, bounds, bounds, this.DisplayRectangle.Location);
            }
        }
        else
        {
            base.OnPaintBackground(e);
        }
    }

    private void PrefixAmpersands(ref string value)
    {
        if (value == null)
        {
            goto Label_000D;
        }
        if (value.Length == 0)
        {
        }
        if (value.IndexOf('&') < 0)
        {
        }
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] == '&')
            {
                if ((i < (value.Length - 1)) && (value[i + 1] == '&'))
                {
                    i++;
                }
                builder.Append("&&");
            }
            else
            {
                builder.Append(value[i]);
            }
        }
        value = builder.ToString();
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        Control parentInternal = this.ParentInternal;
        if ((parentInternal is TabControl) && parentInternal.IsHandleCreated)
        {
            Rectangle displayRectangle = parentInternal.DisplayRectangle;
            base.SetBoundsCore(displayRectangle.X, displayRectangle.Y, displayRectangle.Width, displayRectangle.Height, (specified == BoundsSpecified.None) ? BoundsSpecified.None : BoundsSpecified.All);
        }
        else
        {
            base.SetBoundsCore(x, y, width, height, specified);
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    private bool ShouldSerializeLocation()
    {
        if (base.Left == 0)
        {
            return (base.Top != 0);
        }
        return true;
    }

    public override string ToString()
    {
        return ("TabPage: {" + this.Text + "}");
    }

    internal void UpdateParent()
    {
        TabControl control = this.ParentInternal as TabControl;
        if (control != null)
        {
            control.UpdateTab(this);
        }
    }

    // Properties
    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoSize
    {
        get
        {
            return base.AutoSize;
        }
        set
        {
            base.AutoSize = value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Localizable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
    public override AutoSizeMode AutoSizeMode
    {
        get
        {
            return AutoSizeMode.GrowOnly;
        }
        set
        {
        }
    }

    [SRDescription("ControlBackColorDescr"), SRCategory("CatAppearance")]
    public override Color BackColor
    {
        get
        {
            Color backColor = base.BackColor;
            if (backColor == Control.DefaultBackColor)
            {
                TabControl control = this.ParentInternal as TabControl;
                if ((Application.RenderWithVisualStyles && this.UseVisualStyleBackColor) && ((control != null) && (control.Appearance == TabAppearance.Normal)))
                {
                    return Color.Transparent;
                }
            }
            return backColor;
        }
        set
        {
            if (base.DesignMode)
            {
                if (value != Color.Empty)
                {
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this)["UseVisualStyleBackColor"];
                    if (descriptor != null)
                    {
                        descriptor.SetValue(this, false);
                    }
                }
            }
            else
            {
                this.UseVisualStyleBackColor = false;
            }
            base.BackColor = value;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public bool Enabled
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

    [RefreshProperties(RefreshProperties.Repaint), DefaultValue(-1), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), TypeConverter(typeof(ImageIndexConverter)), Localizable(true), SRDescription("TabItemImageIndexDescr")]
    public int ImageIndex
    {
        get
        {
            return this.ImageIndexer.Index;
        }
        set
        {
            if (value < -1)
            {
                object[] args = new object[] { "imageIndex", value.ToString(CultureInfo.CurrentCulture), -1.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", args));
            }
            TabControl control = this.ParentInternal as TabControl;
            if (control != null)
            {
                this.ImageIndexer.ImageList = control.ImageList;
            }
            this.ImageIndexer.Index = value;
            this.UpdateParent();
        }
    }

    internal ImageList.Indexer ImageIndexer
    {
        get
        {
            if (this.imageIndexer == null)
            {
                this.imageIndexer = new ImageList.Indexer();
            }
            return this.imageIndexer;
        }
    }

    [RefreshProperties(RefreshProperties.Repaint), DefaultValue(""), TypeConverter(typeof(ImageKeyConverter)), SRDescription("TabItemImageIndexDescr"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]
    public string ImageKey
    {
        get
        {
            return this.ImageIndexer.Key;
        }
        set
        {
            this.ImageIndexer.Key = value;
            TabControl control = this.ParentInternal as TabControl;
            if (control != null)
            {
                this.ImageIndexer.ImageList = control.ImageList;
            }
            this.UpdateParent();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public Point Location
    {
        get
        {
            return base.Location;
        }
        set
        {
            base.Location = value;
        }
    }

    [Browsable(false), DefaultValue(typeof(Size), "0, 0"), EditorBrowsable(EditorBrowsableState.Never)]
    public override Size MaximumSize
    {
        get
        {
            return base.MaximumSize;
        }
        set
        {
            base.MaximumSize = value;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public override Size MinimumSize
    {
        get
        {
            return base.MinimumSize;
        }
        set
        {
            base.MinimumSize = value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public Size PreferredSize
    {
        get
        {
            return base.PreferredSize;
        }
    }

    internal override bool RenderTransparencyWithVisualStyles
    {
        get
        {
            return true;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public int TabIndex
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

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool TabStop
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

    [Localizable(true), EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
    public override string Text
    {
        get
        {
            return base.Text;
        }
        set
        {
            base.Text = value;
            this.UpdateParent();
        }
    }

    [SRDescription("TabItemToolTipTextDescr"), Localizable(true), DefaultValue("")]
    public string ToolTipText
    {
        get
        {
            return this.toolTipText;
        }
        set
        {
            if (value == null)
            {
                value = "";
            }
            if (value == this.toolTipText)
            {
            }
            this.toolTipText = value;
            this.UpdateParent();
        }
    }

    [DefaultValue(false), SRDescription("TabItemUseVisualStyleBackColorDescr"), SRCategory("CatAppearance")]
    public bool UseVisualStyleBackColor
    {
        get
        {
            return this.useVisualStyleBackColor;
        }
        set
        {
            this.useVisualStyleBackColor = value;
            base.Invalidate(true);
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public bool Visible
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

    // Nested Types
    [ComVisible(false)]
    public class TabPageControlCollection : Control.ControlCollection
    {
        // Methods
        public TabPageControlCollection(TabPage owner) : base(owner)
        {
        }

        public override void Add(Control value)
        {
            if (value is TabPage)
            {
                throw new ArgumentException(SR.GetString("TABCONTROLTabPageOnTabPage"));
            }
            base.Add(value);
        }
    }
}

}
