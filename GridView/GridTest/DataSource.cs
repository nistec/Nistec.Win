	using System;
	using System.Windows.Forms;
	using System.Data ;
  
	namespace GridTest
	{
		/// <summary>
		/// Summary description for DataSource.
		/// </summary>
		public class DataSource
		{

			public DataSource()
			{
			}


			public static System.Data.DataView CreateDataCombo(int Rows) 
			{
			
				System.Data.DataTable dt = new System.Data.DataTable("DataCombo");
				dt.Columns.Add(new System.Data.DataColumn("Icon", typeof(int)));
				dt.Columns.Add(new System.Data.DataColumn("Lbl", typeof(string)));
				int i;
				System.Data.DataRow dr = dt.NewRow();
				for (i = 0; (i < Rows); i++) 
				{
					dr = dt.NewRow();
					dr["Icon"] = (i);
					dr["Lbl"] = ("Lable" + i);
					dt.Rows.Add(dr);
				}
				//this.Combo.DataSource = dt.DefaultView;
				//this.Combo.DisplayMember = "Lbl";
				//this.Combo.ValueMember = "Icon";
				return dt.DefaultView; 
			
			}

			public static  DataTable CreateDataTable(string TableName,int Rows,int loops) 
			{
			
				DataTable dmTable = new DataTable(TableName);
				dmTable.Columns.Add(new DataColumn("Icon", typeof(int)));
				dmTable.Columns.Add(new DataColumn("Lbl", typeof(string)));
				dmTable.Columns.Add(new DataColumn("Progress", typeof(int)));
				dmTable.Columns.Add(new DataColumn("Combo", typeof(string)));
				dmTable.Columns.Add(new DataColumn("Button", typeof(string)));
				dmTable.Columns.Add(new DataColumn("Date", typeof(System.DateTime)));
				dmTable.Columns.Add(new DataColumn("Num", typeof(double)));
				dmTable.Columns.Add(new DataColumn("Bool", typeof(bool)));
				dmTable.Columns.Add(new DataColumn("Txt", typeof(string)));
				dmTable.Columns.Add(new DataColumn("Multi", typeof(string)));
				FillDataTable(dmTable,Rows,loops);
				return dmTable;
			}
            public static DataTable CreateDataRelation(string TableName, int Rows, int loops)
            {

                DataTable dmTable = new DataTable(TableName);
                dmTable.Columns.Add(new DataColumn("Id", typeof(int)));
                dmTable.Columns.Add(new DataColumn("Date", typeof(System.DateTime)));
                dmTable.Columns.Add(new DataColumn("Num", typeof(double)));
                dmTable.Columns.Add(new DataColumn("Txt", typeof(string)));
                FillDataRelation(dmTable, Rows, loops);
                return dmTable;
            }

			public static  DataSet CreateDataSource(string TableName,int Rows) 
			{
			
				DataTable dmTable =CreateDataTable(TableName,Rows,1);
				DataSet mDataSet = new DataSet();
				mDataSet.Tables.Add(dmTable);
				mDataSet.Tables[0].TableName = TableName;
				return mDataSet;
			}
    
			public static  void FillDataTable(System.Data.DataTable dmTable ,int Rows,int loops) 
			{
			
				int i;
				DataRow dr = dmTable.NewRow();
                for (int j = 1; (j <= loops); j++)
                {

                    for (i = 1; (i <= Rows); i++)
                    {
                        dr = dmTable.NewRow();
                        dr["Icon"] = (i - 1);
                        dr["Lbl"] = ("Lable" + i);
                        dr["Progress"] = i;
                        dr["Combo"] = "Item"+i.ToString();
                        dr["Button"] = "Start";
                        dr["Date"] = System.DateTime.Today;
                        dr["Num"] = i;
                        dr["Bool"] = false;
                        dr["Txt"] = "Text item";
                        dr["Multi"] = "03-5602547";
                        dmTable.Rows.Add(dr);
                    }
                }
			}

            public static void FillDataRelation(System.Data.DataTable dmTable, int Rows, int maxPerId) 
			{
			
				int i;
				DataRow dr = dmTable.NewRow();
	
				for (i = 1; (i <= Rows); i++) 
				{
                    for (int j = 1; (j <= maxPerId); j++)
                    {
                        dr = dmTable.NewRow();
                        dr["Id"] = i;
                        dr["Date"] = System.DateTime.Today.ToString();
                        dr["Num"] = j;
                        dr["Txt"] = "Text_" + i.ToString();
                        dmTable.Rows.Add(dr);
                    }
				}
			}
		}
	}

