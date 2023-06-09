using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Security.Permissions;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using mControl.Util;
using mControl.Win32;
using mControl.Collections;

namespace mControl.WinCtl.Controls
{
    [ComVisible(true), Designer("System.Windows.Forms.Design.TextBoxBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultEvent("TextChanged"), DefaultBindingProperty("Text"), ClassInterface(ClassInterfaceType.AutoDispatch)]
    public abstract class TextBoxBase : Control
    {
        // Fields
        private static readonly int acceptsTab = BitVector32.CreateMask(readOnly);
        private static readonly int autoSize = BitVector32.CreateMask();
        private BorderStyle borderStyle = BorderStyle.Fixed3D;
        private static readonly int codeUpdateText = BitVector32.CreateMask(creatingHandle);
        private static readonly int creatingHandle = BitVector32.CreateMask(wordWrap);
        private bool doubleClickFired;
        private static readonly object EVENT_ACCEPTSTABCHANGED = new object();
        private static readonly object EVENT_BORDERSTYLECHANGED = new object();
        private static readonly object EVENT_HIDESELECTIONCHANGED = new object();
        private static readonly object EVENT_MODIFIEDCHANGED = new object();
        private static readonly object EVENT_MULTILINECHANGED = new object();
        private static readonly object EVENT_READONLYCHANGED = new object();
        private static readonly int hideSelection = BitVector32.CreateMask(autoSize);
        private bool integralHeightAdjust;
        private int maxLength = 0x7fff;
        private static readonly int modified = BitVector32.CreateMask(multiline);
        private static readonly int multiline = BitVector32.CreateMask(hideSelection);
        private static readonly int readOnly = BitVector32.CreateMask(modified);
        private int requestedHeight;
        private static readonly int scrollToCaretOnHandleCreated = BitVector32.CreateMask(shortcutsEnabled);
        private int selectionLength;
        private int selectionStart;
        private static readonly int setSelectionOnHandleCreated = BitVector32.CreateMask(scrollToCaretOnHandleCreated);
        private static readonly int shortcutsEnabled = BitVector32.CreateMask(codeUpdateText);
        private static int[] shortcutsToDisable;
        private BitVector32 textBoxFlags = new BitVector32();
        private static readonly int wordWrap = BitVector32.CreateMask(acceptsTab);

        // Events
        [Category("PropertyChanged"), Description("TextBoxBaseOnAcceptsTabChanged")]
        public event EventHandler AcceptsTabChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_ACCEPTSTABCHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_ACCEPTSTABCHANGED, value);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Description("TextBoxBaseOnBorderStyleChanged"), Category("PropertyChanged")]
        public event EventHandler BorderStyleChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_BORDERSTYLECHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_BORDERSTYLECHANGED, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public event EventHandler Click
        {
            add
            {
                base.Click += value;
            }
            remove
            {
                base.Click -= value;
            }
        }

        [Description("TextBoxBaseOnHideSelectionChanged"), Category("PropertyChanged")]
        public event EventHandler HideSelectionChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_HIDESELECTIONCHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_HIDESELECTIONCHANGED, value);
            }
        }

        [Description("TextBoxBaseOnModifiedChanged"), Category("PropertyChanged")]
        public event EventHandler ModifiedChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_MODIFIEDCHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_MODIFIEDCHANGED, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public event MouseEventHandler MouseClick
        {
            add
            {
                base.MouseClick += value;
            }
            remove
            {
                base.MouseClick -= value;
            }
        }

        [Category("PropertyChanged"), Description("TextBoxBaseOnMultilineChanged")]
        public event EventHandler MultilineChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_MULTILINECHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_MULTILINECHANGED, value);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ControlOnPaddingChanged"), Category("Layout")]
        public event EventHandler PaddingChanged
        {
            add
            {
                base.PaddingChanged += value;
            }
            remove
            {
                base.PaddingChanged -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Description("TextBoxBaseOnReadOnlyChanged"), Category("PropertyChanged")]
        public event EventHandler ReadOnlyChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_READONLYCHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_READONLYCHANGED, value);
            }
        }

        // Methods
        internal TextBoxBase()
        {
            base.SetState2(0x800, true);
            this.textBoxFlags[((autoSize | hideSelection) | wordWrap) | shortcutsEnabled] = true;
            base.SetStyle(ControlStyles.FixedHeight, this.textBoxFlags[autoSize]);
            base.SetStyle(ControlStyles.UseTextForAccessibility | ControlStyles.StandardDoubleClick | ControlStyles.StandardClick | ControlStyles.UserPaint, false);
            this.requestedHeight = base.Height;
        }

        private void AdjustHeight(bool returnIfAnchored)
        {
            if (!returnIfAnchored || ((this.Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top)))
            {
                int requestedHeight = this.requestedHeight;
                try
                {
                    if (this.textBoxFlags[autoSize] && !this.textBoxFlags[multiline])
                    {
                        base.Height = this.PreferredHeight;
                    }
                    else
                    {
                        int height = base.Height;
                        if (this.textBoxFlags[multiline])
                        {
                            base.Height = Math.Max(requestedHeight, this.PreferredHeight + 2);
                        }
                        this.integralHeightAdjust = true;
                        try
                        {
                            base.Height = requestedHeight;
                        }
                        finally
                        {
                            this.integralHeightAdjust = false;
                        }
                    }
                }
                finally
                {
                    this.requestedHeight = requestedHeight;
                }
            }
        }

        internal void AdjustSelectionStartAndEnd(int selStart, int selLength, out int start, out int end, int textLen)
        {
            start = selStart;
            end = 0;
            if (start <= -1)
            {
                start = -1;
            }
            else
            {
                int textLength;
                if (textLen >= 0)
                {
                    textLength = textLen;
                }
                else
                {
                    textLength = this.TextLength;
                }
                if (start > textLength)
                {
                    start = textLength;
                }
                try
                {
                    end = start + selLength;
                }
                catch (OverflowException)
                {
                    end = (start > 0) ? 0x7fffffff : -2147483648;
                }
                if (end < 0)
                {
                    end = 0;
                }
                else if (end > textLength)
                {
                    end = textLength;
                }
                if (this.SelectionUsesDbcsOffsetsInWin9x && (Marshal.SystemDefaultCharSize == 1))
                {
                    ToDbcsOffsets(this.WindowText, ref start, ref end);
                }
            }
        }

        public void AppendText(string text)
        {
            if (text.Length > 0)
            {
                int num;
                int num2;
                this.GetSelectionStartAndLength(out num, out num2);
                try
                {
                    int endPosition = this.GetEndPosition();
                    this.SelectInternal(endPosition, endPosition, endPosition);
                    this.SelectedText = text;
                }
                finally
                {
                    if ((base.Width == 0) || (base.Height == 0))
                    {
                        this.Select(num, num2);
                    }
                }
            }
        }

        public void Clear()
        {
            this.Text = null;
        }

        public void ClearUndo()
        {
            if (base.IsHandleCreated)
            {
                base.SendMessage(0xcd, 0, 0);
            }
        }

        [UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
        public void Copy()
        {
            base.SendMessage(0x301, 0, 0);
        }

        protected override void CreateHandle()
        {
            this.textBoxFlags[creatingHandle] = true;
            try
            {
                base.CreateHandle();
                if (this.SetSelectionInCreateHandle)
                {
                    this.SetSelectionOnHandle();
                }
            }
            finally
            {
                this.textBoxFlags[creatingHandle] = false;
            }
        }

        public void Cut()
        {
            base.SendMessage(0x300, 0, 0);
        }

        public void DeselectAll()
        {
            this.SelectionLength = 0;
        }

        internal void ForceWindowText(string value)
        {
            if (value == null)
            {
                value = "";
            }
            this.textBoxFlags[codeUpdateText] = true;
            try
            {
                if (base.IsHandleCreated)
                {
                    UnsafeNativeMethods.SetWindowText(new HandleRef(this, base.Handle), value);
                }
                else if (value.Length == 0)
                {
                    this.Text = null;
                }
                else
                {
                    this.Text = value;
                }
            }
            finally
            {
                this.textBoxFlags[codeUpdateText] = false;
            }
        }

        public virtual char GetCharFromPosition(Point pt)
        {
            string text = this.Text;
            int charIndexFromPosition = this.GetCharIndexFromPosition(pt);
            if ((charIndexFromPosition >= 0) && (charIndexFromPosition < text.Length))
            {
                return text[charIndexFromPosition];
            }
            return '\0';
        }

        public virtual int GetCharIndexFromPosition(Point pt)
        {
            int lParam = NativeMethods.Util.MAKELONG(pt.X, pt.Y);
            int n = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0xd7, 0, lParam);
            n = NativeMethods.Util.LOWORD(n);
            if (n < 0)
            {
                return 0;
            }
            string text = this.Text;
            if (n >= text.Length)
            {
                n = Math.Max(text.Length - 1, 0);
            }
            return n;
        }

        internal virtual int GetEndPosition()
        {
            if (!base.IsHandleCreated)
            {
                return this.TextLength;
            }
            return (this.TextLength + 1);
        }

        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            if (lineNumber < 0)
            {
                throw new ArgumentOutOfRangeException("lineNumber", "InvalidArgument", new object[] { "lineNumber", lineNumber.ToString(CultureInfo.CurrentCulture) });
            }
            return (int)base.SendMessage(0xbb, lineNumber, 0);
        }

        public int GetFirstCharIndexOfCurrentLine()
        {
            return (int)base.SendMessage(0xbb, -1, 0);
        }

        public virtual int GetLineFromCharIndex(int index)
        {
            return (int)base.SendMessage(0xc9, index, 0);
        }

        public virtual Point GetPositionFromCharIndex(int index)
        {
            if ((index < 0) || (index >= this.Text.Length))
            {
                return Point.Empty;
            }
            int n = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0xd6, index, 0);
            return new Point(NativeMethods.Util.LOWORD(n), NativeMethods.Util.HIWORD(n));
        }

        internal Size GetPreferredSizeCore(Size proposedConstraints)
        {
            Size size = this.SizeFromClientSize(Size.Empty) + this.Padding.Size;
            if (this.BorderStyle != BorderStyle.None)
            {
                size += new Size(0, 3);
            }
            if (this.BorderStyle == BorderStyle.FixedSingle)
            {
                size.Width += 2;
                size.Height += 2;
            }
            proposedConstraints -= size;
            TextFormatFlags glyphOverhangPadding = TextFormatFlags.GlyphOverhangPadding;
            if (!this.Multiline)
            {
                glyphOverhangPadding = TextFormatFlags.SingleLine;
            }
            else if (this.WordWrap)
            {
                glyphOverhangPadding = TextFormatFlags.WordBreak;
            }
            Size size2 = TextRenderer.MeasureText(this.Text, this.Font, proposedConstraints, glyphOverhangPadding);
            size2.Height = Math.Max(size2.Height, base.FontHeight);
            return (size2 + size);
        }

        internal void GetSelectionStartAndLength(out int start, out int length)
        {
            int end = 0;
            if (!base.IsHandleCreated)
            {
                this.AdjustSelectionStartAndEnd(this.selectionStart, this.selectionLength, out start, out end, -1);
                length = end - start;
            }
            else
            {
                start = 0;
                UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref start, ref end);
                start = Math.Max(0, start);
                end = Math.Max(0, end);
                if (this.SelectionUsesDbcsOffsetsInWin9x && (Marshal.SystemDefaultCharSize == 1))
                {
                    ToUnicodeOffsets(this.WindowText, ref start, ref end);
                }
                length = end - start;
            }
        }

        internal IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
        {
            if ((msg == 0x138) && !this.ShouldSerializeBackColor())
            {
                return IntPtr.Zero;
            }
            return base.InitializeDCForWmCtlColor(dc, msg);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) != Keys.Alt)
            {
                switch ((keyData & Keys.KeyCode))
                {
                    case Keys.Prior:
                    case Keys.Next:
                    case Keys.End:
                    case Keys.Home:
                        return true;

                    case Keys.Escape:
                        if (this.Multiline)
                        {
                            return false;
                        }
                        break;

                    case Keys.Tab:
                        if (this.Multiline && this.textBoxFlags[acceptsTab])
                        {
                            return ((keyData & Keys.Control) == Keys.None);
                        }
                        return false;
                }
            }
            return base.IsInputKey(keyData);
        }

        protected virtual void OnAcceptsTabChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_ACCEPTSTABCHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_BORDERSTYLECHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.AdjustHeight(false);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CommonProperties.xClearPreferredSizeCache(this);
            this.AdjustHeight(true);
            this.UpdateMaxLength();
            if (this.textBoxFlags[modified])
            {
                base.SendMessage(0xb9, 1, 0);
            }
            if (this.textBoxFlags[scrollToCaretOnHandleCreated])
            {
                this.ScrollToCaret();
                this.textBoxFlags[scrollToCaretOnHandleCreated] = false;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            this.textBoxFlags[modified] = this.Modified;
            this.textBoxFlags[setSelectionOnHandleCreated] = true;
            this.GetSelectionStartAndLength(out this.selectionStart, out this.selectionLength);
            base.OnHandleDestroyed(e);
        }

        protected virtual void OnHideSelectionChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_HIDESELECTIONCHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnModifiedChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_MODIFIEDCHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            Point point = base.PointToScreen(mevent.Location);
            if (mevent.Button == MouseButtons.Left)
            {
                if (!base.ValidationCancelled && (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle))
                {
                    if (!this.doubleClickFired)
                    {
                        this.OnClick(mevent);
                        this.OnMouseClick(mevent);
                    }
                    else
                    {
                        this.doubleClickFired = false;
                        this.OnDoubleClick(mevent);
                        this.OnMouseDoubleClick(mevent);
                    }
                }
                this.doubleClickFired = false;
            }
            base.OnMouseUp(mevent);
        }

        protected virtual void OnMultilineChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_MULTILINECHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            this.AdjustHeight(false);
        }

        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_READONLYCHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            CommonProperties.xClearPreferredSizeCache(this);
            base.OnTextChanged(e);
        }

        [UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
        public void Paste()
        {
            IntSecurity.ClipboardRead.Demand();
            base.SendMessage(770, 0, 0);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!this.ShortcutsEnabled)
            {
                foreach (int num in shortcutsToDisable)
                {
                    if ((keyData == num) || (keyData == (num | 0x10000)))
                    {
                        return true;
                    }
                }
            }
            if (this.textBoxFlags[readOnly])
            {
                switch (((int)keyData))
                {
                    case 0x2004c:
                    case 0x20052:
                    case 0x20045:
                    case 0x2004a:
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys keys = keyData & Keys.KeyCode;
            if (((keys == Keys.Tab) && this.AcceptsTab) && ((keyData & Keys.Control) != Keys.None))
            {
                keyData &= ~Keys.Control;
            }
            return base.ProcessDialogKey(keyData);
        }

        public void ScrollToCaret()
        {
            if (base.IsHandleCreated)
            {
                if (!string.IsNullOrEmpty(this.WindowText))
                {
                    bool flag = false;
                    object editOle = null;
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        if (UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0x43c, 0, out editOle) != 0)
                        {
                            zero = Marshal.GetIUnknownForObject(editOle);
                            if (zero != IntPtr.Zero)
                            {
                                IntPtr ppv = IntPtr.Zero;
                                Guid gUID = typeof(UnsafeNativeMethods.ITextDocument).GUID;
                                try
                                {
                                    Marshal.QueryInterface(zero, ref gUID, out ppv);
                                    UnsafeNativeMethods.ITextDocument objectForIUnknown = Marshal.GetObjectForIUnknown(ppv) as UnsafeNativeMethods.ITextDocument;
                                    if (objectForIUnknown != null)
                                    {
                                        int num;
                                        int num2;
                                        this.GetSelectionStartAndLength(out num, out num2);
                                        int lineFromCharIndex = this.GetLineFromCharIndex(num);
                                        objectForIUnknown.Range(this.WindowText.Length - 1, this.WindowText.Length - 1).ScrollIntoView(0);
                                        int num4 = (int)base.SendMessage(0xce, 0, 0);
                                        if (num4 > lineFromCharIndex)
                                        {
                                            objectForIUnknown.Range(num, num + num2).ScrollIntoView(0x20);
                                        }
                                        flag = true;
                                    }
                                }
                                finally
                                {
                                    if (ppv != IntPtr.Zero)
                                    {
                                        Marshal.Release(ppv);
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                            Marshal.Release(zero);
                        }
                    }
                    if (!flag)
                    {
                        base.SendMessage(0xb7, 0, 0);
                    }
                }
            }
            else
            {
                this.textBoxFlags[scrollToCaretOnHandleCreated] = true;
            }
        }

        public void Select(int start, int length)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", "InvalidArgument", new object[] { "start", start.ToString(CultureInfo.CurrentCulture) });
            }
            int textLength = this.TextLength;
            if (start > textLength)
            {
                long num2 = Math.Min(0L, (long)((length + start) - textLength));
                if (num2 < -2147483648L)
                {
                    length = -2147483648;
                }
                else
                {
                    length = (int)num2;
                }
                start = textLength;
            }
            this.SelectInternal(start, length, textLength);
        }

        public void SelectAll()
        {
            int textLength = this.TextLength;
            this.SelectInternal(0, textLength, textLength);
        }

        internal virtual void SelectInternal(int start, int length, int textLen)
        {
            if (base.IsHandleCreated)
            {
                int num;
                int num2;
                this.AdjustSelectionStartAndEnd(start, length, out num, out num2, textLen);
                base.SendMessage(0xb1, num, num2);
            }
            else
            {
                this.selectionStart = start;
                this.selectionLength = length;
                this.textBoxFlags[setSelectionOnHandleCreated] = true;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!this.integralHeightAdjust && (height != base.Height))
            {
                this.requestedHeight = height;
            }
            if (this.textBoxFlags[autoSize] && !this.textBoxFlags[multiline])
            {
                height = this.PreferredHeight;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        internal virtual void SetSelectedTextInternal(string text, bool clearUndo)
        {
            if (!base.IsHandleCreated)
            {
                this.CreateHandle();
            }
            if (text == null)
            {
                text = "";
            }
            base.SendMessage(0xc5, 0, 0);
            if (clearUndo)
            {
                base.SendMessage(0xc2, 0, text);
                base.SendMessage(0xb9, 0, 0);
                this.ClearUndo();
            }
            else
            {
                base.SendMessage(0xc2, -1, text);
            }
            base.SendMessage(0xc5, this.maxLength, 0);
        }

        internal void SetSelectionOnHandle()
        {
            if (this.textBoxFlags[setSelectionOnHandleCreated])
            {
                int num;
                int num2;
                this.textBoxFlags[setSelectionOnHandleCreated] = false;
                this.AdjustSelectionStartAndEnd(this.selectionStart, this.selectionLength, out num, out num2, -1);
                base.SendMessage(0xb1, num, num2);
            }
        }

        private static void Swap(ref int n1, ref int n2)
        {
            int num = n2;
            n2 = n1;
            n1 = num;
        }

        internal static void ToDbcsOffsets(string str, ref int start, ref int end)
        {
            Encoding encoding = Encoding.Default;
            bool flag = start > end;
            if (flag)
            {
                Swap(ref start, ref end);
            }
            if (start < 0)
            {
                start = 0;
            }
            if (start > str.Length)
            {
                start = str.Length;
            }
            if (end < start)
            {
                end = start;
            }
            if (end > str.Length)
            {
                end = str.Length;
            }
            int num = (start == 0) ? 0 : encoding.GetByteCount(str.Substring(0, start));
            end = num + encoding.GetByteCount(str.Substring(start, end - start));
            start = num;
            if (flag)
            {
                Swap(ref start, ref end);
            }
        }

        public override string ToString()
        {
            string str = base.ToString();
            string text = this.Text;
            if (text.Length > 40)
            {
                text = text.Substring(0, 40) + "...";
            }
            return (str + ", Text: " + text.ToString());
        }

        private static void ToUnicodeOffsets(string str, ref int start, ref int end)
        {
            Encoding encoding = Encoding.Default;
            byte[] bytes = encoding.GetBytes(str);
            bool flag = start > end;
            if (flag)
            {
                Swap(ref start, ref end);
            }
            if (start < 0)
            {
                start = 0;
            }
            if (start > bytes.Length)
            {
                start = bytes.Length;
            }
            if (end > bytes.Length)
            {
                end = bytes.Length;
            }
            int num = (start == 0) ? 0 : encoding.GetCharCount(bytes, 0, start);
            end = num + encoding.GetCharCount(bytes, start, end - start);
            start = num;
            if (flag)
            {
                Swap(ref start, ref end);
            }
        }

        public void Undo()
        {
            base.SendMessage(0xc7, 0, 0);
        }

        internal virtual void UpdateMaxLength()
        {
            if (base.IsHandleCreated)
            {
                base.SendMessage(0xc5, this.maxLength, 0);
            }
        }

        private void WmGetDlgCode(ref Message m)
        {
            base.WndProc(ref m);
            if (this.AcceptsTab)
            {
                m.Result = (IntPtr)(((int)m.Result) | 2);
            }
            else
            {
                m.Result = (IntPtr)(((int)m.Result) & -7);
            }
        }

        private void WmReflectCommand(ref Message m)
        {
            if (!this.textBoxFlags[codeUpdateText] && !this.textBoxFlags[creatingHandle])
            {
                if ((NativeMethods.Util.HIWORD(m.WParam) == 0x300) && this.CanRaiseTextChangedEvent)
                {
                    this.OnTextChanged(EventArgs.Empty);
                }
                else if (NativeMethods.Util.HIWORD(m.WParam) == 0x400)
                {
                    bool modified = this.Modified;
                }
            }
        }

        private void WmSetFont(ref Message m)
        {
            base.WndProc(ref m);
            if (!this.textBoxFlags[multiline])
            {
                base.SendMessage(0xd3, 3, 0);
            }
        }

        private void WmTextBoxContextMenu(ref Message m)
        {
            if ((this.ContextMenu != null) || (this.ContextMenuStrip != null))
            {
                Point point;
                int x = NativeMethods.Util.SignedLOWORD(m.LParam);
                int y = NativeMethods.Util.SignedHIWORD(m.LParam);
                bool isKeyboardActivated = false;
                if (((int)((long)m.LParam)) == -1)
                {
                    isKeyboardActivated = true;
                    point = new Point(base.Width / 2, base.Height / 2);
                }
                else
                {
                    point = base.PointToClientInternal(new Point(x, y));
                }
                if (base.ClientRectangle.Contains(point))
                {
                    if (this.ContextMenu != null)
                    {
                        this.ContextMenu.Show(this, point);
                    }
                    else if (this.ContextMenuStrip != null)
                    {
                        this.ContextMenuStrip.ShowInternal(this, point, isKeyboardActivated);
                    }
                    else
                    {
                        this.DefWndProc(ref m);
                    }
                }
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x87:
                    this.WmGetDlgCode(ref m);
                    return;

                case 0x203:
                    this.doubleClickFired = true;
                    base.WndProc(ref m);
                    return;

                case 0x2111:
                    this.WmReflectCommand(ref m);
                    return;

                case 0x30:
                    this.WmSetFont(ref m);
                    return;

                case 0x7b:
                    if (this.ShortcutsEnabled)
                    {
                        base.WndProc(ref m);
                        return;
                    }
                    this.WmTextBoxContextMenu(ref m);
                    return;
            }
            base.WndProc(ref m);
        }

        // Properties
        [DefaultValue(false), Description("TextBoxAcceptsTab"), Category("Behavior")]
        public bool AcceptsTab
        {
            get
            {
                return this.textBoxFlags[acceptsTab];
            }
            set
            {
                if (this.textBoxFlags[acceptsTab] != value)
                {
                    this.textBoxFlags[acceptsTab] = value;
                    this.OnAcceptsTabChanged(EventArgs.Empty);
                }
            }
        }

        [DefaultValue(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Category("Behavior"), Localizable(true), Description("TextBoxAutoSize"), RefreshProperties(RefreshProperties.Repaint)]
        public override bool AutoSize
        {
            get
            {
                return this.textBoxFlags[autoSize];
            }
            set
            {
                if (this.textBoxFlags[autoSize] != value)
                {
                    this.textBoxFlags[autoSize] = value;
                    if (!this.Multiline)
                    {
                        base.SetStyle(ControlStyles.FixedHeight, value);
                        this.AdjustHeight(false);
                    }
                    this.OnAutoSizeChanged(EventArgs.Empty);
                }
            }
        }

        [Description("ControlBackColor"), Category("Appearance"), DispId(-501)]
        public override Color BackColor
        {
            get
            {
                if (this.ShouldSerializeBackColor())
                {
                    return base.BackColor;
                }
                if (this.ReadOnly)
                {
                    return SystemColors.Control;
                }
                return SystemColors.Window;
            }
            set
            {
                base.BackColor = value;
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

        [Category("Appearance"), DefaultValue(2), DispId(-504), Description("TextBoxBorder")]
        public BorderStyle BorderStyle
        {
            get
            {
                return this.borderStyle;
            }
            set
            {
                if (this.borderStyle != value)
                {
                    if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
                    {
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
                    }
                    this.borderStyle = value;
                    base.UpdateStyles();
                    base.RecreateHandle();
                    using (IDisposable disposable = LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle))
                    {
                        this.OnBorderStyleChanged(EventArgs.Empty);
                    }
                }
            }
        }

        protected override bool CanEnableIme
        {
            get
            {
                return ((!this.ReadOnly && !this.PasswordProtect) && base.CanEnableIme);
            }
        }

        internal virtual bool CanRaiseTextChangedEvent
        {
            get
            {
                return true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("TextBoxCanUndo"), Category("Behavior"), Browsable(false)]
        public bool CanUndo
        {
            get
            {
                if (base.IsHandleCreated)
                {
                    return (((int)base.SendMessage(0xc6, 0, 0)) != 0);
                }
                return false;
            }
        }

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassName = "EDIT";
                createParams.Style |= 0xc0;
                if (!this.textBoxFlags[hideSelection])
                {
                    createParams.Style |= 0x100;
                }
                if (this.textBoxFlags[readOnly])
                {
                    createParams.Style |= 0x800;
                }
                createParams.ExStyle &= -513;
                createParams.Style &= -8388609;
                switch (this.borderStyle)
                {
                    case BorderStyle.FixedSingle:
                        createParams.Style |= 0x800000;
                        break;

                    case BorderStyle.Fixed3D:
                        createParams.ExStyle |= 0x200;
                        break;
                }
                if (this.textBoxFlags[multiline])
                {
                    createParams.Style |= 4;
                    if (this.textBoxFlags[wordWrap])
                    {
                        createParams.Style &= -129;
                    }
                }
                return createParams;
            }
        }

        protected override Cursor DefaultCursor
        {
            get
            {
                return Cursors.IBeam;
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, this.PreferredHeight);
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

        [Category("Appearance"), Description("ControlForeColor"), DispId(-513)]
        public override Color ForeColor
        {
            get
            {
                if (this.ShouldSerializeForeColor())
                {
                    return base.ForeColor;
                }
                return SystemColors.WindowText;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [Description("TextBoxHideSelection"), Category("Behavior"), DefaultValue(true)]
        public bool HideSelection
        {
            get
            {
                return this.textBoxFlags[hideSelection];
            }
            set
            {
                if (this.textBoxFlags[hideSelection] != value)
                {
                    this.textBoxFlags[hideSelection] = value;
                    base.RecreateHandle();
                    this.OnHideSelectionChanged(EventArgs.Empty);
                }
            }
        }

        internal ImeMode ImeModeInternal
        {
            get
            {
                if (base.DesignMode)
                {
                    return base.ImeModeInternal;
                }
                return (this.CanEnableIme ? base.ImeModeInternal : ImeMode.Disable);
            }
        }

        [Category("Appearance"), Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), MergableProperty(false), Localizable(true), Description("TextBoxLines")]
        public string[] Lines
        {
            get
            {
                int num2;
                string text = this.Text;
                ArrayList list = new ArrayList();
                for (int i = 0; i < text.Length; i = num2)
                {
                    num2 = i;
                    while (num2 < text.Length)
                    {
                        char ch = text[num2];
                        if ((ch == '\r') || (ch == '\n'))
                        {
                            break;
                        }
                        num2++;
                    }
                    string str2 = text.Substring(i, num2 - i);
                    list.Add(str2);
                    if ((num2 < text.Length) && (text[num2] == '\r'))
                    {
                        num2++;
                    }
                    if ((num2 < text.Length) && (text[num2] == '\n'))
                    {
                        num2++;
                    }
                }
                if ((text.Length > 0) && ((text[text.Length - 1] == '\r') || (text[text.Length - 1] == '\n')))
                {
                    list.Add("");
                }
                return (string[])list.ToArray(typeof(string));
            }
            set
            {
                if ((value != null) && (value.Length > 0))
                {
                    StringBuilder builder = new StringBuilder(value[0]);
                    for (int i = 1; i < value.Length; i++)
                    {
                        builder.Append("\r\n");
                        builder.Append(value[i]);
                    }
                    this.Text = builder.ToString();
                }
                else
                {
                    this.Text = "";
                }
            }
        }

        [Description("TextBoxMaxLength"), Category("Behavior"), Localizable(true), DefaultValue(0x7fff)]
        public virtual int MaxLength
        {
            get
            {
                return this.maxLength;
            }
            set
            {
                if (value < 0)
                {
                    object[] args = new object[] { "MaxLength", value.ToString(CultureInfo.CurrentCulture), 0.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentOutOfRangeException("MaxLength", "InvalidLowBoundArgumentEx", args);
                }
                if (this.maxLength != value)
                {
                    this.maxLength = value;
                    this.UpdateMaxLength();
                }
            }
        }

        [Browsable(false), Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("TextBoxModified")]
        public bool Modified
        {
            get
            {
                if (!base.IsHandleCreated)
                {
                    return this.textBoxFlags[modified];
                }
                bool flag = 0 != ((int)base.SendMessage(0xb8, 0, 0));
                if (this.textBoxFlags[modified] != flag)
                {
                    this.textBoxFlags[modified] = flag;
                    this.OnModifiedChanged(EventArgs.Empty);
                }
                return flag;
            }
            set
            {
                if (this.Modified != value)
                {
                    if (base.IsHandleCreated)
                    {
                        base.SendMessage(0xb9, value ? 1 : 0, 0);
                    }
                    this.textBoxFlags[modified] = value;
                    this.OnModifiedChanged(EventArgs.Empty);
                }
            }
        }

        [DefaultValue(false), Description("TextBoxMultiline"), RefreshProperties(RefreshProperties.All), Localizable(true), Category("Behavior")]
        public virtual bool Multiline
        {
            get
            {
                return this.textBoxFlags[multiline];
            }
            set
            {
                if (this.textBoxFlags[multiline] != value)
                {
                    using (IDisposable disposable = LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Multiline))
                    {
                        this.textBoxFlags[multiline] = value;
                        if (value)
                        {
                            base.SetStyle(ControlStyles.FixedHeight, false);
                        }
                        else
                        {
                            base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
                        }
                        base.RecreateHandle();
                        this.AdjustHeight(false);
                        this.OnMultilineChanged(EventArgs.Empty);
                    }
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                base.Padding = value;
            }
        }

        internal virtual bool PasswordProtect
        {
            get
            {
                return false;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Category("Layout"), Description("TextBoxPreferredHeight"), Browsable(false)]
        public int PreferredHeight
        {
            get
            {
                int fontHeight = base.FontHeight;
                if (this.borderStyle != BorderStyle.None)
                {
                    fontHeight += (SystemInformation.BorderSize.Height * 4) + 3;
                }
                return fontHeight;
            }
        }

        [DefaultValue(false), RefreshProperties(RefreshProperties.Repaint), Category("Behavior"), Description("TextBoxReadOnly")]
        public bool ReadOnly
        {
            get
            {
                return this.textBoxFlags[readOnly];
            }
            set
            {
                if (this.textBoxFlags[readOnly] != value)
                {
                    this.textBoxFlags[readOnly] = value;
                    if (base.IsHandleCreated)
                    {
                        base.SendMessage(0xcf, value ? -1 : 0, 0);
                    }
                    this.OnReadOnlyChanged(EventArgs.Empty);
                    base.VerifyImeRestrictedModeChanged();
                }
            }
        }

        [Category("Appearance"), Description("TextBoxSelectedText"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual string SelectedText
        {
            get
            {
                int num;
                int num2;
                this.GetSelectionStartAndLength(out num, out num2);
                return this.Text.Substring(num, num2);
            }
            set
            {
                this.SetSelectedTextInternal(value, true);
            }
        }

        [Description("TextBoxSelectionLength"), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual int SelectionLength
        {
            get
            {
                int num;
                int num2;
                this.GetSelectionStartAndLength(out num, out num2);
                return num2;
            }
            set
            {
                int num;
                int num2;
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("SelectionLength", "InvalidArgument", new object[] { "SelectionLength", value.ToString(CultureInfo.CurrentCulture) });
                }
                this.GetSelectionStartAndLength(out num, out num2);
                if (value != num2)
                {
                    this.Select(num, value);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance"), Browsable(false), Description("TextBoxSelectionStart")]
        public int SelectionStart
        {
            get
            {
                int num;
                int num2;
                this.GetSelectionStartAndLength(out num, out num2);
                return num;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("SelectionStart", "InvalidArgument", new object[] { "SelectionStart", value.ToString(CultureInfo.CurrentCulture) });
                }
                this.Select(value, this.SelectionLength);
            }
        }

        internal virtual bool SelectionUsesDbcsOffsetsInWin9x
        {
            get
            {
                return true;
            }
        }

        internal virtual bool SetSelectionInCreateHandle
        {
            get
            {
                return true;
            }
        }

        [DefaultValue(true), Category("Behavior"), Description("TextBoxShortcutsEnabled")]
        public virtual bool ShortcutsEnabled
        {
            get
            {
                return this.textBoxFlags[shortcutsEnabled];
            }
            set
            {
                if (shortcutsToDisable == null)
                {
                    shortcutsToDisable = new int[] { 0x2005a, 0x20043, 0x20058, 0x20056, 0x20041, 0x2004c, 0x20052, 0x20045, 0x20059, 0x20008, 0x2002e, 0x1002e, 0x1002d, 0x2004a };
                }
                this.textBoxFlags[shortcutsEnabled] = value;
            }
        }

        [Localizable(true), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (value != base.Text)
                {
                    base.Text = value;
                    if (base.IsHandleCreated)
                    {
                        base.SendMessage(0xb9, 0, 0);
                    }
                }
            }
        }

        [Browsable(false)]
        public virtual int TextLength
        {
            get
            {
                if (base.IsHandleCreated && (Marshal.SystemDefaultCharSize == 2))
                {
                    return SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
                }
                return this.Text.Length;
            }
        }

        internal string WindowText
        {
            get
            {
                return base.WindowText;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (!this.WindowText.Equals(value))
                {
                    this.textBoxFlags[codeUpdateText] = true;
                    try
                    {
                        base.WindowText = value;
                    }
                    finally
                    {
                        this.textBoxFlags[codeUpdateText] = false;
                    }
                }
            }
        }

        [Description("TextBoxWordWrap"), Category("Behavior"), Localizable(true), DefaultValue(true)]
        public bool WordWrap
        {
            get
            {
                return this.textBoxFlags[wordWrap];
            }
            set
            {
                using (IDisposable disposable = LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.WordWrap))
                {
                    if (this.textBoxFlags[wordWrap] != value)
                    {
                        this.textBoxFlags[wordWrap] = value;
                        base.RecreateHandle();
                    }
                }
            }
        }
    }
 

}
