using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Data.SqlClient;

using MControl.Data;
using MControl.Data.SqlClient;


namespace MControl.Wizards.DataAccess
{


     /// <summary>
    /// DalAsync Methods
	/// </summary>
    public class DalAsync : MControl.Data.SqlClient.DalAsync
	{

        public DalAsync(IDalBase dalBase)
            : base(dalBase)
        {

        }

        public void Users()
        {
            base.AsyncExecute("SELECT * FROM Users", null, 0, 0, 0);
        }

        public void Users(AsyncCallback callback )
        {
            base.AsyncExecuteBegin(callback,"SELECT * FROM Users",null, 0,0);
        }
  
        public void Users_PermissionUI(AsyncCallback callback,int PermsID)
        {
            string sql = "SELECT * FROM vw_Users_PermissionUI Where PermsID=" + PermsID.ToString();
            base.AsyncExecuteBegin(callback, sql, null, 0, 0);
        }

        public void Users_Permissions(AsyncCallback callback)
        {
            string sql = "SELECT * FROM Users_Permissions";
            base.AsyncExecuteBegin(callback, sql, null, 0, 0);
        }

        public void Users_UIObjects(AsyncCallback callback)
        {
            base.AsyncExecuteBegin(callback, "SELECT * FROM Users_UIObjects", null, 0, 0);
        }
        public void Users_Group(AsyncCallback callback)
        {
            base.AsyncExecuteBegin(callback, "SELECT * FROM Users_Group", null, 0, 0);
        }


	}

  }
