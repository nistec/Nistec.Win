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

    public static class DalConfig
    {

        private static IDalBase _IDALBase;

        public static IDalBase DalBase
        {
            get { return _IDALBase; }
            set { _IDALBase = value;}
        }

    }

    /// <summary>
    /// DalLibrary Methods
	/// </summary>
	public class DalLibrary : MControl.Data.SqlClient.DalCommand
	{

        public DalLibrary(IDalBase dalBase)
            : base(dalBase)
        {

        }

 
        #region Security

        [DBCommand("Select * from Users")]
        public DataTable Users()
        {
            return (DataTable)base.Execute();
        }

        [DBCommand("Select * from Users_Group")]
        public DataTable Users_Group()
        {
            return (DataTable)base.Execute();
        }

        [DBCommand("Select * from Users_Lvl")]
        public DataTable Users_Lvl()
        {
            return (DataTable)base.Execute();
        }

        [DBCommand("Select * from Users_Permissions Where PermsID=@PermsID")]
        public DataTable Users_Permissions(int PermsID)
        {
            return (DataTable)base.Execute(PermsID);
        }

        [DBCommand("Select * from Users_UIObjects")]
        public DataTable Users_UIObjects()
        {
            return (DataTable)base.Execute();
        }

        [DBCommand("Select * from Users_UITypes")]
        public DataTable Users_UITypes()
        {
            return (DataTable)base.Execute();
        }
 
  #endregion

    }
    public class DalAlerts : MControl.Data.SqlClient.DalCommand
        {

            public DalAlerts()
                : base(DalConfig.DalBase)
            { }

            public DalAlerts(IDalBase dalBase)
            : base(dalBase)
        { }


            public static DalAlerts Instance
            {
                get { return new DalAlerts(); }
            }

            [DBCommand("SELECT ConfigValue FROM [Alert_Config]WHERE ConfigSection=@section")]
            public DataTable Alert_Config(string section)
            {
                return (DataTable)base.Execute(section);
            }
            [DBCommand("SELECT ConfigValue FROM [Alert_Config]")]
            public DataTable Alert_Config()
            {
                return (DataTable)base.Execute();
            }

            [DBCommand("SELECT ConfigValue FROM [App_Config]WHERE ConfigType='Mail'")]
            public DataTable GetEmailList()
            {
                return (DataTable)base.Execute();
            }

            [DBCommand("SELECT ConfigValue FROM [App_Config]WHERE ConfigType='Phone'")]
            public DataTable GetPhoneList()
            {
                return (DataTable)base.Execute();
            }

            [DBCommand("SELECT ConfigValue FROM [App_Config]WHERE ConfigType='UserAcount'")]
            public DataTable GetUserAcount()
            {
                return (DataTable)base.Execute();
            }

            [DBCommand("SELECT ConfigValue FROM [App_Config]WHERE ConfigType='UserPWD'")]
            public DataTable GetUserPWD()
            {
                return (DataTable)base.Execute();
            }



            [DBCommand(DBCommandType.Insert, "Alert_Exeptions")]
            public void Alert_Exeptions
                (
                [DalParam(255)]string Namespace,
                [DalParam(4000)]string ExceptionText,
                [DalParam()]int Priority,
                [DalParam(255)]string Method,
                [DalParam()]string OrderId
            )
            {
                base.Execute(ExceptionText, Priority, Method);
            }


            public int ExeptionsInsert(string Namespace, string ExceptionText, int Priority, string Method, Guid OrderId)
            {
                string sql = string.Format("INSERT INTO Alert_Exeptions( Namespace,ExceptionText,Priority,Method)VALUES('{0}','{1}',{2},'{3}','{4}')", Namespace, ExceptionText, Priority, Method, OrderId);
                return base.ExecuteNonQuery(sql);
            }


            public int GetExceptionPriority(int ex)
            {
                return base.Dlookup("Priority", "AlertPriority", "ExceptionId=" + ex.ToString(), 0);
            }

            public int GetExceptionAlertType(int ex)
            {
                return base.Dlookup("AlertType", "AlertPriority", "ExceptionId=" + ex.ToString(), 0);
            }

            [DBCommand("SELECT * FROM [vw_AlertsUserException]WHERE ExceptionId=@ExceptionId")]
            public DataTable GetAlertsUserException(int ExceptionId)
            {
                return (DataTable)base.Execute(ExceptionId);
            }


        }


	}


