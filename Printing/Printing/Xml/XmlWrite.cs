namespace Nistec.Printing.Xml
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Nistec.Printing.Data;

    public class XmlWriter : AdoMap,IAdoWriter
    {

        public XmlWriter(XmlWriteProperties properties)
        {
            this.Properties = properties.Clone();// new XmlWriteProperties();
            base.Output=new AdoOutput("Records To Save", "Records successfully saved to the Xml File.");//, true);
        }

    
        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            XmlWriteProperties properties = this.Properties as XmlWriteProperties;
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
                WriteXml();
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


        private void WriteXml()
        {
            XmlWriteProperties properties = this.Properties as XmlWriteProperties;
            DataTable table = base.Output.Value as DataTable;

            if (properties.DataSource.Columns.Count == 0)
            {
                table.WriteXml(properties.Filename);
                return;
            }
            else
            {
                properties.DataSource.CreateFields(table);
                properties.DataSource.ValidateTableName(table.TableName);
            }
 
            System.Xml.XmlTextWriter xwriter = new System.Xml.XmlTextWriter(properties.Filename, Encoding.GetEncoding(properties.Encoding));
            xwriter.WriteStartDocument();
            xwriter.WriteStartElement(properties.DataSource.TableName);

            for (int r = 0; r < table.Rows.Count; r++)
            {

                xwriter.WriteStartElement("ROW");
                foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
                {
                    xwriter.WriteStartElement(f.ColumnName);
                    xwriter.WriteValue(table.Rows[r][f.ColumnName]);
                    xwriter.WriteEndElement();
                    //sw.Write("<{1}>{0}</{1}>", table.Rows[r][f.ColumnName], f.ColumnName);
                }
                xwriter.WriteEndElement();
                base.UpdateProcessing();
            }

            xwriter.WriteEndElement();
            xwriter.Close();
        }

        public override AdoTable GetSchema()
        {
            XmlWriteProperties properties = this.Properties as XmlWriteProperties;
                return properties.DataSource;
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader)
        {
            return Export(table, fileName, firstRowHeader, null);
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            bool flag = true;
            XmlWriteProperties properties = new XmlWriteProperties();
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

            System.Xml.XmlTextWriter xwriter = null;
            try
            {

                xwriter = new System.Xml.XmlTextWriter(properties.Filename, Encoding.GetEncoding(properties.Encoding));
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement(properties.DataSource.TableName);

                for (int r = 0; r < table.Rows.Count; r++)
                {

                    xwriter.WriteStartElement("ROW");
                    foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
                    {
                        xwriter.WriteStartElement(f.ColumnName);
                        xwriter.WriteValue(table.Rows[r][f.ColumnName]);
                        xwriter.WriteEndElement();
                        //sw.Write("<{1}>{0}</{1}>", table.Rows[r][f.ColumnName], f.ColumnName);
                    }
                    xwriter.WriteEndElement();
                }

                xwriter.WriteEndElement();
                xwriter.Close();
            }
            finally
            {
                xwriter.Close();
            }
            return flag;

        }

    
    }
}

