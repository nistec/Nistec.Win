using System;
using System.Data;
using mControl.Util;
using mControl.Data;

namespace mControl.WinUI.Permissions
{
	/// <summary>
	/// Summary description for User.
	/// </summary>
	public class ActiveUser:mControl.Data.ActiveRecord,IPermsUser
	{
		
		static readonly ActiveUser instance=new ActiveUser();
        internal IDalBase DalBase;
        bool _OwnerAdmin;
		
		#region <Ctor>
		static ActiveUser()
		{
		}

        private ActiveUser()
		{
		}

        public ActiveUser(IDalBase dalBase, string userName, string password)
        {
            if (dalBase == null)
            {
                throw new Exception("Invalid Dal Connection String");
            }
            DalBase = dalBase;
            mControl.Data.SqlClient.DalCommand dalCmd = new mControl.Data.SqlClient.DalCommand(DalBase);
            DataRow row = dalCmd.DRow(/*UserID,PermsGroup*/"*", "Users", string.Format("LogInName='{0}' and Password='{1}'", userName, password));
            base.Init(row);
            if (base.IsEmpty)
            {
                throw new Exception("Could not load active user");
            }
        }

        public static ActiveUser Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		#region Properties

 
        /// <summary>
        /// Get Userid
        /// </summary>
		public int UserId
		{
            get { return base.GetIntValue("UserID"); }
		}
        /// <summary>
        /// Get User name
        /// </summary>
		public string UserName
		{
            get { return base.GetStringValue("UserName"); }
		}
        /// <summary>
        /// Get PermsGroup
        /// </summary>
		public int PermsGroup
		{
            get { return base.GetIntValue("PermsGroup"); }
		}
        /// <summary>
        /// Get deatils
        /// </summary>
		public string Details
		{
            get { return GetStringValue("Details"); }
		}
        /// <summary>
        /// Get accountId
        /// </summary>
        public int AccountId
		{
            get { return base.GetIntValue("AccountId"); }
		}
        /// <summary>
        /// Get Lang
        /// </summary>
		public string Lang
		{
            get { return GetStringValue("Lang"); }
		}
        /// <summary>
        /// Get MailAddress
        /// </summary>
        public string MailAddress
        {
            get { return GetStringValue("MailAddress"); }
        }
        /// <summary>
        /// Get Phone
        /// </summary>
        public string Phone
        {
            get { return GetStringValue("Phone"); }
        }
        /// <summary>
        /// Get or Set IsOwner
        /// </summary>
        public bool IsOwner
        {
            get { return _OwnerAdmin; }
            set { _OwnerAdmin = value; }
        }

		#endregion

   


    }
}
