using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data;
using Nistec.Printing.Data;

namespace Nistec.Printing.Html
{
    public class HtmlHelper
    {

            //public void ExportToHtml(DataTable dt)
            //{
            //    if (dt == null)
            //    {
            //        throw new Exception("DataTable is null");
            //    }
            //    string filePath = "";
            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.Title = "Save As";
            //    dialog.Filter = "HTML file (*.htm;*.html)|*.htm;*.html";
            //    dialog.FilterIndex = 1;
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        filePath = dialog.FileName;
            //        this.ToHtml(filePath);
            //    }
            //    dialog.Dispose();
            //}

        #region export html

        //public static void ExportHtml(DataTable dataSource, string fileName)
        //{
        //    ExportHtml(dataSource, AdoField.CreateFields(dataSource), fileName);
        //}

        //public static void ExportHtml(DataTable dataSource, string[] fields, string fileName)
        //{

        //    ExportHtml(dataSource, AdoField.CreateFields(fields), fileName);
        //}

        public static void ExportHtml(DataTable dataSource, HtmlWriteProperties properties)//, string fileName)
        {
            // Create Dataset
            DataTable dtExport = dataSource.Copy();
            if (dtExport.TableName == "")
                dtExport.TableName = "ExportValues";

            if (properties.DataSource.Columns.Count == 0)
            {
                properties.DataSource.CreateFields(dataSource);
            }

            //if (fields == null)
            //{
            //    fields = AdoField.CreateFields(dtExport);
            //}


            System.IO.FileStream fs = new System.IO.FileStream(properties.Filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            System.IO.BufferedStream bs = new System.IO.BufferedStream(fs);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(bs);
            sw.WriteLine("<html>");
            sw.WriteLine("<head>");
            sw.WriteLine("<title>");
            sw.WriteLine(dtExport.TableName);
            sw.WriteLine("</title>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");

            sw.WriteLine("<table border=1>");
            sw.WriteLine("<tr>");
            
            foreach(AdoColumn c in properties.DataSource.Columns)// (int c = 0; c < fields.Length; c++)
            {
                sw.Write("<td>{0}</td>", c.ToString());
            }

            sw.WriteLine("</tr>");

            for (int r = 0; r < dtExport.Rows.Count; r++)
            {
                sw.WriteLine("<tr>");
                foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
                {
                    sw.Write("<td>{0}</td>", dtExport.Rows[r][f.ColumnName]);
                }

                sw.WriteLine("</tr>");
            }

            sw.WriteLine("</table>");
            sw.WriteLine("</hr>");

            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
            sw.Close();
            bs.Close();
            fs.Close();
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

            //public static void ExportToHtml(DataTable dt,string FilePath)
            //{
            //    if (dt == null)
            //    {
            //        throw new Exception("DataTable property is null");
            //    }
            //    int count = dt.Rows.Count;
            //    int num2 = dt.Columns.Count;
            //    StreamWriter writer = new StreamWriter(FilePath);
            //    writer.WriteLine("<HTML>");
            //    writer.WriteLine("");
            //    writer.WriteLine("<HEAD>");
            //    writer.WriteLine("<TITLE>");
            //    writer.WriteLine("Data");
            //    writer.WriteLine("</TITLE>");
            //    writer.WriteLine("</HEAD>");
            //    writer.WriteLine("");
            //    writer.WriteLine("<BODY>");
            //    writer.WriteLine("");
            //    writer.WriteLine("<TABLE BORDER=1 CELLSPACING=0>");
            //    writer.WriteLine("");
            //    if (this.m_IncludeColumnNames)
            //    {
            //        string headerText = "";
            //        writer.WriteLine("<TR>");
            //        for (int j = 0; j < num2; j++)
            //        {
            //            headerText = this.m_DataGridView.Columns[j].HeaderText;
            //            if (headerText.Length == 0)
            //            {
            //                headerText = "&nbsp";
            //            }
            //            writer.Write("<TH>");
            //            writer.Write(headerText);
            //            writer.WriteLine("</TH>");
            //        }
            //        writer.WriteLine("</TR>");
            //        writer.WriteLine();
            //    }
            //    for (int i = 0; i < count; i++)
            //    {
            //        writer.WriteLine("<TR>");
            //        for (int k = 0; k < num2; k++)
            //        {
            //            string str = dt.Rows[k, i].FormattedValue.ToString();
            //            if (str.Length == 0)
            //            {
            //                str = "&nbsp";
            //            }
            //            writer.Write("<TD>");
            //            writer.Write(str);
            //            writer.WriteLine("</TD>");
            //        }
            //        writer.WriteLine("</TR>");
            //        if (i < (count - 1))
            //        {
            //            writer.WriteLine();
            //        }
            //    }
            //    writer.WriteLine("");
            //    writer.WriteLine("");
            //    writer.WriteLine("</TABLE>");
            //    writer.WriteLine("");
            //    writer.WriteLine("</BODY>");
            //    writer.WriteLine("");
            //    writer.WriteLine("</HTML>");
            //    writer.Close();
            //    //if (this.m_OpenAfterExport)
            //    //{
            //    //    Process.Start(FilePath);
            //    //}
            //    //if (this.ExportCompleted != null)
            //    //{
            //    //    this.ExportCompleted(this, new EventArgs());
            //    //}
            //}


        }
    }



