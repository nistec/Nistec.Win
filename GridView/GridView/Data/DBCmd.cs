using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using mControl.Printing.Sections;


namespace mControl.GridCmd
{
	public enum DBProvider
	{
		OleDb,
		SqlServer
	}


	public interface IDBCmd
	{
		#region IDBCmd

		string ConnectionString {get;set;}
		
		System.Data.IDbConnection Connection{get;set;}

		 void SetConnection(string connectionString);

		System.Data.IDbDataAdapter DataAdapter{get;set;}

		string GetSelectCommand(DataTable dataTable,string dbTableName);

		int ExecuteCmd(string command);

		object ExecuteCmdScalar(string command);

		 DataTable Read_DataTable (string table);

		 DataTable Execute_DataTable (string tabeName,string sql);

		 DataSet GetSchemaList();

		 DataSet GetDB();

		 void FillDB(DataSet dataSet,DataTable tableSchema,string prefix);

		 DataSet GetSchemaCatalog();
		
		 void FillSechema(DataSet dataSet,DataTable tableSchema,string prefix);

		 DataTable GetSchemaTable();

		 DataTable GetSchemaView();


		#endregion

		#region dataAdpter

		 System.Data.Common.DataTableMappingCollection DataTableMapping{get;}

		 DataTable FillSchema(DataTable dataTable, SchemaType type);

		 DataTable[] FillSchema(DataSet dataSet, SchemaType type);

		 DataTable[] FillSchema(DataSet dataSet, SchemaType type,string srcTable);

		 void CreateDataAdapter(string strSQL);

		 DataTable GetChanges(DataTable dataTable);

		 DataSet GetChanges(DataSet dataSet);

		#endregion

		#region UpdateChanges Methods

		 int UpdateChanges(DataTable dataTable,string dbTableName,string selectCommand);

		 int UpdateChanges(DataTable dataTable,string dbTableName,string selectCommand,SchemaType type);

		 int UpdateChanges(DataSet dataSet,string tableName,string dbTableName,string selectCommand,SchemaType type);

		 int UpdateChanges(DataSet dataSet,string selectCommand);

		 int UpdateChanges(DataTable dataTable,string dbTableName);

		 int UpdateChanges(DataTable dataTable);

		#endregion

	}

	public abstract class DBUtil
	{

		public static IDBCmd Create(IDbConnection cnn)
		{
			if(cnn is OleDbConnection)
				return new OleDb.DBCmd(cnn as OleDbConnection);
			else if(cnn is SqlConnection)
				return new SqlClient.DBCmd(cnn as SqlConnection);
			else
				return null;
		}

		public static IDBCmd Create(string connectionString ,DBProvider provider)
		{
			if(provider ==DBProvider.OleDb)
				return new OleDb.DBCmd(connectionString);
			else if(provider ==DBProvider.SqlServer)
				return new SqlClient.DBCmd(connectionString);
			else
				return null;
		}

	}
}

#region OleDb

namespace mControl.GridCmd.OleDb
{
	using System.Data.OleDb;


	public class DBCmd:IDBCmd
	{
		#region Members
		private  System.Data.OleDb.OleDbDataAdapter dataAdapter;
		private  System.Data.OleDb.OleDbConnection conn;
		private  string connString;
		#endregion

		#region Ctor

		private DBCmd(){}

		public DBCmd(OleDbConnection cnn)
		{
		  conn=cnn;
		}
		public DBCmd(string connectionString)
		{
			connString=connectionString;
			conn = new System.Data.OleDb.OleDbConnection(connectionString);
		}

		private void ConnectionOpen()
		{
			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
		}

		#endregion

		#region Properties
         
		public string ConnectionString 
		{
			get{return connString;}
			set{connString=value;}
		}

		public System.Data.IDbDataAdapter DataAdapter
		{
			get{return dataAdapter;}
			set{dataAdapter=value as OleDbDataAdapter;}
		}

		public System.Data.IDbConnection Connection
		{
			get{return conn;}
			set{conn=value as OleDbConnection;}
		}

		public void SetConnection(string connectionString)
		{
			if(conn!=null)
			{
				if(conn.State==ConnectionState.Open)
					conn.Close();
				conn=null;
			}
			connString=connectionString;
			conn = new System.Data.OleDb.OleDbConnection(connString);
		}

		public void SetConnection(string FilePath,string FileType)
		{
			if(conn!=null)
			{
				if(conn.State==ConnectionState.Open)
					conn.Close();
				conn=null;
			}
			connString=DBCmd.GetProvider(FilePath,FileType);
			conn = new System.Data.OleDb.OleDbConnection(connString);
		}

		public System.Data.Common.DataTableMappingCollection DataTableMapping
		{
			get{return this.dataAdapter.TableMappings;}
		}

		public DataTable FillSchema(DataTable dataTable, SchemaType type)
		{
          return dataAdapter.FillSchema(dataTable, type); 
		}

		public DataTable[] FillSchema(DataSet dataSet, SchemaType type)
		{
			return dataAdapter.FillSchema(dataSet, type); 
		}

		public DataTable[] FillSchema(DataSet dataSet, SchemaType type,string srcTable)
		{
			return dataAdapter.FillSchema(dataSet, type,srcTable); 
		}

		public void CreateDataAdapter(string strSQL)
		{
			if(dataAdapter!=null)
			{
              return; 
			}
			if(conn.State!=ConnectionState.Open)
			{
				conn.Open();
			}
			//System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionString);
			dataAdapter = new System.Data.OleDb.OleDbDataAdapter();
			dataAdapter.SelectCommand = new System.Data.OleDb.OleDbCommand(strSQL,conn);
			System.Data.OleDb.OleDbCommandBuilder cb = new System.Data.OleDb.OleDbCommandBuilder(dataAdapter);
		}

		public DataTable GetChanges(DataTable dataTable)
		{
			System.Data.DataTable dsChanges=dataTable.GetChanges();
			if(dsChanges.HasErrors)
			{
				throw new Exception("Data Changes Has Errors");
			}
			return dsChanges;
		}

		public DataSet GetChanges(DataSet dataSet)
		{
			System.Data.DataSet dsChanges=dataSet.GetChanges();
			if(dsChanges.HasErrors)
			{
				throw new Exception("Data Changes Has Errors");
			}
			return dsChanges;
		}

		#endregion

		#region UpdateChanges Methods

		private void Close()
		{
			conn.Close();
			dataAdapter.Dispose();
			dataAdapter=null;
		}

		public int UpdateChanges(DataTable dataTable,string dbTableName,string selectCommand)
		{
			int res=0;
			System.Data.DataTable dsChanges=GetChanges(dataTable);
			if(dsChanges==null)
				return 0;
  
			CreateDataAdapter(selectCommand);
			dataAdapter.FillSchema(dataTable, SchemaType.Source);
			try
			{
//				if(dataAdapter.TableMappings.Count ==0 )
//				{
//					dataAdapter.TableMappings.Add(dbTableName, dataTable.TableName);
//					//dataAdapter.Fill(dataTable);
//				}
				res= dataAdapter.Update(dsChanges);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataTable dataTable,string dbTableName,string selectCommand,SchemaType type)
		{
			int res=0;
			System.Data.DataTable dsChanges=GetChanges(dataTable);
			if(dsChanges==null)
				return 0;
  
			CreateDataAdapter(selectCommand);
			dataAdapter.FillSchema(dataTable, type);
			try
			{
				if(dataAdapter.TableMappings.Count ==0 )
				{
					dataAdapter.TableMappings.Add(dbTableName, dataTable.TableName);
					//dataAdapter.Fill(dataTable);
				}
				res= dataAdapter.Update(dsChanges);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataSet dataSet,string tableName,string dbTableName,string selectCommand,SchemaType type)
		{
			int res=0;
			System.Data.DataSet dsChanges=GetChanges(dataSet);
			if(dsChanges==null)
				return 0;
  
			CreateDataAdapter(selectCommand);
			dataAdapter.FillSchema(dataSet, type,tableName);
			try
			{
				if(dataAdapter.TableMappings.Count ==0 )
				{
					dataAdapter.TableMappings.Add(dbTableName, tableName);
					//dataAdapter.Fill(dataSet,tableName);
				}
				res= dataAdapter.Update(dsChanges,tableName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataSet dataSet,string selectCommand)
		{
			int res=0;
			System.Data.DataSet dsChanges=GetChanges(dataSet);
			if(dsChanges==null)
				return 0;
 
			CreateDataAdapter(selectCommand);
			try
			{
				res= dataAdapter.Update(dsChanges);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataTable dataTable,string dbTableName)
		{
			string strSql=GetSelectCommand(dataTable,dbTableName);
			return UpdateChanges(dataTable,dbTableName,strSql);
		}

		public int UpdateChanges(DataTable dataTable)
		{
			string strSql=GetSelectCommand(dataTable,dataTable.TableName);
			return UpdateChanges(dataTable,strSql);
		}

		#endregion

		#region OleDb command

		public string GetSelectCommand(DataTable dataTable,string dbTableName)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			sb.Append("SELECT ");
			foreach(System.Data.DataColumn c in dataTable.Columns)
			{
				sb.AppendFormat("{0},",c.ColumnName);
			}
			sb.Remove(sb.Length-1,1);
			sb.AppendFormat(" FROM {0}",dbTableName);
			return sb.ToString();

			//			string strSql="SELECT ";
			//			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			//			foreach(System.Data.DataColumn c in dataTable.Columns)
			//			{
			//				sb.AppendFormat("{0},",c.ColumnName);
			//			}
			//			strSql+=sb.ToString(); 
			//			strSql=strSql.TrimEnd(','); 
			//			strSql+=" FROM [" + dbTableName + "]"; 
			//			return strSql;
		}

		public int ExecuteCmd(string command)
		{
			OleDbCommand cmd=null;
			try
			{
				cmd=new OleDbCommand (command,conn);
				this.ConnectionOpen();
				return cmd.ExecuteNonQuery ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public object ExecuteCmdScalar(string command)
		{
			OleDbCommand cmd=null;
			try
			{
				cmd=new OleDbCommand (command,conn);
				this.ConnectionOpen();
				return cmd.ExecuteScalar ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public DataTable Read_DataTable (string table)
		{
			return Execute_DataTable(table,"SELECT * FROM [" + table +"]");
		}

		public DataTable Execute_DataTable (string tabeName,string sql)
		{
			DataTable dt = new DataTable();
			dt.TableName=tabeName;
			try
			{
				ConnectionOpen();
				OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
				da.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message, "Database Error");
				return null;
			}
			finally
			{
				if (conn != null)
				{
					conn.Close();
				}
			}
		} 

		public DataSet GetSchemaList()
		{
			DataSet ds=null;
			ds=new DataSet ();
				
			DataTable dt=GetSchemaTable();
			dt.TableName="TABLES";
			ds.Tables.Add(dt);
	
			DataTable dv=GetSchemaView();
			dv.TableName="VIEWS";
			ds.Tables.Add(dv);
			return ds;
		}

		public DataSet GetDB()
		{
			DataSet ds=null;
			ds=new DataSet ();

			DataTable dt=GetSchemaTable();
			FillDB(ds,dt,"TABLE");
			DataTable dv=GetSchemaView();
			FillDB(ds,dv,"VIEW");
			return ds;
		}

		public void FillDB(DataSet dataSet,DataTable tableSchema,string prefix)
		{

			try
			{
				OleDbDataAdapter da = new OleDbDataAdapter();
				OleDbCommand cmd=new OleDbCommand();
				cmd.CommandType=CommandType.TableDirect;
				cmd.Connection=conn;
				string src=null;

				foreach(DataRow dr in tableSchema.Rows)
				{
					src=(string)dr["TABLE_NAME"];
					cmd.CommandText=src;
					da.SelectCommand=cmd;
					DataTable[] dt= da.FillSchema(dataSet,SchemaType.Source,src);
					foreach(DataTable d in dt)
					{
						d.Prefix=prefix;
					}
					da.Fill(dataSet);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
			}
		}

		public DataSet GetSchemaCatalog()
		{
			DataSet ds=null;
			ds=new DataSet ();

			DataTable dt=GetSchemaTable();
			FillSechema(ds,dt,"TABLE");
			DataTable dv=GetSchemaView();
			FillSechema(ds,dv,"VIEW");

			return ds;
		}
		public void FillSechema(DataSet dataSet,DataTable tableSchema,string prefix)
		{

			try
			{
				OleDbDataAdapter da = new OleDbDataAdapter();
				OleDbCommand cmd=new OleDbCommand();
				cmd.CommandType=CommandType.TableDirect;
				cmd.Connection=conn;
				string src=null;

				DataTable dtSrc=null;
				DataTable dt=null;

				foreach(DataRow dr in tableSchema.Rows)
				{
					src=(string)dr["TABLE_NAME"];
					dtSrc=new DataTable(src);
					cmd.CommandText=src;
					da.SelectCommand=cmd;
					da.FillSchema(dtSrc,SchemaType.Source);
	
					dt=new DataTable(src);
					dt.Prefix=prefix;
			
					dt.Columns.Add("ColumnName");
					dt.Columns.Add("DataType");
					dt.Columns.Add("MaxLength");
			
					foreach(DataColumn c in dtSrc.Columns)
					{
						dt.Rows.Add(new object[]{c.ColumnName,c.DataType.Name,c.MaxLength});
					}
					dataSet.Tables.Add(dt);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
			}
		}

		public DataTable GetSchemaTable()
		{
			conn.Open();
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
				new object[] {null, null, null, "TABLE"});
			conn.Close();
			return schemaTable;
		}

		public DataTable GetSchemaView()
		{
			conn.Open();
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
				new object[] {null, null, null, "VIEW"});
			conn.Close();
			return schemaTable;
		}

		#endregion

		#region static

		public static int ExecuteOleDbCmd(string connectionString, string command)
		{
			OleDbConnection conn=null;
			OleDbCommand cmd=null;
			try
			{
				conn=new OleDbConnection(connectionString);
				cmd=new OleDbCommand (command,conn);
				conn.Open();
				return cmd.ExecuteNonQuery ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public static object ExecuteOleDbCmdScalar(string connectionString, string command)
		{
			OleDbConnection conn=null;
			OleDbCommand cmd=null;
			try
			{
				conn=new OleDbConnection(connectionString);
				cmd=new OleDbCommand (command,conn);
				conn.Open();
				return cmd.ExecuteScalar ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public static DataTable ReadOleDB (string ConnectionString,string table)
		{
			return ExecuteOleDB(ConnectionString,table,"SELECT * FROM [" + table +"]");
	
		}

		public static DataTable ExecuteOleDB (string ConnectionString,string tabeName,string sql)
		{
			OleDbConnection cn=null;
			DataTable dt = new DataTable();
			dt.TableName=tabeName;
			try
			{
				cn = new OleDbConnection(ConnectionString);
				cn.Open();

				//string sql = "SELECT * FROM [" + table +"]";
				OleDbDataAdapter da = new OleDbDataAdapter(sql, cn);
				da.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message, "Database Error");
				return null;
			}
			finally
			{
				if (cn != null)
				{
					cn.Close();
				}
			}
		} 

		public static DataSet GetAccessSchemaList()
		{
			string path=OpenAccessDB();
			return GetOleSchemaList(path);
		}

		public static string OpenAccessDB()
		{
			return Util.CommonDialog.FileDialog("Access files (*.mdb)|*.mdb|All files (*.*)|*.*");
		}

		public static DataSet GetOleSchemaList(string path)
		{
			DataSet ds=null;
			if(path!="")
			{
				ds=new DataSet ();
				string connectionString=GetProvider(path,"mdb");
				OleDbConnection conn=new OleDbConnection(connectionString);
				
				DataTable dt=GetOleSchemaTable(conn);
				dt.TableName="TABLES";
				ds.Tables.Add(dt);
	
				DataTable dv=GetOleSchemaView(conn);
				dv.TableName="VIEWS";
				ds.Tables.Add(dv);
			}
			return ds;
		}

		public static DataTable GetOleSchemaTable(OleDbConnection conn)
		{
			conn.Open();
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
				new object[] {null, null, null, "TABLE"});
			conn.Close();
			return schemaTable;
		}

		public static DataTable GetOleSchemaView(OleDbConnection conn)
		{
			conn.Open();
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
				new object[] {null, null, null, "VIEW"});
			conn.Close();
			return schemaTable;
		}
		public static string  GetProvider(string FilePath,string FileType)
		{
			string strConn="";
			
			switch(FileType)
			{
				case "xls":
					strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0;";//HDR=Yes;IMEX=1";
					break;
				case "txt":
					strConn ="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"\"text;HDR=Yes;FMT=Delimited;\"\";";
					break;
				case "csv":
					strConn ="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"\"csv;HDR=Yes;FMT=Delimited;\"\";";
					break;
				case "mdb":
					strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath ;
					break;
				case "outlook":
					strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Outlook 9.0;MAPILEVEL=;DATABASE=C:\\Temp\\;";
					break;
				case "exchange":
					strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Exchange 4.0;MAPILEVEL=Mailbox - Pat Smith|;DATABASE=C:\\Temp\\;";
					break;
			}
			return strConn;
			//oleConn = new OleDbConnection(strConn); 
		}

		#endregion

	}

}

#endregion

#region SqlClient

namespace mControl.GridCmd.SqlClient
{
	using System.Data.SqlClient;

	public class DBCmd:IDBCmd
	{
		#region Members
		private  System.Data.SqlClient.SqlDataAdapter dataAdapter;
		private  System.Data.SqlClient.SqlConnection conn;
		private  string connString;
		#endregion

		#region Ctor

		private DBCmd(){}

		public DBCmd(SqlConnection cnn)
		{
			conn=cnn;
		}
		public DBCmd(string connectionString)
		{
			connString=connectionString;
			conn = new System.Data.SqlClient.SqlConnection(connectionString);
		}

		private void ConnectionOpen()
		{
			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
		}
		#endregion

		#region Properties
        
		public string ConnectionString 
		{
			get{return connString;}
			set{connString=value;}
		}

		public System.Data.IDbConnection Connection
		{
			get{return conn;}
			set{conn=value as SqlConnection;}
		}

		public void SetConnection(string connectionString)
		{
			if(conn!=null)
			{
				if(conn.State==ConnectionState.Open)
					conn.Close();
				conn=null;
			}
			connString=connectionString;
			conn = new System.Data.SqlClient.SqlConnection(connString);
		}

		public System.Data.IDbDataAdapter DataAdapter
		{
			get{return dataAdapter;}
			set{dataAdapter=value as SqlDataAdapter;}
		}

		public System.Data.Common.DataTableMappingCollection DataTableMapping
		{
			get{return this.dataAdapter.TableMappings;}
		}

		public DataTable FillSchema(DataTable dataTable, SchemaType type)
		{
			return dataAdapter.FillSchema(dataTable, type); 
		}

		public DataTable[] FillSchema(DataSet dataSet, SchemaType type)
		{
			return dataAdapter.FillSchema(dataSet, type); 
		}

		public DataTable[] FillSchema(DataSet dataSet, SchemaType type,string srcTable)
		{
			return dataAdapter.FillSchema(dataSet, type,srcTable); 
		}

		public void CreateDataAdapter(string strSQL)
		{
			if(dataAdapter!=null)
			{
				return; 
			}
			if(conn.State!=ConnectionState.Open)
			{
				conn.Open();
			}
			//System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString);
			dataAdapter = new System.Data.SqlClient.SqlDataAdapter();
			dataAdapter.SelectCommand = new System.Data.SqlClient.SqlCommand(strSQL,conn);
			System.Data.SqlClient.SqlCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder(dataAdapter);
		}

		public DataTable GetChanges(DataTable dataTable)
		{
			System.Data.DataTable dsChanges=dataTable.GetChanges();
			if(dsChanges.HasErrors)
			{
				throw new Exception("Data Changes Has Errors");
			}
			return dsChanges;
		}

		public DataSet GetChanges(DataSet dataSet)
		{
			System.Data.DataSet dsChanges=dataSet.GetChanges();
			if(dsChanges.HasErrors)
			{
				throw new Exception("Data Changes Has Errors");
			}
			return dsChanges;
		}

		#endregion

		#region UpdateChanges Methods

		private void Close()
		{
			conn.Close();
			dataAdapter.Dispose();
			dataAdapter=null;
		}

		public int UpdateChanges(DataTable dataTable,string dbTableName,string selectCommand)
		{
			int res=0;
			System.Data.DataTable dsChanges=GetChanges(dataTable);
			if(dsChanges==null)
				return 0;
  
			CreateDataAdapter(selectCommand);
			try
			{
				if(dataAdapter.TableMappings.Count ==0 )
				{
					dataAdapter.TableMappings.Add(dbTableName, dataTable.TableName);
					//dataAdapter.Fill(dataTable);
				}
				res= dataAdapter.Update(dsChanges);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataTable dataTable,string dbTableName,string selectCommand,SchemaType type)
		{
			int res=0;
			System.Data.DataTable dsChanges=GetChanges(dataTable);
			if(dsChanges==null)
				return 0;
  
			CreateDataAdapter(selectCommand);
			dataAdapter.FillSchema(dataTable, type);
			try
			{
				if(dataAdapter.TableMappings.Count ==0 )
				{
					dataAdapter.TableMappings.Add(dbTableName, dataTable.TableName);
					//dataAdapter.Fill(dataTable);
				}
				res= dataAdapter.Update(dsChanges);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataSet dataSet,string tableName,string dbTableName,string selectCommand,SchemaType type)
		{
			int res=0;
			System.Data.DataSet dsChanges=GetChanges(dataSet);
			if(dsChanges==null)
				return 0;
  
			CreateDataAdapter(selectCommand);
			dataAdapter.FillSchema(dataSet, type,tableName);
			try
			{
				if(dataAdapter.TableMappings.Count ==0 )
				{
					dataAdapter.TableMappings.Add(dbTableName, tableName);
					//dataAdapter.Fill(dataSet,tableName);
				}
				res= dataAdapter.Update(dsChanges,tableName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataSet dataSet,string selectCommand)
		{
			int res=0;
			System.Data.DataSet dsChanges=GetChanges(dataSet);
			if(dsChanges==null)
				return 0;
 
			CreateDataAdapter(selectCommand);
			try
			{
				res= dataAdapter.Update(dsChanges);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				Close();
			}
			return res;
		}

		public int UpdateChanges(DataTable dataTable,string dbTableName)
		{
			string strSql=GetSelectCommand(dataTable,dbTableName);
			return UpdateChanges(dataTable,dbTableName,strSql);
		}

		public int UpdateChanges(DataTable dataTable)
		{
			string strSql=GetSelectCommand(dataTable,dataTable.TableName);
			return UpdateChanges(dataTable,strSql);
		}

		#endregion

		#region SqlClient

		public string GetSelectCommand(DataTable dataTable,string dbTableName)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			sb.Append("SELECT ");
			foreach(System.Data.DataColumn c in dataTable.Columns)
			{
				sb.AppendFormat("{0},",c.ColumnName);
			}
			sb.Remove(sb.Length-1,1);
			sb.AppendFormat(" FROM [{0}]",dbTableName);
			return sb.ToString();

			//			string strSql="SELECT ";
			//			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			//			foreach(System.Data.DataColumn c in dataTable.Columns)
			//			{
			//				sb.AppendFormat("{0},",c.ColumnName);
			//			}
			//			strSql+=sb.ToString(); 
			//			strSql=strSql.TrimEnd(','); 
			//			strSql+=" FROM [" + dbTableName + "]"; 
			//			return strSql;
		}


		public int ExecuteCmd(string command)
		{
			SqlCommand cmd=null;
			try
			{
				cmd=new SqlCommand (command,conn);
				this.ConnectionOpen();
				return cmd.ExecuteNonQuery ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public object ExecuteCmdScalar(string command)
		{
			SqlCommand cmd=null;
			try
			{
				cmd=new SqlCommand (command,conn);
				this.ConnectionOpen();
				return cmd.ExecuteScalar ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}
		public DataTable Read_DataTable (string table)
		{
			return Execute_DataTable(table,"SELECT * FROM [" + table +"]");
	
		}

		public DataTable Execute_DataTable (string tabeName,string sql)
		{
			DataTable dt = new DataTable();
			dt.TableName=tabeName;
			try
			{
				this.ConnectionOpen();
				SqlDataAdapter da = new SqlDataAdapter(sql, conn);
				da.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message, "Database Error");
				return null;
			}
			finally
			{
				if (conn != null)
				{
					conn.Close();
				}
			}
		} 

		public DataSet GetDB()
		{
			DataSet ds=null;
			ds=new DataSet ();

			DataTable dt=GetSchemaTable();
			FillDB(ds,dt,"TABLE");
			DataTable dv=GetSchemaView();
			FillDB(ds,dv,"VIEW");

			return ds;
		}

		public void FillDB(DataSet dataSet,DataTable tableSchema,string prefix)
		{
			try
			{

				SqlDataAdapter da = new SqlDataAdapter();
				SqlCommand cmd=new SqlCommand();
				cmd.CommandType=CommandType.Text;
				cmd.Connection=conn;
				string src=null;

				foreach(DataRow dr in tableSchema.Rows)
				{
					src=(string)dr["TABLE_NAME"];
					cmd.CommandText="SELECT * FROM [" + src + "]";
					da.SelectCommand=cmd;
					DataTable[] dt=da.FillSchema(dataSet,SchemaType.Source,src);
					foreach(DataTable d in dt)
					{
						d.Prefix=prefix;
					}
					da.Fill(dataSet);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
			}
		}


		public DataSet GetSchemaCatalog()
		{
			DataSet ds=null;
			ds=new DataSet ();

			DataTable dt=GetSchemaTable();
			FillSechema(ds,dt,"TABLE");
			DataTable dv=GetSchemaView();
			FillSechema(ds,dv,"VIEW");

			return ds;
		}

		public void FillSechema(DataSet dataSet,DataTable tableSchema,string prefix)
		{

			try
			{
				SqlDataAdapter da = new SqlDataAdapter();
				SqlCommand cmd=new SqlCommand();
				cmd.CommandType=CommandType.Text;
				cmd.Connection=conn;
				string src=null;

				DataTable dtSrc=null;
				DataTable dt=null;

				foreach(DataRow dr in tableSchema.Rows)
				{
					src=(string)dr["TABLE_NAME"];
					dtSrc=new DataTable(src);
					cmd.CommandText="SELECT * FROM [" + src + "]";
					da.SelectCommand=cmd;
					da.FillSchema(dtSrc,SchemaType.Source);
	
					dt=new DataTable(src);
					dt.Prefix=prefix;
			
					dt.Columns.Add("ColumnName");
					dt.Columns.Add("DataType");
					dt.Columns.Add("MaxLength");
				
					foreach(DataColumn c in dtSrc.Columns)
					{
						dt.Rows.Add(new object[]{c.ColumnName,c.DataType.Name,c.MaxLength});
					}
					dataSet.Tables.Add(dt);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
			}
		}



		public DataSet GetSchemaList()
		{
			DataSet ds=null;
			try
			{
				ds=new DataSet ();
				
				DataTable dt=GetSchemaTable();
				dt.TableName="TABLES";
				ds.Tables.Add(dt);
	
				DataTable dv=GetSchemaView();
				dv.TableName="VIEWS";
				ds.Tables.Add(dv);
				return ds;
			}
			catch(Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message);
				return null;
			}
			finally
			{
				conn.Close();
			}
		}

		public DataTable GetSchemaTable()
		{

			//conn = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=northwind");
	
			SqlDataAdapter schemaDA = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES " +
				"WHERE TABLE_TYPE = 'BASE TABLE' " +
				"ORDER BY TABLE_TYPE", 
				conn);
	
			DataTable schemaTable = new DataTable();
			schemaDA.Fill(schemaTable);
			return schemaTable;
		}
	
		public DataTable GetSchemaView()
		{

			//conn = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=northwind");
	
			SqlDataAdapter schemaDA = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES " +
				"WHERE TABLE_TYPE = 'VIEW' " +
				"ORDER BY TABLE_TYPE", 
				conn);
	
			DataTable schemaTable = new DataTable();
			schemaDA.Fill(schemaTable);
			return schemaTable;
		}

		#endregion

		#region Static
	
		public static int ExecuteSqlDbCmd(string connectionString, string command)
		{
			SqlConnection conn=null;
			SqlCommand cmd=null;
			try
			{
				conn=new SqlConnection(connectionString);
				cmd=new SqlCommand (command,conn);
				conn.Open();
				return cmd.ExecuteNonQuery ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public static object ExecuteSqlCmdScalar(string connectionString, string command)
		{
			SqlConnection conn=null;
			SqlCommand cmd=null;
			try
			{
				conn=new SqlConnection(connectionString);
				cmd=new SqlCommand (command,conn);
				conn.Open();
				return cmd.ExecuteScalar ();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
			finally
			{
				cmd.Connection.Close (); 
			}
		}

		public static DataTable ReadSqlDB (string ConnectionString,string table)
		{
			return ExecuteSqlDB(ConnectionString,table,"SELECT * FROM [" + table +"]");
	
		}

		public static DataTable ExecuteSqlDB (string ConnectionString,string tabeName,string sql)
		{
			SqlConnection cn=null;
			DataTable dt = new DataTable();
			dt.TableName=tabeName;
			try
			{
				cn = new SqlConnection(ConnectionString);
				cn.Open();

				//string sql = "SELECT * FROM [" + table +"]";
				SqlDataAdapter da = new SqlDataAdapter(sql, cn);
				da.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message, "Database Error");
				return null;
			}
			finally
			{
				if (cn != null)
				{
					cn.Close();
				}
			}
		} 

		public static DataSet GetSqlSchemaList(string connectionString)
		{
			DataSet ds=null;
			SqlConnection conn = new SqlConnection(connectionString);
			try
			{
				if(connectionString!="")
				{
	
					ds=new DataSet ();
				
					DataTable dt=GetSqlSchemaTable(conn);
					dt.TableName="TABLES";
					ds.Tables.Add(dt);
	
					DataTable dv=GetSqlSchemaView(conn);
					dv.TableName="VIEWS";
					ds.Tables.Add(dv);
				}
				return ds;
			}
			catch(Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message);
				return null;
			}
			finally
			{
				conn.Close();
			}
		}

		public static DataTable GetSqlSchemaTable(SqlConnection conn)
		{

			//conn = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=northwind");
	
			SqlDataAdapter schemaDA = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES " +
				"WHERE TABLE_TYPE = 'BASE TABLE' " +
				"ORDER BY TABLE_TYPE", 
				conn);
	
			DataTable schemaTable = new DataTable();
			schemaDA.Fill(schemaTable);
			return schemaTable;
		}
	
		public static DataTable GetSqlSchemaView(SqlConnection conn)
		{

			//conn = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=northwind");
	
			SqlDataAdapter schemaDA = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES " +
				"WHERE TABLE_TYPE = 'VIEW' " +
				"ORDER BY TABLE_TYPE", 
				conn);
	
			DataTable schemaTable = new DataTable();
			schemaDA.Fill(schemaTable);
			return schemaTable;
		}

		public static string GetSqlConnection(string serverName,string database)
		{

			//localhost
			if(serverName.Length==0 || database.Length==0)
			{
				Util.MsgBox.ShowWarning("Enter Server name and Databse name");
				return "";
			}
			return string.Format("Data Source={0};Integrated Security=SSPI;Initial Catalog={1}",serverName,database);
		}
	
		public static bool TestConncteion(string strConn,string dataBase)
		{

			if(strConn=="")
				return false;

			SqlConnection scnn = new SqlConnection(strConn);

			// DROP "GetProducts"
			// This SQL statement drops the stored procedure "GetProducts", 
			// if it exists.

			string strSQL  = "IF EXISTS (" + 
				"SELECT * " + 
				"FROM "+ dataBase + ".dbo.sysobjects " + 
				"WHERE Name = 'GetProducts' " + 
				"AND TYPE = 'p')" + Environment.NewLine + 
				"DROP PROCEDURE GetProducts";

			SqlCommand scmd = new SqlCommand(strSQL, scnn);

			try 
			{

				scnn.Open();
				return true;
			} 
			catch(SqlException expSql)
			{
				Util.MsgBox.ShowError(expSql.ToString());
				return false;
			}
			finally
			{
				scnn.Close();
			}

		}
		#endregion

	}

}

#endregion


