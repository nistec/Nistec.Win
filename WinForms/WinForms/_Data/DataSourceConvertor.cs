using System;

namespace Nistec.Data.Advanced
{
	/// <summary>
	/// Summary description for DataSource.
	/// </summary>
	public class DataSourceConvertor
	{
		public DataSourceConvertor()	{}

		public static System.Data.DataTable GetDataTable(object dataSource,string dataMember) 
		{
			if(dataSource!=null)
			{
				try
				{
					if(dataSource is System.Data.DataSet)
					{
						if(dataMember!=null && dataMember.Length >0) 
							return (System.Data.DataTable)((System.Data.DataSet)dataSource).Tables[dataMember];
						else
							return (System.Data.DataTable)((System.Data.DataSet)dataSource).Tables[0];
					}
					if(dataSource is System.Data.DataTable)
					{
						return (System.Data.DataTable) ((System.Data.DataView)dataSource).Table;
					}
					if(dataSource is System.Data.DataTable)
					{
						return (System.Data.DataTable) dataSource;
					}
					if(dataSource is System.Data.DataViewManager)
					{
						if(dataMember.Length >0) 
							return (System.Data.DataTable) ((System.Data.DataViewManager)dataSource).DataSet.Tables[dataMember];
						else
							return (System.Data.DataTable) ((System.Data.DataViewManager)dataSource).DataSet.Tables[0];
					}

					throw new Exception ("Data Source not valid");	
				}
				catch(Exception ex)
				{
					throw  ex;
				}
			}
			else
				return null;
		}

		public static System.Data.DataView GetDataView(object dataSource,string dataMember) 
		{
				if(dataSource!=null)
				{
					try
					{
						if(dataSource is System.Data.DataSet)
						{
							if(dataMember!=null && dataMember.Length >0) 
								return (System.Data.DataView)((System.Data.DataSet)dataSource).Tables[dataMember].DefaultView;
							else
								return (System.Data.DataView)((System.Data.DataSet)dataSource).Tables[0].DefaultView;
						}
						if(dataSource is System.Data.DataView)
						{
							return (System.Data.DataView) dataSource;
							}
						if(dataSource is System.Data.DataTable)
						{
							return (System.Data.DataView) ((System.Data.DataTable)dataSource).DefaultView;
						}
						if(dataSource is System.Data.DataViewManager)
						{
							if(dataMember.Length >0) 
								return (System.Data.DataView) ((System.Data.DataViewManager)dataSource).DataSet.Tables[dataMember].DefaultView;
							else
								return (System.Data.DataView) ((System.Data.DataViewManager)dataSource).DataSet.Tables[0].DefaultView;
						}

						throw new Exception ("Data Source not valid");	
					}
					catch(Exception ex)
					{
						throw  ex;
					}
				}
				else
					return null;
			}

	}
}
