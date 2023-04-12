using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
//==using Nistec.Util;
using System.Windows.Forms;
using System.Collections;

namespace Nistec.Printing.Clipboard
{
    public class Helper
    {
        //public static DataTable GetDataClipboard()
        //{
        //    _Message = "";
        //    try
        //    {
        //        //Read the copied data from the Clipboard
        //        IDataObject objData = System.Windows.Forms.Clipboard.GetDataObject();
        //        if (objData == null)
        //        {
        //            //MsgBox.ShowWarning("Clipboard is empty!");
        //            _Message = ("Clipboard is empty!");
        //            return null;
        //        }

        //        //char[] charDelimiter = new char[] { ',', '\t' };

        //        //Next proceed only of the copied data is in the CSV format indicating Excel content
        //        if (objData.GetDataPresent(DataFormats.CommaSeparatedValue))
        //        {
        //            return ReadClipboardComma(objData, new char[] { ',', '\t' });
        //        }
        //        else if (objData.GetDataPresent(DataFormats.UnicodeText))
        //        {
        //            return ReadClipboardText(objData, DataFormats.UnicodeText, new char[] { '\t' });
        //        }
        //        else if (objData.GetDataPresent(DataFormats.Text))
        //        {
        //            return ReadClipboardText(objData, DataFormats.Text, new char[] { '\t' });
        //        }
        //        else
        //        {
        //            //MsgBox.ShowWarning("Clipboard data does not seem to be copied from Excel!");
        //            _Message = ("Clipboard data does not seem to be copied from Excel!");
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        _Message = exp.Message;
        //    }
        //    return null;
        //}

        //public static DataTable GetDataClipboard(string dataFormats)
        //{
        //    return GetDataClipboard(dataFormats, new char[] { ',', '\t' });
        //}

        //public static DataTable GetDataClipboard(string dataFormats, char[] charDelimiter)
        //{
        //    _Message = "";
        //    try
        //    {
        //        //Read the copied data from the Clipboard
        //        IDataObject objData = Clipboard.GetDataObject();
        //        if (objData == null)
        //        {
        //            //MsgBox.ShowWarning("Clipboard is empty!");
        //            _Message = ("Clipboard is empty!");
        //            return null;
        //        }
        //        //Proceed if some copied data is present
        //        //Next proceed only of the copied data is in the CSV format indicating Excel content
        //        if (!objData.GetDataPresent(dataFormats.ToString()))
        //        {
        //            //MsgBox.ShowWarning("Clipboard data does not seem to be copied from Excel!");
        //            _Message = ("Clipboard data does not seem to be copied from Excel!");
        //            return null;
        //        }
        //        if (dataFormats == DataFormats.CommaSeparatedValue)
        //        {
        //            return ReadClipboardComma(objData, charDelimiter);
        //        }
        //        if (dataFormats == DataFormats.Text)
        //        {
        //            return ReadClipboardText(objData, dataFormats, charDelimiter);
        //        }
        //        if (dataFormats == DataFormats.UnicodeText)
        //        {
        //            return ReadClipboardText(objData, dataFormats, charDelimiter);
        //        }

        //        _Message = ("Clipboard data does not seem to be copied from Excel!");
        //    }
        //    catch (Exception exp)
        //    {
        //        _Message = exp.Message;
        //    }
        //    return null;
        //}

        public static bool GetIsCommaSeparatedValueClipboard()
        {
            try
            {
                //Read the copied data from the Clipboard
                IDataObject objData = System.Windows.Forms.Clipboard.GetDataObject();

                //Proceed if some copied data is present
                if (objData != null)
                {
                    //Next proceed only of the copied data is in the CSV format indicating Excel content
                    if (objData.GetDataPresent(DataFormats.CommaSeparatedValue))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }

        public static bool GetIsDataClipboard()
        {
            try
            {
                //Read the copied data from the Clipboard
                IDataObject objData = System.Windows.Forms.Clipboard.GetDataObject();

                if (objData.GetDataPresent(System.Windows.Forms.DataFormats.CommaSeparatedValue))
                {
                    return true;
                }
                else if (objData.GetDataPresent(DataFormats.UnicodeText))
                {
                    return true;
                }
                else if (objData.GetDataPresent(DataFormats.Text))
                {
                    return true;
                }
                 return false;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }


        public static DataTable ReadClipboardText(IDataObject objData, string dataFormats, char[] charDelimiter)
        {
            DataTable tbl = null;
            using (System.IO.StringReader srReadExcel = new System.IO.StringReader((string)objData.GetData(dataFormats.ToString())))
            {
                string sFormattedData = "";
                //string[] charDelimiter = new string[] { delimiter };

                //Define a DataTable to hold the copied data for binding to the DataGrid
                tbl = new DataTable();
                int cols = 0;

                //Loop till no further data is available
                while (srReadExcel.Peek() > 0)
                {
                    //Array to hold the split data for each row
                    System.Array arrSplitData = null;

                    int i = 0;

                    //Read a line of data from the StreamReader object
                    sFormattedData = srReadExcel.ReadLine();

                    //Split the string contents into an array
                    arrSplitData = sFormattedData.Split(charDelimiter, StringSplitOptions.None);// (charDelimiterArray);

                    if (cols == 0)
                    {
                        for (i = 0; i < arrSplitData.Length; i++)
                        {
                            tbl.Columns.Add();
                        }
                        i = 0;
                        cols = tbl.Columns.Count;
                    }

                    //Row to hold a single row of the Excel Data
                    //DataRow rowNew ;
                    DataRow rowNew = tbl.NewRow();

                    int colSplit = arrSplitData.Length;
                    if (colSplit != cols)
                    {
                        throw new ArgumentException("Error split DataFormats , Try format cells to general format or remove Comma Separated from cells;");
                    }

                    else //if (colSplit == cols)
                    {
                        for (i = 0; i < cols; i++)
                        {
                            rowNew[i] = arrSplitData.GetValue(i);//.ToString().TrimStart(new char[]{'"'}).TrimEnd(new char[]{'"'});
                        }

                    }

                    i = 0;

                    //Add the row back to the DataTable
                    tbl.Rows.Add(rowNew);

                    rowNew = null;
                }

                //Close the StreamReader object
                srReadExcel.Close();
            }
            return tbl;
        }

        public static DataTable ReadClipboardComma(IDataObject objData, char[] charDelimiter)
        {
            DataTable tbl = null;
            using (StreamReader srReadExcel = new StreamReader((Stream)objData.GetData(DataFormats.CommaSeparatedValue), Encoding.Default))
            {
                string sFormattedData = "";

                //Set the delimiter character for use in splitting the copied data
                //char[] charDelimiter = new char[] { ',','\t' };

                //Define a DataTable to hold the copied data for binding to the DataGrid
                tbl = new DataTable();
                int cols = 0;

                //Loop till no further data is available
                while (srReadExcel.Peek() > 0)
                {
                    //Array to hold the split data for each row
                    System.Array arrSplitData = null;

                    int i = 0;

                    //Read a line of data from the StreamReader object
                    sFormattedData = srReadExcel.ReadLine();

                    //Split the string contents into an array
                    arrSplitData = sFormattedData.Split(charDelimiter);//, StringSplitOptions.RemoveEmptyEntries);// (charDelimiterArray);

                    if (cols == 0)
                    {
                        for (i = 0; i < arrSplitData.Length; i++)
                        {
                            tbl.Columns.Add();
                        }
                        i = 0;
                        cols = tbl.Columns.Count;
                    }

                    //Row to hold a single row of the Excel Data
                    //DataRow rowNew ;
                    DataRow rowNew = tbl.NewRow();

                    int colSplit = arrSplitData.Length;
                    if (colSplit != cols)
                    {
                        throw new ArgumentException("Error split DataFormats , Try format cells to general format or remove Comma Separated from cells;");
                    }

                    else //if (colSplit == cols)
                    {
                        for (i = 0; i < cols; i++)
                        {
                            rowNew[i] = arrSplitData.GetValue(i);//.ToString().TrimStart(new char[]{'"'}).TrimEnd(new char[]{'"'});
                        }

                    }

                    i = 0;

                    //Add the row back to the DataTable
                    tbl.Rows.Add(rowNew);

                    rowNew = null;
                }

                //Close the StreamReader object
                srReadExcel.Close();
            }
            return tbl;

        }
        //public string GetCSVFromClipBoard()
        //{

        //    IDataObject o = Clipboard.GetDataObject();
        //    string s = null;

        //    if (o.GetDataPresent(DataFormats.CommaSeparatedValue))
        //    {

        //        StreamReader sr = new StreamReader((Stream)o.GetData(DataFormats.CommaSeparatedValue));

        //        s = sr.ReadToEnd();

        //        sr.Close();
        //        //Console.WriteLine(s);

        //    }
        //    return s;

        //}



        //public void CopyCSVToClipBoard(string csv)
        //{

        //    //String csv = "1,2,3" + Environment.NewLine + "6,8,3";

        //    byte[] blob = System.Text.Encoding.Default.GetBytes(csv);

        //    MemoryStream s = new MemoryStream(blob);

        //    DataObject data = new DataObject();

        //    data.SetData(DataFormats.CommaSeparatedValue, s);

        //    Clipboard.SetDataObject(data, true);

        //}

        //public void CopyCSVToClipBoard(string csv, System.Text.Encoding encode)
        //{

        //    //String csv = "1,2,3" + Environment.NewLine + "6,8,3";

        //    byte[] blob = encode.GetBytes(csv);
        //    //byte[] blob = System.Text.Encoding.UTF8.GetBytes(csv);

        //    MemoryStream s = new MemoryStream(blob);

        //    DataObject data = new DataObject();

        //    data.SetData(DataFormats.CommaSeparatedValue, s);

        //    Clipboard.SetDataObject(data, true);

        //}


    }
}
