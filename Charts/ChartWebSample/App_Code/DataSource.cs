using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MControl.Data;
using MControl.Data.Factory;

public class DataSource
{

    //const string cnn = "Data Source=IL-TLV-NTRUJMAN; Initial Catalog=Northwind; Integrated Security=SSPI; Connection Timeout=30";
    static IDbCmd cmd;

    static DataSource()
    {
        string cnn = System.Configuration.ConfigurationManager.ConnectionStrings["NorthwindExp"].ConnectionString;
        cmd = DbFactory.Create(cnn, MControl.Data.DBProvider.SqlServer);
    }

 
    public static DataTable Top10Orders()
    {
        return cmd.ExecuteDataTable("Orders", "SELECT top 10 SaleAmount, CompanyName, OrderID FROM [Sales Totals by Amount]", false);
    }
    public static DataTable SalesByCategory()
    {
        return cmd.ExecuteDataTable("SalesByCategory", @"SELECT CategoryID, CategoryName, SUM(ProductSales) AS ProductSales FROM [Sales by Category] GROUP BY CategoryID, CategoryName", false);
    }

    public static DataTable EmployeeSalesByQuarter()
    {
        return cmd.ExecuteDataTable("EmployeeSalesByQuarter", @"SELECT     Orders.EmployeeID, Employees.LastName, 
                      COALESCE (SUM(CASE WHEN month(ShippedDate) BETWEEN 1 AND 3 THEN Freight END), 0) AS Quarter1, 
                      COALESCE (SUM(CASE WHEN Month(ShippedDate) BETWEEN 4 AND 6 THEN Freight END), 0) AS Quarter2, 
                      COALESCE (SUM(CASE WHEN Month(ShippedDate) BETWEEN 7 AND 9 THEN Freight END), 0) AS Quarter3, 
                      COALESCE (SUM(CASE WHEN Month(ShippedDate) BETWEEN 10 AND 12 THEN Freight END), 0) AS Quarter4
                      FROM  [Orders] INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID
                      GROUP BY Orders.EmployeeID, Employees.LastName ORDER BY Orders.EmployeeID", false);
    }
    public static DataTable SalesByYears()
    {
        return cmd.ExecuteDataTable("Orders", @"SELECT DATEPART(month, ShippedDate) AS MonthNumber, DATENAME(month, ShippedDate) AS Month, COALESCE (SUM(CASE WHEN Year(ShippedDate) 
                        = 1997 THEN Subtotal END), 0) AS 'Total1997', COALESCE (SUM(CASE WHEN Year(ShippedDate) = 1998 THEN Subtotal END), 0) AS 'Total1998', 
                        COALESCE (SUM(CASE WHEN Year(ShippedDate) = 1996 THEN Subtotal END), 0) AS 'Total1996'
                        FROM         [Summary of Sales by Year]
                        GROUP BY DATEName(month, ShippedDate), DATEPART(month, ShippedDate)
                        ORDER BY DATEPART(month, ShippedDate)", false);
    }
}
