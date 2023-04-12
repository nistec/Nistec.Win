using System;
using System.Data;
using System.Collections;
using mControl.WinForms;
using mControl.Util;

namespace mControl.WinUI.Permissions
{

    //public enum UITypes
    //{
    //    Form = 1,
    //    Menu = 2,
    //    Button = 3,
    //    Report = 4,
    //    EditBox = 5
    //}

	
	/// <summary>
	/// Summary description for Permissions.
	/// </summary>
	public class ActivePerms:IPerms
	{

		#region Members
			static ActivePerms instance;	
			int GroupId = -1;
            bool isOwner;
			Hashtable PermObjectList;
            Hashtable PermObjectListNames;

		#endregion
		
		#region Ctor


        private ActivePerms()
		{
		}

        public static ActivePerms Instance
        {
            get
            {
                if (ActivePerms.instance == null)
                {
                    ActivePerms.instance = new ActivePerms();
                }
                return ActivePerms.instance;
            }
        }
		
		#endregion
		
		#region Utillity Functions

        public void Init(ActiveUser au)
        {
            if (PermObjectList != null)
                return;
            if (au.UserId == 0)
            {
                throw new Exception("Invalid User definition");
            }
            if (au.DalBase == null)
            {
                throw new Exception("Invalid Connection String");
            }
            mControl.Data.SqlClient.DalCommand dalCmd = new mControl.Data.SqlClient.DalCommand(au.DalBase);
            this.GroupId = au.PermsGroup;
            this.isOwner = GroupId == 1;
            PermObjectList = dalCmd.DHashtable("ObjectID", "Lvl", "Users_Permissions", string.Format("PermsID={0}", au.PermsGroup));
            PermObjectListNames = dalCmd.DHashtable("ObjectName", "ObjectID", "Users_UIObjects", "");

        }

        public PermsLevel GetPermsLevel(string  ObjectName, PermsLevel defaultValue)
        {
            object o = PermObjectListNames[ObjectName];
            if (o == null)
                return defaultValue;
            return (PermsLevel)GetPermsLevelToInt((int)o, defaultValue);
        }

        public PermsLevel GetPermsLevel(int ObjectId ,PermsLevel defaultValue)
        {
            return (PermsLevel)GetPermsLevelToInt(ObjectId, defaultValue);
        }

        public int GetPermsLevelToInt(int ObjectId, PermsLevel defaultValue)
		{
			try
			{
                if (PermObjectList.Contains(ObjectId))
                    return Types.ToInt(PermObjectList[ObjectId], (int)defaultValue);
				else
                    return (int)defaultValue;
			}
			catch(Exception)
			{
                return (int)defaultValue;
			}
		}

  
		#endregion

		#region Properties
		
        public bool IsOwner
        {
            get { return isOwner; }
        }

        public int GroupID
		{
			get
			{
				return GroupId;
			}
		}
		#endregion

	}
}
