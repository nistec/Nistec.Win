namespace Nistec.Printing.Xml
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;
    using System.Threading;
    using Nistec.Printing.Data;

    public class XmlReader : AdoMap,IAdoReader
    {
        private string _filenameColumnName = string.Empty;


        public XmlReader(XmlReadProperties properties)
        {
            this.Properties = properties.Clone();// new XmlReadProperties();
            base.Output = new AdoOutput("Records Read", "Records successfully read from the configured Xml File.");
        }

 
        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            XmlReadProperties properties = this.Properties as XmlReadProperties;
            DataTable table = new DataTable();
            try
            {
                if (properties.Filename == "")
                {
                    base.CancelExecute("No filename specified");
                    return false;
                }
                if (properties.TableName == "")
                {
                    base.CancelExecute("No TableName specified");
                    return false;
                }
                if (base.Output == null)//s.Count < 1)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }
                //DataSet ds = new DataSet();
                table.ReadXml(properties.Filename);
                table.TableName = properties.TableName;

                base.Output.Value = table;// ds.Tables[properties.TableName];
            }
            catch (Exception exception2)
            {
                flag = false;
                base.CancelExecute( exception2.Message);
            }
            return flag;
        }


        public static DataTable Import(string fileName, string tableName)
        {
            DataTable table = new DataTable();
            if (fileName == "")
            {
                throw new ArgumentNullException("No filename specified");
            }
            table.ReadXml(fileName);
            table.TableName = tableName;

            return table;
        }

     
    }
}

