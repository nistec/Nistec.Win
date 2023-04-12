namespace Nistec.Printing.Html
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Nistec.Printing.Data;

    public class HtmlWriter : AdoMap,IAdoWriter
    {

        public HtmlWriter(HtmlWriteProperties properties)
        {
            this.Properties = properties.Clone();// new HtmlWriteProperties();
            base.Output=new AdoOutput("Records To Save", "Records successfully saved to the Html File.");//, true);
        }

    
        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            HtmlWriteProperties properties = this.Properties as HtmlWriteProperties;
            try
            {
                if (properties.Filename == "")
                {
                    base.CancelExecute( "No filename specified");
                    return false;
                }
                if (base.Output==null)//s.Count < 1)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }
                this.UpdateSchema();
                //DataTable table = base.Output.Value as DataTable;
                //this.ExecuteBegin((uint)table.Rows.Count);
                //long length = 0L;
                //if (File.Exists(properties.Filename))
                //{
                //    FileInfo info = new FileInfo(properties.Filename);
                //    length = info.Length;
                //}
                WriteHtml();

                //using (StreamWriter writer = new StreamWriter(properties.Filename, false, Encoding.GetEncoding(properties.Encoding)))
                //{
                //    HtmlHelper.ExportHtml(table, properties);//.Fields, properties.Filename);
                //}
                //base.UpdateProcessing();
                //ExecuteCommit();
                return flag;
            }
            catch (Exception exception)
            {
                flag = false;
                base.CancelExecute(exception.Message);
            }
            finally
            {
                //base.EndProcessing();
                //if (output != null)
                //{
                //    output.WriteLine(base.ObjectsProcessed + " records written.");
                //}
            }
            return flag;
        }

  
        private void WriteHtml()
        {
            HtmlWriteProperties properties = this.Properties as HtmlWriteProperties;
            DataTable table = base.Output.Value as DataTable;

            // Create Dataset
            //DataTable dtExport = dataSource.Copy();
            //if (dtExport.TableName == "")
            //    dtExport.TableName = "ExportValues";

            if (properties.DataSource.Columns.Count == 0)
            {
                properties.DataSource.CreateFields(table);
            }
            properties.DataSource.ValidateTableName(table.TableName);

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
            sw.WriteLine(properties.DataSource.TableName);
            sw.WriteLine("</title>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");

            sw.WriteLine("<table border=1>");
            sw.WriteLine("<tr>");

            foreach (AdoColumn c in properties.DataSource.Columns)// (int c = 0; c < fields.Length; c++)
            {
                sw.Write("<td>{0}</td>", c.ToString());
            }

            sw.WriteLine("</tr>");

            for (int r = 0; r < table.Rows.Count; r++)
            {
                sw.WriteLine("<tr>");
                foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
                {
                    sw.Write("<td>{0}</td>", table.Rows[r][f.ColumnName]);
                }
                base.UpdateProcessing();
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

        public override AdoTable GetSchema()
        {
            HtmlWriteProperties properties = this.Properties as HtmlWriteProperties;
                return properties.DataSource;
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader)
        {
            return Export(table, fileName, firstRowHeader, null);
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            bool flag = true;
            HtmlWriteProperties properties = new HtmlWriteProperties();
            properties.Filename = fileName;
            properties.FirstRowHeaders = firstRowHeader;

            if (properties.Filename == "")
            {
                throw new ArgumentNullException("No filename specified");
            }
            if (table == null)
            {
                throw new ArgumentNullException("Inputs/Outputs are invalid");
            }
            properties.DataSource = AdoTable.CreateSchema(table, fields);

            System.IO.FileStream fs = null;
            System.IO.BufferedStream bs =null;
            System.IO.StreamWriter sw = null;

            try
            {

                fs = new System.IO.FileStream(properties.Filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                bs = new System.IO.BufferedStream(fs);
                sw = new System.IO.StreamWriter(bs);
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<title>");
                sw.WriteLine(properties.DataSource.TableName);
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");

                sw.WriteLine("<table border=1>");
                sw.WriteLine("<tr>");

                foreach (AdoColumn c in properties.DataSource.Columns)
                {
                    sw.Write("<td>{0}</td>", c.ToString());
                }

                sw.WriteLine("</tr>");

                for (int r = 0; r < table.Rows.Count; r++)
                {
                    sw.WriteLine("<tr>");
                    foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
                    {
                        sw.Write("<td>{0}</td>", table.Rows[r][f.ColumnName]);
                    }
                    sw.WriteLine("</tr>");
                }

                sw.WriteLine("</table>");
                sw.WriteLine("</hr>");

                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }
            finally
            {
                sw.Close();
                bs.Close();
                fs.Close();
            }
            return flag;

        }

  
    }
}

