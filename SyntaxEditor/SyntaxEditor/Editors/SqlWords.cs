using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace Nistec.SyntaxEditor
{

     public class SqlWords
    {
        public static string[] GetSqlWords()
        {
            //21
            return new string[] { "DECLARE","SET", "CREATE", "ALTER", "TRUNCATE", "IF", 
											 "ELSE", "ELSEIF", "WHILE", "BEGIN", "END", "EXEC", "DROP", "ROLLBACK", "COMMIT", "GOTO", "GO",
											 "UPDATE", "INSERT", "DELETE", "SELECT","FROM"};
        }

        public static List<string> GetSqlCatalog(string connectionString, bool includeSqlWords)
        {
            Nistec.Data.SqlClient.DbAdapter adpter=new Nistec.Data.SqlClient.DbAdapter(connectionString);
            DataSet ds = adpter.GetSchemaDB();
            List<string> list = new List<string>();
            //System.Windows.Forms.ListView lstv = new System.Windows.Forms.ListView();

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr["TABLE_NAME"].ToString());
                }
            }
            if (includeSqlWords)
            {
                string[] _SqlWords = GetSqlWords();

                foreach (string s in _SqlWords)
                {
                    list.Add(s);
                }
            }
            list.Sort();
            return list;
        }
    }

}
