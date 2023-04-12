using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Nistec.WinForms
{
    public enum Buttons
    {
        None,
        Ok,
        YesNo,
    }

    public enum MsgStyle
    {
        Dialog,
        Msg,
        Info,
        InputBox,
    }
    public partial class MsgDlg : Nistec.WinForms.McForm
    {
        MsgStyle _MsgStyle;

        public MsgDlg(MsgStyle msgStyle)
        {
            InitializeComponent();
            _MsgStyle = msgStyle;
        }

        public MsgDlg(MsgStyle msgStyle,IStyle style)
        {
            InitializeComponent();
            this.SetStyleLayout(style);
            this.SetChildrenStyle();
            _MsgStyle = msgStyle;
        }

        #region InputBox

        public static string ShowInputBox(Control parent, string DisplayValue, string msg, string Caption)
        {
            return ShowInputBox(parent, DisplayValue, msg, Caption, true, 0);
        }

        public static string ShowInputBox(Control parent, string DisplayValue, string msg, string Caption, bool captionVisible, int width)
        {
            MsgDlg f = new MsgDlg(MsgStyle.InputBox);
            f.LblMsg.Visible = false;
            f.CaptionVisible = captionVisible;
            //f.SetRightToLeft();
            int captionHeight = captionVisible ? f.Caption.Height : 0;
            f.Height = 10 + captionHeight + f.panelFooter.Height + f.Input.Height;// (this.Height - LblMsg.Height) + size.Height;

            if (parent != null)
            {
                f.RightToLeft = parent.RightToLeft;
                Point pt = new Point(parent.Left, parent.Top - f.Height);
                
                f.Location = parent.Parent.PointToScreen(pt);

                if (parent is ILayout)
                {
                    f.StylePainter= ((ILayout)parent).StylePainter;//.SetStyleLayout(((ILayout)parent).LayoutManager.Layout);
                    f.SetChildrenStyle();
                }
                //f.Width = parent.Width;
            }
            if (width > 0)
            {
                f.Width = width;
            }

            f.lblBottom.Text = msg;
            f.Text = Caption;
            //f.LblMsg.Text = msg;
            f.Input.Text = DisplayValue;

            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                return f.Input.Text;
            return "";
        }


        public static string ShowInputBox(IStyle style, string DisplayValue, string msg, string Caption)
        {
            MsgDlg f = new MsgDlg(MsgStyle.InputBox);
            f.LblMsg.Visible = false;
            f.CaptionVisible = true;
            int captionHeight = f.Caption.Height;
            f.Height = 10 + captionHeight + f.panelFooter.Height + f.Input.Height;// (this.Height - LblMsg.Height) + size.Height;
            f.SetStyleLayout(style);
            f.SetChildrenStyle();

            f.lblBottom.Text = msg;
            f.Text = Caption;
            //f.LblMsg.Text = msg;
            f.Input.Text = DisplayValue;

            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                return f.Input.Text;
            return "";
        }

        #endregion

        #region Notify Dialog Static Function

        public static DialogResult ShowDialog(Control parent, string text, string strTitle)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Dialog);
            f.DialogOpen(parent, text, strTitle, Buttons.YesNo);
            return f.DialogResult;
        }
        public static DialogResult ShowDialog(string text, string strTitle, Buttons buttons)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Dialog);
            f.DialogOpen(null, text, strTitle, buttons);
            return f.DialogResult;
        }

        public static DialogResult ShowDialog(string text, string strTitle)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Dialog);
            f.DialogOpen(null, text, strTitle, Buttons.Ok);
            return f.DialogResult;
        }
        public static DialogResult ShowDialog(IStyle style, string text, string strTitle, Buttons buttons)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Dialog,style);
            f.DialogOpen(null, text, strTitle, buttons);
            return f.DialogResult;
        }
        #endregion

        #region Notify interval Static Function

        public static void ShowMsg(Control parent,string text, string strTitle, double interval)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Msg);
            f.MsgOpen(parent, text, strTitle, interval, false);
        }

        public static void ShowMsg(string text, string strTitle, double interval)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Msg);
            f.MsgOpen(null, text, strTitle, interval, false);
        }

        public static void ShowMsg(string text, string strTitle)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Msg);
            f.MsgOpen(null, text, strTitle, DefaultInterval, false);
        }
        public static void ShowMsg(string text)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Msg);
            f.MsgOpen(null, text, "Nistec", DefaultInterval, false);
        }
        public static void ShowMsg(Control parent, string text, string strTitle)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Msg);
            f.MsgOpen(parent, text, strTitle, DefaultInterval, false);
        }


        public static void ShowMsg(IStyle style, string text, string caption)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Msg,style);
            f.MsgOpen(null, text, caption, DefaultInterval, false);
        }

        #endregion

        #region MsgInfo interval Static Function

        public static void ShowInfo(Control parent, string text, double interval)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Info);
            f.MsgOpen(parent, text, "", interval, true);
        }

        public static void ShowInfo(string text, double interval)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Info);
            f.MsgOpen(null, text, "", interval, true);
        }

        public static void ShowInfo(string text)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Info);
            f.MsgOpen(null, text, "", DefaultInterval, true);
        }

        public static void ShowInfo(Control parent, string text)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Info);
            f.MsgOpen(parent, text, "", DefaultInterval, true);
        }

        public static void ShowInfo(IStyle style, string text)
        {
            MsgDlg f = new MsgDlg(MsgStyle.Info,style);
            f.MsgOpen(null, text, "", DefaultInterval, true);
        }

        #endregion

        #region Property

        public const int DefaultInterval = 2000;
        private double mInterval = DefaultInterval;
        private bool IsTimer = false;

        public double TimeInterval
        {
            get { return mInterval; }
            set { mInterval = value; }
        }


        private void DialogOpen(Control parent, string text, string strTitle,Buttons buttons)
        {
            SetButtons(buttons);
            SetMsg(parent,text, strTitle);
            IsTimer = false;
            this.ShowDialog();
        }

        private void SetButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case Buttons.None:
                    this.panelFooter.Visible = false;
                    break;
                case Buttons.Ok:
                    this.btnColse.Visible = false;
                    break;
                case Buttons.YesNo:
                    break;
            }

        }
        private void MsgOpen(Control parent, string text, string strTitle, double interval, bool ShowInfo)
        {
            MsgOpen(parent, text, strTitle, interval, ShowInfo, Buttons.None);
        }

        private void MsgOpen(Control parent, string text, string strTitle, double interval, bool ShowInfo, Buttons buttons)
        {
            if (ShowInfo)
            {
                buttons = Buttons.None;
                this.Padding = new Padding(6);
                //this.FormBorderStyle = FormBorderStyle.None;
                //panelText.StylePainter = this.StyleGuideBase;
                //this.panelText.BorderStyle =BorderStyle.FixedSingle;
                this.StyleGuideBase.FormColor = SystemColors.Info;
            }

            SetButtons(buttons);
            this.CaptionVisible = (!ShowInfo);
            SetMsg(parent,text, strTitle );
            WaitTime =(int) interval;
            IsTimer = true;
            //this.timer1.Interval = interval;
            //this.timer1.Start();

            this.Opacity = 0;
            ShowWindow(Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(Handle, HWND_TOPMOST, this.Location.X, this.Location.Y, this.Width, this.Height, SWP_NOACTIVATE);
            //SetWindowPos(Handle, HWND_TOPMOST, rScreen.Width - ActualWidth - 11, rScreen.Bottom, ActualWidth, 0, SWP_NOACTIVATE);

            //Show();
            viewClock = new System.Windows.Forms.Timer();
            viewClock.Tick += new System.EventHandler(viewTimer);
            viewClock.Interval = 1;
            viewClock.Start();
        }

        private void SetMsg(Control parent, string text, string strTitle)
        {
            const int minWidth = 200;
            const int minHeight = 32;

            this.Input.Visible = false;
            this.Text = strTitle;
            int rows = GetRows(text);
            System.Drawing.Graphics gr = this.CreateGraphics();

            Size Ssize = Nistec.Drawing.TextUtils.GetTextSize(gr, text, LblMsg.Font);
            Size Tsize = GetTextRect(text.Length, Ssize, rows, LblMsg.Font.Height);
            System.Drawing.Rectangle rect = new Rectangle(0, 0, Tsize.Width, Tsize.Height);
            Size size = Nistec.Drawing.TextUtils.GetTextSize(gr, text, LblMsg.Font, ref rect, Nistec.Win32.DrawTextFormatFlags.DT_WORDBREAK | Nistec.Win32.DrawTextFormatFlags.DT_INTERNAL | Nistec.Win32.DrawTextFormatFlags.DT_CALCRECT | Nistec.Win32.DrawTextFormatFlags.DT_VCENTER);


            if (size.Width < minWidth)
                this.Width = minWidth;
            else
                this.Width = size.Width;

            if (size.Height < minHeight)
                this.Height =32+ this.Caption.Height + this.panelFooter.Height + minHeight;//(this.Height - LblMsg.Height) + minHeight;
            else
                this.Height =32+ this.Caption.Height + this.panelFooter.Height + size.Height;// (this.Height - LblMsg.Height) + size.Height;

            this.LblMsg.Size = this.panelText.Size;

            //RightToLeft Rtl = rtl;

            if (parent != null)
            {
                //this.Parent=parent;
                if (parent is ILayout)
                {
                    base.StylePainter = ((ILayout)parent).StylePainter;
                    //base.SetStyleLayout(((ILayout)parent).LayoutManager.Layout);
                    base.SetChildrenStyle();
                }

                //Rtl = parent.RightToLeft;
                //Point pt = new Point(parent.Left, parent.Top - this.Height);
                if (parent is Form)
                {
                    this.Location = GetCenterScreen();// parent.PointToScreen (pt);
                }
                else
                {
                    Point pt = new Point(parent.Left, parent.Top - this.Height);
                    this.Location = parent.Parent.PointToScreen(pt);
                }
                if (this.Location.Y < 50)
                    this.Location = new Point(this.Location.X, 50);
                else if (this.Location.X < 50)
                    this.Location = new Point(50, this.Location.Y);
                else if (this.Location.X + this.Size.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Location = GetCenterScreen();
                else if (this.Location.Y + this.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Location = GetCenterScreen();

            }
            else
            {
                this.Location = GetCenterScreen();
                //this.StartPosition  =System.Windows.Forms.FormStartPosition.CenterScreen;
            }

            //if(Rtl== RightToLeft.Yes)
            //this.LblMsg.TextAlign = ContentAlignment.MiddleRight;
            //else
            //this.LblMsg.TextAlign = ContentAlignment.MiddleLeft;
          
            //this.LblMsg.RightToLeft = Rtl; ;
            //this.LblTitle.RightToLeft = Rtl;; 

            this.LblMsg.Text = text;
            //this.LblTitle.Text  = strTitle; 
            //this.TopLevel =true;

        }

        private Point GetCenterScreen()
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            return new Point(x, y);
        }

        private int GetRows(string text)
        {
            if (text == null) return 0;

            int len = text.Length;
            int rows = 0;
            int k = 0;
            int l = -1;

            do
            {
                rows++;
                l = text.IndexOf("\r\n", k);
                k = l + 1;

            } while (l > -1);

            //for (int i = 0; i < len; i++)
            //{
            //    l = text.IndexOf("\r\n", k);
            //    if (l > 0)
            //    {
            //        rows++;
            //        k = l + 1;
            //        i = l + 1;
            //    }
            //    else
            //        break;
            //}
            return rows;
        }

        private Size GetTextRect(int len, Size textSize, int rn, int FontHeight)
        {
            Size size = textSize;
            int rowAdd = 0;
            int rows = rn;

            if (size.Width < 300)
                rowAdd = 0;
            else if (size.Width < 1000)
                rowAdd = 2;
            else if (size.Width < 5000)
                rowAdd = 5;
            else
                rowAdd = size.Width / 1500;

            if (rowAdd > 0)
                size.Width = textSize.Width / rowAdd;
            rows += rowAdd;
            size.Height = (rows * FontHeight) + 10;

            return size;
        }

        //		public override void SetStyleLayout(StyleGuide sg)
        //		{
        //			base.SetStyleLayout(sg);
        //			if(sg!=null)
        //			{
        //			    StyleLayout sgl=sg.GetStyleLayout();		
        //				BackColor=sgl.FormColor;
        //				Rect1.StyleMc.StylePlan  =sgl.StylePlan ;
        //				Rect2.StyleMc.StylePlan  =sgl.StylePlan ;
        //				Invalidate();
        //			}
        //		}

        #endregion

        #region Methods


        //private void NotifyBox_SizeChanged(object sender, System.EventArgs e)
        //{
        // panel2.Width =Width-pnlFooter.Width;   
        //}

        //private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    this.timer1.Stop();
        //    this.timer1.Dispose();
        //    if (IsTimer)
        //        this.Close();
        //}

 

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();

        }
        #endregion

        #region info

        //protected bool closePressed = false, textPressed = false, titlePressed = false, closeHot = false, textHot = false, titleHot = false;
        //protected Rectangle rClose, rText, rTitle, rDisplay, rScreen, rGlobClose, rGlobText, rGlobTitle, rGlobDisplay;
        System.Windows.Forms.Timer viewClock;
        bool showing = true;
        int WaitTime;


        /// <summary>
        /// Determine whether or not XP Visual Styles are active.  Compatible with pre-UxTheme.dll versions of Windows.
        /// </summary>
        protected bool visualStylesEnabled()
        {
            try
            {
                if (IsThemeActive() == 1)
                    return true;
                else
                    return false;
            }
            catch (System.DllNotFoundException)  // pre-XP systems which don't have UxTheme.dll
            {
                return false;
            }
        }



        private void CloseWindow()
        {
             if (IsTimer)
            {
                viewClock.Stop();
                viewClock.Dispose();
            }
            Close();
        }
 
        protected void viewTimer(object sender, System.EventArgs e)
        {
            if (showing)
            {
                if (this.Opacity >= 1)
                {
                    showing = false;
                    viewClock.Interval = WaitTime;
                }
                else
                {
                    this.Opacity += 0.01;
                }
            }
            else
            {
                if (this.Opacity <= 0)
                {
                    CloseWindow();
                }
                else if (this.Opacity >= 1)
                {
                    viewClock.Interval = 1;
                    this.Opacity -= 0.01;
                }
                else
                {
                    this.Opacity -= 0.01;
                }
            }

        }

  
        // DrawThemeBackground()
        protected const Int32 WP_CLOSEBUTTON = 18;
        protected const Int32 CBS_NORMAL = 1;
        protected const Int32 CBS_HOT = 2;
        protected const Int32 CBS_PUSHED = 3;

        [StructLayout(LayoutKind.Explicit)]
        protected struct RECT
        {
            [FieldOffset(0)]
            public Int32 Left;
            [FieldOffset(4)]
            public Int32 Top;
            [FieldOffset(8)]
            public Int32 Right;
            [FieldOffset(12)]
            public Int32 Bottom;

            public RECT(System.Drawing.Rectangle bounds)
            {
                Left = bounds.Left;
                Top = bounds.Top;
                Right = bounds.Right;
                Bottom = bounds.Bottom;
            }
        }

        // SetWindowPos()
        protected const Int32 HWND_TOPMOST = -1;
        protected const Int32 SWP_NOACTIVATE = 0x0010;

        // ShowWindow()
        protected const Int32 SW_SHOWNOACTIVATE = 4;

        // UxTheme.dll
        [DllImport("UxTheme.dll")]
        protected static extern Int32 IsThemeActive();

        [DllImport("UxTheme.dll")]
        protected static extern IntPtr OpenThemeData(IntPtr hWnd, [MarshalAs(UnmanagedType.LPTStr)] string classList);

        [DllImport("UxTheme.dll")]
        protected static extern void CloseThemeData(IntPtr hTheme);

        [DllImport("UxTheme.dll")]
        protected static extern void DrawThemeBackground(IntPtr hTheme, IntPtr hDC, Int32 partId, Int32 stateId, ref RECT rect, ref RECT clipRect);

        // user32.dll
        [DllImport("user32.dll")]
        protected static extern bool ShowWindow(IntPtr hWnd, Int32 flags);

        [DllImport("user32.dll")]
        protected static extern bool SetWindowPos(IntPtr hWnd, Int32 hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, uint uFlags);
        #endregion


    }
}