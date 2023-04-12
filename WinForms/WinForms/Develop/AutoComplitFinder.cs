using System;
using System.Collections.Generic;
using System.Text;

namespace mControl.WinCtl.Develop
{
  private class AutoCompleteDropDownFinder
{
    // Fields
    private const string AutoCompleteClassName = "Auto-Suggest Dropdown";
    private const int MaxClassName = 0x100;
    private bool shouldSubClass;

    // Methods
    private bool Callback(IntPtr hWnd, IntPtr lParam)
    {
        HandleRef hRef = new HandleRef(null, hWnd);
        if (GetClassName(hRef) == "Auto-Suggest Dropdown")
        {
            ComboBox.ACNativeWindow.RegisterACWindow(hRef.Handle, this.shouldSubClass);
        }
        return true;
    }

    internal void FindDropDowns()
    {
        this.FindDropDowns(true);
    }

    internal void FindDropDowns(bool subclass)
    {
        if (!subclass)
        {
        }
        this.shouldSubClass = subclass;
        UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(this.Callback), new HandleRef(null, IntPtr.Zero));
    }

    private static string GetClassName(HandleRef hRef)
    {
        StringBuilder lpClassName = new StringBuilder(0x100);
        UnsafeNativeMethods.GetClassName(hRef, lpClassName, 0x100);
        return lpClassName.ToString();
    }
}


}
