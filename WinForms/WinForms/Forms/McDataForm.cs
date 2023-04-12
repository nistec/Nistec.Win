using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Security.Permissions;


using System.Threading;

using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;



namespace Nistec.WinForms
{
    public partial class McDataForm : McForm
    {
        public McDataForm()
        {
            showNavBar = true;
            InitializeComponent();
        }
        public McDataForm(IStyle style):base(style)
        {
            showNavBar = true;
            InitializeComponent();
        }

        #region Private Members

        protected McNavBar ctlNavBar;
        protected OpenMode openMode = OpenMode.Default;
        private bool showNavBar;
        #endregion

        #region internal Open

        private bool initialized = false;

        protected new bool Initialized
        {
            get { return initialized; }
        }

        /// <summary>
        /// Initialize McDataForm
        /// </summary>
        /// <param name="args">args passing from Open methods</param>
        /// <returns>true if successed</returns>
        protected override bool Initialize(params object[] args)
        {
            return base.Initialize(args);
        }

        /// <summary>
        /// Open McDataForm
        /// </summary>
        /// <param name="mode">OpenMode</param>
        /// <param name="args">args passing from Initilaidze methods</param>
        /// </param>
        public virtual void Open(OpenMode mode, params object[] args)
        {
            if (OpenInternal(mode, null, null, args))
            {
                this.Show();
            }
        }

        /// <summary>
        /// Open McDataForm in Where Mode
        /// </summary>
        /// <param name="mode">OpenMode</param>
        /// <param name="filter">
        /// string filter 
        /// In Filter Mode : example: "CustomerID > 50"
        /// In Where Mode  : if sortKey="CustomerID" then the filter is "50" so navigatore move to "CustomerID=50"
        /// </param>
        /// <param name="sortKey">Field key for sort</param>
        /// <param name="args">args passing from Initilaidze methods</param>
        public virtual void Open(OpenMode mode, object filter, object sortKey, params object[] args)
        {
            if (OpenInternal(mode, filter, sortKey, args))
            {
                this.Show();
            }
        }

        /// <summary>
        /// Open McDataForm as Dialog
        /// </summary>
        /// <param name="mode">OpenMode</param>
        /// <param name="args">args passing from Initilaidze methods</param>
        /// <returns></returns>
        public virtual DialogResult OpenDialog(OpenMode mode, params object[] args)
        {
            if (!OpenInternal(mode, null, null, args)) return DialogResult.No;
            return this.ShowDialog();
        }


        /// <summary>
        /// Open McDataForm as Dialog
        /// </summary>
        /// <param name="mode">OpenMode</param>
        /// <param name="filter">
        /// string filter 
        /// In Filter Mode : example: "CustomerID > 50"
        /// In Where Mode  : if sortKey="CustomerID" then the filter is "50" so navigatore move to "CustomerID=50"
        /// </param>
        /// <param name="sortKey">Field key for sort</param>
        /// <param name="args">args passing from Initilaidze methods</param>
        /// <returns></returns>
        public virtual DialogResult OpenDialog(OpenMode mode, object filter, object sortKey, params object[] args)
        {
            if (!OpenInternal(mode, filter, sortKey, args)) return DialogResult.No;
            return this.ShowDialog();
        }


        private bool OpenInternal(OpenMode mode, object filter, object sortKey, params object[] args)
        {
            try
            {
                openMode = mode;
                bool res = Initialize(args);
                initialized = res;
                if (!initialized) return false;

                switch (mode)
                {
                    case OpenMode.New:
                        this.ctlNavBar.MoveNew();
                        break;
                    case OpenMode.Filter:
                        if (sortKey != null)
                        {
                            this.ctlNavBar.Sort = sortKey.ToString();
                        }
                        if (filter != null)
                        {
                            this.ctlNavBar.RowFilter = filter.ToString();
                            this.ctlNavBar.SetFilter();
                        }
                        break;
                    case OpenMode.Where:
                        if (sortKey != null)
                        {
                            this.ctlNavBar.Sort = sortKey.ToString();
                        }
                        if (filter != null && this.ctlNavBar.Sort.Length > 0)
                        {
                            this.ctlNavBar.MoveTo(filter);
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Form Event

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.ctlNavBar != null)
                return this.ctlNavBar.ProcessNavKey(ref msg, keyData);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        //protected override void OnMouseWheel(MouseEventArgs e)
        //{
        //    base.OnMouseWheel(e);
        //    if (this.ctlNavBar != null)
        //        this.ctlNavBar.PerformMouseWheel(e);
        //}

        protected virtual void OnDirty(System.EventArgs e) { }

        protected virtual void OnPositionChanged(System.EventArgs e) { }

        protected virtual void OnCurrentChanged(System.EventArgs e) { }

        //		protected virtual void OnCustomValidating(CancelEventArgs e) {}
        //
        //		protected virtual void OnCustomDeleting(CancelEventArgs e) {}

        protected virtual void OnRowUpdating(RowUpdatingEventArgs e) { }

        protected virtual void OnRowUpdated(System.Data.Common.RowUpdatedEventArgs e) { }

        protected virtual void OnRowDeleting(DataRowChangeEventArgs e) { }

        protected virtual void OnRowDeleted(DataRowChangeEventArgs e) { }

        protected virtual void OnItemChanged(ItemChangedEventArgs e) { }

        protected virtual void OnNavBarUpdated(Nistec.WinForms.NavBarUpdatedEventArgs e) { }

        protected virtual void OnNavBarUpdating(Nistec.WinForms.NavBarUpdatingEventArgs e) { }

        protected virtual void OnNavBarRowNew(System.Data.DataTableNewRowEventArgs e) { }
  
        #endregion

        #region NavBar Events

        private void ctlNavBar_Dirty(object sender, System.EventArgs e)
        {
            OnDirty(e);
        }

        private void ctlNavBar_BindCurrentChanged(object sender, System.EventArgs e)
        {
            OnCurrentChanged(e);
        }

        private void ctlNavBar_BindPositionChanged(object sender, System.EventArgs e)
        {
            OnPositionChanged(e);
        }

        //		private void ctlNavBar_CustomValidating(object sender, CancelEventArgs e)
        //		{ 
        //          OnCustomValidating(e);
        //		}
        //
        //		private void ctlNavBar_CustomDeleting(object sender, CancelEventArgs e)
        //		{
        //          OnCustomValidating(e);
        //		}

        private void ctlNavBar_RowUpdating(object sender, System.Data.Common.RowUpdatingEventArgs e)
        {
            OnRowUpdating(e);
        }

        private void ctlNavBar_RowUpdated(object sender, System.Data.Common.RowUpdatedEventArgs e)
        {
            OnRowUpdated(e);
        }

        private void ctlNavBar_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            OnRowDeleting(e);
        }

        private void ctlNavBar_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            OnRowDeleted(e);
        }

        private void ctlNavBar_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            OnItemChanged(e);
        }

        private void ctlNavBar_NavBarUpdated(object sender, Nistec.WinForms.NavBarUpdatedEventArgs e)
        {
            OnNavBarUpdated(e);
        }

        private void ctlNavBar_NavBarUpdating(object sender, Nistec.WinForms.NavBarUpdatingEventArgs e)
        {
            OnNavBarUpdating(e);
        }

        void ctlNavBar_RowNew(object sender, System.Data.DataTableNewRowEventArgs e)
        {
            OnNavBarRowNew(e);
        }

        #endregion

        #region Properties

        [Browsable(false)]
        public McNavBar NavBar
        {
            get { return this.ctlNavBar; }
        }

        public bool ShowNavBar
        {
            get { return showNavBar; }
            set 
            {
                showNavBar = value;
                this.ctlNavBar.Visible = value; 
            }
        }

        //protected override bool ShouldShowSizingGrip()
        //{
        //    if (showNavBar)
        //        return false;
        //    return base.ShouldShowSizingGrip();
        //}
        #endregion
    }
}