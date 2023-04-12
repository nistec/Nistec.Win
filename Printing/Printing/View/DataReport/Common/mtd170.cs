namespace Nistec.Printing.View
{
    using System;
    using System.Data;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Reflection;

    internal class DataFields//mtd170
    {
        protected FieldsCollection _Fields;
        protected bool _mtd175;
        protected Report _Report;
        protected CodeProvider _CodeProvider;
        protected int _mtd179 = -1;

        internal DataFields(Report var0, CodeProvider var1)
        {
            this._Report = var0;
            this._Fields = var0.Fields;
            this._CodeProvider = var1;
        }

        internal virtual void mtd111()
        {
            this._Report.EOF = false;
            this._mtd179 = -1;
            this._mtd175 = false;
            if ((this._Fields != null) && (this._Fields.Count > 0))
            {
                for (int i = 0; i < this._Fields.Count; i++)
                {
                    this._Fields[i].mtd111();
                }
            }
        }

        internal virtual void mtd172()
        {
        }

        internal virtual void mtd174()
        {
        }

        internal virtual bool mtd186(object var6)
        {
            return false;
        }

        internal object mtd193(int var4)
        {
            if (this._Fields != null)
            {
                foreach (McField field in this._Fields)
                {
                    if ((field.ColIndex == -1) || (field.ColIndex != var4))
                    {
                        continue;
                    }
                    if (!this._mtd175)
                    {
                        return field.mtd106;
                    }
                    return field.mtd107;
                }
            }
            return DBNull.Value;
        }

        internal object mtd193(string var5)
        {
            if (this._Fields != null)
            {
                McField field = this._Fields[var5];
                if (field != null)
                {
                    if (!this._mtd175)
                    {
                        return field.mtd106;
                    }
                    return field.mtd107;
                }
            }
            return DBNull.Value;
        }

        internal static void mtd194(Report var0, CodeProvider var1)
        {
            bool flag = false;
            if (var0.DataFields != null)
            {
                var0.DataFields.mtd111();
            }
            var0.mtd117(Msg.DataInitialize);
            if (var1.mtd178)
            {
                object[] objArray = new object[] { var0, new EventArgs() };
                var1.mtd71("Report", Methods._DataInitialize, objArray);
            }
            if ((var0.DataAdapter != null) || (var0.DataSource != null))
            {
                if (var0.DataSource is DataSet)
                {
                    var0.DataFields = new mtd195(var0, var1, (DataSet) var0.DataSource);
                    flag = true;
                }
                else if (var0.DataSource is DataTable)
                {
                    var0.DataFields = new mtd195(var0, var1, (DataTable) var0.DataSource);
                    flag = true;
                }
                else if (var0.DataSource is DataView)
                {
                    var0.DataFields = new mtd196(var0, var1, (DataView) var0.DataSource);
                    flag = true;
                }
                else if (var0.DataSource is IListDataSource)
                {
                    if ((var0.DataFields == null) || !var0.DataFields.mtd186(var0.DataSource))
                    {
                        var0.DataFields = new mtd180(var0, var1, (IListDataSource) var0.DataSource);
                    }
                    flag = true;
                }
                else if (var0.DataSource is XmlDataSource)
                {
                    if ((var0.DataFields == null) || !var0.DataFields.mtd186(var0.DataSource))
                    {
                        var0.DataFields = new mtd183(var0, var1, (XmlDataSource) var0.DataSource);
                    }
                    flag = true;
                }
                else if (var0.DataSource is IDataReader)
                {
                    var0.DataFields = new mtd197(var0, var1, (IDataReader) var0.DataSource);
                    flag = true;
                }
                else if (var0.DataAdapter is OleDbDataAdapter)
                {
                    OleDbDataAdapter dataAdapter = (OleDbDataAdapter) var0.DataAdapter;
                    OleDbCommand selectCommand = dataAdapter.SelectCommand;
                    if ((selectCommand != null) && (selectCommand.Connection != null))
                    {
                        if (selectCommand.Connection.State == ConnectionState.Closed)
                        {
                            selectCommand.Connection.Open();
                        }
                        IDataReader reader = dataAdapter.SelectCommand.ExecuteReader();
                        if (reader != null)
                        {
                            var0.DataFields = new mtd197(var0, var1, reader);
                            flag = true;
                        }
                    }
                }
                else if (var0.DataAdapter is OdbcDataAdapter)
                {
                    OdbcDataAdapter adapter2 = (OdbcDataAdapter) var0.DataAdapter;
                    OdbcCommand command2 = adapter2.SelectCommand;
                    if ((command2 != null) && (command2.Connection != null))
                    {
                        if (command2.Connection.State == ConnectionState.Closed)
                        {
                            command2.Connection.Open();
                        }
                        IDataReader reader2 = adapter2.SelectCommand.ExecuteReader();
                        if (reader2 != null)
                        {
                            var0.DataFields = new mtd197(var0, var1, reader2);
                            flag = true;
                        }
                    }
                }
                else if (var0.DataAdapter is SqlDataAdapter)
                {
                    SqlDataAdapter adapter3 = (SqlDataAdapter) var0.DataAdapter;
                    SqlCommand command3 = adapter3.SelectCommand;
                    if ((command3 != null) && (command3.Connection != null))
                    {
                        if (command3.Connection.State == ConnectionState.Closed)
                        {
                            command3.Connection.Open();
                        }
                        IDataReader reader3 = adapter3.SelectCommand.ExecuteReader();
                        if (reader3 != null)
                        {
                            var0.DataFields = new mtd197(var0, var1, reader3);
                            flag = true;
                        }
                    }
                }
                else if (var0.DataSource is ExternalDataSource)
                {
                    if ((var0.DataFields == null) || !var0.DataFields.mtd186(var0.DataSource))
                    {
                        var0.DataFields = new mtd169(var0, var1, (ExternalDataSource) var0.DataSource);
                    }
                    flag = true;
                }
            }
            if (!flag)
            {
                var0.DataFields = null;
                var0.mtd117(Msg.NoData);
                if (var1.mtd178)
                {
                    object[] objArray2 = new object[] { var0, new EventArgs() };
                    var1.mtd71("Report", Methods._NoData, objArray2);
                }
            }
        }

        internal bool mtd189
        {
            get
            {
                return this._Report.EOF;
            }
        }

        internal int mtd190
        {
            get
            {
                return this._mtd179;
            }
        }

        internal FieldsCollection Fields
        {
            get
            {
                return this._Fields;
            }
        }

        internal bool mtd192
        {
            get
            {
                return this._mtd175;
            }
            set
            {
                this._mtd175 = value;
                if (value)
                {
                    this._mtd179--;
                }
            }
        }

        internal McField this[int var2]
        {
            get
            {
                return this._Fields[var2];
            }
        }

        internal McField this[string var3]
        {
            get
            {
                return this._Fields[var3];
            }
        }
    }
}

