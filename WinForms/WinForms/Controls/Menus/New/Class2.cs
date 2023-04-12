using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms.Layout;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Specialized;

namespace mControl.WinCtl.Controls.Menus
{

    internal delegate void ToolStripLocationCancelEventHandler(object sender, ToolStripLocationCancelEventArgs e);

 

 

    internal interface ISupportOleDropSource
    {
        // Methods
        void OnGiveFeedback(GiveFeedbackEventArgs gfbevent);
        void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent);
    }


    internal interface IArrangedElement : IComponent, IDisposable
    {
        // Methods
        Size GetPreferredSize(Size proposedSize);
        void PerformLayout(IArrangedElement affectedElement, string propertyName);
        void SetBounds(Rectangle bounds, BoundsSpecified specified);

        // Properties
        Rectangle Bounds { get; }
        ArrangedElementCollection Children { get; }
        IArrangedElement Container { get; }
        Rectangle DisplayRectangle { get; }
        bool ParticipatesInLayout { get; }
        PropertyStore Properties { get; }
    }

    internal interface ISupportToolStripPanel
    {
        // Methods
        void BeginDrag();
        void EndDrag();

        // Properties
        bool IsCurrentlyDragging { get; }
        bool Stretch { get; set; }
        ToolStripPanelCell ToolStripPanelCell { get; }
        ToolStripPanelRow ToolStripPanelRow { get; set; }
    }


    internal class ToolStripPanelCell : ArrangedElement
    {
        // Fields
        private ToolStrip _wrappedToolStrip;
        private Rectangle cachedBounds;
        private bool currentlyDragging;
        private bool currentlySizing;
        private Size maxSize;
        private ToolStripPanelRow parent;
        private bool restoreOnVisibleChanged;

        // Methods
        public ToolStripPanelCell(Control control)
            : this(null, control)
        {
        }

        public ToolStripPanelCell(ToolStripPanelRow parent, Control control)
        {
            this.maxSize = LayoutUtils.MaxSize;
            this.cachedBounds = Rectangle.Empty;
            this.ToolStripPanelRow = parent;
            this._wrappedToolStrip = control as ToolStrip;
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (this._wrappedToolStrip == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfType", new object[] { typeof(ToolStrip).Name }), new object[0]), control.GetType().Name);
            }
            CommonProperties.SetAutoSize(this, true);
            this._wrappedToolStrip.LocationChanging += new ToolStripLocationCancelEventHandler(this.OnToolStripLocationChanging);
            this._wrappedToolStrip.VisibleChanged += new EventHandler(this.OnToolStripVisibleChanged);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this._wrappedToolStrip != null)
                    {
                        this._wrappedToolStrip.LocationChanging -= new ToolStripLocationCancelEventHandler(this.OnToolStripLocationChanging);
                        this._wrappedToolStrip.VisibleChanged -= new EventHandler(this.OnToolStripVisibleChanged);
                    }
                    this._wrappedToolStrip = null;
                    if (this.parent != null)
                    {
                        ((IList)this.parent.Cells).Remove(this);
                    }
                    this.parent = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected override ArrangedElementCollection GetChildren()
        {
            return ArrangedElementCollection.Empty;
        }

        protected override IArrangedElement GetContainer()
        {
            return this.parent;
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            ISupportToolStripPanel draggedControl = this.DraggedControl;
            Size empty = Size.Empty;
            if (draggedControl.Stretch)
            {
                if (this.ToolStripPanelRow.Orientation == Orientation.Horizontal)
                {
                    constrainingSize.Width = this.ToolStripPanelRow.Bounds.Width;
                    empty = this._wrappedToolStrip.GetPreferredSize(constrainingSize);
                    empty.Width = constrainingSize.Width;
                    return empty;
                }
                constrainingSize.Height = this.ToolStripPanelRow.Bounds.Height;
                empty = this._wrappedToolStrip.GetPreferredSize(constrainingSize);
                empty.Height = constrainingSize.Height;
                return empty;
            }
            return (!this._wrappedToolStrip.AutoSize ? this._wrappedToolStrip.Size : this._wrappedToolStrip.GetPreferredSize(constrainingSize));
        }

        public int Grow(int growBy)
        {
            if (this.ToolStripPanelRow.Orientation == Orientation.Vertical)
            {
                return this.GrowVertical(growBy);
            }
            return this.GrowHorizontal(growBy);
        }

        private int GrowHorizontal(int growBy)
        {
            if (this.MaximumSize.Width < this.Control.PreferredSize.Width)
            {
                if ((this.MaximumSize.Width + growBy) >= this.Control.PreferredSize.Width)
                {
                    int num = this.Control.PreferredSize.Width - this.MaximumSize.Width;
                    this.maxSize = LayoutUtils.MaxSize;
                    return num;
                }
                if ((this.MaximumSize.Width + growBy) < this.Control.PreferredSize.Width)
                {
                    this.maxSize.Width += growBy;
                    return growBy;
                }
            }
            return 0;
        }

        private int GrowVertical(int growBy)
        {
            if (this.MaximumSize.Height < this.Control.PreferredSize.Height)
            {
                if ((this.MaximumSize.Height + growBy) >= this.Control.PreferredSize.Height)
                {
                    int num = this.Control.PreferredSize.Height - this.MaximumSize.Height;
                    this.maxSize = LayoutUtils.MaxSize;
                    return num;
                }
                if ((this.MaximumSize.Height + growBy) < this.Control.PreferredSize.Height)
                {
                    this.maxSize.Height += growBy;
                    return growBy;
                }
            }
            return 0;
        }

        private void OnToolStripLocationChanging(object sender, ToolStripLocationCancelEventArgs e)
        {
            if ((this.ToolStripPanelRow != null) && (!this.currentlySizing && !this.currentlyDragging))
            {
                try
                {
                    this.currentlyDragging = true;
                    Point location = e.NewLocation;
                    if ((this.ToolStripPanelRow != null) && (this.ToolStripPanelRow.Bounds == Rectangle.Empty))
                    {
                        this.ToolStripPanelRow.ToolStripPanel.PerformUpdate(true);
                    }
                    if (this._wrappedToolStrip != null)
                    {
                        this.ToolStripPanelRow.ToolStripPanel.Join(this._wrappedToolStrip, location);
                    }
                }
                finally
                {
                    this.currentlyDragging = false;
                    e.Cancel = true;
                }
            }
        }

        private void OnToolStripVisibleChanged(object sender, EventArgs e)
        {
            if ((((this._wrappedToolStrip != null) && !this._wrappedToolStrip.IsInDesignMode) && (!this._wrappedToolStrip.IsCurrentlyDragging && !this._wrappedToolStrip.IsDisposed)) && !this._wrappedToolStrip.Disposing)
            {
                if (!this.Control.Visible)
                {
                    this.restoreOnVisibleChanged = (this.ToolStripPanelRow != null) && ((IList)this.ToolStripPanelRow.Cells).Contains(this);
                }
                else if (this.restoreOnVisibleChanged)
                {
                    try
                    {
                        if ((this.ToolStripPanelRow != null) && ((IList)this.ToolStripPanelRow.Cells).Contains(this))
                        {
                            this.ToolStripPanelRow.ToolStripPanel.Join(this._wrappedToolStrip, this._wrappedToolStrip.Location);
                        }
                    }
                    finally
                    {
                        this.restoreOnVisibleChanged = false;
                    }
                }
            }
        }

        protected override void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
        {
            this.currentlySizing = true;
            this.CachedBounds = bounds;
            try
            {
                if (this.DraggedControl.IsCurrentlyDragging)
                {
                    if (this.ToolStripPanelRow.Cells[this.ToolStripPanelRow.Cells.Count - 1] == this)
                    {
                        Rectangle displayRectangle = this.ToolStripPanelRow.DisplayRectangle;
                        if (this.ToolStripPanelRow.Orientation == Orientation.Horizontal)
                        {
                            int num = bounds.Right - displayRectangle.Right;
                            if ((num > 0) && (bounds.Width > num))
                            {
                                bounds.Width -= num;
                            }
                        }
                        else
                        {
                            int num2 = bounds.Bottom - displayRectangle.Bottom;
                            if ((num2 > 0) && (bounds.Height > num2))
                            {
                                bounds.Height -= num2;
                            }
                        }
                    }
                    base.SetBoundsCore(bounds, specified);
                    this.InnerElement.SetBounds(bounds, specified);
                }
                else if (!this.ToolStripPanelRow.CachedBoundsMode)
                {
                    base.SetBoundsCore(bounds, specified);
                    this.InnerElement.SetBounds(bounds, specified);
                }
            }
            finally
            {
                this.currentlySizing = false;
            }
        }

        public int Shrink(int shrinkBy)
        {
            if (this.ToolStripPanelRow.Orientation == Orientation.Vertical)
            {
                return this.ShrinkVertical(shrinkBy);
            }
            return this.ShrinkHorizontal(shrinkBy);
        }

        private int ShrinkHorizontal(int shrinkBy)
        {
            return 0;
        }

        private int ShrinkVertical(int shrinkBy)
        {
            return 0;
        }

        // Properties
        public Rectangle CachedBounds
        {
            get
            {
                return this.cachedBounds;
            }
            set
            {
                this.cachedBounds = value;
            }
        }

        public Control Control
        {
            get
            {
                return this._wrappedToolStrip;
            }
        }

        public bool ControlInDesignMode
        {
            get
            {
                if (this._wrappedToolStrip != null)
                {
                    return this._wrappedToolStrip.IsInDesignMode;
                }
                return false;
            }
        }

        public ISupportToolStripPanel DraggedControl
        {
            get
            {
                return this._wrappedToolStrip;
            }
        }

        public IArrangedElement InnerElement
        {
            get
            {
                return this._wrappedToolStrip;
            }
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                return DefaultLayout.Instance;
            }
        }

        public Size MaximumSize
        {
            get
            {
                return this.maxSize;
            }
        }

        public ToolStripPanelRow ToolStripPanelRow
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (this.parent != value)
                {
                    if (this.parent != null)
                    {
                        ((IList)this.parent.Cells).Remove(this);
                    }
                    this.parent = value;
                    base.Margin = Padding.Empty;
                }
            }
        }

        public override bool Visible
        {
            get
            {
                if ((this.Control != null) && (this.Control.ParentInternal == this.ToolStripPanelRow.ToolStripPanel))
                {
                    return this.InnerElement.ParticipatesInLayout;
                }
                return false;
            }
            set
            {
                this.Control.Visible = value;
            }
        }
    }





    internal class PropertyStore
    {
        // Fields
        private static int currentKey;
        private IntegerEntry[] intEntries;
        private ObjectEntry[] objEntries;

        // Methods
        public bool ContainsInteger(int key)
        {
            bool found;
            this.GetInteger(key, out found);
            return found;
        }

        public bool ContainsObject(int key)
        {
            bool found;
            this.GetObject(key, out found);
            return found;
        }

        public static int CreateKey()
        {
            return currentKey++;
        }

        [Conditional("DEBUG_PROPERTYSTORE")]
        private void Debug_VerifyLocateIntegerEntry(int index, short entryKey, int length)
        {
            int num = length - 1;
            int num2 = 0;
            int num3 = 0;
            do
            {
                num3 = (num + num2) / 2;
                short key = this.intEntries[num3].Key;
                if (key != entryKey)
                {
                    if (entryKey < key)
                    {
                        num = num3 - 1;
                    }
                    else
                    {
                        num2 = num3 + 1;
                    }
                }
            }
            while (num >= num2);
            if (entryKey > this.intEntries[num3].Key)
            {
                num3++;
            }
        }

        [Conditional("DEBUG_PROPERTYSTORE")]
        private void Debug_VerifyLocateObjectEntry(int index, short entryKey, int length)
        {
            int num = length - 1;
            int num2 = 0;
            int num3 = 0;
            do
            {
                num3 = (num + num2) / 2;
                short key = this.objEntries[num3].Key;
                if (key != entryKey)
                {
                    if (entryKey < key)
                    {
                        num = num3 - 1;
                    }
                    else
                    {
                        num2 = num3 + 1;
                    }
                }
            }
            while (num >= num2);
            if (entryKey > this.objEntries[num3].Key)
            {
                num3++;
            }
        }

        public Color GetColor(int key)
        {
            bool found;
            return this.GetColor(key, out found);
        }

        public Color GetColor(int key, out bool found)
        {
            object obj2 = this.GetObject(key, out found);
            if (found)
            {
                ColorWrapper wrapper = obj2 as ColorWrapper;
                if (wrapper != null)
                {
                    return wrapper.Color;
                }
            }
            found = false;
            return Color.Empty;
        }

        public int GetInteger(int key)
        {
            bool found;
            return this.GetInteger(key, out found);
        }

        public int GetInteger(int key, out bool found)
        {
            int index;
            short element;
            short entryKey = this.SplitKey(key, out element);
            found = false;
            if (this.LocateIntegerEntry(entryKey, out index) && (((((int)1) << element) & this.intEntries[index].Mask) != 0))
            {
                found = true;
                switch (element)
                {
                    case 0:
                        return this.intEntries[index].Value1;

                    case 1:
                        return this.intEntries[index].Value2;

                    case 2:
                        return this.intEntries[index].Value3;

                    case 3:
                        return this.intEntries[index].Value4;
                }
            }
            return 0;
        }

        public object GetObject(int key)
        {
            bool found;
            return this.GetObject(key, out found);
        }

        public object GetObject(int key, out bool found)
        {
            int index;
            short element;
            short entryKey = this.SplitKey(key, out element);
            found = false;
            if (this.LocateObjectEntry(entryKey, out index) && (((((int)1) << element) & this.objEntries[index].Mask) != 0))
            {
                found = true;
                switch (element)
                {
                    case 0:
                        return this.objEntries[index].Value1;

                    case 1:
                        return this.objEntries[index].Value2;

                    case 2:
                        return this.objEntries[index].Value3;

                    case 3:
                        return this.objEntries[index].Value4;
                }
            }
            return null;
        }

        public Padding GetPadding(int key)
        {
            bool found;
            return this.GetPadding(key, out found);
        }

        public Padding GetPadding(int key, out bool found)
        {
            object obj2 = this.GetObject(key, out found);
            if (found)
            {
                PaddingWrapper wrapper = obj2 as PaddingWrapper;
                if (wrapper != null)
                {
                    return wrapper.Padding;
                }
            }
            found = false;
            return Padding.Empty;
        }

        public Rectangle GetRectangle(int key)
        {
            bool found;
            return this.GetRectangle(key, out found);
        }

        public Rectangle GetRectangle(int key, out bool found)
        {
            object obj2 = this.GetObject(key, out found);
            if (found)
            {
                RectangleWrapper wrapper = obj2 as RectangleWrapper;
                if (wrapper != null)
                {
                    return wrapper.Rectangle;
                }
            }
            found = false;
            return Rectangle.Empty;
        }

        public Size GetSize(int key, out bool found)
        {
            object obj2 = this.GetObject(key, out found);
            if (found)
            {
                SizeWrapper wrapper = obj2 as SizeWrapper;
                if (wrapper != null)
                {
                    return wrapper.Size;
                }
            }
            found = false;
            return Size.Empty;
        }

        private bool LocateIntegerEntry(short entryKey, out int index)
        {
            if (this.intEntries != null)
            {
                int length = this.intEntries.Length;
                if (length <= 0x10)
                {
                    index = 0;
                    int num2 = length / 2;
                    if (this.intEntries[num2].Key <= entryKey)
                    {
                        index = num2;
                    }
                    if (this.intEntries[index].Key == entryKey)
                    {
                        return true;
                    }
                    num2 = (length + 1) / 4;
                    if (this.intEntries[index + num2].Key <= entryKey)
                    {
                        index += num2;
                        if (this.intEntries[index].Key == entryKey)
                        {
                            return true;
                        }
                    }
                    num2 = (length + 3) / 8;
                    if (this.intEntries[index + num2].Key <= entryKey)
                    {
                        index += num2;
                        if (this.intEntries[index].Key == entryKey)
                        {
                            return true;
                        }
                    }
                    num2 = (length + 7) / 0x10;
                    if (this.intEntries[index + num2].Key <= entryKey)
                    {
                        index += num2;
                        if (this.intEntries[index].Key == entryKey)
                        {
                            return true;
                        }
                    }
                    if (entryKey > this.intEntries[index].Key)
                    {
                        index++;
                    }
                    return false;
                }
                int num3 = length - 1;
                int num4 = 0;
                int num5 = 0;
                do
                {
                    num5 = (num3 + num4) / 2;
                    short key = this.intEntries[num5].Key;
                    if (key == entryKey)
                    {
                        index = num5;
                        return true;
                    }
                    if (entryKey < key)
                    {
                        num3 = num5 - 1;
                    }
                    else
                    {
                        num4 = num5 + 1;
                    }
                }
                while (num3 >= num4);
                index = num5;
                if (entryKey > this.intEntries[num5].Key)
                {
                    index++;
                }
                return false;
            }
            index = 0;
            return false;
        }

        private bool LocateObjectEntry(short entryKey, out int index)
        {
            if (this.objEntries != null)
            {
                int length = this.objEntries.Length;
                if (length <= 0x10)
                {
                    index = 0;
                    int num2 = length / 2;
                    if (this.objEntries[num2].Key <= entryKey)
                    {
                        index = num2;
                    }
                    if (this.objEntries[index].Key == entryKey)
                    {
                        return true;
                    }
                    num2 = (length + 1) / 4;
                    if (this.objEntries[index + num2].Key <= entryKey)
                    {
                        index += num2;
                        if (this.objEntries[index].Key == entryKey)
                        {
                            return true;
                        }
                    }
                    num2 = (length + 3) / 8;
                    if (this.objEntries[index + num2].Key <= entryKey)
                    {
                        index += num2;
                        if (this.objEntries[index].Key == entryKey)
                        {
                            return true;
                        }
                    }
                    num2 = (length + 7) / 0x10;
                    if (this.objEntries[index + num2].Key <= entryKey)
                    {
                        index += num2;
                        if (this.objEntries[index].Key == entryKey)
                        {
                            return true;
                        }
                    }
                    if (entryKey > this.objEntries[index].Key)
                    {
                        index++;
                    }
                    return false;
                }
                int num3 = length - 1;
                int num4 = 0;
                int num5 = 0;
                do
                {
                    num5 = (num3 + num4) / 2;
                    short key = this.objEntries[num5].Key;
                    if (key == entryKey)
                    {
                        index = num5;
                        return true;
                    }
                    if (entryKey < key)
                    {
                        num3 = num5 - 1;
                    }
                    else
                    {
                        num4 = num5 + 1;
                    }
                }
                while (num3 >= num4);
                index = num5;
                if (entryKey > this.objEntries[num5].Key)
                {
                    index++;
                }
                return false;
            }
            index = 0;
            return false;
        }

        public void RemoveInteger(int key)
        {
            int index;
            short element;
            short entryKey = this.SplitKey(key, out element);
            if (this.LocateIntegerEntry(entryKey, out index) && (((((int)1) << element) & this.intEntries[index].Mask) != 0))
            {
                this.intEntries[index].Mask = (short)(this.intEntries[index].Mask & ~((short)(((int)1) << element)));
                if (this.intEntries[index].Mask == 0)
                {
                    IntegerEntry[] destinationArray = new IntegerEntry[this.intEntries.Length - 1];
                    if (index > 0)
                    {
                        Array.Copy(this.intEntries, 0, destinationArray, 0, index);
                    }
                    if (index < destinationArray.Length)
                    {
                        Array.Copy(this.intEntries, index + 1, destinationArray, index, (this.intEntries.Length - index) - 1);
                    }
                    this.intEntries = destinationArray;
                }
                else
                {
                    switch (element)
                    {
                        case 0:
                            this.intEntries[index].Value1 = 0;
                            return;

                        case 1:
                            this.intEntries[index].Value2 = 0;
                            return;

                        case 2:
                            this.intEntries[index].Value3 = 0;
                            return;

                        case 3:
                            this.intEntries[index].Value4 = 0;
                            return;
                    }
                }
            }
        }

        public void RemoveObject(int key)
        {
            int index;
            short element;
            short entryKey = this.SplitKey(key, out element);
            if (this.LocateObjectEntry(entryKey, out index) && (((((int)1) << element) & this.objEntries[index].Mask) != 0))
            {
                this.objEntries[index].Mask = (short)(this.objEntries[index].Mask & ~((short)(((int)1) << element)));
                if (this.objEntries[index].Mask == 0)
                {
                    if (this.objEntries.Length == 1)
                    {
                        this.objEntries = null;
                    }
                    else
                    {
                        ObjectEntry[] destinationArray = new ObjectEntry[this.objEntries.Length - 1];
                        if (index > 0)
                        {
                            Array.Copy(this.objEntries, 0, destinationArray, 0, index);
                        }
                        if (index < destinationArray.Length)
                        {
                            Array.Copy(this.objEntries, index + 1, destinationArray, index, (this.objEntries.Length - index) - 1);
                        }
                        this.objEntries = destinationArray;
                    }
                }
                else
                {
                    switch (element)
                    {
                        case 0:
                            this.objEntries[index].Value1 = null;
                            return;

                        case 1:
                            this.objEntries[index].Value2 = null;
                            return;

                        case 2:
                            this.objEntries[index].Value3 = null;
                            return;

                        case 3:
                            this.objEntries[index].Value4 = null;
                            return;
                    }
                }
            }
        }

        public void SetColor(int key, Color value)
        {
            bool found;
            object obj2 = this.GetObject(key, out found);
            if (!found)
            {
                this.SetObject(key, new ColorWrapper(value));
            }
            else
            {
                ColorWrapper wrapper = obj2 as ColorWrapper;
                if (wrapper != null)
                {
                    wrapper.Color = value;
                }
                else
                {
                    this.SetObject(key, new ColorWrapper(value));
                }
            }
        }

        public void SetInteger(int key, int value)
        {
            int index;
            short element;
            short entryKey = this.SplitKey(key, out element);
            if (!this.LocateIntegerEntry(entryKey, out index))
            {
                if (this.intEntries != null)
                {
                    IntegerEntry[] destinationArray = new IntegerEntry[this.intEntries.Length + 1];
                    if (index > 0)
                    {
                        Array.Copy(this.intEntries, 0, destinationArray, 0, index);
                    }
                    if ((this.intEntries.Length - index) > 0)
                    {
                        Array.Copy(this.intEntries, index, destinationArray, index + 1, this.intEntries.Length - index);
                    }
                    this.intEntries = destinationArray;
                }
                else
                {
                    this.intEntries = new IntegerEntry[1];
                }
                this.intEntries[index].Key = entryKey;
            }
            switch (element)
            {
                case 0:
                    this.intEntries[index].Value1 = value;
                    break;

                case 1:
                    this.intEntries[index].Value2 = value;
                    break;

                case 2:
                    this.intEntries[index].Value3 = value;
                    break;

                case 3:
                    this.intEntries[index].Value4 = value;
                    break;
            }
            this.intEntries[index].Mask = (short)((((int)1) << element) | ((ushort)this.intEntries[index].Mask));
        }

        public void SetObject(int key, object value)
        {
            int index;
            short element;
            short entryKey = this.SplitKey(key, out element);
            if (!this.LocateObjectEntry(entryKey, out index))
            {
                if (this.objEntries != null)
                {
                    ObjectEntry[] destinationArray = new ObjectEntry[this.objEntries.Length + 1];
                    if (index > 0)
                    {
                        Array.Copy(this.objEntries, 0, destinationArray, 0, index);
                    }
                    if ((this.objEntries.Length - index) > 0)
                    {
                        Array.Copy(this.objEntries, index, destinationArray, index + 1, this.objEntries.Length - index);
                    }
                    this.objEntries = destinationArray;
                }
                else
                {
                    this.objEntries = new ObjectEntry[1];
                }
                this.objEntries[index].Key = entryKey;
            }
            switch (element)
            {
                case 0:
                    this.objEntries[index].Value1 = value;
                    break;

                case 1:
                    this.objEntries[index].Value2 = value;
                    break;

                case 2:
                    this.objEntries[index].Value3 = value;
                    break;

                case 3:
                    this.objEntries[index].Value4 = value;
                    break;
            }
            this.objEntries[index].Mask = (short)(((ushort)this.objEntries[index].Mask) | (((int)1) << element));
        }

        public void SetPadding(int key, Padding value)
        {
            bool found;
            object obj2 = this.GetObject(key, out found);
            if (!found)
            {
                this.SetObject(key, new PaddingWrapper(value));
            }
            else
            {
                PaddingWrapper wrapper = obj2 as PaddingWrapper;
                if (wrapper != null)
                {
                    wrapper.Padding = value;
                }
                else
                {
                    this.SetObject(key, new PaddingWrapper(value));
                }
            }
        }

        public void SetRectangle(int key, Rectangle value)
        {
            bool found;
            object obj2 = this.GetObject(key, out found);
            if (!found)
            {
                this.SetObject(key, new RectangleWrapper(value));
            }
            else
            {
                RectangleWrapper wrapper = obj2 as RectangleWrapper;
                if (wrapper != null)
                {
                    wrapper.Rectangle = value;
                }
                else
                {
                    this.SetObject(key, new RectangleWrapper(value));
                }
            }
        }

        public void SetSize(int key, Size value)
        {
            bool found;
            object obj2 = this.GetObject(key, out found);
            if (!found)
            {
                this.SetObject(key, new SizeWrapper(value));
            }
            else
            {
                SizeWrapper wrapper = obj2 as SizeWrapper;
                if (wrapper != null)
                {
                    wrapper.Size = value;
                }
                else
                {
                    this.SetObject(key, new SizeWrapper(value));
                }
            }
        }

        private short SplitKey(int key, out short element)
        {
            element = (short)(key & 3);
            return (short)(key & 0xfffffffc);
        }

        // Nested Types
        private sealed class ColorWrapper
        {
            // Fields
            public Color Color;

            // Methods
            public ColorWrapper(Color color)
            {
                this.Color = color;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IntegerEntry
        {
            public short Key;
            public short Mask;
            public int Value1;
            public int Value2;
            public int Value3;
            public int Value4;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ObjectEntry
        {
            public short Key;
            public short Mask;
            public object Value1;
            public object Value2;
            public object Value3;
            public object Value4;
        }

        private sealed class PaddingWrapper
        {
            // Fields
            public Padding Padding;

            // Methods
            public PaddingWrapper(Padding padding)
            {
                this.Padding = padding;
            }
        }

        private sealed class RectangleWrapper
        {
            // Fields
            public Rectangle Rectangle;

            // Methods
            public RectangleWrapper(Rectangle rectangle)
            {
                this.Rectangle = rectangle;
            }
        }

        private sealed class SizeWrapper
        {
            // Fields
            public Size Size;

            // Methods
            public SizeWrapper(Size size)
            {
                this.Size = size;
            }
        }
    }

    internal class CachedItemHdcInfo : IDisposable
    {
        // Fields
        private Size cachedHDCSize = Size.Empty;
        private HandleRef cachedItemBitmap = NativeMethods.NullHandleRef;
        private HandleRef cachedItemHDC = NativeMethods.NullHandleRef;

        // Methods
        internal CachedItemHdcInfo()
        {
        }

        private void DeleteCachedItemHDC()
        {
            if (this.cachedItemHDC.Handle != IntPtr.Zero)
            {
                if (this.cachedItemBitmap.Handle != IntPtr.Zero)
                {
                    SafeNativeMethods.DeleteObject(this.cachedItemBitmap);
                    this.cachedItemBitmap = NativeMethods.NullHandleRef;
                }
                UnsafeNativeMethods.DeleteCompatibleDC(this.cachedItemHDC);
            }
            this.cachedItemHDC = NativeMethods.NullHandleRef;
            this.cachedItemBitmap = NativeMethods.NullHandleRef;
            this.cachedHDCSize = Size.Empty;
        }

        public void Dispose()
        {
            this.DeleteCachedItemHDC();
            GC.SuppressFinalize(this);
        }

        ~CachedItemHdcInfo()
        {
            this.Dispose();
        }

        public HandleRef GetCachedItemDC(HandleRef toolStripHDC, Size bitmapSize)
        {
            if ((this.cachedHDCSize.Width < bitmapSize.Width) || (this.cachedHDCSize.Height < bitmapSize.Height))
            {
                if (this.cachedItemHDC.Handle == IntPtr.Zero)
                {
                    IntPtr handle = UnsafeNativeMethods.CreateCompatibleDC(toolStripHDC);
                    this.cachedItemHDC = new HandleRef(this, handle);
                }
                this.cachedItemBitmap = new HandleRef(this, SafeNativeMethods.CreateCompatibleBitmap(toolStripHDC, bitmapSize.Width, bitmapSize.Height));
                IntPtr handle = SafeNativeMethods.SelectObject(this.cachedItemHDC, this.cachedItemBitmap);
                if (handle != IntPtr.Zero)
                {
                    SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, handle));
                    handle = IntPtr.Zero;
                }
                this.cachedHDCSize = bitmapSize;
            }
            return this.cachedItemHDC;
        }
    }

    internal class ToolStripDropTargetManager : IDropTarget
    {
        // Fields
        internal static readonly TraceSwitch DragDropDebug;
        private IDropTarget lastDropTarget;
        private ToolStrip owner;

        // Methods
        public ToolStripDropTargetManager(ToolStrip owner)
        {
            this.owner = owner;
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
        }

        public void EnsureRegistered(IDropTarget dropTarget)
        {
            this.SetAcceptDrops(true);
        }

        public void EnsureUnRegistered(IDropTarget dropTarget)
        {
            for (int i = 0; i < this.owner.Items.Count; i++)
            {
                if (this.owner.Items[i].AllowDrop)
                {
                    return;
                }
            }
            if (!this.owner.AllowDrop && !this.owner.AllowItemReorder)
            {
                this.SetAcceptDrops(false);
                this.owner.DropTargetManager = null;
            }
        }

        private ToolStripItem FindItemAtPoint(int x, int y)
        {
            return this.owner.GetItemAt(this.owner.PointToClient(new Point(x, y)));
        }

        public void OnDragDrop(DragEventArgs e)
        {
            if (this.lastDropTarget != null)
            {
                this.lastDropTarget.OnDragDrop(e);
            }
            this.lastDropTarget = null;
        }

        public void OnDragEnter(DragEventArgs e)
        {
            if (this.owner.AllowItemReorder && e.Data.GetDataPresent(typeof(ToolStripItem)))
            {
                this.lastDropTarget = this.owner.ItemReorderDropTarget;
            }
            else
            {
                ToolStripItem item = this.FindItemAtPoint(e.X, e.Y);
                if ((item != null) && item.AllowDrop)
                {
                    this.lastDropTarget = item;
                }
                else if (this.owner.AllowDrop)
                {
                    this.lastDropTarget = this.owner;
                }
                else
                {
                    this.lastDropTarget = null;
                }
            }
            if (this.lastDropTarget != null)
            {
                this.lastDropTarget.OnDragEnter(e);
            }
        }

        public void OnDragLeave(EventArgs e)
        {
            if (this.lastDropTarget != null)
            {
                this.lastDropTarget.OnDragLeave(e);
            }
            this.lastDropTarget = null;
        }

        public void OnDragOver(DragEventArgs e)
        {
            IDropTarget newTarget = null;
            if (this.owner.AllowItemReorder && e.Data.GetDataPresent(typeof(ToolStripItem)))
            {
                newTarget = this.owner.ItemReorderDropTarget;
            }
            else
            {
                ToolStripItem item = this.FindItemAtPoint(e.X, e.Y);
                if ((item != null) && item.AllowDrop)
                {
                    newTarget = item;
                }
                else if (this.owner.AllowDrop)
                {
                    newTarget = this.owner;
                }
                else
                {
                    newTarget = null;
                }
            }
            if (newTarget != this.lastDropTarget)
            {
                this.UpdateDropTarget(newTarget, e);
            }
            if (this.lastDropTarget != null)
            {
                this.lastDropTarget.OnDragOver(e);
            }
        }

        private void SetAcceptDrops(bool accept)
        {
            if (this.owner.AllowDrop && accept)
            {
                IntSecurity.ClipboardRead.Demand();
            }
            if (accept && this.owner.IsHandleCreated)
            {
                try
                {
                    if (Application.OleRequired() != ApartmentState.STA)
                    {
                        throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
                    }
                    if (accept)
                    {
                        int error = UnsafeNativeMethods.RegisterDragDrop(new HandleRef(this.owner, this.owner.Handle), new DropTarget(this));
                        if ((error != 0) && (error != -2147221247))
                        {
                            throw new Win32Exception(error);
                        }
                    }
                    else
                    {
                        IntSecurity.ClipboardRead.Assert();
                        try
                        {
                            int error = UnsafeNativeMethods.RevokeDragDrop(new HandleRef(this.owner, this.owner.Handle));
                            if ((error != 0) && (error != -2147221248))
                            {
                                throw new Win32Exception(error);
                            }
                        }
                        finally
                        {
                            CodeAccessPermission.RevertAssert();
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), exception);
                }
            }
        }

        private void UpdateDropTarget(IDropTarget newTarget, DragEventArgs e)
        {
            if (newTarget != this.lastDropTarget)
            {
                if (this.lastDropTarget != null)
                {
                    this.OnDragLeave(new EventArgs());
                }
                this.lastDropTarget = newTarget;
                if (newTarget != null)
                {
                    DragEventArgs args = new DragEventArgs(e.Data, e.KeyState, e.X, e.Y, e.AllowedEffect, e.Effect);
                    args.Effect = DragDropEffects.None;
                    this.OnDragEnter(args);
                }
            }
        }
    }

    internal abstract class ArrangedElement : Component, IArrangedElement, IComponent, IDisposable
    {
        // Fields
        private Rectangle bounds = Rectangle.Empty;
        private IArrangedElement parent;
        private static readonly int PropControlsCollection = PropertyStore.CreateKey();
        private PropertyStore propertyStore = new PropertyStore();
        private Control spacer = new Control();
        private BitVector32 state = new BitVector32();
        private static readonly int stateDisposing = BitVector32.CreateMask(stateVisible);
        private static readonly int stateLocked = BitVector32.CreateMask(stateDisposing);
        private static readonly int stateVisible = BitVector32.CreateMask();
        private int suspendCount;

        // Methods
        internal ArrangedElement()
        {
            this.Padding = this.DefaultPadding;
            this.Margin = this.DefaultMargin;
            this.state[stateVisible] = true;
        }

        protected abstract ArrangedElementCollection GetChildren();
        protected abstract IArrangedElement GetContainer();
        public virtual Size GetPreferredSize(Size constrainingSize)
        {
            return (this.LayoutEngine.GetPreferredSize(this, constrainingSize - this.Padding.Size) + this.Padding.Size);
        }

        protected virtual void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            this.PerformLayout(this, PropertyNames.Size);
        }

        protected virtual void OnLayout(LayoutEventArgs e)
        {
            this.LayoutEngine.Layout(this, e);
        }

        public virtual void PerformLayout(IArrangedElement container, string propertyName)
        {
            if (this.suspendCount <= 0)
            {
                this.OnLayout(new LayoutEventArgs(container, propertyName));
            }
        }

        public void SetBounds(Rectangle bounds, BoundsSpecified specified)
        {
            this.SetBoundsCore(bounds, specified);
        }

        protected virtual void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
        {
            if (bounds != this.bounds)
            {
                Rectangle oldBounds = this.bounds;
                this.bounds = bounds;
                this.OnBoundsChanged(oldBounds, bounds);
            }
        }

        // Properties
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
        }

        protected virtual Padding DefaultMargin
        {
            get
            {
                return Padding.Empty;
            }
        }

        protected virtual Padding DefaultPadding
        {
            get
            {
                return Padding.Empty;
            }
        }

        public virtual Rectangle DisplayRectangle
        {
            get
            {
                return this.Bounds;
            }
        }

        public abstract LayoutEngine LayoutEngine { get; }

        public Padding Margin
        {
            get
            {
                return CommonProperties.GetMargin(this);
            }
            set
            {
                value = LayoutUtils.ClampNegativePaddingToZero(value);
                if (this.Margin != value)
                {
                    CommonProperties.SetMargin(this, value);
                }
            }
        }

        public virtual Padding Padding
        {
            get
            {
                return CommonProperties.GetPadding(this, this.DefaultPadding);
            }
            set
            {
                value = LayoutUtils.ClampNegativePaddingToZero(value);
                if (this.Padding != value)
                {
                    CommonProperties.SetPadding(this, value);
                }
            }
        }

        public virtual IArrangedElement Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public virtual bool ParticipatesInLayout
        {
            get
            {
                return this.Visible;
            }
        }

        private PropertyStore Properties
        {
            get
            {
                return this.propertyStore;
            }
        }

        ArrangedElementCollection IArrangedElement.Children
        {
            get
            {
                return this.GetChildren();
            }
        }

        IArrangedElement IArrangedElement.Container
        {
            get
            {
                return this.GetContainer();
            }
        }

        PropertyStore IArrangedElement.Properties
        {
            get
            {
                return this.Properties;
            }
        }

        public virtual bool Visible
        {
            get
            {
                return this.state[stateVisible];
            }
            set
            {
                if (this.state[stateVisible] != value)
                {
                    this.state[stateVisible] = value;
                    if (this.Parent != null)
                    {
                        LayoutTransaction.DoLayout(this.Parent, this, PropertyNames.Visible);
                    }
                }
            }
        }
    }
    internal class ToolStripLocationCancelEventArgs : CancelEventArgs
    {
        // Fields
        private Point newLocation;

        // Methods
        public ToolStripLocationCancelEventArgs(Point newLocation, bool value)
            : base(value)
        {
            this.newLocation = newLocation;
        }

        // Properties
        public Point NewLocation
        {
            get
            {
                return this.newLocation;
            }
        }
    }
    internal class MergeHistory
    {
        // Fields
        private ToolStrip mergedToolStrip;
        private Stack<MergeHistoryItem> mergeHistoryItemsStack;

        // Methods
        public MergeHistory(ToolStrip mergedToolStrip)
        {
            this.mergedToolStrip = mergedToolStrip;
        }

        // Properties
        public ToolStrip MergedToolStrip
        {
            get
            {
                return this.mergedToolStrip;
            }
        }

        public Stack<MergeHistoryItem> MergeHistoryItemsStack
        {
            get
            {
                if (this.mergeHistoryItemsStack == null)
                {
                    this.mergeHistoryItemsStack = new Stack<MergeHistoryItem>();
                }
                return this.mergeHistoryItemsStack;
            }
        }
    }

    internal class MergeHistoryItem
    {
        // Fields
        private int index = -1;
        private ToolStripItemCollection indexCollection;
        private MergeAction mergeAction;
        private int previousIndex = -1;
        private ToolStripItemCollection previousIndexCollection;
        private ToolStripItem targetItem;

        // Methods
        public MergeHistoryItem(MergeAction mergeAction)
        {
            this.mergeAction = mergeAction;
        }

        public override string ToString()
        {
            return ("MergeAction: " + this.mergeAction.ToString() + " | TargetItem: " + ((this.TargetItem == null) ? "null" : this.TargetItem.Text) + " Index: " + this.index.ToString(CultureInfo.CurrentCulture));
        }

        // Properties
        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public ToolStripItemCollection IndexCollection
        {
            get
            {
                return this.indexCollection;
            }
            set
            {
                this.indexCollection = value;
            }
        }

        public MergeAction MergeAction
        {
            get
            {
                return this.mergeAction;
            }
        }

        public int PreviousIndex
        {
            get
            {
                return this.previousIndex;
            }
            set
            {
                this.previousIndex = value;
            }
        }

        public ToolStripItemCollection PreviousIndexCollection
        {
            get
            {
                return this.previousIndexCollection;
            }
            set
            {
                this.previousIndexCollection = value;
            }
        }

        public ToolStripItem TargetItem
        {
            get
            {
                return this.targetItem;
            }
            set
            {
                this.targetItem = value;
            }
        }
    }

    internal class MouseHoverTimer : IDisposable
    {
        // Fields
        private ToolStripItem currentItem;
        private Timer mouseHoverTimer = new Timer();
        private const int SPI_GETMOUSEHOVERTIME_WIN9X = 400;

        // Methods
        public MouseHoverTimer()
        {
            int mouseHoverTime = SystemInformation.MouseHoverTime;
            if (mouseHoverTime == 0)
            {
                mouseHoverTime = 400;
            }
            this.mouseHoverTimer.Interval = mouseHoverTime;
            this.mouseHoverTimer.Tick += new EventHandler(this.OnTick);
        }

        public void Cancel()
        {
            this.mouseHoverTimer.Enabled = false;
            this.currentItem = null;
        }

        public void Cancel(ToolStripItem item)
        {
            if (item == this.currentItem)
            {
                this.Cancel();
            }
        }

        public void Dispose()
        {
            if (this.mouseHoverTimer != null)
            {
                this.Cancel();
                this.mouseHoverTimer.Dispose();
                this.mouseHoverTimer = null;
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.mouseHoverTimer.Enabled = false;
            if ((this.currentItem != null) && !this.currentItem.IsDisposed)
            {
                this.currentItem.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseHover);
            }
        }

        public void Start(ToolStripItem item)
        {
            if (item != this.currentItem)
            {
                this.Cancel(this.currentItem);
            }
            this.currentItem = item;
            if (this.currentItem != null)
            {
                this.mouseHoverTimer.Enabled = true;
            }
        }
    }

    internal class ToolStripGrip : ToolStripButton
    {
        // Fields
        private static Size DragSize = LayoutUtils.MaxSize;
        private int gripThickness = (ToolStripManager.VisualStylesEnabled ? 5 : 3);
        private Point lastEndLocation = ToolStrip.InvalidMouseEnter;
        private bool movingToolStrip;
        private Cursor oldCursor;
        private Point startLocation = Point.Empty;

        // Methods
        internal ToolStripGrip()
        {
            base.SupportsItemClick = false;
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new ToolStripGripAccessibleObject(this);
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            Size empty = Size.Empty;
            if (base.ParentInternal != null)
            {
                if (base.ParentInternal.LayoutStyle == ToolStripLayoutStyle.VerticalStackWithOverflow)
                {
                    empty = new Size(base.ParentInternal.Width, this.gripThickness);
                }
                else
                {
                    empty = new Size(this.gripThickness, base.ParentInternal.Height);
                }
            }
            if (empty.Width > constrainingSize.Width)
            {
                empty.Width = constrainingSize.Width;
            }
            if (empty.Height > constrainingSize.Height)
            {
                empty.Height = constrainingSize.Height;
            }
            return empty;
        }

        private bool LeftMouseButtonIsDown()
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                return (Control.ModifierKeys == Keys.None);
            }
            return false;
        }

        protected override void OnMouseDown(MouseEventArgs mea)
        {
            this.startLocation = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
            base.OnMouseDown(mea);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (((base.ParentInternal != null) && (this.ToolStripPanelRow != null)) && !base.ParentInternal.IsInDesignMode)
            {
                this.oldCursor = base.ParentInternal.Cursor;
                SetCursor(base.ParentInternal, Cursors.SizeAll);
            }
            else
            {
                this.oldCursor = null;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if ((this.oldCursor != null) && !base.ParentInternal.IsInDesignMode)
            {
                SetCursor(base.ParentInternal, this.oldCursor);
            }
            if (!this.MovingToolStrip && this.LeftMouseButtonIsDown())
            {
                this.MovingToolStrip = true;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            bool flag = this.LeftMouseButtonIsDown();
            if (!this.MovingToolStrip && flag)
            {
                Point point = base.TranslatePoint(mea.Location, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
                int num = point.X - this.startLocation.X;
                num = (num < 0) ? (num * -1) : num;
                if (DragSize == LayoutUtils.MaxSize)
                {
                    DragSize = SystemInformation.DragSize;
                }
                if (num >= DragSize.Width)
                {
                    this.MovingToolStrip = true;
                }
                else
                {
                    int num2 = point.Y - this.startLocation.Y;
                    num2 = (num2 < 0) ? (num2 * -1) : num2;
                    if (num2 >= DragSize.Height)
                    {
                        this.MovingToolStrip = true;
                    }
                }
            }
            if (this.MovingToolStrip)
            {
                if (flag)
                {
                    Point screenLocation = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
                    if (screenLocation != this.lastEndLocation)
                    {
                        this.ToolStripPanelRow.ToolStripPanel.MoveControl(base.ParentInternal, screenLocation);
                        this.lastEndLocation = screenLocation;
                    }
                    this.startLocation = screenLocation;
                }
                else
                {
                    this.MovingToolStrip = false;
                }
            }
            base.OnMouseMove(mea);
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            if (this.MovingToolStrip)
            {
                Point screenLocation = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
                this.ToolStripPanelRow.ToolStripPanel.MoveControl(base.ParentInternal, screenLocation);
            }
            if (!base.ParentInternal.IsInDesignMode)
            {
                SetCursor(base.ParentInternal, this.oldCursor);
            }
            ToolStripPanel.ClearDragFeedback();
            this.MovingToolStrip = false;
            base.OnMouseUp(mea);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (base.ParentInternal != null)
            {
                base.ParentInternal.OnPaintGrip(e);
            }
        }

        private static void SetCursor(Control control, Cursor cursor)
        {
            IntSecurity.ModifyCursor.Assert();
            control.Cursor = cursor;
        }

        // Properties
        public override bool CanSelect
        {
            get
            {
                return false;
            }
        }

        protected internal override Padding DefaultMargin
        {
            get
            {
                return new Padding(2);
            }
        }

        internal int GripThickness
        {
            get
            {
                return this.gripThickness;
            }
        }

        internal bool MovingToolStrip
        {
            get
            {
                if (this.ToolStripPanelRow != null)
                {
                    return this.movingToolStrip;
                }
                return false;
            }
            set
            {
                if (((this.movingToolStrip != value) && (base.ParentInternal != null)) && (!value || (base.ParentInternal.ToolStripPanelRow != null)))
                {
                    this.movingToolStrip = value;
                    this.lastEndLocation = ToolStrip.InvalidMouseEnter;
                    if (this.movingToolStrip)
                    {
                        ((ISupportToolStripPanel)base.ParentInternal).BeginDrag();
                    }
                    else
                    {
                        ((ISupportToolStripPanel)base.ParentInternal).EndDrag();
                    }
                }
            }
        }

        private ToolStripPanelRow ToolStripPanelRow
        {
            get
            {
                if (base.ParentInternal != null)
                {
                    return ((ISupportToolStripPanel)base.ParentInternal).ToolStripPanelRow;
                }
                return null;
            }
        }

        // Nested Types
        //internal class ToolStripGripAccessibleObject : ToolStripButton.ToolStripButtonAccessibleObject
        //{
        //    // Fields
        //    private string stockName;

        //    // Methods
        //    public ToolStripGripAccessibleObject(ToolStripGrip owner)
        //        : base(owner)
        //    {
        //    }

        //    // Properties
        //    public override string Name
        //    {
        //        get
        //        {
        //            string accessibleName = base.Owner.AccessibleName;
        //            if (accessibleName != null)
        //            {
        //                return accessibleName;
        //            }
        //            if (string.IsNullOrEmpty(this.stockName))
        //            {
        //                this.stockName = SR.GetString("ToolStripGripAccessibleName");
        //            }
        //            return this.stockName;
        //        }
        //        set
        //        {
        //            base.Name = value;
        //        }
        //    }

        //    public override AccessibleRole Role
        //    {
        //        get
        //        {
        //            AccessibleRole accessibleRole = base.Owner.AccessibleRole;
        //            if (accessibleRole != AccessibleRole.Default)
        //            {
        //                return accessibleRole;
        //            }
        //            return AccessibleRole.Grip;
        //        }
        //    }
        //}
    }


}
