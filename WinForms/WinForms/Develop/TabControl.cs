using System;
using System.Collections.Generic;
using System.Text;

namespace mControl.WinCtl.Develop
{
  [DefaultProperty("TabPages"), Designer("System.Windows.Forms.Design.TabControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), SRDescription("DescriptionTabControl"), ComVisible(true), ClassInterface(ClassInterfaceType.AutoDispatch), DefaultEvent("SelectedIndexChanged")]
public class TabControl : Control
{
    // Fields
    private TabAlignment alignment;
    private TabAppearance appearance;
    private Rectangle cachedDisplayRect = Rectangle.Empty;
    private Size cachedSize = Size.Empty;
    private string controlTipText = string.Empty;
    private bool currentlyScaling;
    private static readonly Size DEFAULT_ITEMSIZE = Size.Empty;
    private static readonly Point DEFAULT_PADDING = new Point(6, 3);
    private TabDrawMode drawMode;
    private static readonly object EVENT_DESELECTED = new object();
    private static readonly object EVENT_DESELECTING = new object();
    private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();
    private static readonly object EVENT_SELECTED = new object();
    private static readonly object EVENT_SELECTING = new object();
    private ImageList imageList;
    private Size itemSize = DEFAULT_ITEMSIZE;
    private int lastSelection;
    private Point padding = DEFAULT_PADDING;
    private bool rightToLeftLayout;
    private int selectedIndex = -1;
    private TabSizeMode sizeMode;
    private bool skipUpdateSize;
    private readonly int tabBaseReLayoutMessage = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_TabBaseReLayout");
    private TabPageCollection tabCollection;
    private BitVector32 tabControlState = new BitVector32(0);
    private const int TABCONTROLSTATE_autoSize = 0x100;
    private const int TABCONTROLSTATE_fromCreateHandles = 0x10;
    private const int TABCONTROLSTATE_getTabRectfromItemSize = 8;
    private const int TABCONTROLSTATE_hotTrack = 1;
    private const int TABCONTROLSTATE_insertingItem = 0x80;
    private const int TABCONTROLSTATE_multiline = 2;
    private const int TABCONTROLSTATE_selectFirstControl = 0x40;
    private const int TABCONTROLSTATE_showToolTips = 4;
    private const int TABCONTROLSTATE_UISelection = 0x20;
    private int tabPageCount;
    private TabPage[] tabPages;

    // Events
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler BackColorChanged
    {
        add
        {
            base.BackColorChanged += value;
        }
        remove
        {
            base.BackColorChanged -= value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public event EventHandler BackgroundImageChanged
    {
        add
        {
            base.BackgroundImageChanged += value;
        }
        remove
        {
            base.BackgroundImageChanged -= value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public event EventHandler BackgroundImageLayoutChanged
    {
        add
        {
            base.BackgroundImageLayoutChanged += value;
        }
        remove
        {
            base.BackgroundImageLayoutChanged -= value;
        }
    }

    [SRDescription("TabControlDeselectedEventDescr"), SRCategory("CatAction")]
    public event TabControlEventHandler Deselected
    {
        add
        {
            base.Events.AddHandler(EVENT_DESELECTED, value);
        }
        remove
        {
            base.Events.RemoveHandler(EVENT_DESELECTED, value);
        }
    }

    [SRCategory("CatAction"), SRDescription("TabControlDeselectingEventDescr")]
    public event TabControlCancelEventHandler Deselecting
    {
        add
        {
            base.Events.AddHandler(EVENT_DESELECTING, value);
        }
        remove
        {
            base.Events.RemoveHandler(EVENT_DESELECTING, value);
        }
    }

    [SRDescription("drawItemEventDescr"), SRCategory("CatBehavior")]
    public event DrawItemEventHandler DrawItem;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler ForeColorChanged
    {
        add
        {
            base.ForeColorChanged += value;
        }
        remove
        {
            base.ForeColorChanged -= value;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public event PaintEventHandler Paint
    {
        add
        {
            base.Paint += value;
        }
        remove
        {
            base.Paint -= value;
        }
    }

    [SRCategory("CatPropertyChanged"), SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
    public event EventHandler RightToLeftLayoutChanged
    {
        add
        {
            base.Events.AddHandler(EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
        }
        remove
        {
            base.Events.RemoveHandler(EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
        }
    }

    [SRDescription("TabControlSelectedEventDescr"), SRCategory("CatAction")]
    public event TabControlEventHandler Selected
    {
        add
        {
            base.Events.AddHandler(EVENT_SELECTED, value);
        }
        remove
        {
            base.Events.RemoveHandler(EVENT_SELECTED, value);
        }
    }

    [SRDescription("selectedIndexChangedEventDescr"), SRCategory("CatBehavior")]
    public event EventHandler SelectedIndexChanged;

    [SRCategory("CatAction"), SRDescription("TabControlSelectingEventDescr")]
    public event TabControlCancelEventHandler Selecting
    {
        add
        {
            base.Events.AddHandler(EVENT_SELECTING, value);
        }
        remove
        {
            base.Events.RemoveHandler(EVENT_SELECTING, value);
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

    // Methods
    public TabControl()
    {
        this.tabCollection = new TabPageCollection(this);
        base.SetStyle(ControlStyles.UserPaint, false);
    }

    internal int AddNativeTabPage(NativeMethods.TCITEM_T tcitem)
    {
        int num = (int) UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_INSERTITEM, this.tabPageCount + 1, tcitem);
        UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), this.tabBaseReLayoutMessage, IntPtr.Zero, IntPtr.Zero);
        return num;
    }

    internal int AddTabPage(TabPage tabPage, NativeMethods.TCITEM_T tcitem)
    {
        int index = this.AddNativeTabPage(tcitem);
        if (index >= 0)
        {
            this.Insert(index, tabPage);
        }
        return index;
    }

    internal void ApplyItemSize()
    {
        if (base.IsHandleCreated && this.ShouldSerializeItemSize())
        {
            base.SendMessage(0x1329, 0, (int) NativeMethods.Util.MAKELPARAM(this.itemSize.Width, this.itemSize.Height));
        }
        this.cachedDisplayRect = Rectangle.Empty;
    }

    internal void BeginUpdate()
    {
        base.BeginUpdateInternal();
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
        return new ControlCollection(this);
    }

    protected override void CreateHandle()
    {
        if (!base.RecreatingHandle)
        {
            IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
            try
            {
                NativeMethods.INITCOMMONCONTROLSEX icc = new NativeMethods.INITCOMMONCONTROLSEX();
                icc.dwICC = 8;
                SafeNativeMethods.InitCommonControlsEx(icc);
            }
            finally
            {
                UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
            }
        }
        base.CreateHandle();
    }

    public void DeselectTab(int index)
    {
        TabPage tabPage = this.GetTabPage(index);
        if (this.SelectedTab == tabPage)
        {
            if ((0 <= index) && (index < (this.TabPages.Count - 1)))
            {
                this.SelectedTab = this.GetTabPage(++index);
            }
            else
            {
                this.SelectedTab = this.GetTabPage(0);
            }
        }
    }

    public void DeselectTab(string tabPageName)
    {
        if (tabPageName == null)
        {
            throw new ArgumentNullException("tabPageName");
        }
        TabPage tabPage = this.TabPages[tabPageName];
        this.DeselectTab(tabPage);
    }

    public void DeselectTab(TabPage tabPage)
    {
        if (tabPage == null)
        {
            throw new ArgumentNullException("tabPage");
        }
        int index = this.FindTabPage(tabPage);
        this.DeselectTab(index);
    }

    private void DetachImageList(object sender, EventArgs e)
    {
        this.ImageList = null;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.imageList != null))
        {
            this.imageList.Disposed -= new EventHandler(this.DetachImageList);
        }
        base.Dispose(disposing);
    }

    internal void EndUpdate()
    {
        this.EndUpdate(true);
    }

    internal void EndUpdate(bool invalidate)
    {
        base.EndUpdateInternal(invalidate);
    }

    internal int FindTabPage(TabPage tabPage)
    {
        if (this.tabPages != null)
        {
            for (int i = 0; i < this.tabPageCount; i++)
            {
                if (this.tabPages[i].Equals(tabPage))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public Control GetControl(int index)
    {
        return this.GetTabPage(index);
    }

    protected virtual object[] GetItems()
    {
        TabPage[] destinationArray = new TabPage[this.tabPageCount];
        if (this.tabPageCount > 0)
        {
            Array.Copy(this.tabPages, 0, destinationArray, 0, this.tabPageCount);
        }
        return destinationArray;
    }

    protected virtual object[] GetItems(Type baseType)
    {
        object[] destinationArray = (object[]) Array.CreateInstance(baseType, this.tabPageCount);
        if (this.tabPageCount > 0)
        {
            Array.Copy(this.tabPages, 0, destinationArray, 0, this.tabPageCount);
        }
        return destinationArray;
    }

    internal TabPage GetTabPage(int index)
    {
        if ((index < 0) || (index >= this.tabPageCount))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        return this.tabPages[index];
    }

    internal TabPage[] GetTabPages()
    {
        return (TabPage[]) this.GetItems();
    }

    public Rectangle GetTabRect(int index)
    {
        if ((index < 0) || ((index >= this.tabPageCount) && !this.tabControlState[8]))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        this.tabControlState[8] = false;
        NativeMethods.RECT lparam = new NativeMethods.RECT();
        if (!base.IsHandleCreated)
        {
            this.CreateHandle();
        }
        base.SendMessage(0x130a, index, ref lparam);
        return Rectangle.FromLTRB(lparam.left, lparam.top, lparam.right, lparam.bottom);
    }

    protected string GetToolTipText(object item)
    {
        return ((TabPage) item).ToolTipText;
    }

    private void ImageListRecreateHandle(object sender, EventArgs e)
    {
        if (base.IsHandleCreated)
        {
            base.SendMessage(0x1303, 0, this.ImageList.Handle);
        }
    }

    internal void Insert(int index, TabPage tabPage)
    {
        if (this.tabPages == null)
        {
            this.tabPages = new TabPage[4];
        }
        else if (this.tabPages.Length == this.tabPageCount)
        {
            TabPage[] destinationArray = new TabPage[this.tabPageCount * 2];
            Array.Copy(this.tabPages, 0, destinationArray, 0, this.tabPageCount);
            this.tabPages = destinationArray;
        }
        if (index < this.tabPageCount)
        {
            Array.Copy(this.tabPages, index, this.tabPages, index + 1, this.tabPageCount - index);
        }
        this.tabPages[index] = tabPage;
        this.tabPageCount++;
        this.cachedDisplayRect = Rectangle.Empty;
        this.ApplyItemSize();
        if (this.Appearance == TabAppearance.FlatButtons)
        {
            base.Invalidate();
        }
    }

    private void InsertItem(int index, TabPage tabPage)
    {
        if ((index < 0) || ((this.tabPages != null) && (index > this.tabPageCount)))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        if (tabPage == null)
        {
            throw new ArgumentNullException("tabPage");
        }
        if (base.IsHandleCreated)
        {
            NativeMethods.TCITEM_T lParam = tabPage.GetTCITEM();
            int num = (int) UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_INSERTITEM, index, lParam);
            if (num >= 0)
            {
                this.Insert(num, tabPage);
            }
        }
    }

    protected override bool IsInputKey(Keys keyData)
    {
        if ((keyData & Keys.Alt) == Keys.Alt)
        {
            return false;
        }
        switch ((keyData & Keys.KeyCode))
        {
        }
        return true;
    }

    protected virtual void OnDeselected(TabControlEventArgs e)
    {
        TabControlEventHandler handler = (TabControlEventHandler) base.Events[EVENT_DESELECTED];
        if (handler != null)
        {
            handler(this, e);
        }
        if (this.SelectedTab != null)
        {
            this.SelectedTab.FireLeave(EventArgs.Empty);
        }
    }

    protected virtual void OnDeselecting(TabControlCancelEventArgs e)
    {
        TabControlCancelEventHandler handler = (TabControlCancelEventHandler) base.Events[EVENT_DESELECTING];
        if (handler != null)
        {
            handler(this, e);
        }
    }

    protected virtual void OnDrawItem(DrawItemEventArgs e)
    {
        if (this.onDrawItem != null)
        {
            this.onDrawItem(this, e);
        }
    }

    protected override void OnEnter(EventArgs e)
    {
        base.OnEnter(e);
        if (this.SelectedTab != null)
        {
            this.SelectedTab.FireEnter(e);
        }
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        this.cachedDisplayRect = Rectangle.Empty;
        this.UpdateSize();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        NativeWindow.AddWindowToIDTable(this, base.Handle);
        if (!this.padding.IsEmpty)
        {
            base.SendMessage(0x132b, 0, NativeMethods.Util.MAKELPARAM(this.padding.X, this.padding.Y));
        }
        base.OnHandleCreated(e);
        this.cachedDisplayRect = Rectangle.Empty;
        this.ApplyItemSize();
        if (this.imageList != null)
        {
            base.SendMessage(0x1303, 0, this.imageList.Handle);
        }
        if (this.ShowToolTips)
        {
            IntPtr handle = base.SendMessage(0x132d, 0, 0);
            if (handle != IntPtr.Zero)
            {
                SafeNativeMethods.SetWindowPos(new HandleRef(this, handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 0x13);
            }
        }
        foreach (TabPage page in this.TabPages)
        {
            this.AddNativeTabPage(page.GetTCITEM());
        }
        this.ResizePages();
        if (this.selectedIndex != -1)
        {
            try
            {
                this.tabControlState[0x10] = true;
                this.SelectedIndex = this.selectedIndex;
            }
            finally
            {
                this.tabControlState[0x10] = false;
            }
            this.selectedIndex = -1;
        }
        this.UpdateTabSelection(false);
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
        if (!base.Disposing)
        {
            this.selectedIndex = this.SelectedIndex;
        }
        NativeWindow.RemoveWindowFromIDTable(base.Handle);
        base.OnHandleDestroyed(e);
    }

    protected override void OnKeyDown(KeyEventArgs ke)
    {
        if ((ke.KeyCode == Keys.Tab) && ((ke.KeyData & Keys.Control) != Keys.None))
        {
            bool forward = (ke.KeyData & Keys.Shift) == Keys.None;
            this.SelectNextTab(ke, forward);
        }
        if ((ke.KeyCode == Keys.Next) && ((ke.KeyData & Keys.Control) != Keys.None))
        {
            this.SelectNextTab(ke, true);
        }
        if ((ke.KeyCode == Keys.Prior) && ((ke.KeyData & Keys.Control) != Keys.None))
        {
            this.SelectNextTab(ke, false);
        }
        base.OnKeyDown(ke);
    }

    protected override void OnLeave(EventArgs e)
    {
        if (this.SelectedTab != null)
        {
            this.SelectedTab.FireLeave(e);
        }
        base.OnLeave(e);
    }

    internal override void OnParentHandleRecreated()
    {
        this.skipUpdateSize = true;
        try
        {
            base.OnParentHandleRecreated();
        }
        finally
        {
            this.skipUpdateSize = false;
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        this.cachedDisplayRect = Rectangle.Empty;
        this.UpdateTabSelection(false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
    {
        if (base.GetAnyDisposingInHierarchy())
        {
        }
        if (this.RightToLeft == RightToLeft.Yes)
        {
            base.RecreateHandle();
        }
        EventHandler handler = base.Events[EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    protected virtual void OnSelected(TabControlEventArgs e)
    {
        TabControlEventHandler handler = (TabControlEventHandler) base.Events[EVENT_SELECTED];
        if (handler != null)
        {
            handler(this, e);
        }
        if (this.SelectedTab != null)
        {
            this.SelectedTab.FireEnter(EventArgs.Empty);
        }
    }

    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
        int selectedIndex = this.SelectedIndex;
        this.cachedDisplayRect = Rectangle.Empty;
        this.UpdateTabSelection(this.tabControlState[0x20]);
        this.tabControlState[0x20] = false;
        if (this.onSelectedIndexChanged != null)
        {
            this.onSelectedIndexChanged(this, e);
        }
    }

    protected virtual void OnSelecting(TabControlCancelEventArgs e)
    {
        TabControlCancelEventHandler handler = (TabControlCancelEventHandler) base.Events[EVENT_SELECTING];
        if (handler != null)
        {
            handler(this, e);
        }
    }

    protected override void OnStyleChanged(EventArgs e)
    {
        base.OnStyleChanged(e);
        this.cachedDisplayRect = Rectangle.Empty;
        this.UpdateTabSelection(false);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
    protected override bool ProcessKeyPreview(ref Message m)
    {
        return (this.ProcessKeyEventArgs(ref m) || base.ProcessKeyPreview(ref m));
    }

    internal override void RecreateHandleCore()
    {
        TabPage[] tabPages = this.GetTabPages();
        int num = ((tabPages.Length > 0) && (this.SelectedIndex == -1)) ? 0 : this.SelectedIndex;
        if (base.IsHandleCreated)
        {
            base.SendMessage(0x1309, 0, 0);
        }
        this.tabPages = null;
        this.tabPageCount = 0;
        base.RecreateHandleCore();
        for (int i = 0; i < tabPages.Length; i++)
        {
            this.TabPages.Add(tabPages[i]);
        }
        try
        {
            this.tabControlState[0x10] = true;
            this.SelectedIndex = num;
        }
        finally
        {
            this.tabControlState[0x10] = false;
        }
        this.UpdateSize();
    }

    protected void RemoveAll()
    {
        base.Controls.Clear();
        base.SendMessage(0x1309, 0, 0);
        this.tabPages = null;
        this.tabPageCount = 0;
    }

    internal void RemoveTabPage(int index)
    {
        if ((index < 0) || (index >= this.tabPageCount))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        this.tabPageCount--;
        if (index < this.tabPageCount)
        {
            Array.Copy(this.tabPages, index + 1, this.tabPages, index, this.tabPageCount - index);
        }
        this.tabPages[this.tabPageCount] = null;
        if (base.IsHandleCreated)
        {
            base.SendMessage(0x1308, index, 0);
        }
        this.cachedDisplayRect = Rectangle.Empty;
    }

    private void ResetItemSize()
    {
        this.ItemSize = DEFAULT_ITEMSIZE;
    }

    private void ResetPadding()
    {
        this.Padding = DEFAULT_PADDING;
    }

    private void ResizePages()
    {
        Rectangle displayRectangle = this.DisplayRectangle;
        TabPage[] tabPages = this.GetTabPages();
        for (int i = 0; i < tabPages.Length; i++)
        {
            tabPages[i].Bounds = displayRectangle;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override void ScaleCore(float dx, float dy)
    {
        this.currentlyScaling = true;
        base.ScaleCore(dx, dy);
        this.currentlyScaling = false;
    }

    private void SelectNextTab(KeyEventArgs ke, bool forward)
    {
        bool focused = this.Focused;
        if (this.WmSelChanging())
        {
            this.tabControlState[0x20] = false;
        }
        else if (base.ValidationCancelled)
        {
            this.tabControlState[0x20] = false;
        }
        else
        {
            int selectedIndex = this.SelectedIndex;
            if (selectedIndex != -1)
            {
                int tabCount = this.TabCount;
                if (forward)
                {
                    selectedIndex = (selectedIndex + 1) % tabCount;
                }
                else
                {
                    selectedIndex = ((selectedIndex + tabCount) - 1) % tabCount;
                }
                try
                {
                    this.tabControlState[0x20] = true;
                    this.tabControlState[0x40] = true;
                    this.SelectedIndex = selectedIndex;
                    this.tabControlState[0x40] = !focused;
                    this.WmSelChange();
                }
                finally
                {
                    this.tabControlState[0x40] = false;
                    ke.Handled = true;
                }
            }
        }
    }

    public void SelectTab(int index)
    {
        TabPage tabPage = this.GetTabPage(index);
        if (tabPage != null)
        {
            this.SelectedTab = tabPage;
        }
    }

    public void SelectTab(string tabPageName)
    {
        if (tabPageName == null)
        {
            throw new ArgumentNullException("tabPageName");
        }
        TabPage tabPage = this.TabPages[tabPageName];
        this.SelectTab(tabPage);
    }

    public void SelectTab(TabPage tabPage)
    {
        if (tabPage == null)
        {
            throw new ArgumentNullException("tabPage");
        }
        int index = this.FindTabPage(tabPage);
        this.SelectTab(index);
    }

    internal void SetTabPage(int index, TabPage tabPage, NativeMethods.TCITEM_T tcitem)
    {
        if ((index < 0) || (index >= this.tabPageCount))
        {
            throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[] { "index", index.ToString(CultureInfo.CurrentCulture) }));
        }
        if (base.IsHandleCreated)
        {
            UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_SETITEM, index, tcitem);
        }
        if (base.DesignMode && base.IsHandleCreated)
        {
            UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0x130c, (IntPtr) index, IntPtr.Zero);
        }
        this.tabPages[index] = tabPage;
    }

    internal void SetToolTip(ToolTip toolTip, string controlToolTipText)
    {
        UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0x132e, new HandleRef(toolTip, toolTip.Handle), 0);
        this.controlTipText = controlToolTipText;
    }

    internal override bool ShouldPerformContainerValidation()
    {
        return true;
    }

    private bool ShouldSerializeItemSize()
    {
        return !this.itemSize.Equals(DEFAULT_ITEMSIZE);
    }

    private bool ShouldSerializePadding()
    {
        return !this.padding.Equals(DEFAULT_PADDING);
    }

    public override string ToString()
    {
        string text = base.ToString();
        if (this.TabPages != null)
        {
            text = text + ", TabPages.Count: " + this.TabPages.Count.ToString(CultureInfo.CurrentCulture);
            if (this.TabPages.Count > 0)
            {
                text = text + ", TabPages[0]: " + this.TabPages[0].ToString();
            }
        }
        return text;
    }

    internal void UpdateSize()
    {
        if (this.skipUpdateSize)
        {
        }
        this.BeginUpdate();
        Size size = base.Size;
        base.Size = new Size(size.Width + 1, size.Height);
        base.Size = size;
        this.EndUpdate();
    }

    internal void UpdateTab(TabPage tabPage)
    {
        int index = this.FindTabPage(tabPage);
        this.SetTabPage(index, tabPage, tabPage.GetTCITEM());
        this.cachedDisplayRect = Rectangle.Empty;
        this.UpdateTabSelection(false);
    }

    protected void UpdateTabSelection(bool updateFocus)
    {
        if (base.IsHandleCreated)
        {
            int index = this.SelectedIndex;
            TabPage[] tabPages = this.GetTabPages();
            if (index != -1)
            {
                if (this.currentlyScaling)
                {
                    tabPages[index].SuspendLayout();
                }
                tabPages[index].Bounds = this.DisplayRectangle;
                if (this.currentlyScaling)
                {
                    tabPages[index].ResumeLayout(false);
                }
                tabPages[index].Visible = true;
                if (updateFocus && (!this.Focused || this.tabControlState[0x40]))
                {
                    this.tabControlState[0x20] = false;
                    bool flag = false;
                    IntSecurity.ModifyFocus.Assert();
                    try
                    {
                        flag = tabPages[index].SelectNextControl(null, true, true, false, false);
                    }
                    finally
                    {
                        CodeAccessPermission.RevertAssert();
                    }
                    if (flag)
                    {
                        if (!base.ContainsFocus)
                        {
                            IContainerControl containerControlInternal = base.GetContainerControlInternal();
                            if (containerControlInternal != null)
                            {
                                while (containerControlInternal.ActiveControl is ContainerControl)
                                {
                                    containerControlInternal = (IContainerControl) containerControlInternal.ActiveControl;
                                }
                                if (containerControlInternal.ActiveControl != null)
                                {
                                    containerControlInternal.ActiveControl.FocusInternal();
                                }
                            }
                        }
                    }
                    else
                    {
                        IContainerControl containerControlInternal = base.GetContainerControlInternal();
                        if ((containerControlInternal != null) && !base.DesignMode)
                        {
                            if (containerControlInternal is ContainerControl)
                            {
                                ((ContainerControl) containerControlInternal).SetActiveControlInternal(this);
                            }
                            else
                            {
                                IntSecurity.ModifyFocus.Assert();
                                try
                                {
                                    containerControlInternal.ActiveControl = this;
                                }
                                finally
                                {
                                    CodeAccessPermission.RevertAssert();
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < tabPages.Length; i++)
            {
                if (i != this.SelectedIndex)
                {
                    tabPages[i].Visible = false;
                }
            }
        }
    }

    private void WmNeedText(ref Message m)
    {
        NativeMethods.TOOLTIPTEXT structure = (NativeMethods.TOOLTIPTEXT) m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
        int index = (int) structure.hdr.idFrom;
        string toolTipText = this.GetToolTipText(this.GetTabPage(index));
        if (!string.IsNullOrEmpty(toolTipText))
        {
            structure.lpszText = toolTipText;
        }
        else
        {
            structure.lpszText = this.controlTipText;
        }
        structure.hinst = IntPtr.Zero;
        if (this.RightToLeft == RightToLeft.Yes)
        {
            structure.uFlags |= 4;
        }
        Marshal.StructureToPtr(structure, m.LParam, false);
    }

    private void WmReflectDrawItem(ref Message m)
    {
        NativeMethods.DRAWITEMSTRUCT lParam = (NativeMethods.DRAWITEMSTRUCT) m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
        IntPtr handle = Control.SetUpPalette(lParam.hDC, false, false);
        using (Graphics graphics = Graphics.FromHdcInternal(lParam.hDC))
        {
            this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, Rectangle.FromLTRB(lParam.rcItem.left, lParam.rcItem.top, lParam.rcItem.right, lParam.rcItem.bottom), lParam.itemID, (DrawItemState) lParam.itemState));
        }
        if (handle != IntPtr.Zero)
        {
            SafeNativeMethods.SelectPalette(new HandleRef(null, lParam.hDC), new HandleRef(null, handle), 0);
        }
        m.Result = (IntPtr) 1;
    }

    private bool WmSelChange()
    {
        TabControlCancelEventArgs e = new TabControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Selecting);
        this.OnSelecting(e);
        if (!e.Cancel)
        {
            this.OnSelected(new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Selected));
            this.OnSelectedIndexChanged(EventArgs.Empty);
        }
        else
        {
            base.SendMessage(0x130c, this.lastSelection, 0);
            this.UpdateTabSelection(true);
        }
        return e.Cancel;
    }

    private bool WmSelChanging()
    {
        IContainerControl containerControlInternal = base.GetContainerControlInternal();
        if ((containerControlInternal != null) && !base.DesignMode)
        {
            if (containerControlInternal is ContainerControl)
            {
                ((ContainerControl) containerControlInternal).SetActiveControlInternal(this);
            }
            else
            {
                IntSecurity.ModifyFocus.Assert();
                try
                {
                    containerControlInternal.ActiveControl = this;
                }
                finally
                {
                    CodeAccessPermission.RevertAssert();
                }
            }
        }
        this.lastSelection = this.SelectedIndex;
        TabControlCancelEventArgs e = new TabControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Deselecting);
        this.OnDeselecting(e);
        if (!e.Cancel)
        {
            this.OnDeselected(new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Deselected));
        }
        return e.Cancel;
    }

    private void WmTabBaseReLayout(ref Message m)
    {
        this.BeginUpdate();
        this.cachedDisplayRect = Rectangle.Empty;
        this.UpdateTabSelection(false);
        this.EndUpdate();
        base.Invalidate(true);
        NativeMethods.MSG msg = new NativeMethods.MSG();
        IntPtr handle = base.Handle;
        while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, handle), this.tabBaseReLayoutMessage, this.tabBaseReLayoutMessage, 1))
        {
        }
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case 0x202b:
                this.WmReflectDrawItem(ref m);
                break;

            case 0x204e:
            case 0x4e:
            {
                NativeMethods.NMHDR wrapper = (NativeMethods.NMHDR) m.GetLParam(typeof(NativeMethods.NMHDR));
                switch (wrapper.code)
                {
                    case -552:
                        if (!this.WmSelChanging())
                        {
                            if (base.ValidationCancelled)
                            {
                                m.Result = (IntPtr) 1;
                                this.tabControlState[0x20] = false;
                                return;
                            }
                            this.tabControlState[0x20] = true;
                            break;
                        }
                        m.Result = (IntPtr) 1;
                        this.tabControlState[0x20] = false;
                        return;

                    case -551:
                        if (!this.WmSelChange())
                        {
                            this.tabControlState[0x20] = true;
                            break;
                        }
                        m.Result = (IntPtr) 1;
                        this.tabControlState[0x20] = false;
                        return;

                    case -530:
                    case -520:
                        UnsafeNativeMethods.SendMessage(new HandleRef(wrapper, wrapper.hwndFrom), 0x418, 0, SystemInformation.MaxWindowTrackSize.Width);
                        this.WmNeedText(ref m);
                        m.Result = (IntPtr) 1;
                        return;
                }
                break;
            }
        }
        if (m.Msg == this.tabBaseReLayoutMessage)
        {
            this.WmTabBaseReLayout(ref m);
        }
        else
        {
            base.WndProc(ref m);
        }
    }

    // Properties
    [DefaultValue(0), Localizable(true), RefreshProperties(RefreshProperties.All), SRDescription("TabBaseAlignmentDescr"), SRCategory("CatBehavior")]
    public TabAlignment Alignment
    {
        get
        {
            return this.alignment;
        }
        set
        {
            if (this.alignment != value)
            {
                if (!ClientUtils.IsEnumValid(value, (int) value, 0, 3))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(TabAlignment));
                }
                this.alignment = value;
                if ((this.alignment == TabAlignment.Left) || (this.alignment == TabAlignment.Right))
                {
                    this.Multiline = true;
                }
                base.RecreateHandle();
            }
        }
    }

    [SRDescription("TabBaseAppearanceDescr"), DefaultValue(0), SRCategory("CatBehavior"), Localizable(true)]
    public TabAppearance Appearance
    {
        get
        {
            if ((this.appearance == TabAppearance.FlatButtons) && (this.alignment != TabAlignment.Top))
            {
                return TabAppearance.Buttons;
            }
            return this.appearance;
        }
        set
        {
            if (this.appearance != value)
            {
                if (!ClientUtils.IsEnumValid(value, (int) value, 0, 2))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(TabAppearance));
                }
                this.appearance = value;
                base.RecreateHandle();
                this.OnStyleChanged(EventArgs.Empty);
            }
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
        get
        {
            return SystemColors.Control;
        }
        set
        {
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public override Image BackgroundImage
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

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public override ImageLayout BackgroundImageLayout
    {
        get
        {
            return base.BackgroundImageLayout;
        }
        set
        {
            base.BackgroundImageLayout = value;
        }
    }

    protected override CreateParams CreateParams
    {
        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        get
        {
            CreateParams createParams = base.CreateParams;
            createParams.ClassName = "SysTabControl32";
            if (this.Multiline)
            {
                createParams.Style |= 0x200;
            }
            if (this.drawMode == TabDrawMode.OwnerDrawFixed)
            {
                createParams.Style |= 0x2000;
            }
            if (this.ShowToolTips && !base.DesignMode)
            {
                createParams.Style |= 0x4000;
            }
            if ((this.alignment == TabAlignment.Bottom) || (this.alignment == TabAlignment.Right))
            {
                createParams.Style |= 2;
            }
            if ((this.alignment == TabAlignment.Left) || (this.alignment == TabAlignment.Right))
            {
                createParams.Style |= 640;
            }
            if (this.tabControlState[1])
            {
                createParams.Style |= 0x40;
            }
            if (this.appearance == TabAppearance.Normal)
            {
                createParams.Style = createParams.Style;
            }
            else
            {
                createParams.Style |= 0x100;
                if ((this.appearance == TabAppearance.FlatButtons) && (this.alignment == TabAlignment.Top))
                {
                    createParams.Style |= 8;
                }
            }
            switch (this.sizeMode)
            {
                case TabSizeMode.FillToRight:
                    createParams.Style = createParams.Style;
                    break;

                case TabSizeMode.Fixed:
                    createParams.Style |= 0x400;
                    break;

                default:
                    createParams.Style |= 0x800;
                    break;
            }
            if ((this.RightToLeft == RightToLeft.Yes) && this.RightToLeftLayout)
            {
                createParams.ExStyle |= 0x500000;
                createParams.ExStyle &= -28673;
            }
            return createParams;
        }
    }

    protected override Size DefaultSize
    {
        get
        {
            return new Size(200, 100);
        }
    }

    public override Rectangle DisplayRectangle
    {
        get
        {
            if (!this.cachedDisplayRect.IsEmpty)
            {
                return this.cachedDisplayRect;
            }
            Rectangle bounds = base.Bounds;
            NativeMethods.RECT lparam = NativeMethods.RECT.FromXYWH(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            if (!base.IsDisposed)
            {
                if (!base.IsActiveX && !base.IsHandleCreated)
                {
                    this.CreateHandle();
                }
                if (base.IsHandleCreated)
                {
                    base.SendMessage(0x1328, 0, ref lparam);
                }
            }
            Rectangle rectangle2 = Rectangle.FromLTRB(lparam.left, lparam.top, lparam.right, lparam.bottom);
            Point location = base.Location;
            rectangle2.X -= location.X;
            rectangle2.Y -= location.Y;
            this.cachedDisplayRect = rectangle2;
            return rectangle2;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override bool DoubleBuffered
    {
        get
        {
            return base.DoubleBuffered;
        }
        set
        {
            base.DoubleBuffered = value;
        }
    }

    [SRDescription("TabBaseDrawModeDescr"), DefaultValue(0), SRCategory("CatBehavior")]
    public TabDrawMode DrawMode
    {
        get
        {
            return this.drawMode;
        }
        set
        {
            if (!ClientUtils.IsEnumValid(value, (int) value, 0, 1))
            {
                throw new InvalidEnumArgumentException("value", (int) value, typeof(TabDrawMode));
            }
            if (this.drawMode != value)
            {
                this.drawMode = value;
                base.RecreateHandle();
            }
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
        get
        {
            return base.ForeColor;
        }
        set
        {
            base.ForeColor = value;
        }
    }

    [DefaultValue(false), SRCategory("CatBehavior"), SRDescription("TabBaseHotTrackDescr")]
    public bool HotTrack
    {
        get
        {
            return this.tabControlState[1];
        }
        set
        {
            if (this.HotTrack != value)
            {
                this.tabControlState[1] = value;
                if (base.IsHandleCreated)
                {
                    base.RecreateHandle();
                }
            }
        }
    }

    [SRCategory("CatAppearance"), DefaultValue((string) null), SRDescription("TabBaseImageListDescr"), RefreshProperties(RefreshProperties.Repaint)]
    public ImageList ImageList
    {
        get
        {
            return this.imageList;
        }
        set
        {
            if (this.imageList != value)
            {
                EventHandler handler = new EventHandler(this.ImageListRecreateHandle);
                EventHandler handler2 = new EventHandler(this.DetachImageList);
                if (this.imageList != null)
                {
                    this.imageList.RecreateHandle -= handler;
                    this.imageList.Disposed -= handler2;
                }
                this.imageList = value;
                IntPtr lparam = (value != null) ? value.Handle : IntPtr.Zero;
                if (base.IsHandleCreated)
                {
                    base.SendMessage(0x1303, IntPtr.Zero, lparam);
                }
                foreach (TabPage page in this.TabPages)
                {
                    page.ImageIndexer.ImageList = value;
                }
                if (value != null)
                {
                    value.RecreateHandle += handler;
                    value.Disposed += handler2;
                }
            }
        }
    }

    private bool InsertingItem
    {
        get
        {
            return this.tabControlState[0x80];
        }
        set
        {
            this.tabControlState[0x80] = value;
        }
    }

    [SRCategory("CatBehavior"), Localizable(true), SRDescription("TabBaseItemSizeDescr")]
    public Size ItemSize
    {
        get
        {
            if (!this.itemSize.IsEmpty)
            {
                return this.itemSize;
            }
            if (base.IsHandleCreated)
            {
                this.tabControlState[8] = true;
                return this.GetTabRect(0).Size;
            }
            return DEFAULT_ITEMSIZE;
        }
        set
        {
            if ((value.Width < 0) || (value.Height < 0))
            {
                throw new ArgumentOutOfRangeException("ItemSize", SR.GetString("InvalidArgument", new object[] { "ItemSize", value.ToString() }));
            }
            this.itemSize = value;
            this.ApplyItemSize();
            this.UpdateSize();
            base.Invalidate();
        }
    }

    [SRDescription("TabBaseMultilineDescr"), SRCategory("CatBehavior"), DefaultValue(false)]
    public bool Multiline
    {
        get
        {
            return this.tabControlState[2];
        }
        set
        {
            if (this.Multiline != value)
            {
                this.tabControlState[2] = value;
                if (!this.Multiline && ((this.alignment == TabAlignment.Left) || (this.alignment == TabAlignment.Right)))
                {
                    this.alignment = TabAlignment.Top;
                }
                base.RecreateHandle();
            }
        }
    }

    [SRCategory("CatBehavior"), Localizable(true), SRDescription("TabBasePaddingDescr")]
    public Point Padding
    {
        get
        {
            return this.padding;
        }
        set
        {
            if ((value.X < 0) || (value.Y < 0))
            {
                throw new ArgumentOutOfRangeException("Padding", SR.GetString("InvalidArgument", new object[] { "Padding", value.ToString() }));
            }
            if (this.padding != value)
            {
                this.padding = value;
                if (base.IsHandleCreated)
                {
                    base.RecreateHandle();
                }
            }
        }
    }

    [Localizable(true), SRDescription("ControlRightToLeftLayoutDescr"), DefaultValue(false), SRCategory("CatAppearance")]
    public virtual bool RightToLeftLayout
    {
        get
        {
            return this.rightToLeftLayout;
        }
        set
        {
            if (value != this.rightToLeftLayout)
            {
                this.rightToLeftLayout = value;
                using (LayoutTransaction transaction = new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
                {
                    this.OnRightToLeftLayoutChanged(EventArgs.Empty);
                }
            }
        }
    }

    [Browsable(false), SRCategory("CatAppearance"), SRDescription("TabBaseRowCountDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RowCount
    {
        get
        {
            return (int) base.SendMessage(0x132c, 0, 0);
        }
    }

    [Browsable(false), SRDescription("selectedIndexDescr"), SRCategory("CatBehavior"), DefaultValue(-1)]
    public int SelectedIndex
    {
        get
        {
            if (base.IsHandleCreated)
            {
                return (int) base.SendMessage(0x130b, 0, 0);
            }
            return this.selectedIndex;
        }
        set
        {
            if (value < -1)
            {
                object[] args = new object[] { "SelectedIndex", value.ToString(CultureInfo.CurrentCulture), -1.ToString(CultureInfo.CurrentCulture) };
                throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidLowBoundArgumentEx", args));
            }
            if (this.SelectedIndex != value)
            {
                if (base.IsHandleCreated)
                {
                    if (!this.tabControlState[0x10] && !this.tabControlState[0x40])
                    {
                        this.tabControlState[0x20] = true;
                        if (this.WmSelChanging())
                        {
                            this.tabControlState[0x20] = false;
                            return;
                        }
                        if (base.ValidationCancelled)
                        {
                            this.tabControlState[0x20] = false;
                            return;
                        }
                    }
                    base.SendMessage(0x130c, value, 0);
                    if (!this.tabControlState[0x10] && !this.tabControlState[0x40])
                    {
                        this.tabControlState[0x40] = true;
                        if (this.WmSelChange())
                        {
                            this.tabControlState[0x20] = false;
                            this.tabControlState[0x40] = false;
                        }
                        else
                        {
                            this.tabControlState[0x40] = false;
                        }
                    }
                }
                else
                {
                    this.selectedIndex = value;
                }
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), SRCategory("CatAppearance"), SRDescription("TabControlSelectedTabDescr")]
    public TabPage SelectedTab
    {
        get
        {
            return this.SelectedTabInternal;
        }
        set
        {
            this.SelectedTabInternal = value;
        }
    }

    internal TabPage SelectedTabInternal
    {
        get
        {
            int index = this.SelectedIndex;
            if (index == -1)
            {
                return null;
            }
            return this.tabPages[index];
        }
        set
        {
            int num = this.FindTabPage(value);
            this.SelectedIndex = num;
        }
    }

    [Localizable(true), SRCategory("CatBehavior"), DefaultValue(false), SRDescription("TabBaseShowToolTipsDescr")]
    public bool ShowToolTips
    {
        get
        {
            return this.tabControlState[4];
        }
        set
        {
            if (this.ShowToolTips != value)
            {
                this.tabControlState[4] = value;
                base.RecreateHandle();
            }
        }
    }

    [RefreshProperties(RefreshProperties.Repaint), DefaultValue(0), SRDescription("TabBaseSizeModeDescr"), SRCategory("CatBehavior")]
    public TabSizeMode SizeMode
    {
        get
        {
            return this.sizeMode;
        }
        set
        {
            if (this.sizeMode == value)
            {
            }
            if (!ClientUtils.IsEnumValid(value, (int) value, 0, 2))
            {
                throw new InvalidEnumArgumentException("value", (int) value, typeof(TabSizeMode));
            }
            this.sizeMode = value;
            base.RecreateHandle();
        }
    }

    [SRDescription("TabBaseTabCountDescr"), SRCategory("CatAppearance"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TabCount
    {
        get
        {
            return this.tabPageCount;
        }
    }

    [SRCategory("CatBehavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MergableProperty(false), SRDescription("TabControlTabsDescr"), Editor("System.Windows.Forms.Design.TabPageCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    public TabPageCollection TabPages
    {
        get
        {
            return this.tabCollection;
        }
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Bindable(false)]
    public override string Text
    {
        get
        {
            return base.Text;
        }
        set
        {
            base.Text = value;
        }
    }

    // Nested Types
    [ComVisible(false)]
    public class ControlCollection : Control.ControlCollection
    {
        // Fields
        private TabControl owner;

        // Methods
        public ControlCollection(TabControl owner) : base(owner)
        {
            this.owner = owner;
        }

        public override void Add(Control value)
        {
            if (!(value is TabPage))
            {
                throw new ArgumentException(SR.GetString("TabControlInvalidTabPageType", new object[] { value.GetType().Name }));
            }
            TabPage tabPage = (TabPage) value;
            if (!this.owner.InsertingItem)
            {
                if (this.owner.IsHandleCreated)
                {
                    this.owner.AddTabPage(tabPage, tabPage.GetTCITEM());
                }
                else
                {
                    this.owner.Insert(this.owner.TabCount, tabPage);
                }
            }
            base.Add(tabPage);
            tabPage.Visible = false;
            if (this.owner.IsHandleCreated)
            {
                tabPage.Bounds = this.owner.DisplayRectangle;
            }
            ISite site = this.owner.Site;
            if ((site != null) && (tabPage.Site == null))
            {
                IContainer container = site.Container;
                if (container != null)
                {
                    container.Add(tabPage);
                }
            }
            this.owner.ApplyItemSize();
            this.owner.UpdateTabSelection(false);
        }

        public override void Remove(Control value)
        {
            base.Remove(value);
            if (!(value is TabPage))
            {
            }
            int index = this.owner.FindTabPage((TabPage) value);
            int selectedIndex = this.owner.SelectedIndex;
            if (index != -1)
            {
                this.owner.RemoveTabPage(index);
                if (index == selectedIndex)
                {
                    this.owner.SelectedIndex = 0;
                }
            }
            this.owner.UpdateTabSelection(false);
        }
    }

    public class TabPageCollection : IList, ICollection, IEnumerable
    {
        // Fields
        private int lastAccessedIndex = -1;
        private TabControl owner;

        // Methods
        public TabPageCollection(TabControl owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            this.owner = owner;
        }

        public void Add(string text)
        {
            TabPage page = new TabPage();
            page.Text = text;
            this.Add(page);
        }

        public void Add(TabPage value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.owner.Controls.Add(value);
        }

        public void Add(string key, string text)
        {
            TabPage page = new TabPage();
            page.Name = key;
            page.Text = text;
            this.Add(page);
        }

        public void Add(string key, string text, int imageIndex)
        {
            TabPage page = new TabPage();
            page.Name = key;
            page.Text = text;
            page.ImageIndex = imageIndex;
            this.Add(page);
        }

        public void Add(string key, string text, string imageKey)
        {
            TabPage page = new TabPage();
            page.Name = key;
            page.Text = text;
            page.ImageKey = imageKey;
            this.Add(page);
        }

        public void AddRange(TabPage[] pages)
        {
            if (pages == null)
            {
                throw new ArgumentNullException("pages");
            }
            foreach (TabPage page in pages)
            {
                this.Add(page);
            }
        }

        public virtual void Clear()
        {
            this.owner.RemoveAll();
        }

        public bool Contains(TabPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("value");
            }
            return (this.IndexOf(page) != -1);
        }

        public virtual bool ContainsKey(string key)
        {
            return this.IsValidIndex(this.IndexOfKey(key));
        }

        public IEnumerator GetEnumerator()
        {
            TabPage[] tabPages = this.owner.GetTabPages();
            if (tabPages != null)
            {
                return tabPages.GetEnumerator();
            }
            return new TabPage[0].GetEnumerator();
        }

        public int IndexOf(TabPage page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("value");
            }
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] == page)
                {
                    return i;
                }
            }
            return -1;
        }

        public virtual int IndexOfKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
                {
                    return this.lastAccessedIndex;
                }
                for (int i = 0; i < this.Count; i++)
                {
                    if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
                    {
                        this.lastAccessedIndex = i;
                        return i;
                    }
                }
                this.lastAccessedIndex = -1;
            }
            return -1;
        }

        public void Insert(int index, string text)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = text;
            this.Insert(index, tabPage);
        }

        public void Insert(int index, TabPage tabPage)
        {
            this.owner.InsertItem(index, tabPage);
            try
            {
                this.owner.InsertingItem = true;
                this.owner.Controls.Add(tabPage);
            }
            finally
            {
                this.owner.InsertingItem = false;
            }
            this.owner.Controls.SetChildIndex(tabPage, index);
        }

        public void Insert(int index, string key, string text)
        {
            TabPage tabPage = new TabPage();
            tabPage.Name = key;
            tabPage.Text = text;
            this.Insert(index, tabPage);
        }

        public void Insert(int index, string key, string text, int imageIndex)
        {
            TabPage tabPage = new TabPage();
            tabPage.Name = key;
            tabPage.Text = text;
            this.Insert(index, tabPage);
            tabPage.ImageIndex = imageIndex;
        }

        public void Insert(int index, string key, string text, string imageKey)
        {
            TabPage tabPage = new TabPage();
            tabPage.Name = key;
            tabPage.Text = text;
            this.Insert(index, tabPage);
            tabPage.ImageKey = imageKey;
        }

        private bool IsValidIndex(int index)
        {
            if (index >= 0)
            {
                return (index < this.Count);
            }
            return false;
        }

        public void Remove(TabPage value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.owner.Controls.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.owner.Controls.RemoveAt(index);
        }

        public virtual void RemoveByKey(string key)
        {
            int index = this.IndexOfKey(key);
            if (this.IsValidIndex(index))
            {
                this.RemoveAt(index);
            }
        }

        void ICollection.CopyTo(Array dest, int index)
        {
            if (this.Count > 0)
            {
                Array.Copy(this.owner.GetTabPages(), 0, dest, index, this.Count);
            }
        }

        int IList.Add(object value)
        {
            if (!(value is TabPage))
            {
                throw new ArgumentException("value");
            }
            this.Add((TabPage) value);
            return this.IndexOf((TabPage) value);
        }

        bool IList.Contains(object page)
        {
            if (page is TabPage)
            {
                return this.Contains((TabPage) page);
            }
            return false;
        }

        int IList.IndexOf(object page)
        {
            if (page is TabPage)
            {
                return this.IndexOf((TabPage) page);
            }
            return -1;
        }

        void IList.Insert(int index, object tabPage)
        {
            if (!(tabPage is TabPage))
            {
                throw new ArgumentException("tabPage");
            }
            this.Insert(index, (TabPage) tabPage);
        }

        void IList.Remove(object value)
        {
            if (value is TabPage)
            {
                this.Remove((TabPage) value);
            }
        }

        // Properties
        [Browsable(false)]
        public int Count
        {
            get
            {
                return this.owner.tabPageCount;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual TabPage this[int index]
        {
            get
            {
                return this.owner.GetTabPage(index);
            }
            set
            {
                this.owner.SetTabPage(index, value, value.GetTCITEM());
            }
        }

        public virtual TabPage this[string key]
        {
            get
            {
                if (!string.IsNullOrEmpty(key))
                {
                    int index = this.IndexOfKey(key);
                    if (this.IsValidIndex(index))
                    {
                        return this[index];
                    }
                }
                return null;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                if (!(value is TabPage))
                {
                    throw new ArgumentException("value");
                }
                this[index] = (TabPage) value;
            }
        }
    }
}


}
