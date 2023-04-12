using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using Nistec.Data;

using Nistec.Data.Advanced;
using Nistec.Data.Factory;
using Nistec.Win;

namespace Nistec.WinForms
{


    [ToolboxItem(true), Designer(typeof(Design.NavBarDesigner))]
    [ToolboxBitmap(typeof(McNavBar), "Toolbox.NavBar.bmp")]
    public class McNavBar : McNavBase
    {

        #region Members

        private DBProvider dbProvider;
        private System.Data.IDbDataAdapter _DA;
        private System.Data.IDbConnection _Conn;

        public delegate void RowUpdatedEventHandler(object sender, RowUpdatedEventArgs e);
        public delegate void RowUpdatingEventHandler(object sender, RowUpdatingEventArgs e);

        public event RowUpdatingEventHandler RowUpdating;
        public event RowUpdatedEventHandler RowUpdated;

        #endregion

        #region Constructor

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_useViewManager)
                {
                    ViewManager.Dispose();
                }

            }
            base.Dispose(disposing);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_DA != null)
            {
                UnWireDA();
            }

        }
        #endregion

        #region Properties

        public DBProvider DBProvider
        {
            get { return dbProvider; }
            set
            {
                dbProvider = value;
            }
        }

        /// <summary>
        /// DbDataAdapter
        /// </summary>
        public System.Data.IDbDataAdapter ManagerDataAdapter
        {
            get { return _DA; }
            set
            {
                if (_DA != null)
                {
                    UnWireDA();
                }
                _DA = value;
                if (_DA != null)
                {
                    WireDA();
                }

            }
        }

        /// <summary>
        /// Connection
        /// </summary>
        public System.Data.IDbConnection Connection
        {
            get { return _Conn; }
            set
            {
                _Conn = value;
                if (IsHandleCreated && !base.initialising)
                {
                    if (_Conn != null && _DA == null)
                    {
                        CreateCommandBuilder();
                    }
                }
            }
        }

        #endregion

        #region absract Methods

        public override void EndInit()
        {
            base.EndInit();
            if (_Conn != null && _DA == null)
            {
                CreateCommandBuilder();
            }
         }

        public virtual void Init(object dataSource, string dataMember, bool bindControls, IDbConnection connection)
        {
            base.Init(dataSource, dataMember, bindControls);
            _Conn = connection;

            if (_Conn != null && _DA == null)
            {
                CreateCommandBuilder();
            }
        }

        private bool IsAdapterOK()
        {
            if (_DA != null)
            {
                //if(_DA.TableMappings.Count >0 )
                //{
                return true;
                //}
            }
            return false;
        }

        protected override int DataAdpterUpdate(DataRow[] dataRows)
        {
            if (!IsAdapterOK())
                return 0;
            try
            {
                if (dbProvider == DBProvider.SqlServer)
                {
                    return ((SqlDataAdapter)_DA).Update(dataRows);
                }
                else //if(dbProvider==DBProvider.OleDb)
                {
                    return ((OleDbDataAdapter)_DA).Update(dataRows);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override int DataAdpterUpdate(DataTable dataTable)
        {
            if (!IsAdapterOK())
                return 0;
            try
            {
                if (dbProvider == DBProvider.SqlServer)
                {
                    return ((SqlDataAdapter)_DA).Update(dataTable);
                }
                else //if(dbProvider==DBProvider.OleDb)
                {
                    return ((OleDbDataAdapter)_DA).Update(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override int DataAdpterUpdate(DataSet dataSet, string dataMember)
        {
            if (!IsAdapterOK())
                return 0;
            try
            {
                if (dbProvider == DBProvider.SqlServer)
                {
                    return ((SqlDataAdapter)_DA).Update(dataSet, dataMember);
                }
                else //if(dbProvider==DBProvider.OleDb)
                {
                    return ((OleDbDataAdapter)_DA).Update(dataSet, dataMember);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override int DataAdpterUpdate(DataSet dataSet)
        {
            if (!IsAdapterOK())
                return 0;
            DataTable dtChanges = null;
            string selectCommand = "";
            int res = 0;
            Relation relation = null;

            try
            {

                if (IsNew)
                {
                    object key = null;
                    dtChanges = this._DataMaster.GetChanges(DataRowState.Added);
                    if (dtChanges != null && !dtChanges.HasErrors)
                    {
                        res = DataAdpterUpdate(dtChanges);
                        if (ViewManager.PrimaryKey != null && ViewManager.PrimaryKey != "")
                        {
                            key = CurrentRowView[ViewManager.PrimaryKey];
                        }

                        foreach (DataTable dt in _DataSet.Tables)
                        {
                            if (dt != _DataMaster)
                            {
                                dtChanges = dt.GetChanges(DataRowState.Added);
                                relation = ViewManager.relationList[dt.TableName];

                                foreach (DataRow dr in dtChanges.Rows)
                                {
                                    dr[relation.ForiegnKey] = key;
                                }
                                relation = ViewManager.relationList[dt.TableName];
                                selectCommand = relation.CommandSelect;
                                res += UpdateCommand(dtChanges, selectCommand, relation.TableMapping, ViewManager.MappingAction, ViewManager.SchemaAction);

                                //res+=DataAdpterUpdate(dtChanges);
                            }
                        }
                        return res;
                    }
                }
                foreach (DataTable dt in dataSet.Tables)
                {
                    dtChanges = dt.GetChanges();
                    if (dtChanges != null)
                    {
                        if (dt.TableName.Equals(_DataMaster.TableName))
                        {
                            if (dbProvider == DBProvider.SqlServer)
                            {
                                res += ((SqlDataAdapter)_DA).Update(dtChanges);
                            }
                            else
                            {
                                res += ((OleDbDataAdapter)_DA).Update(dtChanges);
                            }
                        }
                        else
                        {
                            relation = ViewManager.relationList[dt.TableName];
                            selectCommand = relation.CommandSelect;
                            res += UpdateCommand(dtChanges, selectCommand, relation.TableMapping, ViewManager.MappingAction, ViewManager.SchemaAction);
                        }

                    }
                }

                return res;//((OleDbDataAdapter)_DA).Update(dataSet); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int UpdateCommand(DataTable dt, string selectCommand, object tableMappings, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
        {

            //string selectCommand=ViewManager.relationList[dt.TableName].commandSelect;
            int res = 0;


            if (dbProvider == DBProvider.SqlServer)
            {
                System.Data.SqlClient.SqlDataAdapter sqlDa = new System.Data.SqlClient.SqlDataAdapter();

                if (tableMappings != null) sqlDa.TableMappings.Add(tableMappings);
                sqlDa.MissingMappingAction = mappingAction;
                sqlDa.MissingSchemaAction = schemaAction;
                sqlDa.SelectCommand = new System.Data.SqlClient.SqlCommand(selectCommand, _Conn as SqlConnection);
                System.Data.SqlClient.SqlCommandBuilder sqlCb = new System.Data.SqlClient.SqlCommandBuilder(sqlDa);
                sqlDa.ContinueUpdateOnError = false;
                sqlDa.RowUpdating += new SqlRowUpdatingEventHandler(SqlDA_RowUpdating);
                sqlDa.RowUpdated += new SqlRowUpdatedEventHandler(SqlDA_RowUpdated);
                res = sqlDa.Update(dt);
                sqlDa.RowUpdating -= new SqlRowUpdatingEventHandler(SqlDA_RowUpdating);
                sqlDa.RowUpdated -= new SqlRowUpdatedEventHandler(SqlDA_RowUpdated);

            }
            else //if(dbProvider==DBProvider.OleDb)
            {
                System.Data.OleDb.OleDbDataAdapter OleDbDa = new System.Data.OleDb.OleDbDataAdapter();
                if (tableMappings != null) OleDbDa.TableMappings.Add(tableMappings);
                OleDbDa.MissingMappingAction = mappingAction;
                OleDbDa.MissingSchemaAction = schemaAction;
                OleDbDa.SelectCommand = new System.Data.OleDb.OleDbCommand(selectCommand, _Conn as OleDbConnection);
                System.Data.OleDb.OleDbCommandBuilder OleDbCb = new System.Data.OleDb.OleDbCommandBuilder(OleDbDa);
                OleDbDa.ContinueUpdateOnError = false;
                OleDbDa.RowUpdating += new OleDbRowUpdatingEventHandler(OleDbDA_RowUpdating);
                OleDbDa.RowUpdated += new OleDbRowUpdatedEventHandler(OleDbDA_RowUpdated);
                res = OleDbDa.Update(dt);
                OleDbDa.RowUpdating -= new OleDbRowUpdatingEventHandler(OleDbDA_RowUpdating);
                OleDbDa.RowUpdated -= new OleDbRowUpdatedEventHandler(OleDbDA_RowUpdated);
            }
            return res;
        }



        private void CreateCommandBuilder()
        {
            if (_DA == null)
            {
                if (dbProvider == DBProvider.SqlServer)
                {
                    System.Data.SqlClient.SqlDataAdapter sqlDa = new System.Data.SqlClient.SqlDataAdapter();
                    sqlDa.SelectCommand = new System.Data.SqlClient.SqlCommand(base.commandSelect, _Conn as SqlConnection);
                    System.Data.SqlClient.SqlCommandBuilder sqlCb = new System.Data.SqlClient.SqlCommandBuilder(sqlDa);
                    sqlDa.ContinueUpdateOnError = false;
                    _DA = sqlDa;
                    WireSqlDA();
                }
                else //if(dbProvider==DBProvider.OleDb)
                {
                    System.Data.OleDb.OleDbDataAdapter OleDbDa = new System.Data.OleDb.OleDbDataAdapter();
                    OleDbDa.SelectCommand = new System.Data.OleDb.OleDbCommand(base.commandSelect, _Conn as OleDbConnection);
                    System.Data.OleDb.OleDbCommandBuilder OleDbCb = new System.Data.OleDb.OleDbCommandBuilder(OleDbDa);
                    OleDbDa.ContinueUpdateOnError = false;
                    _DA = OleDbDa;
                    WireOleDbDA();
                }

            }
            //			foreach(DataTable tb in _DataSet.Tables)
            //			{
            //				_DA.TableMappings.Add(tb.TableName,_DataSet.DataSetName);
            //			}
        }


        protected override void DoRefresh()
        {
            if (_DA != null)// && _Conn!=null)
            {
                //Data.IDBCmd cmd = Nistec.Data.DBUtil.Create(_Conn);//(this._Conn.ConnectionString, this.dbProvider);
                //cmd.DataAdapter = _DA;
                DataTable ds = _DataMaster.Clone();
                if (dbProvider == DBProvider.SqlServer)
                {
                    System.Data.SqlClient.SqlDataAdapter sqlDa = _DA as System.Data.SqlClient.SqlDataAdapter;
                    sqlDa.Fill(ds);
                }
                else //if(dbProvider==DBProvider.OleDb)
                {
                    System.Data.OleDb.OleDbDataAdapter OleDbDa = _DA as System.Data.OleDb.OleDbDataAdapter;
                    OleDbDa.Fill(ds);
                }
                //this.SetDataBinding(ds,"");
                this.Init(ds, "");

            }
        }

        #endregion

        #region WireDataAdpter

        private void WireDA()
        {
            if (dbProvider == DBProvider.SqlServer)
            {
                WireSqlDA();
            }
            else
            {
                WireOleDbDA();
            }
        }

        private void UnWireDA()
        {
            if (dbProvider == DBProvider.SqlServer)
            {
                UnWireSqlDA();
            }
            else
            {
                UnWireOleDbDA();
            }
        }

        private void WireSqlDA()
        {
            ((SqlDataAdapter)_DA).RowUpdating += new SqlRowUpdatingEventHandler(SqlDA_RowUpdating);
            ((SqlDataAdapter)_DA).RowUpdated += new SqlRowUpdatedEventHandler(SqlDA_RowUpdated);
        }

        private void WireOleDbDA()
        {
            ((OleDbDataAdapter)_DA).RowUpdating += new OleDbRowUpdatingEventHandler(OleDbDA_RowUpdating);
            ((OleDbDataAdapter)_DA).RowUpdated += new OleDbRowUpdatedEventHandler(OleDbDA_RowUpdated);
        }

        private void UnWireSqlDA()
        {
            ((SqlDataAdapter)_DA).RowUpdating -= new SqlRowUpdatingEventHandler(SqlDA_RowUpdating);
            ((SqlDataAdapter)_DA).RowUpdated -= new SqlRowUpdatedEventHandler(SqlDA_RowUpdated);
        }

        private void UnWireOleDbDA()
        {
            ((OleDbDataAdapter)_DA).RowUpdating -= new OleDbRowUpdatingEventHandler(OleDbDA_RowUpdating);
            ((OleDbDataAdapter)_DA).RowUpdated -= new OleDbRowUpdatedEventHandler(OleDbDA_RowUpdated);
        }

        #endregion

        #region internal Events

        private void SqlDA_RowUpdating(object sender, SqlRowUpdatingEventArgs e)
        {
            OnRowUpdating(e);
            if (e.Status == System.Data.UpdateStatus.ErrorsOccurred)
                MessageBox.Show(e.Errors.Message, MessageTitle);
        }

        private void SqlDA_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            OnRowUpdated(e);
            //			if(_ShowRecordsAffected)
            //			{
            //				if(e.RecordsAffected >0)
            //					Nistec.WinForms.MsgDlg.OpenMsg("RecordsAffected " + e.RecordsAffected.ToString (),"Nistec");
            //				else
            //					Nistec.WinForms.MsgDlg.OpenMsg ("No RecordsAffected. ",_MessageTitle );
            //			}
        }

        private void OleDbDA_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            OnRowUpdating(e);
            if (e.Status == System.Data.UpdateStatus.ErrorsOccurred)
                MessageBox.Show(e.Errors.Message, MessageTitle);
        }

        private void OleDbDA_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            OnRowUpdated(e);
            //			if(_ShowRecordsAffected)
            //			{
            //				if(e.RecordsAffected >0)
            //					Nistec.WinForms.MsgDlg.OpenMsg("RecordsAffected " + e.RecordsAffected.ToString (),"Nistec");
            //				else
            //					Nistec.WinForms.MsgDlg.OpenMsg ("No RecordsAffected. ",_MessageTitle );
            //			}
        }

        protected virtual void OnRowUpdating(RowUpdatingEventArgs e)
        {
            if (RowUpdating != null)
                RowUpdating(this, e);
        }

        protected virtual void OnRowUpdated(RowUpdatedEventArgs e)
        {
            if (RowUpdated != null)
                RowUpdated(this, e);
        }

        #endregion

        #region ViewManager

        private DataSourceManager _ViewManager;

        public DataSourceManager ViewManager
        {
            get
            {
                if (_ViewManager == null)
                {
                    _ViewManager = new DataSourceManager(this);
                }
                return _ViewManager;
            }
        }

        protected override void OnBindPositionChanged(EventArgs e)
        {
            ViewManager.ExecRelation();
            base.OnBindPositionChanged(e);
        }


        public class DataSourceManager : IDisposable
        {
            #region memenbers
            internal McNavBar Owner;
            internal ViewCollection viewList;
            internal RelationCollection relationList;
            private int Count;
            private object newKeyValue;
            private bool schemaActionWithKey;
            private MissingSchemaAction missingSchemaAction;
            private MissingMappingAction missingMappingAction;
            private string primaryKey;
            #endregion



            #region ctor

            internal DataSourceManager(McNavBar owner)
            {
                this.Owner = owner;
                //this.Owner.BindPositionChanged += new EventHandler(Owner_BindPositionChanged);
                relationList = new RelationCollection();
                viewList = new ViewCollection();
                missingMappingAction = MissingMappingAction.Passthrough;
                missingSchemaAction = MissingSchemaAction.Add;
            }

            void Owner_BindPositionChanged(object sender, EventArgs e)
            {
                //for (int i = 1; i < viewList.Count; i++)
                //{
                //    viewList[i].RowFilter = "";
                //}

            }

            ~DataSourceManager()
            {
                if (Owner._useViewManager)
                {
                    Dispose();
                }
            }
            public void Dispose()
            {
                for (int i = 1; i < viewList.Count;i++)
                {
                    viewList[i].Table.TableNewRow -= new DataTableNewRowEventHandler(dt_TableNewRow);
                }

                viewList.Clear();
                relationList.Clear();
                Count = 0;
                newKeyValue = null;
                Owner._useViewManager = false;
            }

            #endregion

            #region Add methods

            public void Add(DataSet ds, string parentMappingName, Relation relation, string sort)
            {
                Add(ds, parentMappingName, new Relation[] { relation }, sort, true, null);
            }

            public void Add(DataSet ds, string parentMappingName, Relation relation, string sort, IDbConnection connection)
            {
                Add(ds, parentMappingName, new Relation[] { relation }, sort, true, connection);
            }

            public void Add(DataSet ds, string parentMappingName, Relation[] relation, string sort)
            {
                Add(ds, parentMappingName, relation, sort, true, null);
            }

            public void Add(DataSet ds, string parentMappingName, Relation[] relation, string sort, IDbConnection connection)
            {
                Add(ds, parentMappingName, relation, sort, true, null);
            }

            public void Add(DataSet ds, string parentMappingName, string sort, bool bindControls, IDbConnection connection)
            {
                Owner.Init(ds, parentMappingName, bindControls, connection);
                Relation[] relation = Relation.DataRelationConvert(ds);
                Add(ds, parentMappingName, relation, sort, bindControls, connection);
            }

            public void Add(DataSet ds, string parentMappingName, Relation[] relation, string sort, bool bindControls, IDbConnection connection)
            {
                Owner.Init(ds, parentMappingName, bindControls, connection);
                Owner.Sort = sort;
                if (Owner._DA != null)
                {
                    Owner._DA.TableMappings.Clear();
                    Owner._DA.TableMappings.Add(parentMappingName, parentMappingName);
                }
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    AddChildView(ds.Tables[i], ds.Tables[i].TableName, relation[i - 1], "");
                }
            }

            public void AddParent(DataTable dt, string mappingName, string sort)
            {
                Owner.Init(dt, mappingName);
                Owner.Sort = sort;
                if (Owner._DA != null)
                {
                    Owner._DA.TableMappings.Clear();
                    Owner._DA.TableMappings.Add(mappingName, dt.TableName);
                }
            }
            public void AddParent(DataTable dt, string mappingName, string sort, bool bindControls, IDbConnection connection)
            {
                Owner.Init(dt, "", bindControls, connection);
                Owner.Sort = sort;
                if (Owner._DA != null)
                {
                    Owner._DA.TableMappings.Clear();
                    Owner._DA.TableMappings.Add(mappingName, dt.TableName);
                }
            }

            public void AddChild(DataTable dt, string mappingName, Relation relation)
            {
                AddChild(dt, mappingName, relation, "");
            }

            public void AddChild(DataTable dt, string mappingName, Relation relation, string sort)
            {
                if (Owner._DataSet.Tables.Contains(mappingName))
                {
                    throw new ArgumentException("Duplicate mapping name", mappingName);
                }
                Owner._DataSet.Tables.Add(dt);
                AddChildView(dt, mappingName, relation, sort);
            }

            private void AddChildView(DataTable dt, string mappingName, Relation relation, string sort)
            {
                if (relation == null)
                {
                    throw new ArgumentException("CanNotUse empty relation");
                }
                if (Owner._DataSet == null)
                {
                    throw new ArgumentException("CanNotUseDataViewManager");
                }
                if (viewList.Count == 0)
                {
                    viewList.Add(Owner.DataList);
                }
                dt.TableName = mappingName;
                dt.TableNewRow += new DataTableNewRowEventHandler(dt_TableNewRow);
                relation.RelationName = mappingName;
                relation.CommandSelect = QueryBuilder.GetSelectCommand(dt, mappingName);
                relationList.Add(relation);
                DataView view = new DataView(dt);
                view.Sort = sort;
                view.RowFilter = GetRelation(relationList.Count - 1, Owner.IsNew);
                viewList.Add(view);
                Count = viewList.Count;
                if (Owner._DA != null)
                {
                    Owner._DA.TableMappings.Add(mappingName, dt.TableName);
                    Owner._DA.MissingMappingAction = MappingAction;
                    Owner._DA.MissingSchemaAction = SchemaAction;
                }
                Owner._useViewManager = true;
            }

            void dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
            {
                string mapName=e.Row.Table.TableName;
                string foriegnKey = relationList[mapName].ForiegnKey;
                if (!string.IsNullOrEmpty(foriegnKey) && !string.IsNullOrEmpty(PrimaryKey))
                {
                    e.Row[foriegnKey] = Owner.CurrentRowView[PrimaryKey];
                }
                //for (int i = 1; i < viewList.Count;i++ )
                //{
                //    if (viewList[i].Table.TableName.Equals(mapName))
                //    {
                //        e.Row[relationList[i-1].ForiegnKey] = Owner.CurrentRowView[PrimaryKey];
                //        break;
                //    }
                //}

                //throw new Exception("The method or operation is not implemented.");
            }

            #endregion

            #region methods

            public int UpdateChanges()
            {
                return Owner.UpdateDataSet();
            }

            public void RemoveChild(string mappingName)
            {
                int i = 0;
                foreach (DataTable dt in Owner._DataSet.Tables)
                {
                    if (dt.TableName.Equals(mappingName))
                    {
                        break;
                    }
                    i++;
                }
                RemoveChild(i);
            }

            public void RemoveChild(int index)
            {
                if (index < 0 || index > Count)
                {
                    throw new IndexOutOfRangeException("Index Out Of RangeType");
                }
                viewList.RemoveAt(index);
                relationList.RemoveAt(index - 1);
                Count = viewList.Count;
            }

            private string GetRelation(int index, bool isNew)
            {
                if (isNew)
                {
                    if (schemaActionWithKey)
                    {
                        goto Label_01;
                    }

                    if (newKeyValue == null)// || newKeyValue.Length == 0)
                    {
                        throw new ArgumentException("new key value is empty");
                    }
                }

            Label_01:
                string filter = "";
                string prefix = "";
                string sufix = "";
                object val = null;
                int i = 0;
                if (isNew && !schemaActionWithKey)
                {
                    foreach (string s in relationList[index].ParentColumnsName)
                    {
                        sufix = GetSufix(s);
                        filter += string.Format("{0}{1}={3}{2}{3}", prefix, relationList[index].ChildColumnsName[i], newKeyValue/*[i]*/, sufix);
                        prefix = " AND ";
                        i++;
                    }

                }
                else
                {
                    foreach (string s in relationList[index].ParentColumnsName)
                    {
                        sufix = GetSufix(s);
                        val = Owner.CurrentRowView[s];
                        if (val == null || val.ToString()=="")
                            val= "-1";
                        filter += string.Format("{0}{1}={3}{2}{3}", prefix, relationList[index].ChildColumnsName[i], val, sufix);
                        prefix = " AND ";
                        i++;
                    }
                }

                return filter;
            }

            private string GetSufix(string column)
            {
                if (Owner._DataMaster.Columns[column].DataType == typeof(string))
                {
                    return "'";
                }
                else if (Owner._DataMaster.Columns[column].DataType == typeof(DateTime))
                {
                    return Owner.dbProvider == DBProvider.OleDb ? "#" : "'";
                }
                return "";
            }

            internal void ExecRelation()
            {
                try
                {
                    bool isNew = Owner.IsNew;
                    for (int i = 1; i < viewList.Count; i++)
                    {
                        string filter = GetRelation(i - 1, isNew);
                        viewList[i].RowFilter = filter;
                        //relationList[i-1].ComuteExpresion(viewList[i],filter);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.ShowError(ex.Message);
                }
            }

            //			public object GetComputeResult(string mappingName,string name)
            //			{
            //				Relation r = relationList[mappingName];
            //				if(r!=null)
            //				{
            //					foreach(Compute c in r.ComputeList)
            //					{
            //						if(c._name.Equals(name))
            //							return c._result;
            //					}
            //				}
            //				return null;
            //			}
            //
            //			public object GetComputeResult(string name)
            //			{
            //				foreach(Relation r in relationList)
            //				{
            //					foreach(Compute c in r.ComputeList)
            //					{
            //						if(c._name.Equals(name))
            //							return c._result;
            //					}
            //
            //				}
            //				return null;
            //			}

            public object ComuteExpression(string mappingName, string expression, string filter)
            {
                DataView dv = viewList[mappingName];
                if (dv == null) return null;
                return dv.Table.Compute(expression, filter);
            }

            public object ComuteExpression(string mappingName, string expression)
            {
                DataView dv = viewList[mappingName];
                if (dv == null) return null;
                return dv.Table.Compute(expression, dv.RowFilter);
            }

            //			public string ComuteExpression(string mappingName,string expression,object valueIfNull, string format)
            //			{
            //				DataView dv=viewList[mappingName];
            //				if(dv==null)
            //					return valueIfNull.ToString(format);
            //				object res= dv.Table.Compute(expression,dv.RowFilter);
            //				if(res==null)
            //					res=valueIfNull;
            //				res.ToString(format);
            //			}


            //			public void AddComputeExpresion(string mappingName, Compute[] comute)
            //			{
            //				Relation r = relationList[mappingName];
            //				if(r==null)
            //				{
            //					MsgBox.ShowError("Invalid Relation name or mapping " + mappingName);
            //				}
            //				r.ComputeList=comute;
            //			}

            #endregion

            #region properties

            public string PrimaryKey
            {
                get { return primaryKey; }
                set { primaryKey = value; }
            }


            public object NewKeyValues
            {
                get { return newKeyValue; }
                set { newKeyValue = value; }
            }

            //			public bool SchemaActionWithKey
            //			{
            //				get{return schemaActionWithKey;}
            //				set{schemaActionWithKey=value;}
            //			}

            public MissingSchemaAction SchemaAction
            {
                get { return missingSchemaAction; }
                set
                {
                    missingSchemaAction = value;
                    schemaActionWithKey = (missingSchemaAction == MissingSchemaAction.AddWithKey) ? true : false;
                }
            }

            public MissingMappingAction MappingAction
            {
                get { return missingMappingAction; }
                set { missingMappingAction = value; }
            }


            public DataView this[int index]
            {
                get
                {
                    return viewList[index];
                }
            }

            public DataView this[string name]
            {
                get
                {
                    return viewList[name];
                }
            }

            #endregion

        }

        public class ViewCollection : CollectionBase
        {

            public void Add(DataView dv)
            {
                base.List.Add(dv);
            }

            public DataView this[int index]
            {
                get { return (DataView)base.List[index] as DataView; }
            }

            public DataView this[string name]
            {
                get
                {
                    int i = 0;
                    foreach (DataView dv in this.List)
                    {
                        if (dv.Table.TableName.Equals(name))
                        { break; }
                        i++;
                    }
                    return (DataView)base.List[i] as DataView;
                }
            }

        }


        #endregion

 
 
    }

}
