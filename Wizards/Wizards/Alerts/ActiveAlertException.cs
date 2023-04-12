using System;
using System.Data;
using System.ComponentModel;
using MControl.WinUI.DataAccess;
using MControl.Data;

namespace MControl.Alerts
{
	/// <summary>
	/// Summary description for ActiveAlertException.
	/// </summary>
	public class ActiveAlertException : MControl.Data.ActiveRecordset
	{
		#region members
		#endregion

		#region Ctor
        public ActiveAlertException(IDalBase dalBase, int exc)
		{
            try
            {
                DalAlerts de = new DalAlerts(dalBase);
                DataTable dt = de.GetAlertsUserException(exc);
                base.Init(dt);
                if (base.IsEmpty)
                {
                    //Warning("ActiveAlertException is emtpy ");
                }
            }
            catch //(Exception ex)
            {
                //Error(" Could not load  ActiveAlertException" + ex.Message);
            }
		}

		#endregion

		#region properties

		public int UserId
		{
			get{return base.GetIntValue("UserId");}
		}
		public int ExceptionId
		{
			get{return base.GetIntValue("ExceptionId");}
		}
		public int AlertType
		{
			get{return base.GetIntValue("AlertType");}
		}

		public string UserName
		{
			get{return base.GetStringValue("UserName");}
		}

		public string MailAddress
		{
			get{return base.GetStringValue("MailAddress");}
		}

		public string Phone
		{
			get{return base.GetStringValue("Phone");}
		}
        public string Url
        {
            get { return base.GetStringValue("Url"); }
        }


		#endregion

	}
}
