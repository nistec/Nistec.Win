using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        DataTable dt = new DataTable();
        dt.Columns.Add("number", typeof(string));
        dt.Columns.Add("imagindx", typeof(string));
        for (int i = 1; i <= 10; i++)
        {
            DataRow dr = dt.NewRow();
            dr["number"] = i.ToString();
            dr["imagindx"] = "~/ChartGallery/mc_" + i.ToString() + ".jpeg";
            dt.Rows.Add(dr);
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Response.Redirect("Sample"+e.CommandArgument.ToString() + ".aspx");
    }
}
