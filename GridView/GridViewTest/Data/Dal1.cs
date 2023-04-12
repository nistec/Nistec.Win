using System;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

using Nistec.Data;

namespace Nistec.Testing.Data.Sql
{
	using System.Data.SqlClient;
	using Nistec.Data.SqlClient;
    using Nistec.Data.Factory;

	#region Sql.DALBase

	/// <summary>
	/// Data class.
	/// </summary>
	public sealed class DalSql : Nistec.Data.Factory.AutoBase//.DalBase  
	{
        private static string _ConnectionString = "server=MCONTROL;database=Northwind;uid=sa;password=tishma;";
        private static readonly DalSql Instance = new DalSql();


        private DalSql()
        {
            base.Init(Nistec.Data.DBProvider.SqlServer, _ConnectionString, true);
        }

      
		public SqlApp DBApp{get{return (SqlApp)CreateInstance();}}
	}

	#endregion

	#region Sql.DBBase

	/// <summary>
	/// dalApplication class.
	/// </summary>
    public abstract class SqlApp : Nistec.Data.Factory.AutoDb
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
			[DbField(DalParamType.Identity)]ref int ShipperID,
			[DbField(40)]string CompanyName,
			[DbField(24)]string Phone
			);

		[DBCommand(DBCommandType.Update , "Shippers")]
		public abstract int ShiperUpdate
			(
			[DbField(DalParamType.Key)] int ShipperID,
			[DbField(40)]string CompanyName,
			[DbField(24)]string Phone
			);

		#endregion
		
	}
	#endregion

}


namespace Nistec.Testing.Data.Ole
{
	using System.Data.OleDb;
	using Nistec.Data.OleDb;
    using Nistec.Data.Factory;


	#region OleDb.DALBase

	/// <summary>
	/// Data class.
	/// </summary>
	public sealed class DalOle : Nistec.Data.Factory.AutoBase//.DalBase  
	{

        public static string GetConnectionString()
        {
            string ComoonFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
            string DBpath = ComoonFolder + @"\Nistec\Data\NorhwindDB.mdb";
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", DBpath);
        }

        private static string _ConnectionString;//="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath.Replace("bin","") + "\\Data\\NorhwindDB.mdb";		
        public static readonly DalOle Instance = new DalOle();

        private DalOle()
		{
            base.Init( Nistec.Data.DBProvider.OleDb,_ConnectionString, true);
		}


		public OleApp DBApp{get{return (OleApp)CreateInstance();}}
	}

	#endregion

	#region OleDb.DBBase

	/// <summary>
	/// dalApplication class.
	/// </summary>
	public abstract class OleApp : Nistec.Data.Factory.AutoDb
	{

		#region Sample

		[DBCommand("SELECT * FROM [TblIni]")]
		public abstract DataTable TblIni();

        [DBCommand("SELECT * FROM [Products]")]
        public abstract DataTable Products();

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
			[DbField(40)]string CompanyName,
			[DbField(24)]string Phone
			);

		[DBCommand(DBCommandType.Update , "Shippers")]
		public abstract int ShiperUpdate
			(
			[DbField(DalParamType.Key)] int ShipperID,
			[DbField(40)]string CompanyName,
			[DbField(24)]string Phone
			);

		#endregion
		
	}
	#endregion

}



