using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace mControl.WinCtl.Develop
{
    [Description("DescriptionContextMenuStrip"), ClassInterface(ClassInterfaceType.AutoDispatch), DefaultEvent("Opening"), ComVisible(true)]
    public class ContextMenuStrip : ToolStripDropDownMenu
    {
        // Methods
        public ContextMenuStrip()
        {
        }

        public ContextMenuStrip(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            container.Add(this);
        }

        internal ContextMenuStrip Clone()
        {
            ContextMenuStrip strip = new ContextMenuStrip();
            strip.Events.AddHandlers(base.Events);
            strip.AutoClose = base.AutoClose;
            strip.AutoSize = this.AutoSize;
            strip.Bounds = base.Bounds;
            strip.ImageList = base.ImageList;
            strip.ShowCheckMargin = base.ShowCheckMargin;
            strip.ShowImageMargin = base.ShowImageMargin;
            for (int i = 0; i < this.Items.Count; i++)
            {
                ToolStripItem item = this.Items[i];
                if (item is ToolStripSeparator)
                {
                    strip.Items.Add(new ToolStripSeparator());
                }
                else if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem item2 = item as ToolStripMenuItem;
                    strip.Items.Add(item2.Clone());
                }
            }
            return strip;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void SetVisibleCore(bool visible)
        {
            if (!visible)
            {
                base.WorkingAreaConstrained = true;
            }
            base.SetVisibleCore(visible);
        }

        internal void ShowInTaskbar(int x, int y)
        {
            base.WorkingAreaConstrained = false;
            Rectangle rect = CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.AboveLeft);
            Rectangle constrainingBounds = Screen.FromRectangle(rect).Bounds;
            if (rect.Y < constrainingBounds.Y)
            {
                rect = CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.BelowLeft);
            }
            else if (rect.X < constrainingBounds.X)
            {
                rect = CalculateDropDownLocation(new Point(x, y), ToolStripDropDownDirection.AboveRight);
            }
            rect = ConstrainToBounds(constrainingBounds, rect);
            base.Show(rect.X, rect.Y);
        }

        internal void ShowInternal(Control source, Point location, bool isKeyboardActivated)
        {
            base.Show(source, location);
            if (isKeyboardActivated)
            {
                //ToolStripManager.
                    ModalMenuFilter.Instance.ShowUnderlines = true;
            }
        }

        // Properties
        //[Description("ContextMenuStripSourceControl"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Control SourceControl
        //{
        //    [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        //    get
        //    {
        //        return base.SourceControlInternal;
        //    }
        //}

        #region function

        internal ToolStripMenuItem CloneItem(ToolStripMenuItem itm)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Events.AddHandlers(itm.Events);
            item.AccessibleName = itm.AccessibleName;
            item.AccessibleRole = itm.AccessibleRole;
            item.Alignment = itm.Alignment;
            item.AllowDrop = itm.AllowDrop;
            item.Anchor = itm.Anchor;
            item.AutoSize = itm.AutoSize;
            item.AutoToolTip = itm.AutoToolTip;
            item.BackColor = itm.BackColor;
            item.BackgroundImage = itm.BackgroundImage;
            item.BackgroundImageLayout = itm.BackgroundImageLayout;
            item.Checked = itm.Checked;
            item.CheckOnClick = itm.CheckOnClick;
            item.CheckState = itm.CheckState;
            item.DisplayStyle = itm.DisplayStyle;
            item.Dock = itm.Dock;
            item.DoubleClickEnabled = itm.DoubleClickEnabled;
            item.Enabled = itm.Enabled;
            item.Font = itm.Font;
            item.ForeColor = itm.ForeColor;
            item.Image = itm.Image;
            item.ImageAlign = itm.ImageAlign;
            item.ImageScaling = itm.ImageScaling;
            item.ImageTransparentColor = itm.ImageTransparentColor;
            item.Margin = itm.Margin;
            item.MergeAction = itm.MergeAction;
            item.MergeIndex = itm.MergeIndex;
            item.Name = itm.Name;
            item.Overflow = itm.Overflow;
            item.Padding = itm.Padding;
            item.RightToLeft = itm.RightToLeft;
            item.ShortcutKeys = itm.ShortcutKeys;
            item.ShowShortcutKeys = itm.ShowShortcutKeys;
            item.Tag = itm.Tag;
            item.Text = itm.Text;
            item.TextAlign = itm.TextAlign;
            item.TextDirection = itm.TextDirection;
            item.TextImageRelation = itm.TextImageRelation;
            item.ToolTipText = itm.ToolTipText;
            item.Visible = itm.Visible;// this.ParticipatesInLayout;
            if (!itm.AutoSize)
            {
                item.Size = itm.Size;
            }
            return item;
        }



        internal Rectangle CalculateDropDownLocation(Point start, ToolStripDropDownDirection dropDownDirection)
        {
            Point empty = Point.Empty;
            if (!base.IsHandleCreated)
            {
                LayoutTransaction.DoLayout(this, this, PropertyNames.PreferredSize);
            }
            Rectangle bounds = new Rectangle(Point.Empty, this.GetSuggestedSize());
            switch (dropDownDirection)
            {
                case ToolStripDropDownDirection.AboveLeft:
                    empty.X = -bounds.Width;
                    empty.Y = -bounds.Height;
                    break;

                case ToolStripDropDownDirection.AboveRight:
                    empty.Y = -bounds.Height;
                    break;

                case ToolStripDropDownDirection.BelowLeft:
                case ToolStripDropDownDirection.Left:
                    empty.X = -bounds.Width;
                    break;
            }
            bounds.Location = new Point(start.X + empty.X, start.Y + empty.Y);
            if (this.WorkingAreaConstrained)
            {
                bounds = WindowsFormsUtils.ConstrainToScreenWorkingAreaBounds(bounds);
            }
            return bounds;
        }

        internal static Rectangle ConstrainToScreenWorkingAreaBounds(Rectangle bounds)
        {
            return ConstrainToBounds(Screen.GetWorkingArea(bounds), bounds);
        }

        internal static Rectangle ConstrainToBounds(Rectangle constrainingBounds, Rectangle bounds)
        {
            if (!constrainingBounds.Contains(bounds))
            {
                bounds.Size = new Size(Math.Min(constrainingBounds.Width - 2, bounds.Width), Math.Min(constrainingBounds.Height - 2, bounds.Height));
                if (bounds.Right > constrainingBounds.Right)
                {
                    bounds.X = constrainingBounds.Right - bounds.Width;
                }
                else if (bounds.Left < constrainingBounds.Left)
                {
                    bounds.X = constrainingBounds.Left;
                }
                if (bounds.Bottom > constrainingBounds.Bottom)
                {
                    bounds.Y = (constrainingBounds.Bottom - 1) - bounds.Height;
                    return bounds;
                }
                if (bounds.Top < constrainingBounds.Top)
                {
                    bounds.Y = constrainingBounds.Top;
                }
            }
            return bounds;
        }





        #endregion
    }
    internal interface IMessageModifyAndFilter : IMessageFilter
    {
    }

 

 

    internal class ModalMenuFilter : IMessageModifyAndFilter, IMessageFilter
    {
        // Fields
        private HandleRef _activeHwnd = NativeMethods.NullHandleRef;
        private bool _caretHidden;
        private Timer _ensureMessageProcessingTimer;
        private bool _inMenuMode;
        private List<ToolStrip> _inputFilterQueue;
        [ThreadStatic]
        private static ToolStripManager.ModalMenuFilter _instance;
        private HandleRef _lastActiveWindow = NativeMethods.NullHandleRef;
        private bool _showUnderlines;
        private bool _suspendMenuMode;
        private ToolStrip _toplevelToolStrip;
        private bool menuKeyToggle;
        private const int MESSAGE_PROCESSING_INTERVAL = 500;
        private HostedWindowsFormsMessageHook messageHook;

        // Methods
        private ModalMenuFilter()
        {
        }

        internal static void CloseActiveDropDown(ToolStripDropDown activeToolStripDropDown, ToolStripDropDownCloseReason reason)
        {
            activeToolStripDropDown.SetCloseReason(reason);
            activeToolStripDropDown.Visible = false;
            if (GetActiveToolStrip() == null)
            {
                ExitMenuMode();
                if (activeToolStripDropDown.OwnerItem != null)
                {
                    activeToolStripDropDown.OwnerItem.Unselect();
                }
            }
        }

        private void EnterMenuModeCore()
        {
            if (!InMenuMode)
            {
                IntPtr handle = UnsafeNativeMethods.GetActiveWindow();
                if (handle != IntPtr.Zero)
                {
                    this.ActiveHwndInternal = new HandleRef(this, handle);
                }
                Application.ThreadContext.FromCurrent().AddMessageFilter(this);
                Application.ThreadContext.FromCurrent().TrackInput(true);
                if (!Application.MessageLoop)
                {
                    this.MessageHook.HookMessages = true;
                }
                this._inMenuMode = true;
                this.ProcessMessages(true);
            }
        }

        internal static void ExitMenuMode()
        {
            Instance.ExitMenuModeCore();
        }

        private void ExitMenuModeCore()
        {
            this.ProcessMessages(false);
            if (InMenuMode)
            {
                try
                {
                    if (this.messageHook != null)
                    {
                        this.messageHook.HookMessages = false;
                    }
                    Application.ThreadContext.FromCurrent().RemoveMessageFilter(this);
                    Application.ThreadContext.FromCurrent().TrackInput(false);
                    if (ActiveHwnd.Handle != IntPtr.Zero)
                    {
                        Control control = Control.FromHandleInternal(ActiveHwnd.Handle);
                        if (control != null)
                        {
                            control.HandleCreated -= new EventHandler(this.OnActiveHwndHandleCreated);
                        }
                        this.ActiveHwndInternal = NativeMethods.NullHandleRef;
                    }
                    if (this._inputFilterQueue != null)
                    {
                        this._inputFilterQueue.Clear();
                    }
                    if (this._caretHidden)
                    {
                        this._caretHidden = false;
                        SafeNativeMethods.ShowCaret(NativeMethods.NullHandleRef);
                    }
                }
                finally
                {
                    this._inMenuMode = false;
                    bool invalidateText = this._showUnderlines;
                    this._showUnderlines = false;
                    ToolStripManager.NotifyMenuModeChange(invalidateText, true);
                }
            }
        }

        internal static ToolStrip GetActiveToolStrip()
        {
            return Instance.GetActiveToolStripInternal();
        }

        internal ToolStrip GetActiveToolStripInternal()
        {
            if ((this._inputFilterQueue != null) && (this._inputFilterQueue.Count > 0))
            {
                return this._inputFilterQueue[this._inputFilterQueue.Count - 1];
            }
            return null;
        }

        private ToolStrip GetCurrentToplevelToolStrip()
        {
            if (this._toplevelToolStrip == null)
            {
                ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
                if (activeToolStripInternal != null)
                {
                    this._toplevelToolStrip = activeToolStripInternal.GetToplevelOwnerToolStrip();
                }
            }
            return this._toplevelToolStrip;
        }

        private static bool IsChildOrSameWindow(HandleRef hwndParent, HandleRef hwndChild)
        {
            return ((hwndParent.Handle == hwndChild.Handle) || UnsafeNativeMethods.IsChild(hwndParent, hwndChild));
        }

        private static bool IsKeyOrMouseMessage(Message m)
        {
            bool flag = false;
            if ((m.Msg >= 0x200) && (m.Msg <= 0x20a))
            {
                return true;
            }
            if ((m.Msg >= 0xa1) && (m.Msg <= 0xa9))
            {
                return true;
            }
            if ((m.Msg >= 0x100) && (m.Msg <= 0x108))
            {
                flag = true;
            }
            return flag;
        }

        private void OnActiveHwndHandleCreated(object sender, EventArgs e)
        {
            Control control = sender as Control;
            this.ActiveHwndInternal = new HandleRef(this, control.Handle);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!this._suspendMenuMode)
            {
                ToolStrip toolStrip = GetActiveToolStrip();
                if (toolStrip == null)
                {
                    return false;
                }
                if (toolStrip.IsDisposed)
                {
                    this.RemoveActiveToolStripCore(toolStrip);
                    return false;
                }
                HandleRef hwndChild = new HandleRef(toolStrip, toolStrip.Handle);
                HandleRef hwndParent = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
                if (hwndParent.Handle != this._lastActiveWindow.Handle)
                {
                    if (hwndParent.Handle == IntPtr.Zero)
                    {
                        this.ProcessActivationChange();
                    }
                    else if ((!(Control.FromChildHandleInternal(hwndParent.Handle) is ToolStripDropDown) && !IsChildOrSameWindow(hwndParent, hwndChild)) && !IsChildOrSameWindow(hwndParent, ActiveHwnd))
                    {
                        this.ProcessActivationChange();
                    }
                }
                this._lastActiveWindow = hwndParent;
                if (!IsKeyOrMouseMessage(m))
                {
                    return false;
                }
                switch (m.Msg)
                {
                    case 160:
                    case 0x200:
                        {
                            Control control = Control.FromChildHandleInternal(m.HWnd);
                            if (((control != null) && (control.TopLevelControlInternal is ToolStripDropDown)) || IsChildOrSameWindow(hwndChild, new HandleRef(null, m.HWnd)))
                            {
                                break;
                            }
                            ToolStrip wrapper = this.GetCurrentToplevelToolStrip();
                            if ((wrapper != null) && IsChildOrSameWindow(new HandleRef(wrapper, wrapper.Handle), new HandleRef(null, m.HWnd)))
                            {
                                return false;
                            }
                            if (!IsChildOrSameWindow(ActiveHwnd, new HandleRef(null, m.HWnd)))
                            {
                                return false;
                            }
                            return true;
                        }
                    case 0xa1:
                    case 0xa4:
                    case 0xa7:
                        this.ProcessMouseButtonPressed(IntPtr.Zero, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
                        break;

                    case 0x100:
                    case 0x101:
                    case 0x102:
                    case 0x103:
                    case 260:
                    case 0x105:
                    case 0x106:
                    case 0x107:
                        if (!toolStrip.ContainsFocus)
                        {
                            m.HWnd = toolStrip.Handle;
                        }
                        break;

                    case 0x201:
                    case 0x204:
                    case 0x207:
                        this.ProcessMouseButtonPressed(m.HWnd, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
                        break;
                }
            }
            return false;
        }

        private bool ProcessActivationChange()
        {
            int count = this._inputFilterQueue.Count;
            for (int i = 0; i < count; i++)
            {
                ToolStripDropDown down = this.GetActiveToolStripInternal() as ToolStripDropDown;
                if ((down != null) && down.AutoClose)
                {
                    down.Visible = false;
                }
            }
            this.ExitMenuModeCore();
            return true;
        }

        internal static void ProcessMenuKeyDown(ref Message m)
        {
            Keys keyData = (Keys)((int)m.WParam);
            ToolStrip strip = Control.FromHandleInternal(m.HWnd) as ToolStrip;
            if (((strip == null) || strip.IsDropDown) && ToolStripManager.IsMenuKey(keyData))
            {
                if (!InMenuMode && MenuKeyToggle)
                {
                    MenuKeyToggle = false;
                }
                else if (!MenuKeyToggle)
                {
                    Instance.ShowUnderlines = true;
                }
            }
        }

        private void ProcessMessages(bool process)
        {
            if (process)
            {
                if (this._ensureMessageProcessingTimer == null)
                {
                    this._ensureMessageProcessingTimer = new Timer();
                }
                this._ensureMessageProcessingTimer.Interval = 500;
                this._ensureMessageProcessingTimer.Enabled = true;
            }
            else if (this._ensureMessageProcessingTimer != null)
            {
                this._ensureMessageProcessingTimer.Enabled = false;
                this._ensureMessageProcessingTimer.Dispose();
                this._ensureMessageProcessingTimer = null;
            }
        }

        private void ProcessMouseButtonPressed(IntPtr hwndMouseMessageIsFrom, int x, int y)
        {
            int count = this._inputFilterQueue.Count;
            for (int i = 0; i < count; i++)
            {
                ToolStrip wrapper = this.GetActiveToolStripInternal();
                if (wrapper == null)
                {
                    break;
                }
                NativeMethods.POINT pt = new NativeMethods.POINT();
                pt.x = x;
                pt.y = y;
                UnsafeNativeMethods.MapWindowPoints(new HandleRef(wrapper, hwndMouseMessageIsFrom), new HandleRef(wrapper, wrapper.Handle), pt, 1);
                if (wrapper.ClientRectangle.Contains(pt.x, pt.y))
                {
                    break;
                }
                ToolStripDropDown activeToolStripDropDown = wrapper as ToolStripDropDown;
                if (activeToolStripDropDown != null)
                {
                    if (((activeToolStripDropDown.OwnerToolStrip == null) || (activeToolStripDropDown.OwnerToolStrip.Handle != hwndMouseMessageIsFrom)) || ((activeToolStripDropDown.OwnerDropDownItem == null) || !activeToolStripDropDown.OwnerDropDownItem.DropDownButtonArea.Contains(x, y)))
                    {
                        CloseActiveDropDown(activeToolStripDropDown, ToolStripDropDownCloseReason.AppClicked);
                    }
                }
                else
                {
                    wrapper.NotifySelectionChange(null);
                    this.ExitMenuModeCore();
                }
            }
        }

        internal static void RemoveActiveToolStrip(ToolStrip toolStrip)
        {
            Instance.RemoveActiveToolStripCore(toolStrip);
        }

        private void RemoveActiveToolStripCore(ToolStrip toolStrip)
        {
            this._toplevelToolStrip = null;
            if (this._inputFilterQueue != null)
            {
                this._inputFilterQueue.Remove(toolStrip);
            }
        }

        internal static void ResumeMenuMode()
        {
            Instance._suspendMenuMode = false;
        }

        internal static void SetActiveToolStrip(ToolStrip toolStrip)
        {
            Instance.SetActiveToolStripCore(toolStrip);
        }

        internal static void SetActiveToolStrip(ToolStrip toolStrip, bool menuKeyPressed)
        {
            if (!InMenuMode && menuKeyPressed)
            {
                Instance.ShowUnderlines = true;
            }
            Instance.SetActiveToolStripCore(toolStrip);
        }

        private void SetActiveToolStripCore(ToolStrip toolStrip)
        {
            if (toolStrip != null)
            {
                if (toolStrip.IsDropDown)
                {
                    ToolStripDropDown down = toolStrip as ToolStripDropDown;
                    if (!down.AutoClose)
                    {
                        IntPtr handle = UnsafeNativeMethods.GetActiveWindow();
                        if (handle != IntPtr.Zero)
                        {
                            this.ActiveHwndInternal = new HandleRef(this, handle);
                        }
                        return;
                    }
                }
                toolStrip.KeyboardActive = true;
                if (this._inputFilterQueue == null)
                {
                    this._inputFilterQueue = new List<ToolStrip>();
                }
                else
                {
                    ToolStrip item = this.GetActiveToolStripInternal();
                    if (item != null)
                    {
                        if (!item.IsDropDown)
                        {
                            this._inputFilterQueue.Remove(item);
                        }
                        else if (toolStrip.IsDropDown && (ToolStripDropDown.GetFirstDropDown(toolStrip) != ToolStripDropDown.GetFirstDropDown(item)))
                        {
                            this._inputFilterQueue.Remove(item);
                            (item as ToolStripDropDown).DismissAll();
                        }
                    }
                }
                this._toplevelToolStrip = null;
                this._inputFilterQueue.Add(toolStrip);
                if (!InMenuMode && (this._inputFilterQueue.Count > 0))
                {
                    this.EnterMenuModeCore();
                }
                if ((!this._caretHidden && toolStrip.IsDropDown) && InMenuMode)
                {
                    this._caretHidden = true;
                    SafeNativeMethods.HideCaret(NativeMethods.NullHandleRef);
                }
            }
        }

        internal static void SuspendMenuMode()
        {
            Instance._suspendMenuMode = true;
        }

        // Properties
        internal static HandleRef ActiveHwnd
        {
            get
            {
                return Instance.ActiveHwndInternal;
            }
        }

        private HandleRef ActiveHwndInternal
        {
            get
            {
                return this._activeHwnd;
            }
            set
            {
                if (this._activeHwnd.Handle != value.Handle)
                {
                    Control control = null;
                    if (this._activeHwnd.Handle != IntPtr.Zero)
                    {
                        control = Control.FromHandleInternal(this._activeHwnd.Handle);
                        if (control != null)
                        {
                            control.HandleCreated -= new EventHandler(this.OnActiveHwndHandleCreated);
                        }
                    }
                    this._activeHwnd = value;
                    control = Control.FromHandleInternal(this._activeHwnd.Handle);
                    if (control != null)
                    {
                        control.HandleCreated += new EventHandler(this.OnActiveHwndHandleCreated);
                    }
                }
            }
        }

        internal static bool InMenuMode
        {
            get
            {
                return Instance._inMenuMode;
            }
        }

        internal static ToolStripManager.ModalMenuFilter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ToolStripManager.ModalMenuFilter();
                }
                return _instance;
            }
        }

        internal static bool MenuKeyToggle
        {
            get
            {
                return Instance.menuKeyToggle;
            }
            set
            {
                if (Instance.menuKeyToggle != value)
                {
                    Instance.menuKeyToggle = value;
                }
            }
        }

        private HostedWindowsFormsMessageHook MessageHook
        {
            get
            {
                if (this.messageHook == null)
                {
                    this.messageHook = new HostedWindowsFormsMessageHook();
                }
                return this.messageHook;
            }
        }

        public bool ShowUnderlines
        {
            get
            {
                return this._showUnderlines;
            }
            set
            {
                if (this._showUnderlines != value)
                {
                    this._showUnderlines = value;
                    ToolStripManager.NotifyMenuModeChange(true, false);
                }
            }
        }

        // Nested Types
        private class HostedWindowsFormsMessageHook
        {
            // Fields
            private NativeMethods.HookProc hookProc;
            private bool isHooked;
            private IntPtr messageHookHandle = IntPtr.Zero;

            // Methods
            private void InstallMessageHook()
            {
                lock (this)
                {
                    if (this.messageHookHandle == IntPtr.Zero)
                    {
                        this.hookProc = new NativeMethods.HookProc(this.MessageHookProc);
                        this.messageHookHandle = UnsafeNativeMethods.SetWindowsHookEx(3, this.hookProc, new HandleRef(null, IntPtr.Zero), SafeNativeMethods.GetCurrentThreadId());
                        if (this.messageHookHandle != IntPtr.Zero)
                        {
                            this.isHooked = true;
                        }
                    }
                }
            }

        private unsafe IntPtr MessageHookProc(int nCode, IntPtr wparam, IntPtr lparam)
        {
            if (((nCode == 0) && this.isHooked) && (((int) wparam) == 1))
            {
                NativeMethods.MSG* msgPtr = (NativeMethods.MSG*) lparam;
                if ((msgPtr != null) && Application.ThreadContext.FromCurrent().PreTranslateMessage(ref (NativeMethods.MSG) ref msgPtr))
                {
                    msgPtr->message = 0;
                }
            }
            return UnsafeNativeMethods.CallNextHookEx(new HandleRef(this, this.messageHookHandle), nCode, wparam, lparam);
        }

            private void UninstallMessageHook()
            {
                lock (this)
                {
                    if (this.messageHookHandle != IntPtr.Zero)
                    {
                        UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(this, this.messageHookHandle));
                        this.hookProc = null;
                        this.messageHookHandle = IntPtr.Zero;
                        this.isHooked = false;
                    }
                }
            }

            // Properties
            public bool HookMessages
            {
                get
                {
                    return (this.messageHookHandle != IntPtr.Zero);
                }
                set
                {
                    if (value)
                    {
                        this.InstallMessageHook();
                    }
                    else
                    {
                        this.UninstallMessageHook();
                    }
                }
            }
        }
    }



}
