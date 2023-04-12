using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Data;
using MControl.Data;
using MControl.Win32;
using MControl.WinForms;
using MControl.Util;

namespace MControl.Wizards.Forms
{

    /// <summary>
    /// Summary description for AsyncDalDlg.
    /// </summary>
    public class AsyncDalDlg : MControl.WinForms.McForm
    {

        #region contructor

        private System.Windows.Forms.Label LblMsg;
        private MControl.WinForms.McButton cmdExit;
        //private MControl.WinForms.McPanel panel1;
        private System.Windows.Forms.Timer timer1;
        private MControl.WinForms.McSpinner sppiner;
        private MControl.WinForms.McPanel ctlPanel1;
        private System.ComponentModel.IContainer components;

        public AsyncDalDlg()
        {
            InitializeComponent();
            if (AsyncDalDlg.Mc != null)
            {
                if (AsyncDalDlg.Mc is ILayout)
                {
                    this.SetStyleLayout(((ILayout)AsyncDalDlg.Mc).LayoutManager.Layout);
                    this.SetChildrenStyle();
                }
            }
        }

        public AsyncDalDlg(ILayout ctl, bool allowCancel)
        {
            InitializeComponent();
            cmdExit.Enabled = allowCancel;
            AsyncDalDlg.Mc = ctl;
            if (ctl != null)
            {
                this.SetStyleLayout(ctl.LayoutManager.Layout);
                this.SetChildrenStyle();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsyncDalDlg));
            this.LblMsg = new System.Windows.Forms.Label();
            this.cmdExit = new MControl.WinForms.McButton();
            this.sppiner = new MControl.WinForms.McSpinner();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ctlPanel1 = new MControl.WinForms.McPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sppiner)).BeginInit();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // LblMsg
            // 
            this.LblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMsg.Location = new System.Drawing.Point(6, 63);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(281, 90);
            this.LblMsg.TabIndex = 3;
            this.LblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdExit.Location = new System.Drawing.Point(6, 156);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(56, 24);
            this.cmdExit.StylePainter = this.StyleGuideBase;
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "Cancel";
            this.cmdExit.ToolTipText = "Cancel";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // sppiner
            // 
            this.sppiner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sppiner.BackColor = System.Drawing.Color.Transparent;
            this.sppiner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sppiner.Image = ((System.Drawing.Image)(resources.GetObject("sppiner.Image")));
            this.sppiner.Location = new System.Drawing.Point(68, 156);
            this.sppiner.Name = "sppiner";
            this.sppiner.ReadOnly = false;
            this.sppiner.Size = new System.Drawing.Size(24, 24);
            this.sppiner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sppiner.TabIndex = 29;
            this.sppiner.TabStop = false;
            this.sppiner.Visible = false;
            // 
            // ctlPanel1
            // 
            this.ctlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel1.Location = new System.Drawing.Point(0, 0);
            this.ctlPanel1.Name = "ctlPanel1";
            this.ctlPanel1.Size = new System.Drawing.Size(200, 100);
            this.ctlPanel1.TabIndex = 0;
            // 
            // AsyncDalDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(302, 203);
            this.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.Controls.Add(this.sppiner);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.LblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsyncDalDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Async Data Execute";
            this.Controls.SetChildIndex(this.LblMsg, 0);
            this.Controls.SetChildIndex(this.cmdExit, 0);
            this.Controls.SetChildIndex(this.sppiner, 0);
            ((System.ComponentModel.ISupportInitialize)(this.sppiner)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Progress
        internal bool _Completed = false;

        public bool Completed
        {
            get { return _Completed; }
        }
        public STATUS DalStatus
        {
            get
            {
                if (DalAsync == null)
                    return new STATUS("No status", StatusPriority.Warnning);
                return DalAsync.AsyncStatus; 
            }
        }

        
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

                cp.ExStyle |= (int)MControl.Win32.WindowExStyles.WS_EX_TOPMOST;

                return cp;
            }
        }

        public void ShowProgressBar(string msg)
        {
            AsyncDalDlg.Reset();
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
            MControl.Win32.WinAPI.ShowWindow(this.Handle, 4);
            this.timer1.Interval = 200;
            this.timer1.Start();
            this.timer1.Enabled = true;
            this.sppiner.ShowSpinner();
            this.Invalidate(true);
            //On_Start(); 

            while (!Completed)
            {
                Application.DoEvents();
            }
            if (AsyncDalDlg.canceled)
            {
                //throw new OperationCanceledException("Operation Canceled");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _Completed = true;
            this.timer1.Stop();
            this.timer1.Enabled = false;
        }

        internal static void ShowProgress()
        {
            if (waitDlg != null)
            {
                waitDlg = null;
            }
            waitDlg = new AsyncDalDlg();
            waitDlg.cmdExit.Enabled = AsyncDalDlg.AllowCancel;
            waitDlg.ShowProgressBar();
        }

        private void cmdExit_Click(object sender, System.EventArgs e)
        {
            AsyncDalDlg.canceled = true;
            if (AsyncDalDlg.CanceledProggress != null)
                AsyncDalDlg.CanceledProggress(this, EventArgs.Empty);

            //On_Stop();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            this._Completed = AsyncDalDlg.ProgressCompleted;
            //string text = AsyncDalDlg.GetMsg();
            if (AsyncDalDlg.msgChanged/*this.LblMsg.Text != text*/)
            {
                this.LblMsg.Text = AsyncDalDlg.GetMsg();// text;
                AsyncDalDlg.msgChanged = false;
            }

            if (this.Completed)
            {
              //  this.Close();
                this.Hide();
            }
        }

        #endregion

        #region Async Dal

        protected MControl.Data.IDalAsync DalAsync;
        protected IDbConnection Connection;
        private DataTable _DataSource;
        private string _MappingName;

        public DataTable DataSource
        {
            get { return _DataSource; }
        }
        public string MappingName
        {
            get { return _MappingName; }
            set { _MappingName = value; }
        }
        /// <summary>
        /// AsyncDalExecuteStart , use OnAsyncExecuteBegin override for start executing
        /// </summary>
        /// <param name="dalBase"></param>
        public void AsyncDalExecuteStart(MControl.Data.IDalBase dalBase)
        {
            CreateDalAsync(dalBase);
            OnAsyncExecuteBegin(new AsyncCallback(AsyncHandleCallback));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dalBase"></param>
        /// <param name="sql"></param>
        /// <param name="mappingName"></param>
        public void AsyncDalExecuteStart(MControl.Data.IDalBase dalBase, string sql, string mappingName)
        {
            _MappingName = mappingName;
            CreateDalAsync(dalBase);
            DalAsync.AsyncExecuteBegin(new AsyncCallback(AsyncHandleCallback), sql, null, 0, 0);
        }

        private void CreateDalAsync(MControl.Data.IDalBase dalBase)
        {
            Connection = dalBase.IConnection;
            DalAsync = MControl.Data.Common.DalAsyncFactory.Create(dalBase);
            DalAsync.AsyncCompleted += new EventHandler(Async_AsyncCompleted);
            DalAsync.AsyncStatusChanged += new EventHandler(DalAsync_AsyncStatusChanged);
        }

        void DalAsync_AsyncStatusChanged(object sender, EventArgs e)
        {
            OnAsyncStatusChanged(e, DalAsync.AsyncStatus);
        }

        protected virtual void OnAsyncStatusChanged(EventArgs e, STATUS status)
        {
            AsyncDalDlg.SetMsg(status.Display);
            //if (IsHandleCreated)
            //{
            //this.LblMsg.Invoke(StatusDelegate, status.Display);
            //this.statusStrip.Invoke(StatusDelegate, status.Display);
           // }
        }

        public void AsyncDalDispose()
        {
            if (DalAsync != null)
            {
                DalAsync.AsyncCompleted -= new EventHandler(Async_AsyncCompleted);
                DalAsync.AsyncStatusChanged -= new EventHandler(DalAsync_AsyncStatusChanged);
            }
        }

        void AsyncHandleCallback(IAsyncResult result)
        {
            try
            {
             DalAsync.AsyncFillDataSource(   DalAsync.AsyncExecuteEnd(result));

                //if (!this.IsHandleCreated) return;
                // AsyncDataFill del = new AsyncDataFill(DalAsync.AsyncFillDataSource);
                //this.Invoke(del, DalAsync.AsyncExecuteEnd(result));

            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated) 
                    this.Invoke(new AsyncDisplayStatus(DalAsync.SetAsyncStatus), "Error: " + ex.Message, StatusPriority.Error);
            }

        }

        void Async_AsyncCompleted(object sender, EventArgs e)
        {
            OnAsyncDalCompleted(e);
            AsyncDalDispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callBack"></param>
        protected virtual void OnAsyncExecuteBegin(AsyncCallback callBack)
        {
            //DalAsync.AsyncExecuteBegin(callBack, sql, null, 0, 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAsyncDalCompleted(EventArgs e)
        {
            _DataSource = DalAsync.AsyncResult_DataTable;
            _DataSource.TableName = _MappingName;
            OnEndLoading();
        }

        #endregion
        
        #region Invoke status

        public void ExecuteStart(MControl.Data.IDalBase dalBase, string sql, string mappingName)
        {
            ExecuteStart(null, dalBase, sql, mappingName);
        }
     
        public void ExecuteStart(ILayout ctl,MControl.Data.IDalBase dalBase, string sql, string mappingName)
        {
            this.Caption.Text = "Async Dal";
            this.Caption.SubText = mappingName;
            RunProgress(ctl,"Loading",true);
            AsyncDalExecuteStart(dalBase, sql, mappingName);
            while (!Completed && !AsyncDalDlg.canceled)
            {
                Thread.Sleep(500);
            }
        }

        protected virtual void OnEndLoading()
        {
            this._Completed = true;
            AsyncDalDlg.EndProgress();
        }
 
        #endregion

        #region Static

        private static bool msgChanged = false;
        private static bool ProgressCompleted = false;
        private static string Msg = "";
        private static ILayout Mc = null;
        private static bool canceled = false;
        private static AsyncDalDlg waitDlg = null;
        private static bool AllowCancel = false;

        public static event EventHandler CanceledProggress;

        public static bool Canceled
        {
            get { return AsyncDalDlg.canceled; }
        }

        public static string GetMsg()
        {
            return AsyncDalDlg.Msg;// hashMsgs[index].ToString();	
        }

        public static void SetMsg(string msg)
        {
            AsyncDalDlg.Msg = msg;//hashMsgs[++index]=msg;
            msgChanged = true;
            Thread.Sleep(500);
        }

        /// <summary>
        /// Run Wait dialog
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        internal static void RunProgress(ILayout ctl, string msg,bool allowCancel)
        {
            AsyncDalDlg.Reset();
            AsyncDalDlg.Msg = msg;
            AsyncDalDlg.Mc = ctl;
            AsyncDalDlg.AllowCancel = allowCancel;
           
            //SetMsg(msg);
            Thread th = new Thread(new ThreadStart(AsyncDalDlg.ShowProgress));
            th.Start();
            //th.Join();
        }

        /// <summary>
        /// Run Wait dialog
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        internal static void RunProgress(ILayout ctl, string msg)
        {
            RunProgress(null, msg,false);
        }
        /// <summary>
        /// Run Wait dialog
        /// </summary>
         /// <param name="msg"></param>
        internal static void RunProgress(string msg)
        {
            RunProgress(null, msg,false);
        }

        public static void EndProgress()
        {
            AsyncDalDlg.ProgressCompleted = true;
            //th=null;
        }

        public static void Reset()
        {
            //hashMsgs.Clear();
            //index=-1;
            AsyncDalDlg.canceled = false;
            AsyncDalDlg.ProgressCompleted = false;
        }

        #endregion
    }

}

