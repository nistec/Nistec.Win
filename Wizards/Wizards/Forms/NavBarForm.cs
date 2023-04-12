using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MControl.Data;
using MControl.WinForms;

namespace MControl.Wizards.Forms
{
    public partial class NavBarForm : MControl.WinForms.McDataForm
    {
        public NavBarForm()
        {
            InitializeComponent();
        }
        #region Async Dal
        protected MControl.Data.IDalBase DalBase;
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

        public virtual void SetDataSource(DataTable dt, string mappingName)
        {
            _DataSource = dt;
            _MappingName = mappingName;
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
            OnStartLoading();
            DalBase = dalBase;
            Connection = dalBase.IConnection;
            DalAsync = MControl.Data.Common.DalAsyncFactory.Create(dalBase);
            DalAsync.AsyncCompleted += new EventHandler(Async_AsyncCompleted);
            DalAsync.AsyncStatusChanged += new EventHandler(DalAsync_AsyncStatusChanged);
            //OnStartLoading();
        }

        void DalAsync_AsyncStatusChanged(object sender, EventArgs e)
        {
            OnAsyncStatusChanged(e, DalAsync.AsyncStatus);
        }

        protected virtual void OnAsyncStatusChanged(EventArgs e, STATUS status)
        {
            if (IsHandleCreated)
            {

                this.Caption.Invoke(StatusDelegate, status.Display);
                this.Caption.Invalidate();
            }
            else
                this.Caption.SubText = status.Display;
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
                if (!this.IsHandleCreated) return;
                AsyncDataFill del = new AsyncDataFill(DalAsync.AsyncFillDataSource);
                this.Invoke(del, DalAsync.AsyncExecuteEnd(result));

            }
            catch (Exception ex)
            {
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
            this.ctlNavBar.Init(_DataSource, "", true, Connection);
            OnEndLoading();
        }

        #endregion

        #region Invoke status

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    if (!DesignMode)
        //    {
        //        OnStartLoading();
        //    }
        //}

        private delegate void UpdateStatusDelegate(string text);
        private UpdateStatusDelegate StatusDelegate;
        private Thread m_Thread;
        private bool completed = false;

        protected virtual void OnStartLoading()
        {
            this.Show();
            this.ctlNavBar.StartProgress();
            // initialisation the instance of UpdateLabelDelegate 
            StatusDelegate = new UpdateStatusDelegate(this.OnStatusUpdate);

            // creation the  thread ThreadProc 
            m_Thread = new Thread(new ThreadStart(this.ThreadProc));
            m_Thread.Name = "Thread Proc";
            m_Thread.Start();
        }

        private void ThreadProc()
        {
            while (/*DalAsync.AsyncIsExecuting)*/ !completed)
            {
                Thread.Sleep(500);
            }
        }

        // Do not directly call this method.   
        // This method is designed to use only as a delegate target that is invoke on the thread
        protected virtual void OnStatusUpdate(string text)
        {
            this.Caption.SubText = text;
        }
        protected virtual void OnEndLoading()
        {
            completed = true;
            this.ctlNavBar.StopProgress();
        }
        #endregion

     }
}