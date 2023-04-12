using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Runtime.InteropServices;

using Nistec.Win32;

namespace Nistec.WinForms
{
    [ToolboxItem(true), ToolboxBitmap(typeof(McToolTip), "Toolbox.ToolTip.bmp")]
    public class McToolTip : ToolTip
    {
       // internal readonly ToolTip toolTip;
        //internal readonly static McToolTip Instance;

        static McToolTip()
        {
            //Instance = new McToolTip();
        }

        public McToolTip():base()
        {
            //this.toolTip = new ToolTip();
            base.AutomaticDelay = 5000;
            //// Set up the delays for the ToolTip.
            base.AutoPopDelay = 5000;
            base.InitialDelay = 1000;
            base.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            //base.ShowAlways = true;
            //base.Active = true;
            //base.UseAnimation = true;
            //base.UseFading = true;

         }

        public void Show(string text, Control ctl)
        {
           
            Point p = Cursor.Position;
            p = ctl.PointToClient(p);
            p.Y += 10;
            
            this.Show(text, ctl, p);
        }

        public void Show(string text, Control ctl, int x , int y)
        {
            this.Show(text, ctl, new Point(x,y));
        }

        public void Show(string text, Control ctl,Point point)
        {
            if (ctl == null) return;
          
            IntSecurity.AllWindows.Assert();
            try
            {
                base.Show(text, ctl, point);
                //this.Show(ControlUtils.TextWithoutMnemonics(text), ctl);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
             finally
            {
                System.Security.CodeAccessPermission.RevertAssert();
            }
        }

        public void Show(string text, Control ctl, Point point,int duration)
        {
            if (ctl == null) return;

            IntSecurity.AllWindows.Assert();
            try
            {
                base.Show(text, ctl, point, duration);
                //this.Show(ControlUtils.TextWithoutMnemonics(text), ctl);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                System.Security.CodeAccessPermission.RevertAssert();
            }
        }

        public void Hide(Control ctl)
        {
            if (ctl == null) return;
            IntSecurity.AllWindows.Assert();
            try
            {
               
                base.Hide(ctl);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                System.Security.CodeAccessPermission.RevertAssert();
            }
        }

        //public void SetToolTip(Control ctl, string text)
        //{
        //    base.SetToolTip(ctl, text);
        //    base.Active = true;
        //}

       
        //public void ClearToolTip(Control ctl)
        //{
        //   base.SetToolTip(ctl, null);
        //  // base.Active = false;
        //}

        //public static void ShowToolTip(string text, Control ctl)
        //{
        //    if (!string.IsNullOrEmpty(text))
        //    {
        //       Instance.Show( text,ctl);
        //    }
        //}

        //public static void ShowToolTip(string text, Control ctl, Point point)
        //{
        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        Instance.Show(text, ctl,point);
        //    }
        //}

        //public static void HideToolTip(Control ctl)
        //{
        //    if (ctl != null )
        //    {
        //        Instance.Hide(ctl);
        //    }
        //}
        public static bool ContainsMnemonic(string text)
        {
            if (text != null)
            {
                int length = text.Length;
                int index = text.IndexOf('&', 0);
                if (((index >= 0) && (index <= (length - 2))) && (text.IndexOf('&', index + 1) == -1))
                {
                    return true;
                }
            }
            return false;
        }
    }

    //class sample
    //{
        #region ToolTip

        //public void ShowToolTip(string text)
        //{

        //    if ((this.toolTipText != text) && (this.toolTipText.Length > 0))
        //    {
        //        this.ToolTipProvider.RemoveToolTip((IntPtr)this.toolTipId);
        //        this.toolTipText = string.Empty;
        //        this.toolTipId = new IntPtr(-1);
        //    }
        //    if (text.Length != 0)
        //    {
        //        int num1;

        //        this.toolTipText = text;
        //        this.ToolTipId = (num1 = this.ToolTipId) + 1;
        //        this.toolTipId = (IntPtr)num1;
        //        this.ToolTipProvider.AddToolTip(this.toolTipText, this.toolTipId, this.ClientRectangle);
        //    }
        //}
        //public void ShowToolTip()
        //{
        //    if (!string.IsNullOrEmpty(toolTipText) && this.toolTipText.Length > 0)
        //    {
        //        this.ToolTipProvider.RemoveToolTip((IntPtr)this.toolTipId);
        //        int num1;

        //        this.ToolTipId = (num1 = this.ToolTipId) + 1;
        //        this.toolTipId = (IntPtr)num1;
        //        this.ToolTipProvider.AddToolTip(this.toolTipText, toolTipId, this.Bounds);
        //    }
        //}
        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    this.toolTipProvider = new McToolTip(this);
        //    this.toolTipProvider.CreateToolTipHandle();
        //    this.toolTipId = (IntPtr)0;
        //    base.PerformLayout();
        //}

        //protected override void OnHandleDestroyed(EventArgs e)
        //{
        //    base.OnHandleDestroyed(e);
        //    this.toolTipProvider.Destroy();
        //    this.toolTipProvider = null;
        //    this.toolTipId = (IntPtr)0;
        //}

        //protected override void OnLayout(LayoutEventArgs levent)
        //{
        //    try
        //    {
        //        if (base.IsHandleCreated)
        //        {
        //            if (this.ToolTipProvider != null)
        //            {
        //                this.ResetToolTip();
        //            }
        //            //this.ComputeLayout();
        //        }
        //    }
        //    finally { }
        //}

        //private void ResetToolTip()
        //{
        //    this.ToolTipProvider.Destroy();
        //    this.ToolTipProvider.CreateToolTipHandle();
        //    this.ToolTipId = 0;
        //}

        //internal McToolTip ToolTipProvider
        //{
        //    get
        //    {
        //        return this.toolTipProvider;
        //    }
        //}

        //internal int ToolTipId
        //{
        //    get
        //    {
        //        return (int)this.toolTipId;
        //    }
        //    set
        //    {
        //        this.toolTipId = (IntPtr)value;
        //    }
        //}

        //[Category("Behavior"), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), Description("ToolTipText")]
        //public string ToolTipText
        //{
        //    get
        //    {

        //        return this.toolTipText;

        //        //if (!this.AutoToolTip || !string.IsNullOrEmpty(this.toolTipText))
        //        //{
        //        //    return this.toolTipText;
        //        //}
        //        //string text = this.Text;
        //        //if (Global.ContainsMnemonic(text))
        //        //{
        //        //    text = string.Join("", text.Split(new char[] { '&' }));
        //        //}
        //        //return text;
        //    }
        //    set
        //    {
        //        this.toolTipText = value;
        //    }
        //}
        //private IntPtr toolTipId;
        //private McToolTip toolTipProvider;
        //private string toolTipText;

        #endregion

    //}
 
    //internal class McToolTip : MarshalByRefObject
    //{
    
    //     // Methods
    //    public McToolTip(McControl ctl)
    //    {
    //        this.tipWindow = null;
    //        this.Mc = null;
    //        this.Mc = ctl;
    //    }


    //    public void AddToolTip(string toolTipString, IntPtr toolTipId, Rectangle iconBounds)
    //    {
    //        if (toolTipString == null)
    //        {
    //            throw new ArgumentNullException("McToolTipNull");
    //        }
    //        if (iconBounds.IsEmpty)
    //        {
    //            throw new ArgumentNullException("McToolTipEmptyIcon");
    //        }
    //        WinMethods.TOOLINFO_T toolinfo_t1 = new WinMethods.TOOLINFO_T();
    //        toolinfo_t1.cbSize = Marshal.SizeOf(toolinfo_t1);
    //        toolinfo_t1.hwnd = this.Mc.Handle;
    //        toolinfo_t1.uId = toolTipId;
    //        toolinfo_t1.lpszText = toolTipString;
    //        toolinfo_t1.rect = WinMethods.RECT.FromXYWH(iconBounds.X, iconBounds.Y, iconBounds.Width, iconBounds.Height);
    //        toolinfo_t1.uFlags = 0x10;
    //        WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), WinMethods.TTM_ADDTOOL, 0, toolinfo_t1);
    //    }

    //    public void CreateToolTipHandle()
    //    {
    //        if ((this.tipWindow == null) || (this.tipWindow.Handle == IntPtr.Zero))
    //        {
    //            WinMethods.INITCOMMONCONTROLSEX initcommoncontrolsex1 = new WinMethods.INITCOMMONCONTROLSEX();
    //            initcommoncontrolsex1.dwICC = 8;
    //            initcommoncontrolsex1.dwSize = Marshal.SizeOf(initcommoncontrolsex1);
    //            WinMethods.InitCommonControlsEx(initcommoncontrolsex1);
    //            CreateParams params1 = new CreateParams();
    //            params1.Parent = this.Mc.Handle;
    //            params1.ClassName = "tooltips_class32";
    //            params1.Style = 1;
    //            this.tipWindow = new NativeWindow();
    //            this.tipWindow.CreateHandle(params1);
    //            WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 0x418, 0, SystemInformation.MaxWindowTrackSize.Width);
    //            WinMethods.SetWindowPos(new HandleRef(this.tipWindow, this.tipWindow.Handle), WinMethods.HWND_NOTOPMOST, 0, 0, 0, 0, 0x13);
    //            WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 0x403, 3, 0);
    //         }
    //    }

    //    public void DeactivateToolTip()
    //    {
    //        WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 0x401, 0, 0);
    //    }

    //    public void Destroy()
    //    {
    //        this.tipWindow.DestroyHandle();
    //        this.tipWindow = null;
    //    }

    //    public void RemoveToolTip(IntPtr toolTipId)
    //    {
    //        WinMethods.TOOLINFO_T toolinfo_t1 = new WinMethods.TOOLINFO_T();
    //        toolinfo_t1.cbSize = Marshal.SizeOf(toolinfo_t1);
    //        toolinfo_t1.hwnd = this.Mc.Handle;
    //        toolinfo_t1.uId = toolTipId;
    //        WinMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), WinMethods.TTM_DELTOOL, 0, toolinfo_t1);
    //    }


    //    // Fields
    //    private McControl Mc;
    //    private NativeWindow tipWindow;

    //    public static bool ContainsMnemonic(string text)
    //    {
    //        if (text != null)
    //        {
    //            int length = text.Length;
    //            int index = text.IndexOf('&', 0);
    //            if (((index >= 0) && (index <= (length - 2))) && (text.IndexOf('&', index + 1) == -1))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}

}
