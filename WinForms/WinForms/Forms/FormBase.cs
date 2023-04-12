using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Security.Permissions;


using Nistec.Data;
//using Nistec.Threading;
using System.Threading;

using Extension.Nistec.Threading;

namespace Nistec.WinForms
{
	

    public partial class FormBase : Form, IFormBase, ILayout
    {
        public FormBase()
        {
            //showResizing = false;
            //_FormLayout = FormLayout.Default;
            //drowBorder = false;
            closeOnEscape = false;
            
            InitializeComponent();
        }
        public FormBase(IStyle style)
        {
            closeOnEscape = false;

            InitializeComponent();
            this.SetStyleLayout(style);
        }
        

        #region Members

        protected StyleGuide StyleGuideBase;
        private bool closeOnEscape;
        //internal bool drowBorder;
        //private FormLayout _FormLayout;
        //private Nistec.WinForms.McResize ctlResize;
        //protected bool showResizing;

        #endregion

        #region AsyncInvok

        //protected override void OnClosed(EventArgs e)
        //{
        //    base.OnClosed(e);
        //    _isClosed = true;
        //}


        //protected ThreadState AsyncState
        //{
        //    get
        //    {
        //        if (_AsyncThread != null)
        //        {
        //            return _AsyncThread.ThreadState;
        //        }
        //        return ThreadState.Unstarted;
        //    }
        //}

        //private Thread _AsyncThread;
        //private bool _isClosed = false;
        //private bool _isBackground = false;
        //private bool _isComplited = false;
        //private delegate void _AsyncDelegate(object[] arg);

        //protected Thread AsyncCurrentThread
        //{
        //    get
        //    {
        //        return _AsyncThread;
        //    }
        //}
        //protected bool AsyncIsComplited
        //{
        //    get
        //    {
        //        return _isComplited;
        //    }
        //}
        //protected virtual void AsyncAbort()
        //{
        //    if (_AsyncThread != null)
        //    {
        //        _AsyncThread.Abort();
        //    }
        //}

        //protected virtual void AsyncStart()
        //{
        //    AsyncStart(false);
        //}

        //protected virtual void AsyncStart(bool isBackground)
        //{
        //    if (_AsyncThread != null && _AsyncThread.IsAlive)
        //    {
        //        return;
        //    }
        //    _isBackground = isBackground;
        //    _AsyncThread = new Thread(new ThreadStart(AsyncWorker));
        //    _AsyncThread.IsBackground = isBackground;
        //    _AsyncThread.Start();
        //}

        //protected virtual void AsyncWorker()
        //{
        //    //for (int i = 0; i<100; ++i)
        //    //{
        //    //	AsyncInvoke(i);
        //    //	Thread.Sleep(sleepTime);
        //    //}
        //}

        //protected virtual void AsyncInvoke(object[] arg)
        //{

        //    if (_isClosed && !_isBackground)
        //    {
        //        _AsyncThread.Abort();
        //        return;
        //    }
        //    if (!this.IsHandleCreated && !_isBackground)
        //        return;

        //    // Check if we need to call BeginInvoke.
        //    if (this.InvokeRequired)
        //    {
        //        // Pass the same function to BeginInvoke,
        //        // but the call would come on the correct
        //        // thread and InvokeRequired will be false.
        //        this.BeginInvoke(new _AsyncDelegate(AsyncInvoke),
        //            new object[] { arg });

        //        return;
        //    }
        //    else
        //    {
        //        AsyncComplited(arg);
        //    }
        //}

        // protected virtual void AsyncComplited(object[] arg)
        //{
        //    _isComplited = true;
        //}
        #endregion

        #region Async Command
        //private AsyncCommand acmd;

        //[Browsable(false)]
        //protected AsyncCommand AsyncCommand
        //{
        //    get { return acmd; }
        //}
        //[Browsable(false)]
        //public AsyncState AsyncCommandState
        //{
        //    get
        //    {
        //        if (acmd == null)
        //            return AsyncState.None;
        //        return acmd.AsyncState;
        //    }
        //}
        //public void AsyncCmd(IDbConnection cnn, string sql, string tableName)
        //{
        //    AsyncCmd(cnn, sql, tableName, AsyncProgressLevel.None);
        //}
        //public void AsyncCmd(IDbConnection cnn, string sql, string tableName, AsyncProgressLevel level)
        //{
        //    if (AsyncCommandState == AsyncState.Started)
        //        return;
           
        //    _currentAsync = 2;
        //    if (acmd != null)
        //    {
        //        UnWireAsyncCmd();
        //        acmd.Dispose();
        //        acmd = null;
        //    }
        //    acmd = new AsyncCommand(cnn, level);
        //    acmd.AsyncCompleted+=new AsyncDataResultEventHandler(acmd_ExecutingResultEvent);
        //    acmd.AsyncProgress +=new AsyncProgressEventHandler(async_ExecutingProgressEvent);
        //    acmd.AsyncCancelExecuting += new EventHandler(async_CancelExecutingEvent);
        //    acmd.AsyncBeginInvoke(sql, tableName);
        //}

        //private void UnWireAsyncCmd()
        //{
        //    if (acmd == null) return;
        //    acmd.AsyncCompleted -= new AsyncDataResultEventHandler(acmd_ExecutingResultEvent);
        //    acmd.AsyncProgress -= new AsyncProgressEventHandler(async_ExecutingProgressEvent);
        //    acmd.AsyncCancelExecuting -= new EventHandler(async_CancelExecutingEvent);
        //    _currentAsync = 0;
        //}

        //void acmd_ExecutingResultEvent(object sender, AsyncDataResultEventArgs e)
        //{
        //    OnAsyncCommandCompleted(e);
        //}

        //protected virtual void OnAsyncCommandCompleted(AsyncDataResultEventArgs e)
        //{
        //    UnWireAsyncCmd();
        //}  
        #endregion

        #region Async Invoke

        private AsyncInvoke async;
        private int _currentAsync;//0=none,1=invoke,2=cmd
     
        [Browsable(false)]
        protected AsyncInvoke AsyncInvoke
        {
            get { return async; }
        }
        [Browsable(false)]
        public AsyncState AsyncState
        {
            get 
            {
                if (async == null)
                    return  AsyncState.None;
                return async.AsyncState; 
            }
        }
        public void AsyncBeginInvoke(object args)
        {
            AsyncBeginInvoke(args, AsyncProgressLevel.None);
        }
        public void AsyncBeginInvoke(object args, AsyncProgressLevel level)
        {
            if (AsyncState == AsyncState.Started)
                return;
            if (async != null)
            {
                UnWireAsyncInvoke();
                async.Dispose();
                async = null;
            }
            _currentAsync = 1;
            async = new AsyncInvoke(level);
            async.AsyncCompleted += new AsyncCallEventHandler(async_ExecutingResultEvent);
            async.AsyncProgress += new AsyncProgressEventHandler(async_ExecutingProgressEvent);
            async.AsyncExecutingWorker+= new AsyncCallEventHandler(async_AsyncExecutingWorker);
            async.AsyncCancelExecuting += new EventHandler(async_CancelExecutingEvent);
            async.AsyncBeginInvoke(args);
        }

        protected void AsyncDispose()
        {
            if (async != null)
            {
                UnWireAsyncInvoke();
                async.Dispose();
                async = null;
           }
           //if (acmd != null)
           //{
           //    UnWireAsyncCmd();
           //    acmd.Dispose();
           //    acmd = null;
           // }
        }

        private void UnWireAsyncInvoke()
         {
            if (async == null) return;
            async.AsyncCompleted -= new AsyncCallEventHandler(async_ExecutingResultEvent);
            async.AsyncProgress -= new AsyncProgressEventHandler(async_ExecutingProgressEvent);
            async.AsyncExecutingWorker -= new AsyncCallEventHandler(async_AsyncExecutingWorker);
            async.AsyncCancelExecuting -= new EventHandler(async_CancelExecutingEvent);
            _currentAsync = 0;
        }
        protected virtual void AsyncCancelExecution()
        {
            if (async != null && _currentAsync==1)
                async.StopCurrentExecution();
            //else if (acmd != null && _currentAsync==2)
            //    acmd.StopCurrentExecution();
        }
        void async_CancelExecutingEvent(object sender, EventArgs e)
        {
            OnAsyncCancelExecuting(e);
        }
        void async_AsyncExecutingWorker(object sender, AsyncCallEventArgs e)
        {
            OnAsyncExecutingWorker(e);
        }

        void async_ExecutingProgressEvent(object sender, AsyncProgressEventArgs e)
        {
            OnAsyncExecutingProgress(e);
        }

        void async_ExecutingResultEvent(object sender, AsyncCallEventArgs e)
        {
            OnAsyncCompleted(e);
        }

        protected virtual void OnAsyncCancelExecuting(EventArgs e)
        {
            if (_currentAsync == 1)
                UnWireAsyncInvoke();
            //else if (_currentAsync == 2)
            //    UnWireAsyncCmd();
        }

        protected virtual void OnAsyncExecutingWorker(AsyncCallEventArgs e)
        {

        }

        protected virtual void OnAsyncExecutingProgress(AsyncProgressEventArgs e)
        {

        }
        protected virtual void OnAsyncCompleted(AsyncCallEventArgs e)
        {
            UnWireAsyncInvoke();
        }

        #endregion

        #region Async background Invoke

        //private System.ComponentModel.BackgroundWorker backgroundWorker1;


        //// Set up the BackgroundWorker object by 
        //// attaching event handlers. 
        //private void InitializeBackgoundWorker()
        //{
        //    this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        //    backgroundWorker1.DoWork +=
        //        new DoWorkEventHandler(backgroundWorker1_DoWork);
        //    backgroundWorker1.RunWorkerCompleted +=
        //        new RunWorkerCompletedEventHandler(
        //    backgroundWorker1_RunWorkerCompleted);
        //    backgroundWorker1.ProgressChanged +=
        //        new ProgressChangedEventHandler(
        //    backgroundWorker1_ProgressChanged);
        //}

        //protected virtual void AsyncStart(object arg)
        //{
        //    InitializeBackgoundWorker();

        //      // Start the asynchronous operation.
        //    backgroundWorker1.RunWorkerAsync(arg);
        //}

        //// This is the method that does the actual work. For this
        //// example, it computes a Fibonacci number and
        //// reports progress as it does its work.
        //protected virtual object AsyncWorker(object arg, BackgroundWorker worker, DoWorkEventArgs e)
        //{
        //    return null;
        //}

        //protected virtual void AsyncProgress(ProgressChangedEventArgs e)
        //{

        //}

        //protected virtual void AsyncComplited(RunWorkerCompletedEventArgs e)
        //{

        //}

        //protected virtual void AsyncCancel()
        //{
        //    // Cancel the asynchronous operation.
        //    this.backgroundWorker1.CancelAsync();
        //}

        //// This event handler is where the actual,
        //// potentially time-consuming work is done.
        //private void backgroundWorker1_DoWork(object sender,
        //    DoWorkEventArgs e)
        //{
        //    // Get the BackgroundWorker that raised this event.
        //    BackgroundWorker worker = sender as BackgroundWorker;

        //    // Assign the result of the computation
        //    // to the Result property of the DoWorkEventArgs
        //    // object. This is will be available to the 
        //    // RunWorkerCompleted eventhandler.
        //    e.Result = AsyncWorker(e.Argument, worker, e);
        //}

        //// This event handler deals with the results of the
        //// background operation.
        //private void backgroundWorker1_RunWorkerCompleted(
        //    object sender, RunWorkerCompletedEventArgs e)
        //{
        //    AsyncComplited(e);
        //}

        //// This event handler updates the progress bar.
        //private void backgroundWorker1_ProgressChanged(object sender,
        //    ProgressChangedEventArgs e)
        //{
        //     AsyncProgress(e);
        // }
        #endregion

        #region internal Open

         private bool initialized = false;

        protected bool Initialized
        {
            get { return initialized; }
        }

        /// <summary>
        /// Initialize McDataForm
        /// </summary>
        /// <param name="args">args passing from Open methods</param>
        /// <returns>true if successed</returns>
        protected virtual bool Initialize(params object[] args)
        {
            return true;
        }

        /// <summary>
        /// Open McDataForm
        /// </summary>
        /// <param name="args">args passing Initialize methods</param>
        public virtual void Open(params object[] args)
        {
            bool res = Initialize(args);
            initialized = res;
            if (initialized) this.Show();
        }

        /// <summary>
        /// Open McDataForm as Dialog
        /// </summary>
        /// <param name="args">args passing Initialize methods</param>
        /// <returns></returns>
        public virtual DialogResult OpenDialog(params object[] args)
        {
            bool res = Initialize(args);
            initialized = res;
            if (!initialized) return DialogResult.No;
            return this.ShowDialog();
        }

        #endregion

        #region Properties

        public bool CloseOnEscape
        {
            get { return closeOnEscape; }
            set
            {
                closeOnEscape = value;
            }
        }

        //[DefaultValue(false)]
        //public bool DrowBorder
        //{
        //    get { return drowBorder; }
        //    set
        //    {
        //        drowBorder = value;
        //    }
        //}

        //public FormLayout FormLayout
        //{
        //    get { return _FormLayout; }
        //    set
        //    {
        //        if (_FormLayout != value)
        //        {
        //            _FormLayout = value;
        //            FormLayoutSettings();
        //            OnFormLayoutChanged(EventArgs.Empty); 
        //            Invalidate();
        //        }
        //    }
        //}

 

        public override Color BackColor
        {
            get
            {
                if (!DesignMode)
                {
                    if (useContolStyle)
                        return StyleLayout.DefaultLayout.FormColor;
                    if (this.StyleGuideBase != null && this.StyleGuideBase.StylePlan != Styles.None)
                        return this.StyleGuideBase.FormColor;
                }
                return base.BackColor;
            }
            set
            {
                if (useContolStyle)
                    base.BackColor = StyleLayout.DefaultLayout.FormColor;
                else if (this.StyleGuideBase != null && this.StyleGuideBase.StylePlan != Styles.None)
                    base.BackColor = this.StyleGuideBase.FormColor;
                else
                    base.BackColor = value;
            }
        }

        //		public override Color ForeColor
        //		{
        //			get
        //			{
        //				if(this.StyleGuideBase!=null)  
        //					return this.StyleGuideBase.ForeColor; 
        //				return base.ForeColor;
        //			}  
        //			set
        //			{
        //				if(this.StyleGuideBase!=null)  
        //					base.ForeColor=this.StyleGuideBase.ForeColor; 
        //				else
        //					base.ForeColor=value; 
        //			}
        //		}

        #endregion

        #region ILayout

        private ControlLayout m_ControlLayout = ControlLayout.VistaLayout;

        [Category("Style"), DefaultValue(ControlLayout.VistaLayout)]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                m_ControlLayout = value;
            }
        }

        private bool useContolStyle = false;
        protected bool defaultLayout = false;

        [Category("Style"), DefaultValue(false), RefreshProperties(RefreshProperties.All)]
        public bool StyleParam
        {
            get { return useContolStyle; }
            set
            {
                if (value != useContolStyle)
                {
                    useContolStyle = value;
                    if (useContolStyle)
                        base.BackColor = StyleLayout.DefaultLayout.FormColor;
                    else if (this.StyleGuideBase != null && this.StyleGuideBase.StylePlan != Styles.None)
                        base.BackColor = this.StyleGuideBase.FormColor;

                    this.Invalidate(true);
                    if (DesignMode)
                        this.Refresh();
                }
            }
        }
        //[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IStyle StylePainter
        {
            get { return StyleGuideBase; }
            set
            {
                if (value != null)
                {
                    if (value is StyleGuide)
                        StyleGuideBase = value as StyleGuide;
                    else
                        StyleGuideBase.SetStyleLayout(value.Layout);
                    this.Invalidate(true);
                }
            }
        }

        [Browsable(false)]
        public virtual IStyleLayout LayoutManager
        {
            get
            {
                if (!useContolStyle && StyleGuideBase != null)
                    return StyleGuideBase.Layout as IStyleLayout;
                else
                    return StyleLayout.DefaultLayout as IStyleLayout;
            }
        }

        protected virtual void SetDefaultLayout()
        {
            this.SetStyleLayout(StyleLayout.DefaultStylePlan);
            SetChildrenStyle();
        }

        public virtual void SetStyleLayout(IStyle style)
        {
            //if (style is StyleGuide)
            //{
            //    this.StylePainter = style;
            //}
            if (style != null)
            {
                this.SetStyleLayout(style.Layout);
            }
        }

        public virtual void SetStyleLayout(StyleLayout value)
        {
            this.StyleGuideBase.SetStyleLayout(value);
            BackColor = value.FormColor;//ColorBrush2Internal;
            ForeColor = value.ForeColorInternal;
            //SetChildrenStyle();
            Invalidate();
        }

        public virtual void SetStyleLayout(Styles value)
        {
            this.StyleGuideBase.SetStyleLayout(value);
            BackColor = this.StyleGuideBase.FormColor;//.ColorBrush2;
            ForeColor = this.StyleGuideBase.ForeColor;
            //SetChildrenStyle();
            Invalidate();
        }

        internal protected virtual void SetChildrenStyle()
        {
            foreach (Control c in this.Controls)
            {
                if (c is ILayout)
                {
                    ((ILayout)c).StylePainter = this.StylePainter;
                }
            }
        }

        private void StyleGuideBase_ColorStyleChanged(object sender, ColorStyleChangedEventArgs e)
        {
            ColorStyleChanged(e);
        }

        protected virtual void ColorStyleChanged(ColorStyleChangedEventArgs e)
        {
            //
        }

        private void StyleGuideBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            //
        }

        public Nistec.WinForms.PainterTypes PainterType
        {
            get
            {

                return Nistec.WinForms.PainterTypes.Guide;
            }
        }

        #endregion

        #region Keys

        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Escape)
            {
                OnEscapePressed();
                //return true;
            }
            else if (keyData == Keys.Enter)
            {
                OnEnterPressed();
                //return true;
            }
            else if (keyData == Keys.Insert)
            {
                OnInsertPressed();
                //return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        public virtual void OnEnterPressed()
        {
            // 
        }

        public virtual void OnEscapePressed()
        {
            if (closeOnEscape)
                base.Close();
        }

        public virtual void OnInsertPressed()
        {
            // 
        }

        #endregion

        #region resize

        //protected virtual bool ShouldShowSizingGrip()
        //{
        //    return showResizing && this.FormBorderStyle != FormBorderStyle.None;
        //    //return showResizing && this.ControlLayout!= ControlLayout.System;
        //}
        //protected virtual void ShowSizingGrip(bool show)
        //{
        //    if (this.ctlResize == null)
        //        return;
        //    showResizing = show;
        //    this.ctlResize.Visible = show;
        //    if (show)
        //    {
        //        this.ctlResize.SetLocation();
        //    }
        //}

        protected void SetPadding(int value)
        {
            this.Padding = new Padding(value, 0, value, value);
        }

        //public new SizeGripStyle SizeGripStyle
        //{
        //    get { return base.SizeGripStyle; }

        //    set
        //    {
        //        if (base.SizeGripStyle != value)
        //        {
        //            base.SizeGripStyle = value;
        //            switch (value)
        //            {
        //                case SizeGripStyle.Auto:
        //                    ShowSizingGrip(ShouldShowSizingGrip());
        //                    break;
        //                case SizeGripStyle.Hide:
        //                    ShowSizingGrip( false); 
        //                    break;
        //                case SizeGripStyle.Show:
        //                    ShowSizingGrip(true);//ShouldShowSizingGrip());
        //                    //this.ctlResize.Visible = true;
        //                    //this.ctlResize.SetLocation();
        //                    break;
        //            }
        //        }
        //    }
        //}

        //public bool SizingGrip
        //{
        //    get { return showResizing; }
        //    set 
        //    {
        //        if (showResizing != value)
        //        {
        //            showResizing = value;
        //            this.ctlResize.Visible = value;
        //            if (showResizing)
        //            {
        //                this.ctlResize.SetLocation();
        //            }
        //        }
        //    }
        //}

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        ////    Rectangle rect = this.ClientRectangle;
        ////    this.ctlResize.Location = new Point(rect.Width - ctlResize.Width, rect.Height - ctlResize.Height);
        //}

 
 
        //private void FormLayoutSettings()
        //{

        //    switch (FormLayout)
        //    {
        //        case FormLayout.Visual:
        //        case FormLayout.XpLayout:
        //        case FormLayout.Flat:
        //        case FormLayout.VistaLayout:
        //            break;
        //        case FormLayout.FlatDialog:
        //        case FormLayout.XpDialog:
        //        case FormLayout.VisualDialog:
        //        case FormLayout.VistaDialog:
        //            ShowSizingGrip(false);
        //            break;
        //        case FormLayout.XpSizeable:
        //        case FormLayout.VistaSizeable:
        //        case FormLayout.VisualSizeable:
        //        case FormLayout.FlatSizeable:
        //            ShowSizingGrip(true);
        //            break;
        //        default:
        //            ShowSizingGrip(false);
        //            break;
        //    }
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    if (drowBorder)
        //    {
        //        Rectangle rect = ClientRectangle;
        //        //rect.Width--;
        //        //rect.Height--;
        //        //rect.Inflate(-1, -1);
        //        Rectangle rect1 = new Rectangle(rect.X, rect.Y, rect.Width-1, rect.Height-1);
        //        Rectangle rect2 = new Rectangle(rect.X+2, rect.Y+2, rect.Width-5, rect.Height-5);
        //        using (Pen pen = LayoutManager.GetPenBorder())
        //        {
        //            //pen.Width = 2;
        //            e.Graphics.DrawRectangle(pen, rect1);
        //            //e.Graphics.DrawRectangle(pen, rect2);
        //        }
        //        //e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 3), rect);
        //    }
        //}
        #endregion

        #region Perms

        //private PermsLevel _PermsLevel = PermsLevel.FullControl;

        //[Browsable(false)]
        //public PermsLevel PermsLevel
        //{
        //    get { return _PermsLevel; }
        //}

        //protected virtual void SetPermissionsLevel(IPerms perms,PermsLevel defaultLevel)
        //{
        //    _PermsLevel = perms.GetPermsLevel(this.Name, defaultLevel);
        //    OnPermissionsSettings(this);
        //}

        //public virtual void SetPermissionsLevel(PermsLevel level)
        //{
        //    _PermsLevel=level;
        //    OnPermissionsSettings(this);
        //}

        //protected virtual void OnPermissionsSettings(Control cc)
        //{
        //    foreach (Control c in cc.Controls)
        //    {
        //        if (c is ContainerControl || c is McPanel || c is McGroupBox || c is McTabControl)
        //            //if (c is ContainerControl || c is McGroupBox)
        //        {
        //           if (_PermsLevel == PermsLevel.DenyAll)
        //            {
        //                if (!(c is McNavBar))//-- || c is McTaskBar || c is McTaskPanel))
        //                    c.Enabled = false;
        //            }
        //            else
        //                OnPermissionsSettings(c);
        //        }
        //        else if (c is IBind)
        //        {
        //            switch (_PermsLevel)
        //            {
        //                case PermsLevel.DenyAll:
        //                    c.Enabled = false;
        //                    break;
        //                case PermsLevel.EditOnly:
        //                case PermsLevel.ReadOnly:
        //                    ((IBind)c).ReadOnly = true;
        //                    break;
        //                case PermsLevel.FullControl:
        //                    break;
        //            }
        //        }
        //    }
        //}
     
       #endregion

        
    }
}