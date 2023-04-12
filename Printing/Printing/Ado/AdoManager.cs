namespace Nistec.Printing.Data
{
    using System;
    using System.Data;

    public abstract class AdoManagerBase
    {
        protected string _lastError = "";

        protected AdoManagerBase()
        {
        }

        
        public bool CreateTable(IDbConnection connection, DataTable schema)
        {
            if ((connection == null) || (schema == null))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(schema.TableName))
            {
                return this.CreateTable(connection, schema, schema.TableName);
            }
            return this.CreateTable(connection, schema, "NewTable");
        }

        public abstract bool CreateDatabase(IDbConnection connection);//,string databaseName);
        public abstract bool CreateTable(IDbConnection connection, DataTable schema, string tableName);
        public abstract bool DeleteTable(IDbConnection connection, string tableName);
        public abstract string[] GetStoredProcedures(IDbConnection connection);
        public abstract string[] GetTables(IDbConnection connection);
        public abstract AdoTable GetTableSchema(IDbConnection connection, string tableName);
        public abstract string[] GetViews(IDbConnection connection);
        public virtual bool ProcedureExists(IDbConnection connection, string procedureName)
        {
            foreach (string str in this.GetViews(connection))
            {
                if (str == procedureName)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool TableExists(IDbConnection connection, string tableName)
        {
            foreach (string str in this.GetTables(connection))
            {
                if (str == tableName)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool TestConnection(IDbConnection connection)
        {
            bool flag = false;
            this._lastError = "";
            try
            {
                connection.Open();
                flag = true;
            }
            catch (Exception exception)
            {
                this._lastError = exception.Message;
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        public virtual bool ViewExists(IDbConnection connection, string viewName)
        {
            foreach (string str in this.GetViews(connection))
            {
                if (str == viewName)
                {
                    return true;
                }
            }
            return false;
        }

        public string LastError
        {
            get
            {
                return this._lastError;
            }
        }
    }
}

