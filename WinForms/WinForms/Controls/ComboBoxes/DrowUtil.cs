using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using mControl.Util;
using mControl.Win32;
using System.Collections;


namespace mControl.WinCtl.Controls.ComboBoxes
{
    #region enums

    public enum RegionFlags
    {
        ERROR,
        NULLREGION,
        SIMPLEREGION,
        COMPLEXREGION
    }

    internal enum DeviceContextType
    {
        Unknown,
        Display,
        NCWindow,
        NamedDevice,
        Information,
        Memory,
        Metafile
    }

    internal enum GdiObjectType
    {
        Bitmap = 7,
        Brush = 2,
        ColorSpace = 14,
        DisplayDC = 3,
        EnhancedMetafileDC = 12,
        EnhMetafile = 13,
        ExtendedPen = 11,
        Font = 6,
        MemoryDC = 10,
        Metafile = 9,
        MetafileDC = 4,
        Palette = 5,
        Pen = 1,
        Region = 8
    }


    internal enum DeviceCapabilities
    {
        BitsPerPixel = 12,
        DriverVersion = 0,
        HorizontalResolution = 8,
        HorizontalSize = 4,
        LogicalPixelsX = 0x58,
        LogicalPixelsY = 90,
        PhysicalHeight = 0x6f,
        PhysicalOffsetX = 0x70,
        PhysicalOffsetY = 0x71,
        PhysicalWidth = 110,
        ScalingFactorX = 0x72,
        ScalingFactorY = 0x73,
        Technology = 2,
        VerticalResolution = 10,
        VerticalSize = 6
    }


    internal enum DeviceContextBackgroundMode
    {
        Opaque = 2,
        Transparent = 1
    }

    internal enum DeviceContextGraphicsMode
    {
        Advanced = 2,
        Compatible = 1,
        ModifyWorldIdentity = 1
    }


    internal enum DeviceContextMapMode
    {
        Anisotropic = 8,
        HiEnglish = 5,
        HiMetric = 3,
        Isotropic = 7,
        LoEnglish = 4,
        LoMetric = 2,
        Text = 1,
        Twips = 6
    }


    [Flags]
    internal enum DeviceContextLayout
    {
        BitmapOrientationPreserved = 8,
        BottomToTop = 2,
        Normal = 0,
        RightToLeft = 1,
        VerticalBeforeHorizontal = 4
    }

    internal enum DeviceContextTextAlignment
    {
        BaseLine = 0x18,
        Bottom = 8,
        Center = 6,
        Default = 0,
        Left = 0,
        NoUpdateCP = 0,
        Right = 2,
        RtlReading = 0x100,
        Top = 0,
        UpdateCP = 1,
        VerticalBaseLine = 2,
        VerticalCenter = 3
    }

    [Flags]
    internal enum DeviceContextBinaryRasterOperationFlags
    {
        Black = 1,
        CopyPen = 13,
        MaskNotPen = 3,
        MaskPen = 9,
        MaskPenNot = 5,
        MergeNotPen = 12,
        MergePen = 15,
        MergePenNot = 14,
        Nop = 11,
        Not = 6,
        NotCopyPen = 4,
        NotMaskPen = 8,
        NotMergePen = 2,
        NotXorPen = 10,
        White = 0x10,
        XorPen = 7
    }


    internal enum RegionCombineMode
    {
        AND = 1,
        COPY = 5,
        DIFF = 4,
        MAX = 5,
        MIN = 1,
        OR = 2,
        XOR = 3
    }
    #endregion

    public class NativeMethod
    {

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetViewportOrgEx(HandleRef hDC, [In, Out] NativeMethods.POINT point);
        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In, Out] NativeMethods.POINT point);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int CombineRgn(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetRgnBox(HandleRef hRegion, ref WinMethods.RECT clipRect);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        internal static extern bool IntDeleteObject(HandleRef hObject);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeParentBackground(HandleRef hwnd, HandleRef hdc, [In] NativeMethods.COMRECT prc);
 

 

 











        public static bool DeleteObject(HandleRef hObject)
        {
            HandleCollector.Remove((IntPtr)hObject, CommonHandles.GDI);
            return IntDeleteObject(hObject);
        }

 

 

 

        public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
        {
            return HandleCollector.Add(IntCreateRectRgn(x1, y1, x2, y2), CommonHandles.GDI);
        }




    }



    internal delegate void HandleChangeEventHandler(string handleType, IntPtr handleValue, int currentHandleCount);

 

 

    internal sealed class HandleCollector
    {
        // Fields
        private static int handleTypeCount;
        private static HandleType[] handleTypes;
        private static object internalSyncObject = new object();
        private static int suspendCount;

        // Events
        internal static event HandleChangeEventHandler HandleAdded;

        internal static event HandleChangeEventHandler HandleRemoved;

        // Methods
        internal static IntPtr Add(IntPtr handle, int type)
        {
            handleTypes[type - 1].Add(handle);
            return handle;
        }

        internal static int RegisterType(string typeName, int expense, int initialThreshold)
        {
            lock (internalSyncObject)
            {
                if ((handleTypeCount == 0) || (handleTypeCount == handleTypes.Length))
                {
                    HandleType[] destinationArray = new HandleType[handleTypeCount + 10];
                    if (handleTypes != null)
                    {
                        Array.Copy(handleTypes, 0, destinationArray, 0, handleTypeCount);
                    }
                    handleTypes = destinationArray;
                }
                handleTypes[handleTypeCount++] = new HandleType(typeName, expense, initialThreshold);
                return handleTypeCount;
            }
        }

        internal static IntPtr Remove(IntPtr handle, int type)
        {
            return handleTypes[type - 1].Remove(handle);
        }

        internal static void ResumeCollect()
        {
            bool flag = false;
            lock (internalSyncObject)
            {
                if (suspendCount > 0)
                {
                    suspendCount--;
                }
                if (suspendCount == 0)
                {
                    for (int i = 0; i < handleTypeCount; i++)
                    {
                        lock (handleTypes[i])
                        {
                            if (handleTypes[i].NeedCollection())
                            {
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                GC.Collect();
            }
        }

        internal static void SuspendCollect()
        {
            lock (internalSyncObject)
            {
                suspendCount++;
            }
        }

        // Nested Types
        private class HandleType
        {
            // Fields
            private readonly int deltaPercent;
            private int handleCount;
            private int initialThreshHold;
            internal readonly string name;
            private int threshHold;

            // Methods
            internal HandleType(string name, int expense, int initialThreshHold)
            {
                this.name = name;
                this.initialThreshHold = initialThreshHold;
                this.threshHold = initialThreshHold;
                this.deltaPercent = 100 - expense;
            }

            internal void Add(IntPtr handle)
            {
                if (handle != IntPtr.Zero)
                {
                    bool flag = false;
                    int currentHandleCount = 0;
                    lock (this)
                    {
                        this.handleCount++;
                        flag = this.NeedCollection();
                        currentHandleCount = this.handleCount;
                    }
                    lock (HandleCollector.internalSyncObject)
                    {
                        if (HandleCollector.HandleAdded != null)
                        {
                            HandleCollector.HandleAdded(this.name, handle, currentHandleCount);
                        }
                    }
                    if (flag && flag)
                    {
                        GC.Collect();
                        int millisecondsTimeout = (100 - this.deltaPercent) / 4;
                        Thread.Sleep(millisecondsTimeout);
                    }
                }
            }

            internal int GetHandleCount()
            {
                lock (this)
                {
                    return this.handleCount;
                }
            }

            internal bool NeedCollection()
            {
                if (HandleCollector.suspendCount <= 0)
                {
                    if (this.handleCount > this.threshHold)
                    {
                        this.threshHold = this.handleCount + ((this.handleCount * this.deltaPercent) / 100);
                        return true;
                    }
                    int num = (100 * this.threshHold) / (100 + this.deltaPercent);
                    if ((num >= this.initialThreshHold) && (this.handleCount < ((int)(num * 0.9f))))
                    {
                        this.threshHold = num;
                    }
                }
                return false;
            }

            internal IntPtr Remove(IntPtr handle)
            {
                if (handle != IntPtr.Zero)
                {
                    int currentHandleCount = 0;
                    lock (this)
                    {
                        this.handleCount--;
                        if (this.handleCount < 0)
                        {
                            this.handleCount = 0;
                        }
                        currentHandleCount = this.handleCount;
                    }
                    lock (HandleCollector.internalSyncObject)
                    {
                        if (HandleCollector.HandleRemoved != null)
                        {
                            HandleCollector.HandleRemoved(this.name, handle, currentHandleCount);
                        }
                    }
                }
                return handle;
            }
        }
    }
 



 


 

 

 


 

 

 


 



        public sealed class GraphicsState : MarshalByRefObject
        {
            // Fields
            internal int nativeState;

            // Methods
            internal GraphicsState(int nativeState)
            {
                this.nativeState = nativeState;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct DCMapping : IDisposable
        {
            //private DeviceContext dc;
            private Graphics graphics;
            private Rectangle translatedBounds;
            public DCMapping(HandleRef hDC, Rectangle bounds)
            {
                if (hDC.Handle == IntPtr.Zero)
                {
                    throw new ArgumentNullException("hDC");
                }
                NativeMethods.POINT point = new NativeMethods.POINT();
                HandleRef nullHandleRef = NullHandleRef;
                RegionFlags nULLREGION = RegionFlags.NULLREGION;
                this.translatedBounds = bounds;
                this.graphics = null;
                this.dc = DeviceContext.FromHdc(hDC.Handle);
                this.dc.SaveHdc();
                NativeMethod.GetViewportOrgEx(hDC, point);
                HandleRef hRgn = new HandleRef(null, NativeMethod.CreateRectRgn(point.x + bounds.Left, point.y + bounds.Top, point.x + bounds.Right, point.y + bounds.Bottom));
                try
                {
                    nullHandleRef = new HandleRef(this, NativeMethod.CreateRectRgn(0, 0, 0, 0));
                    int clipRgn = NativeMethod.GetClipRgn(hDC, nullHandleRef);
                    NativeMethods.POINT point2 = new NativeMethods.POINT();
                    NativeMethod.SetViewportOrgEx(hDC, point.x + bounds.Left, point.y + bounds.Top, point2);
                    if (clipRgn != 0)
                    {
                        WinMethods.RECT clipRect = new WinMethods.RECT();
                        if (NativeMethod.GetRgnBox(nullHandleRef, ref clipRect) == 2)
                        {
                            NativeMethod.CombineRgn(hRgn, hRgn, nullHandleRef, 1);
                        }
                    }
                    else
                    {
                        NativeMethod.DeleteObject(nullHandleRef);
                        nullHandleRef = new HandleRef(null, IntPtr.Zero);
                        nULLREGION = RegionFlags.SIMPLEREGION;
                    }
                    NativeMethod.SelectClipRgn(hDC, hRgn);
                }
                catch (Exception exception)
                {
                    if (ClientUtils.IsSecurityOrCriticalException(exception))
                    {
                        throw;
                    }
                    this.dc.RestoreHdc();
                    this.dc.Dispose();
                }
                finally
                {
                    SafeNativeMethods.DeleteObject(hRgn);
                    if (nullHandleRef.Handle != IntPtr.Zero)
                    {
                        SafeNativeMethods.DeleteObject(nullHandleRef);
                    }
                }
            }

            public void Dispose()
            {
                if (this.graphics != null)
                {
                    this.graphics.Dispose();
                    this.graphics = null;
                }
                if (this.dc != null)
                {
                    this.dc.RestoreHdc();
                    this.dc.Dispose();
                    this.dc = null;
                }
            }

            public Graphics Graphics
            {
                get
                {
                    if (this.graphics == null)
                    {
                        this.graphics = Graphics.FromHdcInternal(this.dc.Hdc);
                        this.graphics.SetClip(new Rectangle(Point.Empty, this.translatedBounds.Size));
                    }
                    return this.graphics;
                }
            }
        }

        //internal sealed class DeviceContext : MarshalByRefObject, IDeviceContext, IDisposable
        //{
        //    // Fields
        //    private Stack contextStack;
        //    private DeviceContextType dcType;
        //    private IntPtr hCurrentBmp;
        //    private IntPtr hCurrentBrush;
        //    private IntPtr hCurrentFont;
        //    private IntPtr hCurrentPen;
        //    private IntPtr hDC;
        //    private IntPtr hInitialBmp;
        //    private IntPtr hInitialBrush;
        //    private IntPtr hInitialFont;
        //    private IntPtr hInitialPen;
        //    private IntPtr hWnd;
        //    //private WindowsFont selectedFont;

        //    // Methods
        //    private DeviceContext(IntPtr hWnd)
        //    {
        //        this.hWnd = (IntPtr)(-1);
        //        this.hWnd = hWnd;
        //        this.dcType = DeviceContextType.Display;
        //        DeviceContexts.AddDeviceContext(this);
        //    }

        //    private DeviceContext(IntPtr hDC, DeviceContextType dcType)
        //    {
        //        this.hWnd = (IntPtr)(-1);
        //        this.hDC = hDC;
        //        this.dcType = dcType;
        //        this.CacheInitialState();
        //        DeviceContexts.AddDeviceContext(this);
        //        if (dcType == DeviceContextType.Display)
        //        {
        //            this.hWnd = IntUnsafeNativeMethods.WindowFromDC(new HandleRef(this, this.hDC));
        //        }
        //    }

        //    private void CacheInitialState()
        //    {
        //        this.hCurrentPen = this.hInitialPen = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 1);
        //        this.hCurrentBrush = this.hInitialBrush = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 2);
        //        this.hCurrentBmp = this.hInitialBmp = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 7);
        //        this.hCurrentFont = this.hInitialFont = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 6);
        //    }

        //    public static DeviceContext CreateDC(string driverName, string deviceName, string fileName, HandleRef devMode)
        //    {
        //        return new DeviceContext(IntUnsafeNativeMethods.CreateDC(driverName, deviceName, fileName, devMode), DeviceContextType.NamedDevice);
        //    }

        //    public static DeviceContext CreateIC(string driverName, string deviceName, string fileName, HandleRef devMode)
        //    {
        //        return new DeviceContext(IntUnsafeNativeMethods.CreateIC(driverName, deviceName, fileName, devMode), DeviceContextType.Information);
        //    }

        //    public void DeleteObject(IntPtr handle, GdiObjectType type)
        //    {
        //        IntPtr zero = IntPtr.Zero;
        //        switch (type)
        //        {
        //            case GdiObjectType.Pen:
        //                if (handle == this.hCurrentPen)
        //                {
        //                    IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialPen));
        //                    this.hCurrentPen = IntPtr.Zero;
        //                }
        //                zero = handle;
        //                break;

        //            case GdiObjectType.Brush:
        //                if (handle == this.hCurrentBrush)
        //                {
        //                    IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialBrush));
        //                    this.hCurrentBrush = IntPtr.Zero;
        //                }
        //                zero = handle;
        //                break;

        //            case GdiObjectType.Bitmap:
        //                if (handle == this.hCurrentBmp)
        //                {
        //                    IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialBmp));
        //                    this.hCurrentBmp = IntPtr.Zero;
        //                }
        //                zero = handle;
        //                break;
        //        }
        //        IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, zero));
        //    }

        //    public void Dispose()
        //    {
        //        this.Dispose(true);
        //        GC.SuppressFinalize(this);
        //    }

        //    internal void Dispose(bool disposing)
        //    {
        //        if (this.hDC != IntPtr.Zero)
        //        {
        //            this.DisposeFont(disposing);
        //            switch (this.dcType)
        //            {
        //                case DeviceContextType.Unknown:
        //                case DeviceContextType.NCWindow:
        //                    return;

        //                case DeviceContextType.Display:
        //                    this.ReleaseHdc();
        //                    return;

        //                case DeviceContextType.NamedDevice:
        //                case DeviceContextType.Information:
        //                    IntUnsafeNativeMethods.DeleteHDC(new HandleRef(this, this.hDC));
        //                    this.hDC = IntPtr.Zero;
        //                    return;

        //                case DeviceContextType.Memory:
        //                    IntUnsafeNativeMethods.DeleteDC(new HandleRef(this, this.hDC));
        //                    this.hDC = IntPtr.Zero;
        //                    return;
        //            }
        //        }
        //    }

        //    internal void DisposeFont(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            DeviceContexts.RemoveDeviceContext(this);
        //        }
        //        if ((this.selectedFont != null) && (this.selectedFont.Hfont != IntPtr.Zero))
        //        {
        //            if (IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 6) == this.selectedFont.Hfont)
        //            {
        //                IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, this.hInitialFont));
        //                IntPtr hInitialFont = this.hInitialFont;
        //            }
        //            this.selectedFont.Dispose(disposing);
        //            this.selectedFont = null;
        //        }
        //    }

        //    ~DeviceContext()
        //    {
        //        this.Dispose(false);
        //    }

        //    public static DeviceContext FromCompatibleDC(IntPtr hdc)
        //    {
        //        return new DeviceContext(IntUnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, hdc)), DeviceContextType.Memory);
        //    }

        //    public static DeviceContext FromHdc(IntPtr hdc)
        //    {
        //        return new DeviceContext(hdc, DeviceContextType.Unknown);
        //    }

        //    public static DeviceContext FromHwnd(IntPtr hwnd)
        //    {
        //        return new DeviceContext(hwnd);
        //    }

        //    public int GetDeviceCapabilities(DeviceCapabilities capabilityIndex)
        //    {
        //        return IntUnsafeNativeMethods.GetDeviceCaps(new HandleRef(this, this.Hdc), (int)capabilityIndex);
        //    }

        //    public void IntersectClip(WindowsRegion wr)
        //    {
        //        if (wr.HRegion != IntPtr.Zero)
        //        {
        //            WindowsRegion wrapper = new WindowsRegion(0, 0, 0, 0);
        //            try
        //            {
        //                if (IntUnsafeNativeMethods.GetClipRgn(new HandleRef(this, this.Hdc), new HandleRef(wrapper, wrapper.HRegion)) == 1)
        //                {
        //                    wr.CombineRegion(wrapper, wr, RegionCombineMode.AND);
        //                }
        //                this.SetClip(wr);
        //            }
        //            finally
        //            {
        //                wrapper.Dispose();
        //            }
        //        }
        //    }

        //    //public bool IsFontOnContextStack(WindowsFont wf)
        //    //{
        //    //    if (this.contextStack != null)
        //    //    {
        //    //        foreach (GraphicsState state in this.contextStack)
        //    //        {
        //    //            if (state.hFont == wf.Hfont)
        //    //            {
        //    //                return true;
        //    //            }
        //    //        }
        //    //    }
        //    //    return false;
        //    //}

        //    public void ResetFont()
        //    {
        //        MeasurementDCInfo.ResetIfIsMeasurementDC(this.Hdc);
        //        IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, this.hInitialFont));
        //        this.selectedFont = null;
        //        this.hCurrentFont = this.hInitialFont;
        //    }

        //    public void RestoreHdc()
        //    {
        //        IntUnsafeNativeMethods.RestoreDC(new HandleRef(this, this.hDC), -1);
        //        if (this.contextStack != null)
        //        {
        //            GraphicsState state = (GraphicsState)this.contextStack.Pop();
        //            this.hCurrentBmp = state.hBitmap;
        //            this.hCurrentBrush = state.hBrush;
        //            this.hCurrentPen = state.hPen;
        //            this.hCurrentFont = state.hFont;
        //            if ((state.font != null) && state.font.IsAlive)
        //            {
        //                this.selectedFont = state.font.Target as WindowsFont;
        //            }
        //            else
        //            {
        //                WindowsFont selectedFont = this.selectedFont;
        //                this.selectedFont = null;
        //                if ((selectedFont != null) && MeasurementDCInfo.IsMeasurementDC(this))
        //                {
        //                    selectedFont.Dispose();
        //                }
        //            }
        //        }
        //        MeasurementDCInfo.ResetIfIsMeasurementDC(this.hDC);
        //    }

        //    public int SaveHdc()
        //    {
        //        HandleRef hDC = new HandleRef(this, this.Hdc);
        //        int num = IntUnsafeNativeMethods.SaveDC(hDC);
        //        if (this.contextStack == null)
        //        {
        //            this.contextStack = new Stack();
        //        }
        //        GraphicsState state = new GraphicsState();
        //        state.hBitmap = this.hCurrentBmp;
        //        state.hBrush = this.hCurrentBrush;
        //        state.hPen = this.hCurrentPen;
        //        state.hFont = this.hCurrentFont;
        //        state.font = new WeakReference(this.selectedFont);
        //        this.contextStack.Push(state);
        //        return num;
        //    }

        //    //public IntPtr SelectFont(WindowsFont font)
        //    //{
        //    //    if (font.Equals(this.Font))
        //    //    {
        //    //        return IntPtr.Zero;
        //    //    }
        //    //    IntPtr ptr = this.SelectObject(font.Hfont, GdiObjectType.Font);
        //    //    WindowsFont selectedFont = this.selectedFont;
        //    //    this.selectedFont = font;
        //    //    this.hCurrentFont = font.Hfont;
        //    //    if ((selectedFont != null) && MeasurementDCInfo.IsMeasurementDC(this))
        //    //    {
        //    //        selectedFont.Dispose();
        //    //    }
        //    //    if (MeasurementDCInfo.IsMeasurementDC(this))
        //    //    {
        //    //        if (ptr != IntPtr.Zero)
        //    //        {
        //    //            MeasurementDCInfo.LastUsedFont = font;
        //    //            return ptr;
        //    //        }
        //    //        MeasurementDCInfo.Reset();
        //    //    }
        //    //    return ptr;
        //    //}

        //    public IntPtr SelectObject(IntPtr hObj, GdiObjectType type)
        //    {
        //        switch (type)
        //        {
        //            case GdiObjectType.Pen:
        //                this.hCurrentPen = hObj;
        //                break;

        //            case GdiObjectType.Brush:
        //                this.hCurrentBrush = hObj;
        //                break;

        //            case GdiObjectType.Bitmap:
        //                this.hCurrentBmp = hObj;
        //                break;
        //        }
        //        return IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, hObj));
        //    }

        //    public Color SetBackgroundColor(Color newColor)
        //    {
        //        return ColorTranslator.FromWin32(IntUnsafeNativeMethods.SetBkColor(new HandleRef(this, this.Hdc), ColorTranslator.ToWin32(newColor)));
        //    }

        //    public DeviceContextBackgroundMode SetBackgroundMode(DeviceContextBackgroundMode newMode)
        //    {
        //        return (DeviceContextBackgroundMode)IntUnsafeNativeMethods.SetBkMode(new HandleRef(this, this.Hdc), (int)newMode);
        //    }

        //    public void SetClip(WindowsRegion region)
        //    {
        //        HandleRef hDC = new HandleRef(this, this.Hdc);
        //        HandleRef hRgn = new HandleRef(region, region.HRegion);
        //        IntUnsafeNativeMethods.SelectClipRgn(hDC, hRgn);
        //    }

        //    public DeviceContextGraphicsMode SetGraphicsMode(DeviceContextGraphicsMode newMode)
        //    {
        //        return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.SetGraphicsMode(new HandleRef(this, this.Hdc), (int)newMode);
        //    }

        //    public DeviceContextMapMode SetMapMode(DeviceContextMapMode newMode)
        //    {
        //        return (DeviceContextMapMode)IntUnsafeNativeMethods.SetMapMode(new HandleRef(this, this.Hdc), (int)newMode);
        //    }

        //    public DeviceContextBinaryRasterOperationFlags SetRasterOperation(DeviceContextBinaryRasterOperationFlags rasterOperation)
        //    {
        //        return (DeviceContextBinaryRasterOperationFlags)IntUnsafeNativeMethods.SetROP2(new HandleRef(this, this.Hdc), (int)rasterOperation);
        //    }

        //    public DeviceContextTextAlignment SetTextAlignment(DeviceContextTextAlignment newAligment)
        //    {
        //        return (DeviceContextTextAlignment)IntUnsafeNativeMethods.SetTextAlign(new HandleRef(this, this.Hdc), (int)newAligment);
        //    }

        //    public Color SetTextColor(Color newColor)
        //    {
        //        return ColorTranslator.FromWin32(IntUnsafeNativeMethods.SetTextColor(new HandleRef(this, this.Hdc), ColorTranslator.ToWin32(newColor)));
        //    }

        //    public Size SetViewportExtent(Size newExtent)
        //    {
        //        IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
        //        IntUnsafeNativeMethods.SetViewportExtEx(new HandleRef(this, this.Hdc), newExtent.Width, newExtent.Height, size);
        //        return size.ToSize();
        //    }

        //    public Point SetViewportOrigin(Point newOrigin)
        //    {
        //        IntNativeMethods.POINT point = new IntNativeMethods.POINT();
        //        IntUnsafeNativeMethods.SetViewportOrgEx(new HandleRef(this, this.Hdc), newOrigin.X, newOrigin.Y, point);
        //        return point.ToPoint();
        //    }

        //    IntPtr IDeviceContext.GetHdc()
        //    {
        //        if (this.hDC == IntPtr.Zero)
        //        {
        //            this.hDC = IntUnsafeNativeMethods.GetDC(new HandleRef(this, this.hWnd));
        //        }
        //        return this.hDC;
        //    }

        //    void IDeviceContext.ReleaseHdc()
        //    {
        //        if ((this.hDC != IntPtr.Zero) && (this.dcType == DeviceContextType.Display))
        //        {
        //            IntUnsafeNativeMethods.ReleaseDC(new HandleRef(this, this.hWnd), new HandleRef(this, this.hDC));
        //            this.hDC = IntPtr.Zero;
        //        }
        //    }

        //    public void TranslateTransform(int dx, int dy)
        //    {
        //        IntNativeMethods.POINT point = new IntNativeMethods.POINT();
        //        IntUnsafeNativeMethods.OffsetViewportOrgEx(new HandleRef(this, this.Hdc), dx, dy, point);
        //    }

        //    // Properties
        //    //public WindowsFont ActiveFont
        //    //{
        //    //    get
        //    //    {
        //    //        return this.selectedFont;
        //    //    }
        //    //}

        //    public Color BackgroundColor
        //    {
        //        get
        //        {
        //            return ColorTranslator.FromWin32(IntUnsafeNativeMethods.GetBkColor(new HandleRef(this, this.Hdc)));
        //        }
        //    }

        //    public DeviceContextBackgroundMode BackgroundMode
        //    {
        //        get
        //        {
        //            return (DeviceContextBackgroundMode)IntUnsafeNativeMethods.GetBkMode(new HandleRef(this, this.Hdc));
        //        }
        //    }

        //    public DeviceContextBinaryRasterOperationFlags BinaryRasterOperation
        //    {
        //        get
        //        {
        //            return (DeviceContextBinaryRasterOperationFlags)IntUnsafeNativeMethods.GetROP2(new HandleRef(this, this.Hdc));
        //        }
        //    }

        //    public DeviceContextType DeviceContextType
        //    {
        //        get
        //        {
        //            return this.dcType;
        //        }
        //    }

        //    public Size Dpi
        //    {
        //        get
        //        {
        //            return new Size(this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsX), this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsY));
        //        }
        //    }

        //    public int DpiX
        //    {
        //        get
        //        {
        //            return this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsX);
        //        }
        //    }

        //    public int DpiY
        //    {
        //        get
        //        {
        //            return this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsY);
        //        }
        //    }

        //    //public WindowsFont Font
        //    //{
        //    //    get
        //    //    {
        //    //        if (MeasurementDCInfo.IsMeasurementDC(this))
        //    //        {
        //    //            WindowsFont lastUsedFont = MeasurementDCInfo.LastUsedFont;
        //    //            if ((lastUsedFont != null) && (lastUsedFont.Hfont != IntPtr.Zero))
        //    //            {
        //    //                return lastUsedFont;
        //    //            }
        //    //        }
        //    //        return WindowsFont.FromHdc(this.Hdc);
        //    //    }
        //    //}

        //    public DeviceContextGraphicsMode GraphicsMode
        //    {
        //        get
        //        {
        //            return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.GetGraphicsMode(new HandleRef(this, this.Hdc));
        //        }
        //    }

        //    public IntPtr Hdc
        //    {
        //        get
        //        {
        //            if ((this.hDC == IntPtr.Zero) && (this.dcType == DeviceContextType.Display))
        //            {
        //                this.hDC = this.GetHdc();
        //                this.CacheInitialState();
        //            }
        //            return this.hDC;
        //        }
        //    }

        //    public DeviceContextMapMode MapMode
        //    {
        //        get
        //        {
        //            return (DeviceContextMapMode)IntUnsafeNativeMethods.GetMapMode(new HandleRef(this, this.Hdc));
        //        }
        //    }

        //    public static DeviceContext ScreenDC
        //    {
        //        get
        //        {
        //            return FromHwnd(IntPtr.Zero);
        //        }
        //    }

        //    public DeviceContextTextAlignment TextAlignment
        //    {
        //        get
        //        {
        //            return (DeviceContextTextAlignment)IntUnsafeNativeMethods.GetTextAlign(new HandleRef(this, this.Hdc));
        //        }
        //    }

        //    public Color TextColor
        //    {
        //        get
        //        {
        //            return ColorTranslator.FromWin32(IntUnsafeNativeMethods.GetTextColor(new HandleRef(this, this.Hdc)));
        //        }
        //    }

        //    public Size ViewportExtent
        //    {
        //        get
        //        {
        //            IntNativeMethods.SIZE lpSize = new IntNativeMethods.SIZE();
        //            IntUnsafeNativeMethods.GetViewportExtEx(new HandleRef(this, this.Hdc), lpSize);
        //            return lpSize.ToSize();
        //        }
        //        set
        //        {
        //            this.SetViewportExtent(value);
        //        }
        //    }

        //    public Point ViewportOrigin
        //    {
        //        get
        //        {
        //            IntNativeMethods.POINT lpPoint = new IntNativeMethods.POINT();
        //            IntUnsafeNativeMethods.GetViewportOrgEx(new HandleRef(this, this.Hdc), lpPoint);
        //            return lpPoint.ToPoint();
        //        }
        //        set
        //        {
        //            this.SetViewportOrigin(value);
        //        }
        //    }

        //    // Nested Types
        //    internal class GraphicsState
        //    {
        //        // Fields
        //        internal WeakReference font;
        //        internal IntPtr hBitmap;
        //        internal IntPtr hBrush;
        //        internal IntPtr hFont;
        //        internal IntPtr hPen;
        //    }
        //}

    internal sealed class WindowsRegion : MarshalByRefObject, ICloneable, IDisposable
    {
        // Fields
        private IntPtr nativeHandle;
        private bool ownHandle;

        // Methods
        private WindowsRegion()
        {
        }

        public WindowsRegion(Rectangle rect)
        {
            this.CreateRegion(rect);
        }

        public WindowsRegion(int x, int y, int width, int height)
        {
            this.CreateRegion(new Rectangle(x, y, width, height));
        }

        public object Clone()
        {
            if (!this.IsInfinite)
            {
                return new WindowsRegion(this.ToRectangle());
            }
            return new WindowsRegion();
        }

        public RegionFlags CombineRegion(WindowsRegion region1, WindowsRegion region2, RegionCombineMode mode)
        {
            return IntUnsafeNativeMethods.CombineRgn(new HandleRef(this, this.HRegion), new HandleRef(region1, region1.HRegion), new HandleRef(region2, region2.HRegion), mode);
        }

        private void CreateRegion(Rectangle rect)
        {
            this.nativeHandle = IntSafeNativeMethods.CreateRectRgn(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
            this.ownHandle = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (this.nativeHandle != IntPtr.Zero)
            {
                if (this.ownHandle)
                {
                    IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, this.nativeHandle));
                }
                this.nativeHandle = IntPtr.Zero;
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        ~WindowsRegion()
        {
            this.Dispose(false);
        }

        public static WindowsRegion FromHregion(IntPtr hRegion, bool takeOwnership)
        {
            WindowsRegion region = new WindowsRegion();
            if (hRegion != IntPtr.Zero)
            {
                region.nativeHandle = hRegion;
                if (takeOwnership)
                {
                    region.ownHandle = true;
                    HandleCollector.Add(hRegion, IntSafeNativeMethods.CommonHandles.GDI);
                }
            }
            return region;
        }

        public static WindowsRegion FromRegion(Region region, Graphics g)
        {
            if (region.IsInfinite(g))
            {
                return new WindowsRegion();
            }
            return FromHregion(region.GetHrgn(g), true);
        }

        public Rectangle ToRectangle()
        {
            if (this.IsInfinite)
            {
                return new Rectangle(-2147483647, -2147483647, 0x7fffffff, 0x7fffffff);
            }
            IntNativeMethods.RECT clipRect = new IntNativeMethods.RECT();
            IntUnsafeNativeMethods.GetRgnBox(new HandleRef(this, this.nativeHandle), ref clipRect);
            return new Rectangle(new Point(clipRect.left, clipRect.top), clipRect.Size);
        }

        // Properties
        public IntPtr HRegion
        {
            get
            {
                return this.nativeHandle;
            }
        }

        public bool IsInfinite
        {
            get
            {
                return (this.nativeHandle == IntPtr.Zero);
            }
        }
    }



    public class DrowingUtil
    {
        public static HandleRef NullHandleRef;

        public void DrawParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }
            if (childControl == null)
            {
                throw new ArgumentNullException("childControl");
            }
            if (((bounds.Width >= 0) && (bounds.Height >= 0)) && (childControl.Handle != IntPtr.Zero))
            {
                using (WindowsGraphicsWrapper wrapper = new WindowsGraphicsWrapper(dc, TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.PreserveGraphicsClipping))
                {
                    HandleRef hdc = new HandleRef(wrapper, wrapper.WindowsGraphics.DeviceContext.Hdc);
                    this.lastHResult = SafeNativeMethods.DrawThemeParentBackground(new HandleRef(this, childControl.Handle), hdc, new NativeMethods.COMRECT(bounds));
                }
            }
        }



        internal sealed class WindowsGraphicsWrapper : IDisposable
        {
            // Fields
            private IDeviceContext idc;
            private WindowsGraphics wg;

            // Methods
            public WindowsGraphicsWrapper(IDeviceContext idc, TextFormatFlags flags)
            {
                if (idc is Graphics)
                {
                    ApplyGraphicsProperties none = ApplyGraphicsProperties.None;
                    if ((flags & TextFormatFlags.PreserveGraphicsClipping) != TextFormatFlags.GlyphOverhangPadding)
                    {
                        none |= ApplyGraphicsProperties.Clipping;
                    }
                    if ((flags & TextFormatFlags.PreserveGraphicsTranslateTransform) != TextFormatFlags.GlyphOverhangPadding)
                    {
                        none |= ApplyGraphicsProperties.TranslateTransform;
                    }
                    if (none != ApplyGraphicsProperties.None)
                    {
                        this.wg = WindowsGraphics.FromGraphics(idc as Graphics, none);
                    }
                }
                else
                {
                    this.wg = idc as WindowsGraphics;
                    if (this.wg != null)
                    {
                        this.idc = idc;
                    }
                }
                if (this.wg == null)
                {
                    this.idc = idc;
                    this.wg = WindowsGraphics.FromHdc(idc.GetHdc());
                }
                if ((flags & TextFormatFlags.LeftAndRightPadding) != TextFormatFlags.GlyphOverhangPadding)
                {
                    this.wg.TextPadding = TextPaddingOptions.LeftAndRightPadding;
                }
                else if ((flags & TextFormatFlags.NoPadding) != TextFormatFlags.GlyphOverhangPadding)
                {
                    this.wg.TextPadding = TextPaddingOptions.NoPadding;
                }
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void Dispose(bool disposing)
            {
                if (this.wg != null)
                {
                    if (this.wg != this.idc)
                    {
                        this.wg.Dispose();
                        if (this.idc != null)
                        {
                            this.idc.ReleaseHdc();
                        }
                    }
                    this.idc = null;
                    this.wg = null;
                }
            }

            ~WindowsGraphicsWrapper()
            {
                this.Dispose(false);
            }

            // Properties
            public WindowsGraphics WindowsGraphics
            {
                get
                {
                    return this.wg;
                }
            }
        }

 



        internal void PaintTransparentBackground(Control ctl, PaintEventArgs e, Rectangle rectangle, Region transparentRegion)
        {
            Graphics g = e.Graphics;
            Control parentInternal = ctl.Parent;// Internal;
            if (parentInternal != null)
            {
                if (Application.RenderWithVisualStyles && parentInternal.RenderTransparencyWithVisualStyles)
                {
                    GraphicsState gstate = null;
                    if (transparentRegion != null)
                    {
                        gstate = g.Save();
                    }
                    try
                    {
                        if (transparentRegion != null)
                        {
                            g.Clip = transparentRegion;
                        }
                        ButtonRenderer.DrawParentBackground(g, rectangle, this);
                        return;
                    }
                    finally
                    {
                        if (gstate != null)
                        {
                            g.Restore(gstate);
                        }
                    }
                }
                Rectangle bounds = new Rectangle(-this.Left, -this.Top, parentInternal.Width, parentInternal.Height);
                Rectangle clipRect = new Rectangle(rectangle.Left + this.Left, rectangle.Top + this.Top, rectangle.Width, rectangle.Height);
                HandleRef hDC = new HandleRef(this, g.GetHdc());
                try
                {
                    WindowsFormsUtils.DCMapping mapping = new WindowsFormsUtils.DCMapping(hDC, bounds);
                    try
                    {
                        using (PaintEventArgs args = new PaintEventArgs(mapping.Graphics, clipRect))
                        {
                            if (transparentRegion != null)
                            {
                                args.Graphics.Clip = transparentRegion;
                                args.Graphics.TranslateClip(-bounds.X, -bounds.Y);
                            }
                            try
                            {
                                this.InvokePaintBackground(parentInternal, args);
                                this.InvokePaint(parentInternal, args);
                            }
                            finally
                            {
                                if (transparentRegion != null)
                                {
                                    args.Graphics.TranslateClip(bounds.X, bounds.Y);
                                }
                            }
                        }
                    }
                    finally
                    {
                        mapping.Dispose();
                    }
                }
                finally
                {
                    g.ReleaseHdcInternal(hDC.Handle);
                }
            }
            else
            {
                g.FillRectangle(SystemBrushes.Control, rectangle);
            }
        }

    }

}
