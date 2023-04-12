using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;
using System.Runtime.Remoting.Messaging;

using Nistec.Win32;


using Nistec.Win;

namespace Nistec.WinForms
{

    /// <summary>
    /// Summary description for WaitDlg.
    /// </summary>
    public class WaitDlg : Nistec.WinForms.FormBase
    {

        #region contructor

        private System.Windows.Forms.Label LblMsg;
        private Nistec.WinForms.McButton cmdExit;
        private Nistec.WinForms.McPanel panel1;
        private Nistec.WinForms.McMove ctlMove;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Nistec.WinForms.McPanel ctlPanel1;
        private System.ComponentModel.IContainer components;

        public WaitDlg()
        {
            InitializeComponent();
            this.StylePainter = WaitDlg.Mc;
            this.SetChildrenStyle();
            //if (WaitDlg.Mc != null)
            //{
            //    if (WaitDlg.Mc is ILayout)
            //    {
            //        this.SetStyleLayout(((ILayout)WaitDlg.Mc).LayoutManager.Layout);
            //        this.SetChildrenStyle();
            //    }
            //}
        }

        public WaitDlg(IStyle ctl,bool allowCancel)
        {
            InitializeComponent();
            cmdExit.Enabled = allowCancel;
            WaitDlg.Mc = ctl;
            this.StylePainter = ctl;
            this.SetChildrenStyle();
            //if (ctl != null)
            //{
            //    this.SetStyleLayout(ctl.LayoutManager.Layout);
            //    this.SetChildrenStyle();
            //}
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.Controls.Remove(this.panel1);

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitDlg));
            this.LblMsg = new System.Windows.Forms.Label();
            this.cmdExit = new Nistec.WinForms.McButton();
            this.panel1 = new Nistec.WinForms.McPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctlMove = new Nistec.WinForms.McMove();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ctlPanel1 = new Nistec.WinForms.McPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlMove)).BeginInit();
            this.ctlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // LblMsg
            // 
            this.LblMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMsg.Location = new System.Drawing.Point(8, 5);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(232, 67);
            this.LblMsg.TabIndex = 3;
            this.LblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdExit
            // 
            this.cmdExit.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdExit.Location = new System.Drawing.Point(8, 80);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(56, 24);
            this.cmdExit.StylePainter = this.StyleGuideBase;
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "Cancel";
            this.cmdExit.ToolTipText = "Cancel";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.ctlMove);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.LblMsg);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(248, 112);
            this.panel1.StylePainter = this.StyleGuideBase;
            this.panel1.TabIndex = 7;
            this.panel1.Text = "panel1";
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Nistec.WinForms.Properties.Resources.spinner;
            this.pictureBox1.Location = new System.Drawing.Point(70, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // ctlMove
            // 
            this.ctlMove.BackColor = System.Drawing.Color.Transparent;
            this.ctlMove.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.ctlMove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlMove.Image = ((System.Drawing.Image)(resources.GetObject("ctlMove.Image")));
            this.ctlMove.Location = new System.Drawing.Point(216, 80);
            this.ctlMove.Name = "ctlMove";
            this.ctlMove.ReadOnly = false;
            this.ctlMove.Size = new System.Drawing.Size(20, 20);
            this.ctlMove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ctlMove.TabIndex = 7;
            this.ctlMove.TabStop = false;
            // 
            // ctlPanel1
            // 
            this.ctlPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlPanel1.ControlLayout = Nistec.WinForms.ControlLayout.Flat;
            this.ctlPanel1.Controls.Add(this.panel1);
            this.ctlPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel1.Location = new System.Drawing.Point(0, 0);
            this.ctlPanel1.Name = "ctlPanel1";
            this.ctlPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.ctlPanel1.Size = new System.Drawing.Size(256, 120);
            this.ctlPanel1.StylePainter = this.StyleGuideBase;
            this.ctlPanel1.TabIndex = 8;
            this.ctlPanel1.Text = "ctlPanel1";
            // 
            // WaitDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(256, 120);
            this.Controls.Add(this.ctlPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WaitDlg";
            this.Controls.SetChildIndex(this.ctlPanel1, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlMove)).EndInit();
            this.ctlPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Progress
        public bool Exit = false;
  
        
        public bool AllowCancelButton
        {
            get { return this.cmdExit.Enabled; }
            set { this.cmdExit.Enabled = value; }
        }

        [UseApiElements("WindowExStyles.WS_EX_TOPMOST")]
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand)]
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= (int)Nistec.Win32.WindowExStyles.WS_EX_TOPMOST;

                return cp;
            }
        }

        public void ShowProgressBar(string msg)
        {
            WaitDlg.Reset();
            this.LblMsg.Text = msg;

            Thread th = new Thread(new ThreadStart(ShowProgressBar));
            th.Start();
            //th.Join();
        }

        [UseApiElements("ShowWindow")]
        private void ShowProgressBar()
        {
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            //this.TopMost =true;
            Nistec.Win32.WinAPI.ShowWindow(this.Handle, WindowShowStyle.ShowNormalNoActivate);
            this.timer1.Interval = 200;
            this.timer1.Start();
            this.timer1.Enabled = true;
            this.Invalidate(true);
            //On_Start(); 

            while (!Exit)
            {
                Application.DoEvents();
            }
            if (WaitDlg.canceled)
            {
                //throw new OperationCanceledException("Operation Canceled");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            //isStop=true;
            base.OnClosed(e);
            Exit = true;
            this.timer1.Stop();
            this.timer1.Enabled = false;
        }

        internal static void ShowProgress()
        {
            if (waitDlg != null)
            {
                waitDlg = null;
            }
            waitDlg = new WaitDlg();
            waitDlg.cmdExit.Enabled = WaitDlg.AllowCancel;
            waitDlg.ShowProgressBar();
        }

        private void cmdExit_Click(object sender, System.EventArgs e)
        {
            WaitDlg.canceled = true;
            if (WaitDlg.CanceledProggress != null)
                WaitDlg.CanceledProggress(this, EventArgs.Empty);

            //On_Stop();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Exit = WaitDlg.ProgressComplited;
            string text = WaitDlg.GetMsg();
            if (this.LblMsg.Text != text)
            {
                this.LblMsg.Text = text;
            }

            if (this.Exit)
            {
                this.Close();
            }
        }

        #endregion

        #region Move

        private bool isMouseDown = false;
        private int x;
        private int y;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = true;
            x = e.X;
            y = e.Y;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
            x = 0;
            y = 0;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            try
            {
                if (isMouseDown)
                {
                    Point p = new Point(e.X - this.x, e.Y - this.y);
                    this.Location = PointToScreen(p);
                }
            }
            catch { }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }


        #endregion

        #region Invoke Sample

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //On_Start();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            //On_Stop();
        }


        // **************** 
        // 
        private delegate void UpdateLabelDelegate(string text);
        // **************** 

        private UpdateLabelDelegate m_Lbl;
        private Thread m_Thread;
        private bool isStop = false;


        private void On_Start()
        {
            //button_Start.Enabled = false; 
            //btnStop.Enabled=true;

            // affectation the current thread 
            Thread.CurrentThread.Name = "Thread principal";

            // initialisation the instance of UpdateLabelDelegate 
            m_Lbl = new UpdateLabelDelegate(this.OnUpdate);

            // creation the  thread ThreadProc 
            m_Thread = new Thread(new ThreadStart(this.ThreadProc));
            m_Thread.Name = "Thread Proc";
            m_Thread.Start();
        }

        private void ThreadProc()
        {
            object[] args = new object[1];

            int i = 0;
            while (!isStop)
            {
                //args[0] = string.Format("{0} (current : {1})", i.ToString(), Thread.CurrentThread.Name); 
                args[0] = WaitDlg.GetMsg();
                // **************** 
                this.LblMsg.Invoke(m_Lbl, args);
                // **************** 
                i++;

                Thread.Sleep(500);
            }
            args[0] = string.Format("Finshed :-) (current : {0})", Thread.CurrentThread.Name);
            //this.LblMsg.Invoke(m_Lbl, args); 
        }

        // **************** 
        // Do not directly call this method.   
        // This method is designed to use only as a delegate target that is invoke on the thread that    
        // created the Label.    
        private void OnUpdate(string text)
        {
            this.LblMsg.Text = text;
            //label_Item.Text = string.Format("{0}\r\nLabel current : {1}", text, Thread.CurrentThread.Name); 
            //listBox1.Items.Add(string.Format("{0}\r\nLabel current : {1}", text, Thread.CurrentThread.Name)); 
        }

        private void On_Stop()
        {
            this.isStop = true;
            Exit = true;
            //this.button_Start.Enabled=true;
            //this.btnStop.Enabled=false;
        }
        // **************** 

        #endregion

        #region Static

        private static bool ProgressComplited = false;
        private static string Msg = "";
        private static IStyle Mc = null;
        private static bool canceled = false;
        private static WaitDlg waitDlg = null;
        private static bool AllowCancel = false;

        public static event EventHandler CanceledProggress;

        public static bool Canceled
        {
            get { return WaitDlg.canceled; }
        }

        public static string GetMsg()
        {
            return WaitDlg.Msg;// hashMsgs[index].ToString();	
        }

        public static void SetMsg(string msg)
        {
            WaitDlg.Msg = msg;//hashMsgs[++index]=msg;
            Thread.Sleep(500);
        }

        /// <summary>
        /// Run Wait dialog
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        public static void RunProgress(IStyle ctl, string msg, bool allowCancel)
        {
            WaitDlg.Reset();
            WaitDlg.Msg = msg;
            WaitDlg.Mc = ctl;
            WaitDlg.AllowCancel = allowCancel;
           
            //SetMsg(msg);
            Thread th = new Thread(new ThreadStart(WaitDlg.ShowProgress));
            th.Start();
            //th.Join();
        }

        /// <summary>
        /// Run Wait dialog
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        public static void RunProgress(IStyle ctl, string msg)
        {
            RunProgress(ctl, msg,false);
        }
        /// <summary>
        /// Run Wait dialog
        /// </summary>
         /// <param name="msg"></param>
        public static void RunProgress(string msg)
        {
            RunProgress(null, msg,false);
        }

        public static void EndProgress()
        {
            WaitDlg.ProgressComplited = true;
            //th=null;
        }

        public static void Reset()
        {
            //hashMsgs.Clear();
            //index=-1;
            WaitDlg.canceled = false;
            WaitDlg.ProgressComplited = false;
        }

        #endregion
    }

}//namespace

