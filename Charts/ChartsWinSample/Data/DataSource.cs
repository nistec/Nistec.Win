using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MControl.Data;
using MControl.Data.Factory;

namespace ChartsSample
{
    public static class DataSource
    {

        //const string cnn = @"Data Source=MCONTROL; Initial Catalog=Northwind; uid=sa;password=tishma; Connection Timeout=30";
        const string cnn = @"Data Source=.\SQLExpress;Integrated Security=true; Initial Catalog=Northwind; Connection Timeout=30";

        static IDbCmd cmd;

        static DataSource()
        {
            cmd = DbFactory.Create(cnn, MControl.Data.DBProvider.SqlServer);
        }

 
        //public static void RemoveDataTable()
        //{
        //    HttpContext.Current.Session.Remove("dt");
        //}

        public static DataTable Select(string table)
        {
            switch (table)
            {
                case "Top10Orders":
                    return cmd.ExecuteDataTable("Orders", "SELECT top 10 SaleAmount, CompanyName, OrderID FROM [Sales Totals by Amount]",false);
                case "SalesByCategory":
                    return cmd.ExecuteDataTable("SalesByCategory", @"SELECT CategoryID, CategoryName, SUM(ProductSales) AS ProductSales FROM [Sales by Category] GROUP BY CategoryID, CategoryName", false);
                case "EmployeeSalesByQuarter":
                    return cmd.ExecuteDataTable("EmployeeSalesByQuarter", @"SELECT     Orders.EmployeeID, Employees.LastName, COALESCE (SUM(CASE WHEN month(ShippedDate) BETWEEN 1 AND 3 THEN Freight END), 0) AS Quarter1, 
                      COALESCE (SUM(CASE WHEN Month(ShippedDate) BETWEEN 4 AND 6 THEN Freight END), 0) AS Quarter2, 
                      COALESCE (SUM(CASE WHEN Month(ShippedDate) BETWEEN 7 AND 9 THEN Freight END), 0) AS Quarter3, 
                      COALESCE (SUM(CASE WHEN Month(ShippedDate) BETWEEN 10 AND 12 THEN Freight END), 0) AS Quarter4
                      FROM  [Orders] INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID
                      GROUP BY Orders.EmployeeID, Employees.LastName ORDER BY Orders.EmployeeID", false);
                case "SalesByYears":
                    return cmd.ExecuteDataTable("Orders", @"SELECT DATEPART(month, ShippedDate) AS MonthNumber, DATENAME(month, ShippedDate) AS Month, COALESCE (SUM(CASE WHEN Year(ShippedDate) 
                        = 1997 THEN Subtotal END), 0) AS 'Total1997', COALESCE (SUM(CASE WHEN Year(ShippedDate) = 1998 THEN Subtotal END), 0) AS 'Total1998', 
                        COALESCE (SUM(CASE WHEN Year(ShippedDate) = 1996 THEN Subtotal END), 0) AS 'Total1996'
                        FROM         [Summary of Sales by Year]
                        GROUP BY DATEName(month, ShippedDate), DATEPART(month, ShippedDate)
                        ORDER BY DATEPART(month, ShippedDate)", false);
                default:
                    return cmd.ExecuteDataTable("SalesByCategory", @"SELECT CustomerID, ExtendedPrice  FROM [Invoices]", false);

            }
        }
    }
}
