using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using Nistec.Data.Common;
using Nistec.Data.Factory;

namespace Nistec.Data.Advanced
{
    /// <summary>
    /// IDataSource
    /// </summary>
    public interface IDataSource
    {
        object DataSource { get; set;}
        string DataMember { get; set;}
        //string MappingName { get; set;}
        bool AllowAdd { get; set;}
        bool InvokeRequired { get;}
        object Invoke(Delegate callBack, params object[] param);
    }

    /// <summary>
    /// AsyncDataSource
    /// </summary>
    public class AsyncDataSource
    {
        /// <summary>
        /// DataSourceUtil ctor
        /// </summary>
        /// <param name="ctl"></param>
        public AsyncDataSource(IDataSource ctl)
        {
            ctlSource = ctl;
            completed = false;
        }

        #region InvokeDataSource
        private IDataSource ctlSource;
        private delegate DataTable AsyncDataSourceHandler(IDataReader reader, DataTable tblSchema, int fastFirstRows, int maxRows);
        private delegate void SetDataSourceCallBack(object source, bool isEnd);
        public event EventHandler LoadDataSourceEnd;
        private bool allowAddLoading;
        private int index;
        private bool completed;

        /// <summary>
        /// Get Record index
        /// </summary>
        public int RecordIndex
        {
            get { return index; }
        }

        /// <summary>
        /// Get indicate that InvokeDataSource completed 
        /// </summary>
        public bool IsCompleted
        {
            get { return completed; }
        }

        /// <summary>
        /// InvokeDataSource
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void Invoke(IDbConnection connection, string sql, string mappingName, int fastFirstRows, int maxRows)
        {
            IDbCmd cmd = DbFactory.Create(connection);
            //DataTable dtSchema = cmd.GetSchemaTable(sql, SchemaType.Source);
            DataTable dtSchema = cmd.Adapter.GetSchemaTable(sql, SchemaType.Source);

            IDataReader reader = cmd.ExecuteReader(sql, CommandBehavior.CloseConnection);
            Invoke(reader, dtSchema, mappingName, fastFirstRows, maxRows);
        }
        /// <summary>
        /// InvokeDataSource
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tblSchema"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void Invoke(IDataReader reader, DataTable tblSchema, string mappingName, int fastFirstRows, int maxRows)
        {
            allowAddLoading = ctlSource.AllowAdd;
            ctlSource.AllowAdd = false;
            //ctlSource.MappingName = mappingName;
            tblSchema.TableName = mappingName;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, fastFirstRows, maxRows, cb, null);
        }
        /// <summary>
        /// InvokeDataSource
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fields"></param>
        /// <param name="mappingName"></param>
        /// <param name="fastFirstRows"></param>
        /// <param name="maxRows"></param>
        public void Invoke(IDataReader reader,string[] fields, string mappingName, int fastFirstRows, int maxRows)
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

            allowAddLoading = ctlSource.AllowAdd;
            ctlSource.AllowAdd = false;
            //ctlSource.MappingName = mappingName;
            AsyncDataSourceHandler handler = new AsyncDataSourceHandler(RunAsyncCall);
            AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            handler.BeginInvoke(reader, tblSchema, fastFirstRows, maxRows, cb, null);
        }
        /// <summary>
        /// InvokeDataSource
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="mappingName"></param>
        public void Invoke(IDataReader reader, string mappingName)
        {
            DataTable tblSchema = new DataTable();
            tblSchema.TableName = mappingName;
            allowAddLoading = ctlSource.AllowAdd;
            //ctlSource.MappingName = mappingName;
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
            completed = true;
            if (LoadDataSourceEnd != null)
                LoadDataSourceEnd(this, EventArgs.Empty);
        }

        private DataTable RunAsyncCall(IDataReader reader, DataTable tbl, int fastFirstRows, int maxRows)
        {
            try
            {
                //tbl.TableName = ctlSource.MappingName;

                tbl.BeginLoadData();

                if (fastFirstRows == 0)
                {
                    tbl.Load(reader, LoadOption.Upsert);
                }
                else
                {
                    index = 0;
                    int fieldsCount = reader.FieldCount;
                    object[] values = new object[fieldsCount];
                    while (reader.Read() && ((index < maxRows) || (maxRows == 0)))
                    {
                        values.Initialize();
                        for (int i = 0; i < fieldsCount; i++)
                        {
                            values[i] = reader[i];
                        }

                        tbl.LoadDataRow(values, LoadOption.Upsert);
                        if (index == fastFirstRows)
                        {
                            SetDataSource(tbl, false);
                        }
                        index++;
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
            if (ctlSource.InvokeRequired)
            {
                ctlSource.Invoke(new SetDataSourceCallBack(SetDataSource), source, isEnd);
            }
            else
            {
                ctlSource.DataSource = source;
                if (isEnd)
                {
                    ctlSource.AllowAdd = allowAddLoading;
                }
            }
        }

        #endregion

    }
}
