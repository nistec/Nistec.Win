namespace Nistec.Printing.Custom
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using Nistec.Printing.Data;

    public class CustomTable : AdoMap
    {

        public CustomTable()
        {
            this.Properties = new TableProperties();
            base.Output=new AdoOutput("Custom Records", "Records manually created.");
            base.Output.Value = new DataTable();
            base.SaveData = true;
        }

        public override bool ExecuteBatch(uint batchSize)
        {
            //base.BeginProcessing(1);
            if (base.Output.Value is DataTable)
            {
                DataTable table = base.Output.Value as DataTable;
                DataTable table2 = table.Clone();
                foreach (DataRow row in table.Rows)
                {
                    table2.Rows.Add(row.ItemArray);
                }
                table2.AcceptChanges();
                base.Output.Value = table2;
            }
            base.UpdateProcessing();
            Thread.Sleep(100);
            //base.EndProcessing();
            return true;
        }

 
      }
}

