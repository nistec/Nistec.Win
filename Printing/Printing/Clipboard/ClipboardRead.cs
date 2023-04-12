namespace Nistec.Printing.Clipboard
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

    public class ClipboardReader : AdoMap,IAdoReader
    {
        private string _filenameColumnName = string.Empty;


        public ClipboardReader(ClipboardReadProperties properties)
        {
            this.Properties = properties.Clone();// new ClipboardReadProperties();
            base.Output = new AdoOutput("Records Read", "Records successfully read from Clipboard.");
        }


        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            ClipboardReadProperties properties = this.Properties as ClipboardReadProperties;
            DataTable table = null;
            try
            {
                if (base.Output == null)//s.Count < 1)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }
                //Read the copied data from the Clipboard
                System.Windows.Forms.IDataObject objData = System.Windows.Forms.Clipboard.GetDataObject();
                if (objData == null)
                {
                    base.CancelExecute ("Clipboard is empty!");
                    return false;
                }

                //char[] charDelimiter = new char[] { ',', '\t' };

                //Next proceed only of the copied data is in the CSV format indicating Excel content
                if (objData.GetDataPresent(System.Windows.Forms.DataFormats.CommaSeparatedValue))
                {
                    table = Helper.ReadClipboardComma(objData, new char[] { ',', '\t' });
                }
                else if (objData.GetDataPresent(DataFormats.UnicodeText))
                {
                    table = Helper.ReadClipboardText(objData, DataFormats.UnicodeText, new char[] { '\t' });
                }
                else if (objData.GetDataPresent(DataFormats.Text))
                {
                    table = Helper.ReadClipboardText(objData, DataFormats.Text, new char[] { '\t' });
                }
                else
                {
                    throw new  Exception ("Clipboard data does not seem to be copied as DataTable!");
                }
                base.Output.Value = table;
            }
            catch (Exception exception2)
            {
                flag = false;
                base.CancelExecute( exception2.Message);
            }
            return flag;
        }

        /// <summary>
        /// Read Clipboard
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadClipboard()
        {
            //bool flag = true;
            //ClipboardReadProperties properties = this.Properties as ClipboardReadProperties;
            DataTable table = null;
            try
            {
                 //Read the copied data from the Clipboard
                System.Windows.Forms.IDataObject objData = System.Windows.Forms.Clipboard.GetDataObject();
                if (objData == null)
                {
                    //base.CancelExecute("Clipboard is empty!");
                    return null;
                }

                //char[] charDelimiter = new char[] { ',', '\t' };

                //Next proceed only of the copied data is in the CSV format indicating Excel content
                if (objData.GetDataPresent(System.Windows.Forms.DataFormats.CommaSeparatedValue))
                {
                    table = Helper.ReadClipboardComma(objData, new char[] { ',', '\t' });
                }
                else if (objData.GetDataPresent(DataFormats.UnicodeText))
                {
                    table = Helper.ReadClipboardText(objData, DataFormats.UnicodeText, new char[] { '\t' });
                }
                else if (objData.GetDataPresent(DataFormats.Text))
                {
                    table = Helper.ReadClipboardText(objData, DataFormats.Text, new char[] { '\t' });
                }
                else
                {
                    throw new Exception("Clipboard data does not seem to be copied as DataTable!");
                }
                return table;
            }
            catch //(Exception exception2)
            {
                //base.CancelExecute(exception2.Message);
                return null;
            }
        }

    
    }
}

