using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Nistec.Drawing;
using Nistec.Data;
using Nistec.Data.Advanced;
using Nistec.Data.Factory;
using Nistec.Win;

namespace Nistec.WinForms
{

    public enum NavStatus
    {
        Default = 0,
        Edit = 1,
        New = 2,
        Delete = 3,
        ReadOnly = 4,
        UnBind = 5
    }


    [ProvideProperty("DataField", typeof(Control))]//,ProvideProperty("DataFieldProperty",typeof(Control))]
    [ToolboxItem(false)]
    public abstract class McNavBase : McPanel, ISupportInitialize, IExtenderProvider,IDataSource
    {

        #region Members

        private NavStatus navStatus = NavStatus.Default;
        //private NavStatus oldNavStatus=NavStatus.Default;
        private System.ComponentModel.IContainer components = null;

        private Nistec.WinForms.McNavigatore dataNavigatore1;
        private Nistec.WinForms.McTextBox lblNavigator;
        private Nistec.WinForms.McNavChanges ctlNavChanges;
        private Nistec.WinForms.McPictureBox ctlPictureBox;

        private CurrencyManager _BindManager;
        private object _DataSource;
        private string _DataMember;

        private int _SelectedIndex;
        private int _PrevIndex;
        //private int _Count;
        //private bool isCreated;
        private DataView _DataView;

        internal DataSet _DataSet;
        internal DataTable _DataMaster;
        internal string commandSelect;
        internal bool _useViewManager;

        private string _MessageSaveChanges;
        private string _MessageDelete;
        private string _MessageTitle;
        private bool _ShowMessageSaveChanges;
        private bool _ShowErrorMessage;

        private bool _AllowNew;
        private bool _AllowEdit;
        private bool _AllowDelete;
        private string _RowFilter;
        private string _Sort;
        private bool _ReadOnly;
        private bool _LocalOnly;

        private bool _IsDirty;
        private bool _IsBound;
        private bool shouldRefresh;

        private Form form;
        private McResize ctlResize;
        private bool _SizingGrip;
        public static string DefaultSaveMessage { get { return "Save Changes ?"; } }
        public static string DefaultDeleteMessage { get { return "Delete ?"; } }

        public enum MoveOperator
        {
            MoveDelta = 0,
            MoveTo = 1,
            MoveFirst = 2,
            MoveLast = 3,
        }
        #endregion

        #region Event Members

        // Events
        [Category("PropertyChanged"), Description("On Error Ocurred")]
        public event ErrorOcurredEventHandler ErrorOcurred;

        [Category("PropertyChanged"), Description("On DataBinding Master Changed")]
        public event EventHandler BindingChanged;

        [Category("PropertyChanged"), Description("ListControlOnDataSourceChanged")]
        public event EventHandler DataSourceChanged;

        [Category("PropertyChanged"), Description("ListControlOnDisplayMemberChanged")]
        public event EventHandler DataMemberChanged;

        [Category("Data"), Description("When Data value Changed")]
        public event EventHandler Dirty;

        [Category("Data"), Description("After Update changes include navPosition and navStatus")]
        public event NavBarUpdatedEventHandler NavBarUpdated;

        [Category("Data"), Description("Before Update changes or deleting, include navPosition and navStatus,You can cancel updating event")]
        public event NavBarUpdatingEventHandler NavBarUpdating;

        [Category("Data"), Description("When Accept Changes ")]
        public event EventHandler AcceptPressed;

        [Category("Data"), Description("When Reject Changes ")]
        public event EventHandler RejectPressed;

        //		/// <summary>
        //		/// Custom validating
        //		/// </summary>
        //		[Category("Data"), Description("When Custom validating , User can cancel update")]
        //		public event System.ComponentModel.CancelEventHandler CustomValidating;
        //		/// <summary>
        //		/// Custom Deleting
        //		/// </summary>
        //		[Category("Data"), Description("When custom deleting ")]
        //		public event System.ComponentModel.CancelEventHandler CustomDeleting;

        /// <summary>
        /// OnColumnChanging event
        /// </summary>
        [Category("Data"), Description("When Data Column Changing")]
        public event DataColumnChangeEventHandler ColumnChanging;
        /// <summary>
        /// OnColumnChanged event
        /// </summary>
        [Category("Data"), Description("When Data Column Changed")]
        public event DataColumnChangeEventHandler ColumnChanged;
        /// <summary>
        /// OnRowChanging event
        /// </summary>
        [Category("Data"), Description("When Data Row Changing")]
        public event DataRowChangeEventHandler RowChanging;
        /// <summary>
        /// OnRowChanged event
        /// </summary>
        [Category("Data"), Description("When Data Row Changed")]
        public event DataRowChangeEventHandler RowChanged;
        /// <summary>
        /// OnRowDeleting event
        /// </summary>
        [Category("Data"), Description("When Data Row Deleting")]
        public event DataRowChangeEventHandler RowDeleting;
        /// <summary>
        /// OnRowDeleted event
        /// </summary>
        [Category("Data"), Description("When Data Row Deleted")]
        public event DataRowChangeEventHandler RowDeleted;

        /// <summary>
        /// OnRowNew event
        /// </summary>
        [Category("Data"), Description("When Data Row new inserted")]
        public event DataTableNewRowEventHandler RowNew;

        /// <summary>
        /// OnBindingPositionChanged
        /// </summary>
        [Category("BindManager"), Description("When Binding Position Changed")]
        public event EventHandler BindPositionChanged;
        /// <summary>
        /// OnBindingPositionChanged
        /// </summary>
        [Category("BindManager"), Description("When BindingCurrent Changed")]
        public event EventHandler BindCurrentChanged;
        /// <summary>
        /// On Data Item Changed
        /// </summary>
        [Category("BindManager"), Description("When Binding Item Changed")]
        public event ItemChangedEventHandler ItemChanged;
        /// <summary>
        /// On MetaData Changed event
        /// </summary>
        [Category("BindManager"), Description("When MetaData Changed")]
        public event EventHandler MetaDataChanged;

        /// <summary>
        /// OnDataListChanged
        /// </summary>
        public event ListChangedEventHandler DataListChanged;

        //[Category("Behavior"), Description("ComboBoxOnErrorOcurred")]
        //public event ErrorOcurredEventHandler ErrorOcurred;

        #endregion

        #region Constructors


        public McNavBase()
        {
            this.fieldLists = new Hashtable();
            this.bindList = new ArrayList();
            base.autoChildrenStyle = true;
            shouldRefresh = false;
            _SizingGrip = false;
            _MessageTitle = "";
            _ShowMessageSaveChanges = true;
            _ShowErrorMessage = true;
            _AllowNew = true;
            _AllowEdit = true;
            _AllowDelete = true;
            _IsDirty = false;
            _RowFilter = "";
            _Sort = "";
            _DataSource = null;
            _DataMember = "";
            _ReadOnly = false;
            _LocalOnly = false;
            _IsBound = false;
            //isCreated=false;
            _useViewManager = false;
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            //this.dataNavigatore1.m_netFram = true;
            //this.lblNavigator.m_netFram = true;

            this.ctlPictureBox.Image = ResourceUtil.LoadImage(Global.ImagesPath + "navDefault.gif");

            //this.ctlResize = new McResize();
            //this.ctlResize.Location = new Point(this.Right - 22, this.Bottom - 22);
            //this.ctlResize.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            //this.Controls.Add(this.ctlResize);


        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnClosed();
                if (this.fieldLists != null)
                {
                    UnBindControls(false);

                    this.fieldLists.Clear();
                    this.fieldLists = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            form = this.FindForm();

            if (form != null)
            {
                form.Closing += new CancelEventHandler(form_Closing);
                if (ReadOnly)
                    SetReadOnly(true);
            }
            this.lblNavigator.Focus();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (form != null)
            {
                form.Closing -= new CancelEventHandler(form_Closing);
            }

        }

        private void form_Closing(object sender, CancelEventArgs e)
        {
            OnClosing(e);
        }

        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataNavigatore1 = new Nistec.WinForms.McNavigatore();
            this.lblNavigator = new Nistec.WinForms.McTextBox();
            this.ctlNavChanges = new Nistec.WinForms.McNavChanges();
            this.ctlPictureBox = new Nistec.WinForms.McPictureBox();
            this.ctlResize = new McResize();
            this.SuspendLayout();
            // 
            // dataNavigatore1
            // 
            this.dataNavigatore1.Location = new System.Drawing.Point(17, 17);
            this.dataNavigatore1.Name = "dataNavigatore1";
            this.dataNavigatore1.ShowDelete = true;
            this.dataNavigatore1.ShowNew = true;
            //this.dataNavigatore1.Size = new System.Drawing.Size(122, 20);
            this.dataNavigatore1.TabIndex = 0;
            this.dataNavigatore1.TabStop = false;
            this.dataNavigatore1.ClickDelete += new Nistec.WinForms.ButtonClickEventHandler(this.BindNavigatore_ClickDelete);
            this.dataNavigatore1.ClickFirst += new Nistec.WinForms.ButtonClickEventHandler(this.BindNavigatore_ClickFirst);
            this.dataNavigatore1.ClickPrev += new Nistec.WinForms.ButtonClickEventHandler(this.BindNavigatore_ClickPrev);
            this.dataNavigatore1.ClickLast += new Nistec.WinForms.ButtonClickEventHandler(this.BindNavigatore_ClickLast);
            this.dataNavigatore1.ClickNew += new Nistec.WinForms.ButtonClickEventHandler(this.BindNavigatore_ClickNew);
            this.dataNavigatore1.ClickNext += new Nistec.WinForms.ButtonClickEventHandler(this.BindNavigatore_ClickNext);
            // 
            // lblNavigator
            // 
            this.lblNavigator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNavigator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblNavigator.Location = new System.Drawing.Point(265, 17);
            this.lblNavigator.Name = "lblNavigator";
            //this.lblNavigator.Size = new System.Drawing.Size(96, 13);
            this.lblNavigator.TabIndex = 0;
            this.lblNavigator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lblNavigator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lblNavigator_KeyDown);
            this.lblNavigator.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lblNavigator_KeyPress);
            this.lblNavigator.Validated += new System.EventHandler(this.lblNavigator_Validated);
            this.lblNavigator.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lblNavigator_MouseWheel);
            // 
            // ctlNavChanges
            // 
            this.ctlNavChanges.Location = new System.Drawing.Point(376, 17);
            this.ctlNavChanges.Name = "ctlNavChanges";
            //this.ctlNavChanges.Size = new System.Drawing.Size(42, 20);
            this.ctlNavChanges.TabIndex = 1;
            this.ctlNavChanges.Visible = false;
            this.ctlNavChanges.TabStop = false;
            this.ctlNavChanges.ClickAccept += new ButtonClickEventHandler(ctlNavChanges_ClickAccept);
            this.ctlNavChanges.ClickReject += new ButtonClickEventHandler(ctlNavChanges_ClickReject);
            // 
            // ctlPictureBox
            // 
            this.ctlPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this.ctlPictureBox.Location = new System.Drawing.Point(150, 17);
            this.ctlPictureBox.BackColor = Color.Transparent;
            this.ctlPictureBox.Name = "ctlPictureBox";
            this.ctlPictureBox.ReadOnly = false;
            //this.ctlPictureBox.Size = new System.Drawing.Size(20, 20);
            this.ctlPictureBox.TabIndex = 2;
            //
            //
            //
            this.ctlResize.BackColor = Color.Transparent;
            //this.ctlResize.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctlResize.Location = new Point(this.Width - 22, this.Height - 22);
            this.ctlResize.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            // 
            // McNavBase
            //
            this.ControlLayout = ControlLayout.Visual;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dataNavigatore1);
            this.Controls.Add(this.lblNavigator);
            this.Controls.Add(this.ctlNavChanges);
            this.Controls.Add(this.ctlPictureBox);
            this.Controls.Add(this.ctlResize);
            //this.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DockPadding.All = 2;
            //this.Location = new System.Drawing.Point(0, 126);
            //this.Size = new System.Drawing.Size(272, 24);
            this.ResumeLayout(false);

        }
        #endregion

        #region override base

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            SetLocation();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetLocation();
        }

        #endregion

        #region OnClosing

        /// <summary>
        /// OnClosing Event
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnClosing(CancelEventArgs e)
        {
            if (this.DataSource == null || this.Count == 0)
            {
                return;
            }
            //base.OnClosing (e);
            ChangesStatus res = ChangesState;
            if (res == ChangesStatus.Stop)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// On Closed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClosed()//(EventArgs e)
        {
            //base.OnClosed (e);
            if (_DataView != null)
            {
                _DataView.ListChanged -= new ListChangedEventHandler(_DataView_ListChanged);
            }

            if (_DataMaster != null)
            {
                UnWireDataMaster();
            }
        }


        #endregion

        #region DataManger

        /// <summary>
        /// Set data binding or re binding
        /// </summary>
        /// <param name="dataSource">dataSource</param>
        /// <param name="dataMember">dataMember</param>
        public void SetDataBinding(object dataSource, string dataMember)
        {
            UnBindControls(true);
            this.Set_ListManager(dataSource, dataMember, true);
            BindControls();
        }

        private void Set_ListManager(object newDataSource, string newDataMember, bool force)
        {
            bool flag1 = _DataSource != newDataSource;
            bool flag2 = !_DataMember.Equals(newDataMember);
            _IsBound = false;
            if ((force || flag1) || flag2)
            {
                if (_DataSource is IComponent)
                {
                    ((IComponent)DataSource).Disposed -= new EventHandler(this.DataSourceDisposed);
                }
                _DataSource = newDataSource;
                _DataMember = newDataMember;
                if (_DataSource is IComponent)
                {
                    ((IComponent)_DataSource).Disposed += new EventHandler(this.DataSourceDisposed);
                }
                CurrencyManager manager1 = null;
                if (_DataSource is DataTable || _DataSource is DataView)
                {
                    _DataMember = "";
                }
                if (((newDataSource != null) && (this.BindingContext != null)) && (newDataSource != Convert.DBNull))
                {
                    manager1 = (CurrencyManager)this.BindingContext[newDataSource, newDataMember];
                }
                if (_BindManager != manager1)
                {
                    if (_BindManager != null)
                    {
                        UnWireDataSource();

                        if (_DataMaster != null)
                        {
                            this.commandSelect = "";
                            UnWireDataMaster();
                            //isCreated=false;
                        }

                    }
                    _BindManager = manager1;
                    if (_BindManager != null)
                    {
                        WireDataSource();
                        SetDataSourceInternal();
                    }
                }
                //				if (((_BindManager != null) && (flag2 || flag1)) && (!"".Equals(_DataMember) && !this.BindingMemberInfoInDataManager(_DataMember)))
                //				{
                //					_IsBound=false;
                //					throw new ArgumentException("WrongDataMember");//, "newDisplayMember");
                //				}
                //				if ((_BindManager != null) && ((flag1 || flag2) || force))
                //				{
                //					_IsBound=true;
                //				}
            }
            if (flag1)
            {
                this.OnDataSourceChanged(EventArgs.Empty);
            }
            if (flag2)
            {
                this.OnDataMemberChanged(EventArgs.Empty);
            }
            if (_BindManager != null && _DataMaster != null)
            {
                _IsBound = true;
            }
            OnBindingChanged(EventArgs.Empty);
            UpdateCount(false);
        }

        private void SetDataSourceInternal()
        {
            if (DataSource != null && !initialising)
            {
                try
                {
                    if (this._DataSet != null)
                    {
                        this._DataSet.Tables.Clear();
                    }
                    //return (System.Data.DataView)((System.Data.DataSet)CM.List).Tables[this.DataMember].DefaultView;
                    if (DataSource is System.Data.DataSet)
                    {
                        this._DataSet = (System.Data.DataSet)DataSource;

                        if (this.DataMember.Length > 0)
                            this._DataMaster = (System.Data.DataTable)((System.Data.DataSet)DataSource).Tables[this.DataMember];
                        else
                            this._DataMaster = (System.Data.DataTable)((System.Data.DataSet)DataSource).Tables[0];
                    }
                    else if (DataSource is System.Data.DataView)
                    {
                        this._DataMaster = ((System.Data.DataView)DataSource).Table;
                        if (_DataSet == null)
                            this._DataSet = new DataSet();
                        _DataSet.Tables.Add(_DataMaster);
                        
                    }
                    else if (DataSource is System.Data.DataTable)
                    {
                        this._DataMaster = (System.Data.DataTable)((System.Data.DataTable)this.DataSource);
                        if (_DataSet == null)
                            this._DataSet = new DataSet();
                        _DataSet.Tables.Add(_DataMaster);
                    }
                    else if (DataSource is System.Data.DataViewManager)
                    {
                        this._DataSet = (DataSet)((System.Data.DataViewManager)this.DataSource).DataSet;

                        if (this.DataMember.Length > 0)
                            this._DataMaster = (System.Data.DataTable)((System.Data.DataViewManager)this.DataSource).DataSet.Tables[this.DataMember];
                        else
                            this._DataMaster = (System.Data.DataTable)((System.Data.DataViewManager)this.DataSource).DataSet.Tables[0];
                    }
                    else
                        throw new Exception("Data Source not valid");

                    //					if(DataMember!="")
                    //					{
                    //                      _DataMaster.TableName=DataMember;
                    //					}
 
                    CreateCommandSelect();
                    WireDataMaster();
                    SetDataView();
                    //isCreated=true;
                    //UpdateCount();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void SetDataView()
        {
             if (_DataMaster != null)
            {
                if (_DataView != null)
                {
                    _DataView.ListChanged -= new ListChangedEventHandler(_DataView_ListChanged);
                    _DataView = null;
                }
                _DataView = new DataView(_DataMaster, this._RowFilter, this._Sort, DataViewRowState.CurrentRows);

                _DataView.AllowDelete = this._AllowDelete;
                _DataView.AllowEdit = this._AllowEdit;
                _DataView.AllowNew = this._AllowNew;
                _DataView.ListChanged += new ListChangedEventHandler(_DataView_ListChanged);
            }
        }

        [Browsable(false)]
        public System.Data.DataView DataList
        {
            get
            {
                if (_DataView == null)
                {
                    SetDataView();
                }
                return _DataView;
            }
        }

        [Browsable(false)]
        public DataTable DataMaster
        {
            get { return this._DataMaster; }
        }

        [Browsable(false)]
        public DataSet DataManager
        {
            get { return this._DataSet; }
        }

        private void DataSourceDisposed(object sender, EventArgs e)
        {
            this.Set_ListManager(null, "", true);
        }

        private bool BindingMemberInfoInDataManager(string memberInfo)//BindingMemberInfo bindingMemberInfo)
        {
            if (_BindManager == null)
            {
                return false;
            }

            BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(memberInfo);
            PropertyDescriptorCollection collection1 = _BindManager.GetItemProperties();
            int num1 = collection1.Count;
            bool flag1 = false;
            for (int num2 = 0; num2 < num1; num2++)
            {
                //string s= collection1[num2].Name;
                //s=bindingMemberInfo.BindingField;
                if (!typeof(IList).IsAssignableFrom(collection1[num2].PropertyType) && collection1[num2].Name.Equals(bindingMemberInfo.BindingField))//memberInfo))//
                {
                    flag1 = true;
                    break;
                }
            }
            return flag1;
        }

        protected virtual void CreateCommandSelect()
        {
            string strSql = "SELECT ";
            string tableName = _DataMaster.TableName;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (System.Data.DataColumn c in _DataMaster.Columns)
            {
                sb.AppendFormat("[{0}].[{1}],", tableName, c.ColumnName);
            }
            strSql += sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " FROM[" + tableName + "]";
            this.commandSelect = strSql;
        }

        //		internal void SetPosition(int value)
        //		{
        //			if(!_IsBound)
        //				return;
        //			try
        //			{
        //				if(_SelectedIndex!=value) 
        //				{
        //				   if(ChangesState==ChangesStatus.Continue)
        //					  _BindManager.Position =value; 
        //				}
        //			}
        //			catch(Exception ex)
        //			{
        //				string msg=RM.GetString(RM.IndexOutBounds);   
        //				MsgBox.ShowError (msg + "\r\n" + ex.Message);  
        //			}
        //		}


        internal void SetPosition(int value, MoveOperator op)
        {
            if (!_IsBound || _BindManager == null)
                return;

            int nextPos = value;

            try
            {
                switch (op)
                {
                    case MoveOperator.MoveTo:
                        nextPos = value;
                        break;
                    case MoveOperator.MoveFirst:
                        nextPos = 0;
                        goto Lable_01;
                    //break;
                    case MoveOperator.MoveLast:
                        nextPos = _BindManager.Count - 1;
                        goto Lable_01;
                    //break;
                    case MoveOperator.MoveDelta:
                        nextPos = _SelectedIndex + value;
                        break;
                }


                if (nextPos < 0 || nextPos > _BindManager.Count - 1)
                {
                    return;
                }

            Lable_01:
                if (_SelectedIndex != nextPos)
                {
                    if (ChangesState == ChangesStatus.Continue)
                    {
                        _BindManager.Position = nextPos;
                        SetNavStatus(NavStatus.Default);//, false);
                    }
                }
            }
            catch (Exception ex)
            {
                //-/string msg=RM.GetString(RM.IndexOutBounds);   
                //-/MsgBox.ShowError (msg + "\r\n" + ex.Message);  
                OnErrorOcurred(ex.Message);//-/
            }
        }

        internal void ClearInternal()
        {
            if (this.HasChanges())
            {
                this.RejectChanges();
            }
            //this.Items.Clear();
            _SelectedIndex = -1;
            UnBindControls(false);
            SetNavStatus(NavStatus.UnBind);//, false);
        }

        internal void SetLocation()
        {
            this.SuspendLayout();

            int lblTop = (this.Height - this.lblNavigator.Height) / 2;
            this.ctlNavChanges.Height = this.dataNavigatore1.Height;
            this.ctlNavChanges.Top = this.dataNavigatore1.Top;
            this.ctlPictureBox.Size = new System.Drawing.Size(16, 16);
            bool sizeGrip = SizingGrip;
            int gap = sizeGrip ? 20 : 2;
            if (sizeGrip)
            {
                this.ctlResize.Location = new Point(this.Width - 22, this.Height - 22);
                this.ctlResize.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            }

            if (this.RightToLeft == RightToLeft.Yes)
            {
                this.McNav.Dock = System.Windows.Forms.DockStyle.None;
                this.McNav.Location = new Point(this.Width -McNav.Width- gap, (this.Height - this.McNav.Height) / 2);
                this.McNav.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                this.ctlPictureBox.Location = new Point(2, (this.Height - this.ctlPictureBox.Height) / 2);
                this.lblNavigator.Location = new Point(this.ctlPictureBox.Width  + 4, lblTop);
                this.ctlNavChanges.Left = this.Width -gap- this.dataNavigatore1.Width - this.ctlNavChanges.Width;
            }
            else
            {
                this.McNav.Dock = System.Windows.Forms.DockStyle.Left;
                this.ctlNavChanges.Left = this.dataNavigatore1.Left + this.dataNavigatore1.Width;
                this.ctlPictureBox.Location = new Point(this.Width - this.ctlPictureBox.Width - gap, (this.Height - this.ctlPictureBox.Height) / 2);
                this.lblNavigator.Location = new Point(this.Width - ctlPictureBox.Width - lblNavigator.Width - 4-gap, lblTop);
            }
            this.McNav.SetLocation(this.RightToLeft);
            this.ResumeLayout();
        }

        //		internal void SetCountrMsg()
        //		{
        //			int cnt=_BindManager.Count;
        //			string lblFrom=" :: ";
        //			int indx= cnt > 0 ? _SelectedIndex+1:0;
        //			lblNavigator.Text =indx.ToString () + lblFrom + cnt.ToString ();
        //		}

        private void WireDataMaster()
        {
            _DataMaster.ColumnChanging += new DataColumnChangeEventHandler(_dtMaster_ColumnChanging);
            _DataMaster.ColumnChanged += new DataColumnChangeEventHandler(_dtMaster_ColumnChanged);
            _DataMaster.RowChanging += new DataRowChangeEventHandler(_dtMaster_RowChanging);
            _DataMaster.RowChanged += new DataRowChangeEventHandler(_dtMaster_RowChanged);
            _DataMaster.RowDeleting += new DataRowChangeEventHandler(_dtMaster_RowDeleting);
            _DataMaster.RowDeleted += new DataRowChangeEventHandler(_dtMaster_RowDeleted);
            _DataMaster.TableNewRow += new DataTableNewRowEventHandler(_DataMaster_TableNewRow);
            WireDataDetails();
        }

 
        private void UnWireDataMaster()
        {
            _DataMaster.ColumnChanging -= new DataColumnChangeEventHandler(_dtMaster_ColumnChanging);
            _DataMaster.ColumnChanged -= new DataColumnChangeEventHandler(_dtMaster_ColumnChanged);
            _DataMaster.RowChanging -= new DataRowChangeEventHandler(_dtMaster_RowChanging);
            _DataMaster.RowChanged -= new DataRowChangeEventHandler(_dtMaster_RowChanged);
            _DataMaster.RowDeleting -= new DataRowChangeEventHandler(_dtMaster_RowDeleting);
            _DataMaster.RowDeleted -= new DataRowChangeEventHandler(_dtMaster_RowDeleted);
            _DataMaster.TableNewRow -= new DataTableNewRowEventHandler(_DataMaster_TableNewRow);
            UnWireDataDetails();
        }

        private void WireDataSource()
        {
            _BindManager.PositionChanged += new EventHandler(BindManager_PositionChanged);
            _BindManager.CurrentChanged += new EventHandler(BindManager_CurrentChanged);
            _BindManager.ItemChanged += new ItemChangedEventHandler(BindManager_ItemChanged);
            _BindManager.MetaDataChanged += new EventHandler(BindManager_MetaDataChanged);
        }

        private void UnWireDataSource()
        {
            _BindManager.PositionChanged -= new EventHandler(BindManager_PositionChanged);
            _BindManager.CurrentChanged -= new EventHandler(BindManager_CurrentChanged);
            _BindManager.ItemChanged -= new ItemChangedEventHandler(BindManager_ItemChanged);
            _BindManager.MetaDataChanged -= new EventHandler(BindManager_MetaDataChanged);
        }

        private void WireDataDetails()
        {
            for (int i = 1; i < _DataSet.Tables.Count; i++)
            {
                _DataSet.Tables[i].ColumnChanging += new DataColumnChangeEventHandler(_dtMaster_ColumnChanging);
                _DataSet.Tables[i].ColumnChanged += new DataColumnChangeEventHandler(_dtMaster_ColumnChanged);
                _DataSet.Tables[i].RowChanging += new DataRowChangeEventHandler(_dtMaster_RowChanging);
                _DataSet.Tables[i].RowChanged += new DataRowChangeEventHandler(_dtMaster_RowChanged);
                _DataSet.Tables[i].RowDeleting += new DataRowChangeEventHandler(_dtMaster_RowDeleting);
                _DataSet.Tables[i].RowDeleted += new DataRowChangeEventHandler(_dtMaster_RowDeleted);
            }
        }

        private void UnWireDataDetails()
        {
            for (int i = 1; i < _DataSet.Tables.Count; i++)
            {
                _DataSet.Tables[i].ColumnChanging -= new DataColumnChangeEventHandler(_dtMaster_ColumnChanging);
                _DataSet.Tables[i].ColumnChanged -= new DataColumnChangeEventHandler(_dtMaster_ColumnChanged);
                _DataSet.Tables[i].RowChanging -= new DataRowChangeEventHandler(_dtMaster_RowChanging);
                _DataSet.Tables[i].RowChanged -= new DataRowChangeEventHandler(_dtMaster_RowChanged);
                _DataSet.Tables[i].RowDeleting -= new DataRowChangeEventHandler(_dtMaster_RowDeleting);
                _DataSet.Tables[i].RowDeleted -= new DataRowChangeEventHandler(_dtMaster_RowDeleted);
            }

        }

        #endregion

        #region DataManager Events

        private void _DataView_ListChanged(object sender, ListChangedEventArgs e)
        {
            OnDataListChanged(e);
        }

        private void BindManager_PositionChanged(object sender, EventArgs e)
        {
            if (suspendPosition)
                return;

            //_SelectedIndex= _BindManager.Position ;
            //UpdateCount();
            UpdateCount(true);
            OnBindPositionChanged(e);
        }

        private void BindManager_CurrentChanged(object sender, EventArgs e)
        {
            OnBindCurrentChanged(e);
        }

        private void BindManager_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            OnItemChanged(e);
        }

        private void BindManager_MetaDataChanged(object sender, EventArgs e)
        {
            OnMetaDataChanged(e);
        }

        #endregion

        #region VirtualEvents

        protected override void OnBindingContextChanged(EventArgs e)
        {
            //this.Set_ListManager(m_DataSource, m_DisplayMember, true);
            base.OnBindingContextChanged(e);
        }

        protected virtual void OnBindingChanged(EventArgs e)
        {
            if (_IsBound)
            {
                SetNavStatus(NavStatus.Default);//, false);
                InvokeEnableButtons(true);
            }
            else
            {
                this._SelectedIndex = -1;
                this.ClearInternal();
            }
            if (this.BindingChanged != null)
                this.BindingChanged(this, e);
        }

        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            //			if (_DataSource != null && base.Created)
            //			{
            //				_IsBound=true;
            //				SetNavStatus(NavStatus.Default,false);
            //			}
            //			if (_DataSource == null)
            //			{
            //				_IsBound=false;
            //				this._SelectedIndex = -1;
            //				this.ClearInternal();
            //			}

            if (this.DataSourceChanged != null)
                this.DataSourceChanged(this, e);
        }

        protected virtual void OnDataMemberChanged(EventArgs e)
        {
            if (this.DataMemberChanged != null)
                this.DataMemberChanged(this, e);
        }

        protected virtual void OnDataListChanged(ListChangedEventArgs e)
        {
            if (DataListChanged != null)
                DataListChanged(this, e);
        }

        //		protected virtual void  OnCustomValidating(CancelEventArgs e)
        //		{
        //			if(CustomValidating!=null)
        //				CustomValidating(this,e);
        //		}
        //
        //		protected virtual void  OnCustomDeleting(CancelEventArgs e)
        //		{
        //			if(CustomDeleting!=null)
        //				CustomDeleting(this,e);
        //		}

        protected virtual void OnItemChanged(ItemChangedEventArgs e)
        {
            if (ItemChanged != null)
            {
                ItemChanged(this, e);
            }
        }

        protected virtual void OnMetaDataChanged(EventArgs e)
        {
            if (MetaDataChanged != null)
            {
                MetaDataChanged(this, e);
            }
        }

        /// <summary>
        /// On Add new record
        /// </summary>
        /// <param name="e">EventArgs</param>
        /// <returns>return true it is new record position</returns>
        protected virtual bool OnAddNew(EventArgs e)
        {
            if (_AllowNew && navStatus!= NavStatus.New)
            {
                int cnt = this._BindManager.Count;
                _BindManager.AddNew();
                UpdateCount(false);
                SetNavStatus(NavStatus.New);//, false);
                //-/DirtyChanging(true);
                shouldRefresh = true;
                BindControlsDefaultValue();
                return _BindManager.Count > cnt;
            }
            return false;
        }

        protected virtual void OnColumnChanging(DataColumnChangeEventArgs e)
        {
            if (ColumnChanging != null)
                ColumnChanging(this, e);
        }
        protected virtual void OnColumnChanged(DataColumnChangeEventArgs e)
        {
            if (ColumnChanged != null)
                ColumnChanged(this, e);
        }

        protected virtual void OnRowChanging(DataRowChangeEventArgs e)
        {
            if (RowChanging != null)
                RowChanging(this, e);
        }

        protected virtual void OnRowChanged(DataRowChangeEventArgs e)
        {
            if (RowChanged != null)
                RowChanged(this, e);
        }

        protected virtual void OnRowDeleting(DataRowChangeEventArgs e)
        {
            if (RowDeleting != null)
                RowDeleting(this, e);
        }
        protected virtual void OnRowDeleted(DataRowChangeEventArgs e)
        {
            if (RowDeleted != null)
                RowDeleted(this, e);
        }

        protected virtual void OnRowNew(DataTableNewRowEventArgs e)
        {
            if (RowNew != null)
                RowNew(this, e);
        }

        protected virtual void OnBindPositionChanged(EventArgs e)
        {
            if (BindPositionChanged != null)
                BindPositionChanged(this, e);
        }
        protected virtual void OnBindCurrentChanged(EventArgs e)
        {
            if (BindCurrentChanged != null)
                BindCurrentChanged(this, e);
        }

        protected virtual void OnNavBarUpdated(NavBarUpdatedEventArgs e)
        {
            if (NavBarUpdated != null)
                NavBarUpdated(this, e);
        }

        protected virtual void OnNavBarUpdating(NavBarUpdatingEventArgs e)
        {
            if (NavBarUpdating != null)
                NavBarUpdating(this, e);
        }

        private bool OnNavBarUpdateing()
        {
            NavBarUpdatingEventArgs e = new NavBarUpdatingEventArgs(navStatus, _BindManager.Position);
            this.OnNavBarUpdating(e);
            return e.Cancel;
        }

        //-/
        protected virtual void OnErrorOcurred(string msg)
        {
            if (_ShowErrorMessage)
            {
                MsgBox.ShowError(msg, _MessageTitle);
            }
            OnErrorOcurred(new ErrorOcurredEventArgs(msg));
        }

        protected virtual void OnErrorOcurred(ErrorOcurredEventArgs e)
        {
            if (this.ErrorOcurred != null)
                this.ErrorOcurred(this, e);
        }


        #endregion

        #region Navigation

        private void BindNavigatore_ClickFirst(object sender, ButtonClickEventArgs e)
        {
            MoveFirst();
        }
        private void BindNavigatore_ClickLast(object sender, ButtonClickEventArgs e)
        {
            MoveLast();
        }
        private void BindNavigatore_ClickNext(object sender, ButtonClickEventArgs e)
        {
            MoveNext();
        }
        private void BindNavigatore_ClickPrev(object sender, ButtonClickEventArgs e)
        {
            MovePrevious();
        }
        private void BindNavigatore_ClickDelete(object sender, ButtonClickEventArgs e)
        {

            if (this.dataNavigatore1.EnableDelete)
            {
                DelRow();
            }
        }
        private void BindNavigatore_ClickNew(object sender, ButtonClickEventArgs e)
        {
            if (this.dataNavigatore1.EnableNew)
                MoveNew();
        }

        /// <summary>
        /// Position move next
        /// </summary>
        public virtual void MoveNext()
        {
            SetPosition(1, MoveOperator.MoveDelta);
        }

        /// <summary>
        /// Position move Previous
        /// </summary>
        public virtual void MovePrevious()
        {
            SetPosition(-1, MoveOperator.MoveDelta);
        }

        /// <summary>
        /// Position Move first
        /// </summary>
        public virtual void MoveFirst()
        {
            SetPosition(0, MoveOperator.MoveFirst);
        }

        /// <summary>
        /// Position Move last
        /// </summary>
        public virtual void MoveLast()
        {
            SetPosition(0, MoveOperator.MoveLast);
        }

        /// <summary>
        /// Position Add new 
        /// </summary>
        public void MoveNew()
        {
            if (!_AllowNew)
                return;
            if (navStatus == NavStatus.New)
                return;

            try
            {
                if (_BindManager.Count==0)// _DataMaster != null && _DataMaster.Rows.Count == 0 )
                {
                    OnAddNew(EventArgs.Empty);
                }

                else if (ChangesState == ChangesStatus.Continue)
                {
                    OnAddNew(EventArgs.Empty);
                    //bool res=OnAddNew(EventArgs.Empty);
                    //if(res)
                    //{
                    //_BindManager.AddNew();
                    //UpdateCount();
                    //}
                }
                //				if(ClickAddNew!=null)
                //					ClickAddNew(this,EventArgs.Empty);
            }
            catch (System.Exception ex)
            {
                OnErrorOcurred(ex.Message);//-/System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// End edit Update when current row changes
        /// </summary>
        /// <param name="checkCustomValidation"></param>
        /// <returns>return record afected</returns>
        protected int EndEdit(bool checkCustomValidation)
        {
            if (!_AllowEdit)
                return 0;
            if (!IsBinding())
                return 0;

            try
            {
                this._BindManager.EndCurrentEdit();

                if (_useViewManager)
                {
                    return EndEditDataSet(checkCustomValidation);
                }
                else
                {
                    return EndEditMaster(checkCustomValidation);
                }
            }
            catch (Exception ex)
            {
                OnErrorOcurred(ex.Message);	//MsgBox.ShowError(ex.Message);
                return 0;
            }
        }

        private int EndEditMaster(bool checkCustomValidation)
        {
            int res = 0;
            try
            {

                DataTable dsChanges = _DataMaster.GetChanges();//DataRowState.Modified|DataRowState.Added|DataRowState.Deleted);

                if (dsChanges != null)
                {
                    if (checkCustomValidation)
                    {
                        if (OnNavBarUpdateing())
                            return 0;
                    }
                    // Check the DataSet for errors.
                    if (!dsChanges.HasErrors)
                    {
                        if (!this._LocalOnly)
                        {
                            res = DataAdpterUpdate(dsChanges);
                        }
                        else
                        {
                            res = 1;
                        }

                        if (res > 0)
                        {
                            this._DataSet.AcceptChanges();
                            NavStatus oldStatus = navStatus;
                            DirtyChanging(false);//, true);
                            OnNavBarUpdated(new NavBarUpdatedEventArgs(oldStatus, _SelectedIndex));
                        }
                    }

                    //this._BindManager.EndCurrentEdit();
                    //DirtyChanging(false, true);

                }
            }
            catch (System.Exception ex)
            {
                OnErrorOcurred(ex.Message);//-/ MsgBox.ShowError(ex.Message);
            }
            return res;
        }


        private int EndEditDataSet(bool checkCustomValidation)
        {
            int res = 0;
            try
            {

                //DataTable dsChanges =_DataMaster.GetChanges();//DataRowState.Modified|DataRowState.Added|DataRowState.Deleted);
                DataSet dsChanges = this._DataSet.GetChanges();

                if (dsChanges != null)
                {
                    if (checkCustomValidation)
                    {
                        if (OnNavBarUpdateing())
                            return 0;
                    }
                    // Check the DataSet for errors.
                    if (!dsChanges.HasErrors)
                    {
                        if (!this._LocalOnly)
                        {
                            res = DataAdpterUpdate(dsChanges);
                        }
                        else
                        {
                            res = 1;
                        }
                       
                        if (res > 0)
                        {
                            this._DataSet.AcceptChanges();
                            NavStatus oldStatus = navStatus;
                            DirtyChanging(false);//, true);
                            OnNavBarUpdated(new NavBarUpdatedEventArgs(oldStatus, _SelectedIndex));
                        }
                    }

                    //DirtyChanging(false, true);
                }
            }
            catch (System.Exception ex)
            {
                OnErrorOcurred(ex.Message);//-/ MsgBox.ShowError(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// End edit Update current row changes
        /// </summary>
        public int UpdateCurrentEdit(string dataMember)
        {
            if (!_AllowEdit)
                return 0;
            if (!IsBinding())
                return 0;

            int res = 0;

            try
            {
                //TODO : check this
                this._BindManager.EndCurrentEdit();
            }
            catch (Exception ex)
            {
                OnErrorOcurred(ex.Message);	//MsgBox.ShowError(ex.Message);
                return res;
            }

            try
            {

                DataSet dsChanges = this._DataSet.GetChanges();

                if (dsChanges != null)
                {
                    if (OnNavBarUpdateing())
                        return 0;

                    // Check the DataSet for errors.
                    if (!dsChanges.HasErrors)
                    {
                        if (!this._LocalOnly)
                        {
                            res = DataAdpterUpdate(dsChanges, dataMember);
                        }
                        else
                        {
                            res = 1;
                        }

                        if (res > 0)
                        {
                            this._DataSet.AcceptChanges();
                            NavStatus oldStatus = navStatus;
                            DirtyChanging(false);//, true);
                            OnNavBarUpdated(new NavBarUpdatedEventArgs(oldStatus, _SelectedIndex));
                        }
                    }

                    //this._BindManager.EndCurrentEdit();
                    //DirtyChanging(false, true);

                }
            }
            catch (System.Exception ex)
            {
                OnErrorOcurred(ex.Message);//-/System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return res;
        }


        /// <summary>
        /// End edit Update current row changes in dataset
        /// </summary>
        public int UpdateDataSet()
        {
            return EndEdit(true);
        }

        private void CancelCurrentEdit()
        {
            try
            {
                shouldRefresh = false;

                if (this._BindManager.Count > 1)
                {
                    this._BindManager.CancelCurrentEdit();
                    return;
                }
                if (navStatus == NavStatus.New)
                {
                    return;
                }
                //if (_DataMaster.Rows.Count < this._BindManager.Count)
                //{
                //    return;
                //}

                if (this._BindManager.Position > -1)
                {
                    this._BindManager.CancelCurrentEdit();
                }
            }
            catch { }
        }
        private void EndCurrentEdit()
        {
            shouldRefresh = false;
            if (this._BindManager.Position > -1)
            {
                this._BindManager.EndCurrentEdit();
            }
        }
        

        /// <summary>
        /// CancelEdit
        /// </summary>
        protected void CancelEdit()
        {
            if (IsBinding())
            {
                if (_useViewManager)
                {
                    _DataSet.RejectChanges();
                }
                else if(_DataMaster.Rows.Count>1)
                {
                    _DataMaster.RejectChanges();
                }
                CancelCurrentEdit();
                DirtyChanging(false);
            }
        }

        /// <summary>
        /// Cancel Edit for specific table in data set
        /// </summary>
        /// <param name="mappingName">table name in data set</param>
        public void CancelEdit(string mappingName)
        {
            if (!IsBinding())
                return;
            if (_DataSet.Tables.Contains(mappingName))
            {
                _DataSet.Tables[mappingName].RejectChanges();
            }
            else
            {
                MsgBox.ShowError("Invalid MappingName");
            }
        }

        /// <summary>
        /// Cancel edit in all tables in data Set
        /// </summary>
        public void CancelAllChanges()
        {
            CancelCurrentEdit();
            _DataSet.RejectChanges();
            DirtyChanging(false);
        }

        /// <summary>
        /// Delete Row form record set
        /// </summary>
        public void DelRow()
        {
            if (!_AllowDelete)
                return;
            else if (!IsBinding())
                return;
            else if (_BindManager.Count == 0)
                return;
            else if (_DataMaster.Rows.Count == 0)
                return;
            else if (CurrentRowState == DataRowState.Added || CurrentRowState == DataRowState.Detached)
            {
                MovePrevious();
                return;
            }
      
            NavStatus oldStatus = navStatus;
            SetNavStatus(NavStatus.Delete);//, false);

            if (OnNavBarUpdateing())
            {
                SetNavStatus(oldStatus);//, false);
                return;
            }

            if (ShowMessageSaveChanges)
            {
                if (MsgBox.ShowQuestion(MessageDelete, MessageTitle, MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    SetNavStatus(oldStatus);//, false);
                    return;
                }
            }

            if (_DataMaster.Rows.Count < this._BindManager.Count)
            {
                this.RejectChanges();
                return;
            }
            if (_IsDirty)
            {
                this.RejectChanges();
            }
            DataRow dr = _DataMaster.Rows[this._BindManager.Position];
            suspendPosition = true;
            dr.Delete();
            suspendPosition = false;
 
            try
            {
                //this._BindManager.RemoveAt(this._BindManager.Position);

                // GetChanges for deleted rows only.
                DataTable dsChanges = _DataMaster.GetChanges(DataRowState.Deleted);
                //DataSet dsChanges = _DataSet.GetChanges(DataRowState.Deleted);
                if (dsChanges != null)
                {
                    int res = -1;

                    if (!this._LocalOnly)
                    {
                        res = DataAdpterUpdate(dsChanges);
                    }
                    else
                    {
                        res = 1;
                    }
                    if(this._BindManager.Count>0)
                        this._BindManager.EndCurrentEdit();
                    //DirtyChanging(false, true);
                    //UpdateCount(false);
                    if (res > 0)
                    {
                        DirtyChanging(false);//, true);
                        UpdateCount(false);
                        _DataMaster.AcceptChanges();

                        if (this._BindManager.Count==0)//shouldClear)
                        {
                            this._BindManager.SuspendBinding();
                            BindControlsDefaultValue();
                            this._BindManager.ResumeBinding();
                        }
                        else
                        {
                            RefreshBinding(false);
                        }

                        OnNavBarUpdated(new NavBarUpdatedEventArgs(oldStatus, _SelectedIndex));

                    }
                }
 
            }
            catch (System.Exception ex)
            {
                CancelAllChanges();
                OnErrorOcurred(ex.Message);//-/System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private bool HasChanges()
        {
            if (!IsBinding())
                return false;
            if (CurrentRowView.IsNew)
            {
                return IsDirty;// _DataMaster.DataSet.HasChanges();
            }
            _BindManager.Position = _BindManager.Position;

            //TODO : check this 
            return _DataMaster.DataSet.HasChanges();

        }

        private ChangesStatus ChangesState
        {

            get
            {
                if (!IsBinding())
                    return ChangesStatus.None;
                if (suspendPosition)
                    return ChangesStatus.Continue;
                if (!_AllowEdit)
                {
                    CancelEdit();// _DataMaster.RejectChanges ();
                    return ChangesStatus.Continue;
                }

                //_BindManager.Position   = _BindManager.Position;

                if (HasChanges())//-/ || navStatus==NavStatus.New )
                {
                    //					CancelEventArgs e=new CancelEventArgs();
                    //					OnCustomValidating (e);

                    if (OnNavBarUpdateing())
                        return ChangesStatus.Stop;

                    if (_ShowMessageSaveChanges)
                    {
                        DialogResult res = MsgBox.ShowQuestionYNC(MessageSaveChanges, MessageTitle);
                        if (res == DialogResult.Yes)
                        {
                            EndEdit(false);
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            return ChangesStatus.Stop;
                        }
                        else if (res == DialogResult.No)
                        {
                            CancelEdit();// _DataMaster.RejectChanges ();
                        }

                    }
                    else
                    {
                        EndEdit(false);
                    }
                }
                else if (IsNew)
                {
                    CancelCurrentEdit();
                }
                return ChangesStatus.Continue;
            }
        }

        /// <summary>
        /// Is Binding to dada sourec
        /// </summary>
        /// <returns>return true if it is binding to data source</returns>
        public bool IsBinding()
        {
            if (_BindManager != null && _DataMaster != null)
                return true;//_BindManager.Bindings.Count>0 && _DataMaster !=null;
            else
                return false;
        }

        delegate void UpdateCountCallBack(bool refreshIndex);

        private void UpdateCount(bool refreshIndex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateCountCallBack(UpdateCount), refreshIndex);
            }
            else
            {
                string lblFrom = " :: ";
                if (!_IsBound)
                {
                    lblNavigator.Text = "0" + lblFrom + "0";
                    return;
                }

                if (refreshIndex)
                {
                    _PrevIndex = _SelectedIndex;
                    _SelectedIndex = _BindManager.Position;
                    //_Count=_BindManager.Count;
                }
                //SetCountrMsg();
                int cnt = _BindManager.Count;
                int indx = cnt > 0 ? _SelectedIndex + 1 : 0;
                lblNavigator.Text = indx.ToString() + lblFrom + cnt.ToString();
            }
        }

        #endregion

        #region DataEvents

        private void _dtMaster_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            OnColumnChanging(e);
            if (!initialising && textChanged)
            {
                SetNavStatus(NavStatus.Edit);//, false);
                DirtyChanging(true);
            }
        }

        private void _dtMaster_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            OnColumnChanged(e);
            textChanged = false;
            //			if(!initialising)
            //			{
            //				DirtyChanging(false);
            //			}
        }

        private void _dtMaster_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            OnRowChanging(e);
            //			if(!initialising && IsHandleCreated)
            //			{
            //				this._IsDirty =true;
            //				OnDirtyChanged();
            //			}
        }

        private void _dtMaster_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            OnRowChanged(e);
            //			if(!initialising && IsHandleCreated)
            //			{
            //				this._IsDirty =true;
            //				OnDirtyChanged();
            //			}
        }

        private void _dtMaster_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            OnRowDeleting(e);
            SetNavStatus(NavStatus.Delete);//, false);
            DirtyChanging(true);
        }

        private void _dtMaster_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            OnRowDeleted(e);
            //DirtyChanging(false);
        }

        void _DataMaster_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            OnRowNew(e);
        }

        #endregion

        #region McNavigatore Properties

        /// <summary>
        /// get the navigatore control, for access to navigatore buttons
        /// </summary>
        [Browsable(false)]
        //[Category("McNavigatore"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        private McNavigatore McNav
        {
            get { return dataNavigatore1; }
        }

        /// <summary>
        /// Enable/Disable navigatore Buttons
        /// </summary>
        [Category("McNavigatore"), DefaultValue(true),
        RefreshProperties(RefreshProperties.All),
        Description("Enable/Disable navigatore Buttons")]
        public bool EnableButtons
        {
            get { return McNav.EnableButtons; }
            set
            {
                McNav.EnableButtons = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Show/Hide delete button
        /// </summary>
        [Category("McNavigatore"), DefaultValue(true), Description("Show/Hide delete button")]
        public bool ShowDelete
        {
            get { return McNav.ShowDelete; }
            set
            {
                if (_AllowDelete)
                {
                    McNav.ShowDelete = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Show/Hide add new button
        /// </summary>
        [Category("McNavigatore"), DefaultValue(true), Description("Show/Hide add new button")]
        public bool ShowNew
        {
            get { return McNav.ShowNew; }
            set
            {
                if (_AllowNew)
                {
                    McNav.ShowNew = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Show/Hide navigators buttons
        /// </summary>
        [Category("McNavigatore"), DefaultValue(true), Description("Show/Hide navigators buttons")]
        public bool ShowNavigatore
        {
            get { return McNav.Visible; }
            set
            {
                McNav.Visible = value;
                this.Invalidate();
            }
        }

        private void NavigatorSetting()
        {
            if (ShowNavigatore)
            {
                if (!_AllowNew)
                    McNav.ShowNew = false;

                if (!_AllowDelete)
                    McNav.ShowDelete = false;
            }
        }

        #endregion

        #region Properties

        [DefaultValue(""), Category("Data")]
        public string DataMember
        {
            get { return _DataMember; }
            set
            {
                _DataMember = value;
                if (_DataSource != null && !initialising)
                {
                    this.Set_ListManager(_DataSource, value, true);
                }
                this.Invalidate();
            }
        }

        [DefaultValue((string)null), Category("Data"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get { return _DataSource; }
            set
            {

                if (((value != null) && !(value is IList)) && !(value is IListSource))
                {
                    throw new Exception("BadDataSourceForComplexBinding");
                }
                //_DataSource = value;
                //TODO: check this
                if (!initialising)//_DataSource != value &&
                {
                    try
                    {
                        //isCreated=false;
                        //this._DataSet.Tables.Clear();
                        this.Set_ListManager(value, _DataMember, false);
                    }
                    catch (Exception)
                    {
                        this.DataMember = "";
                    }
                    if (value == null)
                    {
                        this.DataMember = "";
                    }
                }
                //				if(!isCreated)//IsHandleCreated && 
                //				{
                //					SetDataSourceInternal(); 
                //					//InitDataMaster();
                //					//SetBindingManagerBase();
                //				}
            }
        }

        /// <summary>
        /// CurrencyManager
        /// </summary>
        [Browsable(false)]
        public CurrencyManager BindingManager
        {
            get { return _BindManager; }
        }

        /// <summary>
        /// IsDirty represent boolean value if form Hase any changes
        /// </summary>
        [Category("Data"), Browsable(false)]
        public bool IsDirty
        {
            get { return _IsDirty; }
        }

        /// <summary>
        /// IsNew indicate wether the current is new
        /// </summary>
        [Category("Data"), Browsable(false)]
        public bool IsNew
        {
            get
            {
                if (_BindManager == null || _BindManager.Current == null)
                    return false;
                return NavigatorStatus == NavStatus.New || ((DataRowView)_BindManager.Current).IsNew;
                //return NavigatorStatus==NavStatus.New;
            }
        }

        /// <summary>
        /// IsNew indicate wether the current row
        /// </summary>
        [Category("Data"), Browsable(false)]
        public DataRowView CurrentRowView
        {
            get
            {
                if (_BindManager == null || _BindManager.Current == null)
                    return null;
                return ((DataRowView)_BindManager.Current);
            }
        }

        /// <summary>
        /// IsDataChanged represent boolean value if form in edit mode
        /// </summary>
        [Category("Data")]
        public bool IsDataChanged
        {
            get { return HasChanges(); }

        }

        /// <summary>
        /// get the last index before Selected index
        /// </summary>
        public int PrevIndex()
        {
            return _PrevIndex;
        }

        /// <summary>
        /// Allow Add New
        /// </summary>
        [Category("Data"), DefaultValue(true)]
        public bool AllowAdd
        {
            get { return _AllowNew; }
            set
            {
                if (_ReadOnly)
                    _AllowEdit = false;
                else
                    _AllowNew = value;
                if (_DataView != null)
                    _DataView.AllowNew = value;
                this.McNav.EnableNew = _AllowNew;
            }
        }
        /// <summary>
        /// Allow Edit
        /// </summary>
        [Category("Data"), DefaultValue(true)]
        public bool AllowEdit
        {
            get { return _AllowEdit; }
            set
            {
                if (_ReadOnly)
                    _AllowEdit = false;
                else
                    _AllowEdit = value;
                if (!_AllowEdit)
                {
                    this.ShowChangesButtons = false;
                }
                if (_DataView != null)
                    _DataView.AllowEdit = value;
            }
        }
        /// <summary>
        /// Allow Delete
        /// </summary>
        [Category("Data"), DefaultValue(true)]
        public bool AllowDelete
        {
            get { return _AllowDelete; }
            set
            {
                if (_ReadOnly)
                    _AllowEdit = false;
                else
                    _AllowDelete = value;
                if (_DataView != null)
                    _DataView.AllowDelete = value;
                this.McNav.EnableDelete = _AllowDelete;
            }
        }

        /// <summary>
        /// Show Message box when error ocurred
        /// </summary>
        [Category("Data"), DefaultValue(true), Description("Show Message box when error ocurred")]
        public bool ShowErrorMessage
        {
            get { return _ShowErrorMessage; }
            set { _ShowErrorMessage = value; }
        }

        /// <summary>
        /// Show Message box before Save Changes or delete
        /// </summary>
        [Category("Data"), DefaultValue(true), Description("Show Message box before Save Changes or Delete")]
        public bool ShowMessageSaveChanges
        {
            get { return _ShowMessageSaveChanges; }
            set { _ShowMessageSaveChanges = value; }
        }

        /// <summary>
        /// Title caption on Messages box
        /// </summary>
        [Category("Appearnce"), DefaultValue(""), Description("Title caption on Messages box")]
        public string MessageTitle
        {
            get { return _MessageTitle; }
            set { _MessageTitle = value; }
        }


        /// <summary>
        /// string Message in Message box whene show Save Changes,default is "Save changes ?"
        /// </summary>
        [Category("Appearnce"), DefaultValue(""), Description("string Message in Message box whene show Save Changes")]
        public string MessageSaveChanges
        {
            get
            {
                if (_MessageSaveChanges == null || _MessageSaveChanges == "")
                {
                    _MessageSaveChanges = DefaultSaveMessage;
                }
                return _MessageSaveChanges;
            }
            set
            {
                _MessageSaveChanges = value;
            }
        }

        /// <summary>
        /// string Message in Message box whene show Delete Changes"
        /// </summary>
        [Category("Appearnce"), DefaultValue(""), Description("string Message in Message box whene show Delete Changes")]
        public string MessageDelete
        {
            get
            {
                if (_MessageDelete == null || _MessageDelete == "")
                {
                    _MessageDelete = DefaultDeleteMessage;
                }
                return _MessageDelete;
            }
            set
            {
                _MessageDelete = value;
            }
        }

        /// <summary>
        /// Show/Hide record count and Selected index
        /// </summary>
        [Category("Data"), DefaultValue(true), Description("Show/Hide record count and Selected index")]
        public bool ShowCounters
        {
            get { return lblNavigator.Visible; }
            set
            {
                lblNavigator.Visible = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Selected record index in records set
        /// </summary>
        [Category("Data"), DefaultValue(0), Description("Selected record index in records set")]
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (_SelectedIndex != value && _IsBound)
                {
                    SetPosition(value, MoveOperator.MoveTo);
                    //_SelectedIndex =value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// String Field name to sort record list, it is use for find and filters
        /// </summary>
        [Category("Data"), DefaultValue(""), Description("String Field name to sort record list")]
        public string Sort
        {
            get
            {
                if (this.DataList != null)
                {
                    return this.DataList.Sort;
                }
                return _Sort;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _Sort = value;
                if (this.DataList != null)
                {

                    if (this.DataList.Sort != value)
                    {
                        try
                        {
                            this.DataList.Sort = value;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// String filetr for filter execute
        /// </summary>
        [Category("Data"), DefaultValue(""), Description("String filetr for filter execute")]
        public string RowFilter
        {
            get
            {
                //				if(this.DataList!=null)
                //				{
                //					return this.DataList.RowFilter; 
                //				}
                return _RowFilter;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                _RowFilter = value;
                //				if(this.DataList!=null)
                //				{
                //
                //					if(this.DataList.RowFilter!=value)
                //					{
                //						try
                //						{
                //							this.DataList.RowFilter=value;
                //
                //						}
                //						catch(Exception ex)
                //						{
                //							throw ex;
                //						}
                //					}
                //				}
            }
        }

        /// <summary>
        /// Record count in records set
        /// </summary>
        public int Count
        {
            get
            {
                if (_BindManager != null)
                    return _BindManager.Count;
                return 0;//_Count; 
            }
            //			set
            //			{
            //				_Count =value;
            //				this.Invalidate();
            //			}
        }

        /// <summary>
        /// Save data on updating in local DataTable only
        /// </summary>
        [Category("Data"), DefaultValue(false), Description("Save data on updating in local DataTable only")]
        public bool LocalOnly
        {
            get { return this._LocalOnly; }
            set
            {
                this._LocalOnly = value;
            }
        }

        /// <summary>
        /// Read only 
        /// </summary>
        [Category("Data"), DefaultValue(false)]
        public bool ReadOnly
        {
            get { return this._ReadOnly; }
            set
            {
                if (value != this._ReadOnly)
                {
                    this._ReadOnly = value;
                    SetReadOnlyProperty();
                    if (!DesignMode)
                    {
                        SetReadOnly(value);
                    }
                    else
                    {
                        if (!this._ReadOnly) { SetNavStatus(NavStatus.Default/*, false*/); }
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Get nav bar status position 
        /// </summary>
        [Category("Behavior")]
        public NavStatus NavigatorStatus
        {
            get { return navStatus; }
        }

  
        private void SetNavStatus(NavStatus ns)//, bool isAccept)
        {
            //NavStatus oldNavStatus=navStatus;

            if (ns == NavStatus.UnBind)
            {
                navStatus = ns;
                InvokeEnableButtons(false);
            }
            else if (_ReadOnly)
            {
                navStatus = NavStatus.ReadOnly;
            }
            else if (ns == NavStatus.Edit && navStatus == NavStatus.New)
            {
                //navStatus=ns;
            }
            //else if (isAccept)
            //{
            //    NavStatus tns = navStatus;
            //    navStatus = ns;
            //    OnNavBarUpdated(new NavBarUpdatedEventArgs(tns, _SelectedIndex));
            //}
            else
            {
                navStatus = ns;
            }
            this.ctlPictureBox.Image = ResourceUtil.LoadImage(Global.ImagesPath + "nav" + navStatus.ToString() + ".gif");

            //			if(oldNavStatus==NavStatus.UnBind )
            //			{
            //				this.McNav.EnableButtons=true;
            //			}

            //this.ctlPictureBox.ImageIndex=(int)navStatus;
        }
        
        delegate void EnableButtonsCallback(bool value);

        private void InvokeEnableButtons(bool value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EnableButtonsCallback(InvokeEnableButtons), value);
            }
            else
            {
                this.McNav.EnableButtons = value;
            }
        }

        [Category("Style"), DefaultValue(ControlLayout.Flat)]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                base.ControlLayout = value;
                this.McNav.ControlLayout = value;
                this.Invalidate();
            }

        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set
            {
                base.BorderStyle = value;
            }
        }

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoChildrenStyle
        {
            get { return base.AutoChildrenStyle; }
            set { base.AutoChildrenStyle = value; }
        }

        //[Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override GradientStyle GradientStyle
        //{
        //    get { return base.GradientStyle; }
        //    set { base.GradientStyle = value; }
        //}

        #endregion

        #region ILayout

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            McNav.StylePainter = base.StylePainter;
        }

        #endregion

        #region Changes

        private bool showChangesButtons;
        private bool suspendPosition;

        public DataRowState CurrentRowState
        {
            get
            {
                if (_BindManager == null)
                    return DataRowState.Unchanged;

                DataRowView dr = null;
                try
                {
                    dr = (DataRowView)_BindManager.Current;
                }
                catch { }
                if (dr == null)
                    return DataRowState.Unchanged;
                return dr.Row.RowState;
            }
        }

        /// <summary>
        /// Get access to nav bar changes control
        /// </summary>
        [Browsable(false)]
        public McNavChanges McChanges
        {
            get { return this.ctlNavChanges; }
        }

        /// <summary>
        /// Show Changes Buttons for accept/reject, when data has changes
        /// </summary>
        [Category("Appearnce"), Description("Show Changes Buttons for accept/reject  when data has changes")]
        public bool ShowChangesButtons
        {
            get { return this.showChangesButtons; }
            set
            {
                if (!_AllowEdit || _ReadOnly)
                    this.showChangesButtons = false;
                else
                    this.showChangesButtons = value;
                if (!this.showChangesButtons)
                {
                    this.ctlNavChanges.Visible = false;
                }
            }
        }

        //private void DirtyChanging(bool value)
        //{
        //    DirtyChanging(value, false);
        //}

        private void DirtyChanging(bool value)//, bool isAccept)
        {
            this._IsDirty = value;
            OnDirtyChanged();
            dirtyInternal = _IsDirty;
            if (!_IsDirty)
            {
                SetNavStatus(NavStatus.Default);//, isAccept);
            }
        }

        bool dirtyInternal = false;
        private bool ShouldFireDirty()
        {
            return (dirtyInternal != _IsDirty);
        }

        protected virtual void OnDirtyChanged()
        {
            
            if (this.showChangesButtons)
            {
                this.ctlNavChanges.Visible = this.IsDirty;
            }
            if (ShouldFireDirty())
            {
                if (Dirty != null)
                    Dirty(this, EventArgs.Empty);
            }
        }

        private void ctlNavChanges_ClickAccept(object sender, ButtonClickEventArgs e)
        {
            OnAcceptPressed(e);
        }

        private void ctlNavChanges_ClickReject(object sender, ButtonClickEventArgs e)
        {
            OnRejectPressed(e);
        }

        /// <summary>
        /// Do Accept Changes for update, use for external execution
        /// </summary>
        public virtual void AcceptChanges()
        {
            OnAcceptPressed(EventArgs.Empty);
        }

        /// <summary>
        /// Do Reject Changes for cancel update, use for external execution
        /// </summary>
        public virtual void RejectChanges()
        {
            OnRejectPressed(EventArgs.Empty);
        }

        protected virtual void OnAcceptPressed(EventArgs e)
        {
            suspendPosition = true;
            int res = 0;
            if (_useViewManager)// _DataSet.Tables.Count>1)
            {
                res = UpdateDataSet();
            }
            else
            {
                res = EndEdit(true);
            }
            suspendPosition = false;

            if (res > 0 )
            {
                if (shouldRefresh)
                {
                    RefreshBinding(true);
                    shouldRefresh = false;
                }
                if (this.AcceptPressed != null)
                    this.AcceptPressed(this, EventArgs.Empty);
            }
        }

        protected virtual void OnRejectPressed(EventArgs e)
        {

            if (_useViewManager)
            {
                CancelAllChanges();
            }
            else
            {
                CancelEdit();
            }

            ChangePositionInternal();
            if (_DataMaster.Rows.Count == 1)
            {
                BindControlsDefaultValue();
            }
            if (this.RejectPressed != null)
            {
                this.RejectPressed(this, EventArgs.Empty);
            }

        }

        private void ChangePositionInternal()
        {
            suspendPosition = true;

            int i = _BindManager.Position;
            int cnt = _BindManager.Count;
            if (i == 0)
                _BindManager.Position = cnt - 1;
            else
                _BindManager.Position = 0;
            _BindManager.Position = i;
            this._BindManager.ResumeBinding();
            suspendPosition = false;

        }
        public void RefreshBinding(bool reload)
        {

            if (_BindManager != null)
            {
                if (reload)
                {
                    DoRefresh();
                }
                _BindManager.Refresh();
            }
        }

        protected virtual void DoRefresh()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Find record in records set by key object 
        /// </summary>
        /// <param name="key">key as field value in recordset</param>
        /// <returns>return the record index in records set if found else return -1</returns>
        public int Find(object key)
        {
            if (!_IsBound)
                return -1;

            //DataView dv = new DataView(_DataMaster, "",_Sort,DataViewRowState.CurrentRows);
            DataView dv = this.DataList;
            int res = -1;
            if (dv == null)
            {
                //do nothing
            }
            else if (dv.Sort != null && dv.Sort != "")
            {
                res = dv.Find(key);
            }
            else
            {
                MsgBox.ShowWarning("Find finds a row based on a Sort order, and no Sort order is specified.");
            }
            return res;
        }

        /// <summary>
        /// Find record in recordset by key value and move to that record if found  
        /// </summary>
        /// <param name="key">key as field value in recordset</param>
        public void MoveTo(object key)
        {
            int res = Find(key);
            if (res != -1)
            {
                SetPosition(res, MoveOperator.MoveTo);
            }
        }

        ///// <summary>
        ///// Move to record index in recordset
        ///// </summary>
        //public void MoveTo(int position)
        //{
        //    if (position != -1)
        //    {
        //        SetPosition(position, MoveOperator.MoveTo);
        //    }
        //}
        #endregion

        #region Virtual Methods

        protected abstract int DataAdpterUpdate(DataRow[] dataRows);
        protected abstract int DataAdpterUpdate(DataTable dataTable);
        protected abstract int DataAdpterUpdate(DataSet dataSet, string dataMember);
        protected abstract int DataAdpterUpdate(DataSet dataSet);

        #endregion

        #region Filter

        /// <summary>
        /// Set filter by RowFilter string property
        /// </summary>
        public void SetFilter()//string filter)
        {

            try
            {
                if ((_RowFilter.Trim().Length == 0))
                {
                    return;
                }
                System.Data.DataView dtv = this.DataList;
                dtv.RowFilter = _RowFilter;// filter;
                this.SetDataBinding(dtv, "");
                if ((dtv.Count == 0))
                {
                    MessageBox.Show("No records were found that match the filter criteria.", "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                OnErrorOcurred(ex.Message);//-/MessageBox.Show (ex.Message ,"Nistec");
            }
        }

        /// <summary>
        /// Remove filter if exists and return to the original view
        /// </summary>
        public void RemoveFilter()
        {
            this.DataList.RowFilter = "";
            this.SetDataBinding(DataList, "");
        }

        #endregion

        #region ISupportInitialize Members

        protected bool initialising = true;

        /// <summary>
        /// Begin initialising
        /// </summary>
        public virtual void BeginInit()
        {
            this.initialising = true;
            SetNavStatus(NavStatus.UnBind);//, false);
            _IsBound = false;
        }

        /// <summary>
        /// End initialising and bind the each control in parent form to dataSource
        /// by Data field property
        /// </summary>
        public virtual void EndInit()
        {
            EndInit(_DataSource,_DataMember, true);
        }


        delegate void EndInitCallBack(object dataSource, string dataMember, bool bindControls);

        /// <summary>
        /// End initialising and binding to data source
        /// </summary>
        /// <param name="bindControls">true to bind each control in parent form to dataSource</param>
        private void EndInit(object dataSource,string dataMember,bool bindControls)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EndInitCallBack(EndInit), dataSource, dataMember, bindControls);
            }
            else
            {

                this.initialising = false;
                if (dataSource != null)
                {
                    //				try
                    //				{
                    //isCreated=false;
                    this.Set_ListManager(dataSource, dataMember, true);
                    if (bindControls)
                    {
                        this.BindControls();

                        if (_BindManager.Count == 0)
                        {
                            OnAddNew(EventArgs.Empty);
                        }

                    }
                    //				}
                    //				catch (Exception)
                    //				{
                    //					this.DataMember = "";
                    //				}
                    if (_DataSource == null)
                    {
                        this.DataMember = "";
                    }
                }
            }
        }

        /// <summary>
        /// Initialising Nav bar and bind each control in parent form to dataSource
        /// by Data field property
        /// </summary>
        /// <param name="dataSource">dataSource object</param>
        /// <param name="dataMember">dataMember a table name in dataSet</param>
        public virtual void Init(object dataSource, string dataMember)
        {
            Init(dataSource, dataMember, true);
        }

 
        /// <summary>
        /// Initialising Nav bar and binding to data sorce
        /// </summary>
        /// <param name="dataSource">dataSource object</param>
        /// <param name="dataMember">dataMember a table name in dataSet</param>
        /// <param name="bindControls">true to bind each control in parent form to dataSource</param>
        public virtual void Init(object dataSource, string dataMember, bool bindControls)
        {
            if (bindControls && _IsBound)
                UnBindControls(true);

            BeginInit();
            //this.initialising = true;
            //this.DataSource = dataSource;
            //this.DataMember = dataMember;

            EndInit(dataSource, dataMember, bindControls);
            UpdateCount(true);
        }

        /// <summary>
        /// Suspend Binding
        /// </summary>
        /// <param name="unBind">if true un bind each control in parent form to data source</param>
        public void SuspendBinding(bool unBind)
        {
            if (unBind)
            {
                UnBindControls(false);
            }
            this.initialising = true;
        }

        /// <summary>
        /// Suspend Binding and un bind each control in parent form to data source
        /// </summary>
        public void SuspendAllBinding()
        {
            UnBindControls(true);
            BeginInit();
            //this.initialising = true;
        }

        /// <summary>
        /// Resume Binding and re bind each control in parent form to data source
        /// </summary>
        /// <param name="dataSource">dataSource</param>
        /// <param name="dataMember">dataMember</param>
        public void ResumeBinding(object dataSource, string dataMember)
        {
            SuspendBinding(false);
            this.DataSource = dataSource;
            this.DataMember = dataMember;
            ResumeBinding(true);
        }

        /// <summary>
        /// Resume Binding to data source
        /// </summary>
        /// <param name="reBind">if true re bind each control in parent form to data source</param>
        public void ResumeBinding(bool reBind)
        {
            if (_DataSource != null)
            {
                this.initialising = false;
                if (reBind)
                    SetDataBinding(_DataSource, _DataMember);
            }
            UpdateCount(true);
        }

        /// <summary>
        /// Resume Binding specific control to data source by field name
        /// </summary>
        /// <param name="control">control</param>
        /// <param name="fieldName">fieldName in data source</param>
        public void ResumeBinding(Control control, string fieldName)
        {
            if (!(control is IBind))
            {
                MsgBox.ShowError("Control not suported , IBind control");
                return;
            }

            if (this.fieldLists == null || this.fieldLists.Count == 0 || this.DataSource == null)
            {
                MsgBox.ShowError("This Bindinig field collection or DataSource is empty");
                return;
            }
            if (!this.fieldLists.Contains(control))
            {
                MsgBox.ShowError("This Control not found in Bindinig collection fields");
                return;
            }

            object dataSource = this.DataSource;
            string dataMember = this.DataMember;
            string bindProperty = "";
            string field = "";

            Control ctl = (Control)control;
            string oldField = this.fieldLists[control].ToString();

            if (fieldName == null || fieldName == "")
            {
                this.fieldsCount--;
                ctl.HandleCreated -= new EventHandler(ctl_HandleCreated);

            }
            this.fieldLists[control] = fieldName;

            if ((fieldName != null) && !(fieldName.Equals("")))
            {
                if (dataMember != "")
                    field = string.Format("{0}.{1}", dataMember, fieldName);
                else
                    field = fieldName;


                bindProperty = ((IBind)ctl).BindPropertyName();

                if (ctl.IsHandleCreated)
                {

                    ctl.DataBindings.Clear();
                    ctl.DataBindings.Add(bindProperty, dataSource, field);
                }
                else
                {
                    ctl.HandleCreated += new EventHandler(ctl_HandleCreated);
                }
                int i = this.bindList.IndexOf(oldField);
                if (i > -1)
                {
                    this.bindList[i] = field;
                }

                fieldsCount = bindList.Count;
                if (this._ReadOnly)
                {
                    ((IBind)ctl).ReadOnly = true;
                }
            }

        }

        #endregion

        #region IExtenderProvider Members

        private Hashtable fieldLists;
        private ArrayList bindList;
        private int fieldsCount;

        /// <summary>
        /// IExtenderProvider Members
        /// </summary>
        /// <param name="extendee"> the control object</param>
        /// <returns>true if the control can bind to data source</returns>
        public bool CanExtend(object extendee)
        {
            return (extendee is IBind);
        }

        /// <summary>
        /// Release all binding controls 
        /// </summary>
        public void ClearExtend()
        {
            this.fieldLists.Clear();
        }

        //[Editor("System.Windows.Forms.Design.DesignBindingEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        //[Editor("Nistec.WinForms.Design.DesignBindingEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        //[Editor("Nistec.WinForms.Design.DesignBindingEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]

        /// <summary>
        /// Get data field name for specific control in data source
        /// </summary>
        /// <param name="control">control</param>
        /// <returns>return data field name in data source</returns>
        public string GetDataField(Control control)
        {
            try
            {
                object text = fieldLists[control];
                if (text == null)
                {
                    text = string.Empty;
                }
                return text.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Set data field name for specific control in data source
        /// </summary>
        /// <param name="control">control</param>
        /// <param name="value">data field name in data source</param>
        public void SetDataField(Control control, object value)
        {
            try
            {
                if (!this.fieldLists.Contains(control))
                {
                    this.fieldLists.Add(control, value);
                    this.fieldsCount++;
                }
                else
                {
                    if ((value == null || value.ToString() == "") && (this.fieldLists.Count > 0))
                    {
                        this.fieldsCount--;
                    }
                    this.fieldLists[control] = value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindControls()
        {
            if (this.fieldLists == null || DataSource == null || this.fieldLists.Count == 0)
            {
                return;
            }
            BindHandelControls();

            fieldsCount = bindList.Count;
            if (this._ReadOnly)
            {
                SetReadOnly(true);
                SetReadOnlyProperty();
            }
        }

        private void BindHandelControls()
        {

            object dataSource = this.DataSource;
            string dataMember = this.DataMember;
            string bindProperty = "";
            string field = "";

            try
            {
                //Nistec.Util.Data.BindControl bind=new Nistec.Util.Data.BindControl();
                //this.ctlCostCode.DataBindings.Add(bind.BindToString("Text", this.dtblAccounts1, "Accounts.CostCode"));
                this.bindList.Clear();

                BindControl b = new BindControl();


                foreach (DictionaryEntry d in this.fieldLists)
                {
                    if ((d.Value != null) && !(d.Value.Equals("")))
                    {
                        if (dataMember != "")
                            field = string.Format("{0}.{1}", dataMember, d.Value);
                        else
                            field = d.Value.ToString();

                        Control ctl = (Control)d.Key;

                        //bindProperty=propertyNames[d.Key].ToString();
                        bindProperty = ((IBind)ctl).BindPropertyName();
                        //if(bindProperty!=null && bindProperty!="" )
                        //{
                        if (ctl.IsHandleCreated)
                        {

                            ctl.DataBindings.Clear();
                            ctl.DataBindings.Add(b.BindFormat(((IBind)ctl).BindFormat, bindProperty, dataSource, field));
                            //ctl.DataBindings.Add(bindProperty,dataSource,field);
                        }
                        else
                        {
                            ctl.HandleCreated += new EventHandler(ctl_HandleCreated);
                        }
                        ctl.TextChanged += new EventHandler(ctl_TextChanged);
                        this.bindList.Add(field);
                        if (!_AllowEdit)
                        {
                            ((IBind)ctl).ReadOnly = true;
                        }

                        //((Control)d.Key).DataBindings.Add(bind.BindToString( bindProperty,dataSource,field));
                        //}
                    }

                }
                intMcs = true;
            }
            catch (FormatException fex)
            {
                throw new FormatException(fex.Message + " at Field '" + field + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool intMcs = false;
        private bool textChanged = false;
        void ctl_TextChanged(object sender, EventArgs e)
        {
            if (intMcs)
                textChanged = true;
        }

        private void ctl_HandleCreated(object sender, EventArgs e)
        {
            string field = "";
            try
            {
                Control ctl = (Control)sender;

                field = this.fieldLists[ctl].ToString();

                if (this.DataMember != "" && field.IndexOf(".") < 0)
                {
                    field = string.Format("{0}.{1}", this.DataMember, field);
                }
                string bindProperty = ((IBind)sender).BindPropertyName();
                ctl.DataBindings.Clear();
                BindControl b = new BindControl();
                ctl.DataBindings.Add(b.BindFormat(((IBind)sender).BindFormat, bindProperty, this.DataSource, field));
                //ctl.DataBindings.Add(bindProperty,this.DataSource,field);

                ctl.HandleCreated -= new EventHandler(ctl_HandleCreated);

            }
            catch (FormatException fex)
            {
                throw new FormatException(fex.Message + " at Field '" + field + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void UnBindControls(bool clearField)
        {

            if (this.fieldLists == null || this.fieldLists.Count==0)
            {
                goto Label_Exit;
            }
            //this.DataBindings.Clear();
            try
            {
                foreach (DictionaryEntry d in this.fieldLists)
                {
                    if ((d.Value != null) && !(d.Value.Equals("")))
                    {
                        Control ctl = (Control)d.Key;

                        ctl.DataBindings.Clear();
                        ctl.HandleCreated -= new EventHandler(ctl_HandleCreated);
                        ctl.TextChanged -= new EventHandler(ctl_TextChanged);
                        if (clearField)
                        {
                            this.fieldLists[d.Key] = null;
                        }
                    }
                }
            }
            catch { }
Label_Exit:
            this.bindList.Clear();
            fieldsCount = 0;
        }

        private void BindControlsDefaultValue()
        {

            if (this.fieldLists == null)
            {
                return;
            }
            intMcs = false;
            foreach (DictionaryEntry d in this.fieldLists)
            {
                if ((d.Value != null) && !(d.Value.Equals("")))
                {
                    Control ctl = (Control)d.Key;
                    if (ctl is IBind)
                    {
                        ((IBind)ctl).BindDefaultValue();
                    }

                }
            }
            intMcs = true;
        }

        /// <summary>
        /// Get list of controls how binding to data source
        /// </summary>
        /// <returns></returns>
        public ArrayList BindingFields()
        {
            ArrayList roList = ArrayList.ReadOnly(this.bindList);
            return roList;
        }

        /// <summary>
        /// Get the number of controls how binding to data source
        /// </summary>
        public int FieldsCount
        {
            get { return this.fieldsCount; }
        }

        /// <summary>
        /// Set all binding control for read only property
        /// </summary>
        /// <param name="value"></param>
        public void SetReadOnly(bool value)
        {

            try
            {

                foreach (DictionaryEntry d in this.fieldLists)
                {
                    if ((d.Value != null) && (d.Key is IBind))
                    {
                        IBind ctl = (IBind)d.Key;

                        if (ctl != null)
                        {
                            ctl.ReadOnly = value;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetReadOnlyProperty()
        {
            if (this._ReadOnly)
            {
                this._AllowEdit = false;
                this._AllowDelete = false;
                this._AllowNew = false;
                McNav.ShowNew = false;
                McNav.ShowDelete = false;
                SetNavStatus(NavStatus.ReadOnly);//, false);
                ShowChangesButtons = false;
                this.ctlNavChanges.Visible = false;
                this.Invalidate();
            }

        }

        #endregion

        #region lblNavigator ang Key events

        private void lblNavigator_Validated(object sender, EventArgs e)
        {
            this.UpdateCount(false);
        }

        private void lblNavigator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    string s = this.lblNavigator.Text;
                    int l = s.IndexOf(":");
                    if (l > -1)
                    {
                        s = s.Substring(0, l);
                    }
                    int indx = int.Parse(s.Trim());
                    this.SetPosition(indx - 1, MoveOperator.MoveTo);
                }
                catch
                {
                    this.UpdateCount(false);
                }
            }
            else
            {
                PerformSendKey(e.KeyData);
            }

        }

        private void lblNavigator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                //
            }
            else if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void lblNavigator_MouseWheel(object sender, MouseEventArgs e)
        {
            PerformMouseWheel(e);
        }

        /// <summary>
        /// Execute ProcessCmdKey method and send the key to PerformSendKey method
        /// </summary>
        /// <param name="msg">string Message by ref</param>
        /// <param name="keyData">Keys</param>
        /// <returns></returns>
        public bool ProcessNavKey(ref Message msg, Keys keyData)
        {
            return ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return PerformSendKey(keyData);
        }

        /// <summary>
        /// Send the key data from Keys and move to the next position
        /// </summary>
        /// <param name="keyData">Keys(End,Home,PageDown,PageUp,Oemplus,OemMinus)</param>
        /// <returns>return true if Position change</returns>
        public virtual bool PerformSendKey(Keys keyData)
        {
            if (!IsBinding())
                return false;

            switch (keyData)
            {
                case Keys.End:
                    MoveLast();
                    return true;
                case Keys.Home:
                    MoveFirst();
                    return true;
                case Keys.PageDown:
                    MoveNext();
                    return true;
                case Keys.PageUp:
                    MovePrevious();
                    return true;
                case Keys.Control | Keys.Oemplus:
                    MoveNew();
                    return true;
                case Keys.Control | Keys.OemMinus:
                    DelRow();
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Send the mouse wheel EventArgs , clac the next position by MouseWheelScrollLines and move to the next position
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            if (_BindManager == null)
                return;

            int cnt = _BindManager.Count;
            if (!IsBinding())
                return;
            if (cnt <= 0)
                return;

            int CurrentInt = _SelectedIndex;
            int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            Delta = Delta * -1;

            if (CurrentInt + Delta >= cnt)
                MoveLast();// SelectedIndex =_Count-1;
            else if (CurrentInt + Delta < 0)
                MoveFirst();//internalList.SelectedIndex =0;
            else
                SelectedIndex += Delta;

        }

        #endregion

        #region sizing grip and progress

        [Category("Style")]
        public bool SizingGrip
        {
            get
            {
                return _SizingGrip;
                //if (ctlResize == null)
                //    return false;
                // return ctlResize.Visible;
            }
            set
            {
                if (_SizingGrip != value)
                {
                    _SizingGrip = value;
                    ctlResize.Visible = value;
                    SetLocation();
                    this.Invalidate();
                }
            }
        }

        private bool m_IsInProgress = false;

        public bool IsInProgress
        {
            get { return m_IsInProgress; }
        }

        public void StartProgress()
        {
            ShowSpinner();
            m_IsInProgress = true;
        }
        public void StopProgress()
        {
              ResetSpinner();
              m_IsInProgress = false;
        }

        private void ShowSpinner()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncDelegate(ShowSpinner));
                return;
            }
            this.ctlPictureBox.Image = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McSpinner"),
                                         "Nistec.WinForms.Images.spinner1.gif");

            this.Invalidate();
        }
        private void ResetSpinner()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncDelegate(ResetSpinner));
                return;
            }

            this.ctlPictureBox.Image = ResourceUtil.LoadImage(Global.ImagesPath + "nav" + navStatus.ToString() + ".gif");
        }
        #endregion

        #region InvokeDataSource

        private delegate DataTable AsyncDataSourceHandler(IDataReader reader, DataTable tblSchema, int fastFirstRows, int maxRows);
        private delegate void SetDataSourceCallBack(object source, bool isEnd);
        public event EventHandler LoadDataSourceEnd;
        private bool allowAddLoading;
        private int loadCounter;

        public int LoadCounter
        {
            get { return loadCounter; }
        }

        public void InvokeDataSource(IDbConnection connection, string sql, string mappingName, int fastFirstRows, int maxRows)
        {
            IDbCmd cmd = DbFactory.Create(connection);
            DataTable dtSchema = cmd.Adapter.GetSchemaTable(sql, SchemaType.Source);
            IDataReader reader = cmd.ExecuteReader(sql, CommandBehavior.CloseConnection);
            InvokeDataSource(reader, dtSchema, mappingName, fastFirstRows, maxRows);
        }

        public void InvokeDataSource(IDataReader reader, DataTable tblSchema, string mappingName, int fastFirstRows, int maxRows)
        {
            allowAddLoading = this.AllowAdd;
            this.AllowAdd = false;
            tblSchema.TableName = mappingName;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, fastFirstRows, maxRows, cb, null);
        }

        public void Invoke(IDataReader reader, string[] fields, string mappingName, int fastFirstRows, int maxRows)
        {

            if (fields.Length == 0)
            {
                throw new ArgumentException("Invalid fields ");
            }
            DataTable tblSchema = new DataTable();
            tblSchema.TableName = mappingName;
            for (int i = 0; i < fields.Length; i++)
            {
                tblSchema.Columns.Add(fields[i]);
            }

            allowAddLoading = this.AllowAdd;
            this.AllowAdd = false;
            //ctlSource.MappingName = mappingName;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, fastFirstRows, maxRows, cb, null);
        }

        public void InvokeDataSource(IDataReader reader, string mappingName)
        {
            DataTable tblSchema = new DataTable();
            allowAddLoading = this.AllowAdd;
            tblSchema.TableName = mappingName;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, 0, 0, cb, null);
        }

  
        private void RunAsyncCallback(IAsyncResult ar)
        {
            System.Threading.Thread th = System.Threading.Thread.CurrentThread;
            AsyncDataSourceHandler handler = (AsyncDataSourceHandler)((System.Runtime.Remoting.Messaging.AsyncResult)ar).AsyncDelegate;
            SetDataSource(handler.EndInvoke(ar), true);
            //this.AllowAdd = allowAddLoading;
            if (LoadDataSourceEnd != null)
                LoadDataSourceEnd(this, EventArgs.Empty);
        }

        private DataTable RunAsyncCall(IDataReader reader, DataTable tbl, int fastFirstRows, int maxRows)
        {
            try
            {
                //tbl.TableName = this.MappingName;

                tbl.BeginLoadData();

                if (fastFirstRows == 0)
                {
                    tbl.Load(reader, LoadOption.Upsert);
                }
                else
                {
                    loadCounter = 0;
                    int fieldsCount = reader.FieldCount;
                    object[] values = new object[fieldsCount];
                    while (reader.Read() && ((loadCounter < maxRows) || (maxRows == 0)))
                    {
                        values.Initialize();
                        for (int i = 0; i < fieldsCount; i++)
                        {
                            values[i] = reader[i];
                        }

                        tbl.LoadDataRow(values, LoadOption.Upsert);
                        if (loadCounter == fastFirstRows)
                        {
                            SetDataSource(tbl, false);
                        }
                        loadCounter++;
                    }
                }

                tbl.EndLoadData();
                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {

                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                catch { }
            }
        }

        private void SetDataSource(object source, bool isEnd)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceCallBack(SetDataSource), source, isEnd);
            }
            else
            {
                this.DataSource = source;
                if (isEnd)
                {
                    this.AllowAdd = allowAddLoading;
                }
            }
        }

        #endregion

    }

}
