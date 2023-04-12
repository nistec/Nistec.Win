using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;

namespace MControl.Web
{
    public class ExportUtil 
    {

        #region Export

        /// <summary>
        /// Export To Excel
        /// </summary>
        /// <param name="gv">GridView control</param>
        public static void ExportToExcel(Page page, System.Web.UI.WebControls.GridView gv)
        {
            string style = @"<style> .text { mso-number-format:\@; } </script> ";
           
            //Response.Buffer = true;
            page.Response.ClearContent();
            page.Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            page.Response.ContentType = "application/excel";//"application/vnd.xls";

            //Response.Charset = "";
            //this.EnableViewState = false;

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);
            // Style is added dynamically
            page.Response.Write(style);
            page.Response.Write(sw.ToString());

            page.Response.End();
        }

        /// <summary>
        /// Export To Excel with encoding
        /// </summary>
        /// <param name="gv">GridView control</param>
        /// <param name="encoding">if null encoding=utf-8</param>
        public static void ExportToExcel(Page page, System.Web.UI.WebControls.GridView gv, string encoding)
        {
            if (string.IsNullOrEmpty(encoding))
            {
                encoding = "utf-8";
            }
            //Response.Buffer = true;
            page.Response.ClearContent();
            page.Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            page.Response.ContentType = "application/excel";//"application/vnd.xls";

            //Response.Charset = "";
            //this.EnableViewState = false;

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);
            byte[] converted = System.Text.Encoding.GetEncoding(encoding).GetBytes(sw.ToString());
            page.Response.BinaryWrite(converted);

            page.Response.End();
        }

        /// <summary>
        /// Export To Excel Paging
        /// </summary>
        /// <param name="gv">GridView</param>
        public static void ExportToExcelPaging(Page page, System.Web.UI.WebControls.GridView gv)
        {
            DisableControls(gv);

            page.Response.ClearContent();
            page.Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            page.Response.ContentType = "application/excel";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);
            page.Response.Write(sw.ToString());
            page.Response.End();
        }

        public  static void DisableControls(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();

            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }

                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }


                if (gv.Controls[i].HasControls())
                {
                    DisableControls(gv.Controls[i]);
                }
            }

        }

        public static  void VerifyRenderingInServerForm(Control control)
        {

        }

        public static void OnRowDataBound(System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Attributes.Add("class", "text");
            }
        }

        public static void ExportHtml(DataSet ds, string fileName)
        {


            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            System.IO.BufferedStream bs = new System.IO.BufferedStream(fs);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(bs);
            sw.WriteLine("<html>");
            sw.WriteLine("<head>");
            sw.WriteLine("<title>");
            sw.WriteLine(ds.DataSetName);
            sw.WriteLine("</title>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            //int i, r, c ;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                sw.WriteLine("<table border=1>");
                sw.WriteLine("<tr>");

                for (int c = 0; c < ds.Tables[i].Columns.Count; c++)
                {
                    sw.Write("<td>{0}</td>", ds.Tables[i].Columns[c].ColumnName);
                }

                sw.WriteLine("</tr>");

                for (int r = 0; r < ds.Tables[i].Rows.Count; r++)
                {
                    sw.WriteLine("<tr>");
                    for (int c = 0; c < ds.Tables[i].Columns.Count; c++)
                    {
                        sw.Write("<td>{0}</td>", ds.Tables[i].Rows[r][c]);
                    }

                    sw.WriteLine("</tr>");
                }

                sw.WriteLine("</table>");
                sw.WriteLine("</hr>");
            }

            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
            sw.Close();
            bs.Close();
            fs.Close();

        }

        #endregion

    }
}
