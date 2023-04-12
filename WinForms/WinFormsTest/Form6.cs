using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;
using Nistec.Win32;
using System.Runtime.InteropServices;
using Nistec.Collections;

namespace WinCtlTest
{
    public partial class Form6 : Nistec.WinForms.McForm
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void mcMultiBox1_ButtonClick(object sender, Nistec.WinForms.ButtonClickEventArgs e)
        {
            DataTable dt=new DataTable();
            dt.TableName="tbl";
            dt.Columns.Add("Col1");
             dt.Columns.Add("Col2");
             dt.Columns.Add("Col3");
            dt.Rows.Add("a","aa","aaa");
            dt.Rows.Add("b","bb","bbb");
            dt.Rows.Add("c","cc","ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            dt.Rows.Add("c", "cc", "ccc");
            Nistec.WinForms.ListDropDown.DoDropDown(this.mcMultiBox1, dt, "Col1", "Col1", true);
        }

        private void mcButton1_Click(object sender, EventArgs e)
        {
            NotifyWindow nf = NotifyWindow.CreateNotifyWindow(this.StylePainter, NotifyStyle.Info,"Nistec", "This is a Notify message");
            nf.ShowNotify(220,110,true,2000);
            //NotifyWindow.ShowNotify("Nistec", "This is a Notify message");
            
            //InputBox.Open(this, "dispaly", "msg", "caption");
            //MsgDlg.OpenDialog(this.StylePainter, "dispaly", "msg", Buttons.YesNo);
        }

        private void mcButton2_Click(object sender, EventArgs e)
        {
            wl = new WindowList(this.mcButton2);
            wl.ImageList = this.imageList1;
            wl.SelectionChanged += new SelectionChangedEventHandler(wl_SelectionChanged);
            wl.DoDropDown(GetItems());
        }
        Nistec.WinForms.WindowList wl;
        private ListItems GetItems()
        {
            ListItems list = new ListItems();
            list.Add("aaaaa", 0);
            list.Add("tttt", 1);
            list.Add("sssss", 2);
            list.Add("gggg", 3);
            list.Add("qqqq", 4);
            list.Add("kkkk", 5);
            list.Add("fffff", 6);
            list.Add("dddd", 7);
            list.Add("ggg", 8);
            list.Add("xxx", 9);
            list.Add("hhhh", 10);
            list.Add("cccc", 11);
            list.Sort();
            return list;
        }

        void wl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Text=e.Value.ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                wl = new WindowList(this.textBox1);
                wl.ImageList = this.imageList1;
                wl.SelectionChanged += new SelectionChangedEventHandler(wl_SelectionChanged);
                wl.DoDropDown(GetItems());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (wl!=null &&  wl.DroppedDown)
            {
                wl.FindString(this.textBox1.Text);
            }
        }




      //  private ComboBoxChildNativeWindow childEdit;
      //  private ComboBoxChildNativeWindow childListBox;

      //  private void mcButton3_Click(object sender, EventArgs e)
      //  {
      //      Message m = this.mcButton3.LastMsg;

      //      RECT rect = new RECT();
      //      WinMethods.GetWindowRect(new System.Runtime.InteropServices.HandleRef(this.mcButton3, base.Handle), ref rect);
      //      Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
      //      int x = WinAPI.SignedLOWORD( m.LParam);
      //      int y = WinAPI.SignedLOWORD(m.LParam);
      //      Point p = new Point(x, y);
      //      p = base.PointToScreen(p);

      //      //NativeWindow w = new NativeWindow();

      //      //WinAPI.SendMessage(0x14f, -1 , 0);

      //      //w.AssignHandle(this.Handle);
      //      childEdit.DefWndProc(ref m);
      //      //w.CreateHandle(CreateParams.);

      //       SendMessage(0x14f, -1, 0);

      //  }
      //  internal IntPtr SendMessage(int msg, int wparam, int lparam)
      //  {
      //      return SendMessage(new HandleRef(this.mcButton3, this.mcButton3.Handle), msg, wparam, lparam);
      //  }
 

      //  [DllImport("user32.dll", CharSet = CharSet.Auto)]
      //  public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);
 

 


      //  private void DefChildWndProc(ref Message m)
      //  {
      //      if (this.childEdit != null)
      //      {
      //          NativeWindow window = (m.HWnd == this.childEdit.Handle) ? this.childEdit : this.childListBox;
      //          if (window != null)
      //          {
      //              window.DefWndProc(ref m);
      //          }
      //      }
      //  }
      //  /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated"></see> event.</summary>
      //  /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
      //  protected override void OnHandleCreated(EventArgs e)
      //  {
      //      bool flag2;
      //      base.OnHandleCreated(e);
      //      if (((this.childEdit == null) && (this.childListBox == null)))// && (this.DropDownStyle != ComboBoxStyle.DropDownList))
      //      {
      //          IntPtr window = WinAPI.GetWindowLong32(new HandleRef(this, base.Handle), 5);
      //          //if (window != IntPtr.Zero)
      //          //{
      //              //if (this.DropDownStyle == ComboBoxStyle.Simple)
      //              //{
      //              //    this.childListBox = new ComboBoxChildNativeWindow(this);
      //              //    this.childListBox.AssignHandle(window);
      //              //    window = System.Windows.Forms.UnsafeNativeMethods.GetWindow(new HandleRef(this, window), 2);
      //              //}
      //              this.childEdit = new ComboBoxChildNativeWindow(this.mcButton3);
      //              this.childEdit.AssignHandle(window);
      //              WinMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 0xd3, 3, 0);
      //          //}
      //      }
      //}

      //  private class ComboBoxChildNativeWindow : NativeWindow
      //  {
      //      //private InternalAccessibleObject _accessibilityObject;
      //      private McButton _owner;

      //      internal ComboBoxChildNativeWindow(McButton comboBox)
      //      {
      //          this._owner = comboBox;
      //      }

      //      //private void WmGetObject(ref Message m)
      //      //{
      //      //    if (-4 == ((int)((long)m.LParam)))
      //      //    {
      //      //        Guid refiid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
      //      //        try
      //      //        {
      //      //            //AccessibleObject accessibleImplemention = null;
      //      //            //if (this._accessibilityObject == null)
      //      //            //{
      //      //            //    System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
      //      //            //    try
      //      //            //    {
      //      //            //        accessibleImplemention = new ComboBox.ChildAccessibleObject(this._owner, base.Handle);
      //      //            //        this._accessibilityObject = new InternalAccessibleObject(accessibleImplemention);
      //      //            //    }
      //      //            //    finally
      //      //            //    {
      //      //            //        CodeAccessPermission.RevertAssert();
      //      //            //    }
      //      //            //}
      //      //            IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(this._accessibilityObject);
      //      //            //System.Windows.Forms.IntSecurity.UnmanagedCode.Assert();
      //      //            try
      //      //            {
      //      //                m.Result = WinMethods.LresultFromObject(ref refiid, m.WParam, new HandleRef(this, iUnknownForObject));
      //      //            }
      //      //            finally
      //      //            {
      //      //               // CodeAccessPermission.RevertAssert();
      //      //                Marshal.Release(iUnknownForObject);
      //      //            }
      //      //            return;
      //      //        }
      //      //        catch (Exception exception)
      //      //        {
      //      //            throw new InvalidOperationException(System.Windows.Forms.SR.GetString("RichControlLresult"), exception);
      //      //        }
      //      //    }
      //      //    base.DefWndProc(ref m);
      //      //}

      //      protected override void WndProc(ref Message m)
      //      {
      //          if (m.Msg == 0x3d)
      //          {
      //              //this.WmGetObject(ref m);
      //          }
      //          else
      //          {
      //             // this._owner.ChildWndProc(ref m);
      //          }
      //      }
      //  }

    }
}