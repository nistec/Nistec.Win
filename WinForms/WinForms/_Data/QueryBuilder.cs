using System;
using System.Text;
using System.Collections;
using System.Data;

namespace Nistec.Data.Advanced
{

    #region struct

    //internal enum QueryType { Insert, Update };

	internal struct QueryItem
	{
		public object value;
		public string name;

		public string GetSafeValue()
		{
			if (value != null)
				return value.ToString();

			return "";
		}
	}

	#endregion

	/// <summary>
	/// QueryBuilder
	/// </summary>
	public class QueryBuilder
	{

		#region Members
		private ArrayList fields = new ArrayList();
		private string tableName;
		private string clause;
		#endregion

		#region constructor

		/// <summary>
		/// QueryBuilder constructor
		/// </summary>
		public QueryBuilder(string tableName)
		{
			this.tableName = tableName;
		}

		/// <summary>
		/// QueryBuilder constructor
		/// </summary>
		public QueryBuilder(string tableName, string clause)
		{
			this.tableName = tableName;
			this.clause = clause;
		}

		#endregion

		#region Methods


		/// <summary>
		/// QueryBuilder add query items
		/// </summary>
		/// <param name="field"></param>
		/// <param name="value"></param>
		public void Add(string field, object value)
		{
			QueryItem item;
			item.value = value;
			item.name = field;
            
			fields.Add(item);
		}

        /// <summary>
        /// QueryBuilder query string
        /// </summary>
        public string InsertCommand()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO ");
            sb.Append(tableName);
            sb.Append(" (");

            StringBuilder sbFields = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            foreach (QueryItem item in fields)
            {
                sbFields.Append("[");
                sbFields.Append(item.name);
                sbFields.Append("], ");

                if (item.value.GetType() == typeof(string) || item.value.GetType() == typeof(char) ||
                    item.value.GetType() == typeof(DateTime))
                {
                    sbValues.Append("'");
                    sbValues.Append(item.GetSafeValue());
                    sbValues.Append("', ");
                }
                else
                {
                    sbValues.Append(item.GetSafeValue());
                    sbValues.Append(", ");
                }
            }

            sbFields.Remove(sbFields.Length - 2, 2);
            sbValues.Remove(sbValues.Length - 2, 2);

            sb.Append(sbFields.ToString());

            sb.Append(") VALUES (");
            sb.Append(sbValues.ToString());
            sb.Append(")");

            return sb.ToString();
        }

		/// <summary>
		/// QueryBuilder query string
		/// </summary>
        public string UpdateCommand()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE ");
            sb.Append(tableName);
            sb.Append(" SET ");

            foreach (QueryItem item in fields)
            {
                sb.Append("[");
                sb.Append(item.name);
                sb.Append("] = ");

                if (item.value.GetType() == typeof(string) || item.value.GetType() == typeof(char) ||
                    item.value.GetType() == typeof(DateTime))
                {
                    sb.Append("'");
                    sb.Append(item.GetSafeValue());
                    sb.Append("', ");
                }
                else
                {
                    sb.Append(item.GetSafeValue());
                    sb.Append(", ");
                }
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append(" WHERE " + clause);

            return sb.ToString();
        }
	#endregion


        public static string GetSelectCommand(DataTable dataTable, string dbTableName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string tblName = dbTableName.TrimStart(new char[] { '[', '\r', '\n' });
            tblName = tblName.TrimEnd(new char[] { ']', '\r', '\n' });
            sb.Append("SELECT ");
            foreach (System.Data.DataColumn c in dataTable.Columns)
            {
                sb.AppendFormat("[{1}],", tblName, c.ColumnName);
            }
            sb.Remove(sb.Length - 1, 1);

            sb.AppendFormat(" FROM [{0}]", tblName);
            return sb.ToString();
        }

        #region static sql builder

        /// <summary>
        /// Create Sql String for command
        /// </summary>
        /// <param name="Select">Fields for select cluse</param>
        /// <param name="From">from string cluse</param>
        /// <param name="Where">where string cluse</param>
        /// <param name="InValues">DataRow Array Values of parameter for IN() Operation </param>
        /// <param name="columnName">columnName in dataRow</param>
        /// <remarks>To use InValues you should write in Where Predicat 
        /// for string values IN('') , 
        /// for numbers values IN() , 
        /// for nvarchar values IN(N''),
        /// for DateTime in jet IN(##)</remarks>
        /// <returns>String</returns>
        public static string GetSqlString(string Select, string From, string Where, DataRow[] InValues, string columnName)
        {
            int len = InValues.Length;
            object[] ob = new object[len];
            int i = 0;
            foreach (DataRow dr in InValues)
            {
                ob[i] = dr[columnName];
                i++;
            }
            return GetSqlString(Select, From, Where, ob);
        }

        /// <summary>
        /// Create Sql String for command
        /// </summary>
        /// <param name="Select">Fields for select cluse</param>
        /// <param name="From">from string cluse</param>
        /// <param name="Where">where string cluse</param>
        /// <param name="InValues">Array Values of parameter for IN() Operation </param>
        /// <remarks>To use InValues you should write in Where Predicat 
        /// for string values IN('') , 
        /// for numbers values IN() , 
        /// for nvarchar values IN(N''),
        /// for DateTime in jet IN(##)</remarks>
        /// <returns>String</returns>
        public static string GetSqlString(string Select, string From, string Where, object[] InValues)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("SELECT {0} ", Select);
            sb.AppendFormat("FROM {0} ", From);
            if (Where != null && Where != "")
            {
                string where = GetSqlString(Where, InValues);
                sb.Append(where);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Create Where Predicat for Sql String command
        /// </summary>
        /// <param name="Where">where string cluse</param>
        /// <param name="InValues">Array Values of parameter for IN() Operation </param>
        /// <remarks>To use InValues you should write in Where Predicat 
        /// for string values IN('') , 
        /// for numbers values IN() , 
        /// for nvarchar values IN(N''),
        /// for DateTime in jet IN(##)</remarks>
        /// <returns>String</returns>
        public static string GetSqlString(string Where, object[] InValues)
        {
            if (Where == null || Where == "")
                return "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (InValues != null && InValues.Length > 0)
            {
                System.Text.StringBuilder sbi = new System.Text.StringBuilder();
                string where = Where.ToLower();
                string inTemplate = "";
                string prefix = "";
                string sufix = "";
                if (where.IndexOf("in()") > -1)
                {
                    inTemplate = "in()";
                }
                else if (where.IndexOf("in('')") > -1)
                {
                    inTemplate = "in('')";
                    prefix = "'";
                    sufix = "'";
                }
                else if (where.IndexOf("in(N'')") > -1)
                {
                    inTemplate = "in('')";
                    prefix = "N'";
                    sufix = "'";
                }
                else if (where.IndexOf("in(##)") > -1)
                {
                    inTemplate = "in(##)";
                    prefix = "#";
                    sufix = "#";
                }

                sbi.Append(" IN(");
                foreach (object o in InValues)
                {
                    sbi.AppendFormat("{0}{1}{2},", prefix, o.ToString(), sufix);
                }

                sbi.Replace(',', ')', sbi.Length - 1, 1);

                where = where.Replace(inTemplate, sbi.ToString());
                sb.AppendFormat("WHERE ({0}) ", where);

            }
            else
            {
                sb.AppendFormat("WHERE ({0}) ", Where);
            }

            return sb.ToString();
        }

        #endregion

    }
}