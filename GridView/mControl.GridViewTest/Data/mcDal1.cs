using System;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

using mControl.DAL;

namespace mControl.NavBar.Data.Sql
{
	using System.Data.SqlClient;
	using mControl.DAL.Sql;

	#region Dal

	/// <summary>
	/// Summary description for Dal.
	/// </summary>
	public sealed class Dal
	{

        private static string _ConnectionString = "server=(local);database=Northwind;Integrated Security=SSPI;";
		private static readonly Dal instance=new Dal ();
		private static readonly SqlBase _SqlBase=new SqlBase ();
		
		static   Dal()
		{
			_SqlBase.Init (_ConnectionString,true);
		}

		private  Dal()	
		{
			
		}

		public static SqlBase SqlDB
		{
			get
			{
				if(_SqlBase==null)
					_SqlBase.Init (_ConnectionString,false);
				return _SqlBase;
			}
		}
	}

	#endregion

	#region Sql.DALBase

	/// <summary>
	/// DAL class.
	/// </summary>
	public sealed class SqlBase: mControl.DAL.Sql.DALBase  
	{
		public SqlApp DBApp{get{return (SqlApp)GetBase();}}
	}

	#endregion

	#region Sql.DBBase

	/// <summary>
	/// dalApplication class.
	/// </summary>
	public abstract class SqlApp : mControl.DAL.Sql.DBBase
	{

		#region Sample

        [DBCommand( DBCommandType.Text,"SELECT * FROM [Orders]",null, MissingSchemaAction.AddWithKey)]
        public abstract DataTable Orders();

        [DBCommand("SELECT * FROM [Shippers]")]
        public abstract DataTable Shippers();

        [DBCommand(DBCommandType.Text, "SELECT * FROM [Order Details]",null, MissingSchemaAction.AddWithKey)]
        public abstract DataTable OrdersDetails();


		[DBCommand("SELECT * FROM [Customers]")]
		public abstract DataTable Customers();

		[DBCommand(DBCommandType.Text,"UPDATE [Products] SET [UnitPrice]=@UnitPrice WHERE [ProductID]=@ProductID")]
		public abstract int UpdateProductUnitPrice (double UnitPrice,int ProductID);

		[DBCommand(DBCommandType.Insert, "Shippers")]
		public abstract int ShiperInsert
			(
			[DBParameter(DBParameterType.Identity)]ref int ShipperID,
			[DBParameter(40)]string CompanyName,
			[DBParameter(24)]string Phone
			);

		[DBCommand(DBCommandType.Update , "Shippers")]
		public abstract int ShiperUpdate
			(
			[DBParameter(DBParameterType.Key)] int ShipperID,
			[DBParameter(40)]string CompanyName,
			[DBParameter(24)]string Phone
			);

		#endregion
		
	}
	#endregion

}


namespace mControl.NavBar.Data.Ole
{
	using System.Data.OleDb;
	using mControl.DAL.OleDb;

	#region Dal

	/// <summary>
	/// Summary description for Dal.
	/// </summary>
	public sealed class Dal
	{

		public static string GetConnectionString()
		{
			string ComoonFolder=Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
			string DBpath=ComoonFolder+ @"\mControl\Data\NorhwindDB.mdb";
			return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};",DBpath);		
		}

		private static string _ConnectionString;//="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath.Replace("bin","") + "\\Data\\NorhwindDB.mdb";		
		private static readonly Dal instance=new Dal ();
		private static readonly Ole.OleBase _OleBase=new Ole.OleBase ();
	
		static   Dal()
		{
			_ConnectionString=GetConnectionString();
			Dal._OleBase.Init (_ConnectionString,true);
		}

		private  Dal()
		{
			
		}

		public static Ole.OleBase OleDB
		{
			get
			{
				if(_OleBase==null)
					_OleBase.Init (_ConnectionString,false);
				return _OleBase;
			}
		}

	}
	#endregion

	#region OleDb.DALBase

	/// <summary>
	/// DAL class.
	/// </summary>
	public sealed class OleBase: mControl.DAL.OleDb.DALBase  
	{
		public OleApp DBApp{get{return (OleApp)GetBase();}}
	}

	#endregion

	#region OleDb.DBBase

	/// <summary>
	/// dalApplication class.
	/// </summary>
	public abstract class OleApp : mControl.DAL.OleDb.DBBase
	{

		#region Sample

		[DBCommand("SELECT * FROM [TblIni]")]
		public abstract DataTable TblIni();


		[DBCommand("SELECT * FROM [Customers]")]
		public abstract DataTable Customers();

		[DBCommand("SELECT * FROM [Orders]")]
		public abstract DataTable Orders();

		[DBCommand("SELECT * FROM [Shippers]")]
		public abstract DataTable Shippers();

		[DBCommand("SELECT * FROM [Order Details]")]
		public abstract DataTable OrdersDetails();

		[DBCommand(DBCommandType.Text,"UPDATE [Products] SET [UnitPrice]=@UnitPrice WHERE [ProductID]=@ProductID")]
		public abstract int UpdateProductUnitPrice (double UnitPrice,int ProductID);

		[DBCommand(DBCommandType.Insert, "Shippers")]
		public abstract int ShiperInsert
			(
			[DBParameter(40)]string CompanyName,
			[DBParameter(24)]string Phone
			);

		[DBCommand(DBCommandType.Update , "Shippers")]
		public abstract int ShiperUpdate
			(
			[DBParameter(DBParameterType.Key)] int ShipperID,
			[DBParameter(40)]string CompanyName,
			[DBParameter(24)]string Phone
			);

		#endregion
		
	}
	#endregion

}



