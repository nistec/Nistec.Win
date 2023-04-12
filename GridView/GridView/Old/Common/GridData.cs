using System;

namespace mControl.GridStyle.Common
{
	/// <summary>
	/// Summary description for GridData.
	/// </summary>
//	public class GridData
//	{
//		public GridData()
//		{
//		}

		
		//		private void SaveChanges(string connectionString)
		//		{
		//			System.Data.DataTable dsChanges=DataList.Table.GetChanges(System.Data.DataRowState.Modified);
		//	        string strUpdate="UPDATE " + m_TableStyle.MappingName + " SET ";
		//			System.Text.StringBuilder sb=new System.Text.StringBuilder();
		//			string [] strExec=new string[dsChanges.Rows];
		//            int i=0;
		//			string strSql="";
		//	
		//			foreach(System.Data.DataRow r in dsChanges.Rows)
		//			{
		//				sb="";
		//				strSql="";
		//				foreach(System.Data.DataColumn c in dsChanges.Columns)
		//				{
		//					if(c.DataType is System.String)
		//						sb.AppendFormat("{0}='{1}',",c.ColumnName, r[c].ToString());
		//					else if(c.DataType is System.DateTime)
		//						sb.AppendFormat("{0}=#{1}#," ,c.ColumnName,r[c].ToString());
		//					else
		//						sb.AppendFormat("{0}={1}," ,c.ColumnName,r[c].ToString());
		//				}
		//                strSql=sb.ToString(); 
		//			    strSql=strSql.PadLeft(strSql.Length-1); 
		//				strExec.SetValue(strSql,i);
		//				i++;
		//			}
		//			  strSql="";
		//
		//			  System.Data.OleDb.OleDbConnection conn =new System.Data.OleDb.OleDbConnection(connectionString);
		//			  System.Data.OleDb.OleDbCommand cb=new System.Data.OleDb.OleDbCommand(conn);
		//			  cb.Connection.Open();
		//			  
		//    		for(int j=0;j<strExec.Length;j++)
		//			{
		//              strSql=strUpdate + strExec[i];
		//	 		  cb.CommandText=strSql;
		//			  cb.ExecuteNonQuery();
		//			}

		//		}

		//		public int UpdateOleDb(string connectionString)
		//		{
		//			System.Data.DataTable dsChanges=DataList.Table.GetChanges(System.Data.DataRowState.Modified);
		//			string strSql="SELECT ";
		//			System.Text.StringBuilder sb=new System.Text.StringBuilder();
		//			foreach(System.Data.DataColumn c in dsChanges.Columns)
		//			{
		//				sb.AppendFormat("{0},",c.ColumnName);
		//			}
		//			strSql+=sb.ToString(); 
		//			strSql=strSql.TrimEnd(','); 
		//			strSql+=" FROM[" + m_TableStyle.MappingName + "]"; 
		//
		//			System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionString);
		//			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
		//			da.SelectCommand = new System.Data.OleDb.OleDbCommand(strSql,conn);
		//			System.Data.OleDb.OleDbCommandBuilder cb = new System.Data.OleDb.OleDbCommandBuilder(da);
		//            conn.Open();
		//			int res=0;
		//
		//			try
		//			{
		//				res=da.Update(dsChanges);
		//			}
		//			catch (Exception ex)
		//			{
		//				MessageBox.Show(ex.Message);
		//			}
		//			finally
		//			{
		//				conn.Close();
		//			}
		//			return res;
		//
		//		}
		//
		//		public int UpdateSql(string connectionString)
		//		{
		//			System.Data.DataTable dsChanges=DataList.Table.GetChanges(System.Data.DataRowState.Modified);
		//			string strSql="SELECT ";
		//			System.Text.StringBuilder sb=new System.Text.StringBuilder();
		//			foreach(System.Data.DataColumn c in dsChanges.Columns)
		//			{
		//				sb.AppendFormat("{0},",c.ColumnName);
		//			}
		//			strSql+=sb.ToString(); 
		//			strSql=strSql.TrimEnd(','); 
		//			strSql+=" FROM[" + m_TableStyle.MappingName + "]"; 
		//
		//			System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString);
		//			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
		//			da.SelectCommand = new System.Data.SqlClient.SqlCommand(strSql,conn);
		//			System.Data.SqlClient.SqlCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder(da);
		//			conn.Open();
		//			int res=0;
		//
		//			try
		//			{
		//				res=da.Update(dsChanges);
		//			}
		//			catch (Exception ex)
		//			{
		//				MessageBox.Show(ex.Message);
		//			}
		//			finally
		//			{
		//				conn.Close();
		//			}
		//			return res;
		//
		//		}

//	}
}
