using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using MControl.Printing.Sections;

namespace MControl.Printing
{
	/// <summary>
	/// Summary description for PrintDbUtil.
	/// </summary>
	public class PrintDbUtil
	{
		public PrintDbUtil()
		{
		}


		public DataTable ReadOleDB (string ConnectionString,string table)
		{
			return ExecuteOleDB(ConnectionString,table,"SELECT * FROM [" + table +"]");
	
		}

		public DataTable ExecuteOleDB (string ConnectionString,string tabeName,string sql)
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
				MessageBox.Show(ex.Message, "Database Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		public DataTable ReadSqlDB (string ConnectionString,string table)
		{
			return ExecuteSqlDB(ConnectionString,table,"SELECT * FROM [" + table +"]");
	
		}

		public DataTable ExecuteSqlDB (string ConnectionString,string tabeName,string sql)
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
				MessageBox.Show(ex.Message, "Database Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		public DataSet GetOleSchemaTableAndViews()
		{
			string path=OpenAccessDB();
			return GetOleSchemaTableAndViews(path);
		}


		public DataSet GetOleSchemaTableAndViews(string path)
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

		public string OpenAccessDB()
		{
			OpenFileDialog openFileDialog1=new OpenFileDialog();
			openFileDialog1.Filter = "Access files (*.mdb)|*.mdb|All files (*.*)|*.*";
			DialogResult result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				return openFileDialog1.FileName;//OpenFile(openFileDialog1.FileName);
			}
			return "";
		}

		public DataTable GetOleSchemaTable(OleDbConnection conn)
		{
			conn.Open();
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
				new object[] {null, null, null, "TABLE"});
			conn.Close();
			return schemaTable;
		}

		public DataTable GetOleSchemaView(OleDbConnection conn)
		{
			conn.Open();
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
				new object[] {null, null, null, "VIEW"});
			conn.Close();
			return schemaTable;
		}


		public DataSet GetSqlSchemaCatalog(string connectionString)
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
            //catch(Exception ex)
            //{
            //    Util.MsgBox.ShowError(ex.Message);
            //    return null;
            //}
			finally
			{
				conn.Close();
			}
		}

		public DataTable GetSqlSchemaTable(SqlConnection conn)
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
	
		public DataTable GetSqlSchemaView(SqlConnection conn)
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

		public string GetSqlConnection(string serverName,string database)
		{

			//localhost
			if(serverName.Length==0 || database.Length==0)
			{
				MsgBox.ShowWarning("Enter Server name and Databse name");
				return "";
			}
			return string.Format("Data Source={0};Integrated Security=SSPI;Initial Catalog={1}",serverName,database);
		}
	
		public bool TestConncteion(string strConn,string dataBase)
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
            //catch(SqlException expSql)
            //{
            //    Util.MsgBox.ShowError(expSql.ToString());
            //    return false;
            //}
			finally
			{
				scnn.Close();
			}

		}

		public string  GetProvider(string FilePath,string FileType)
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

	}
}
