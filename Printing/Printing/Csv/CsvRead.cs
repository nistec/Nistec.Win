namespace Nistec.Printing.Csv
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using Nistec.Printing.Data;

    public class CsvReader : AdoMap, IAdoReader
    {
        private string _filenameColumnName = string.Empty;
        private string[] _files;
        private StreamReader _reader;
        private uint _rowsTotal;
        private const string DV_FILENAME = " - Filename";
        private const string DV_FILENAME_DESCRIPTION = "Name and path of file to be read.";
        private const int ESTIMATE_RECORD_COUNT_SCAN = 100;


        public CsvReader(CsvReadProperties properties)
        {
            this.Properties = properties.Clone();// new CsvReadProperties();
            base.Output = new AdoOutput("Records Read", "Records successfully read from the configured Text File.");
        }

        public override uint ExecuteCommit()
        {
            uint res = base.ExecuteCommit();
            if (this._reader != null)
            {
                this._reader.Close();
                this._reader = null;
            }
            return res;
        }


        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            CsvReadProperties properties = this.Properties as CsvReadProperties;
            DataTable table = null;
            try
            {
                if (properties.Filename == "")
                {
                    base.CancelExecute("No filename specified");
                    return false;
                }
                if (base.Output == null)
                {
                    base.CancelExecute("Inputs/Outputs are invalid");
                    return false;
                }
                if (properties.MultipleFiles && properties.VerticalRecords)
                {
                    table = DataHelper.CreateTable(CsvHelper.GetVerticalRecordSchema(properties), "RecordsRead");
                }
                else
                {
                    table = DataHelper.CreateTable(properties.DataSource, "RecordsRead");
                }
                this._filenameColumnName = "Filename";
                if (properties.IncludeFilename)
                {
                    if (table.Columns.Contains(this._filenameColumnName))
                    {
                        int num = 1;
                        while (table.Columns.Contains(this._filenameColumnName + num))
                        {
                            num++;
                        }
                        this._filenameColumnName = this._filenameColumnName + num;
                    }
                    DataColumn column = new DataColumn(this._filenameColumnName, typeof(string));
                    column.AllowDBNull = true;
                    table.Columns.Add(column);
                }
                string path = string.Empty;
                string line = string.Empty;
                //base.ExecuteBegin(this._rowsTotal);
                table.BeginLoadData();
                //base.OnExecutionStarted(this._rowsTotal);
                while (((batchSize == 0) || (table.Rows.Count < batchSize)) && !base.StopProcessing)
                {
                    if (this._reader == null)
                    {
                        for (int i = 0; i < this._files.Length; i++)
                        {
                            path = this._files[i];
                            if (path != string.Empty)
                            {
                                this._files[i] = string.Empty;
                                this._reader = new StreamReader(path, Encoding.GetEncoding(properties.Encoding));
                                if ((!properties.MultipleFiles || !properties.VerticalRecords) && ((properties.FileType == FileTypes.Delimited) && properties.FirstRowHeaders))
                                {
                                    line = CsvHelper.ReadNextRecord(this._reader, properties);
                                }
                                break;
                            }
                        }
                    }
                    if (this._reader == null)
                    {
                        base.BatchNotResponding = true;
                        break;
                    }
                    if (properties.MultipleFiles && properties.VerticalRecords)
                    {
                        DataRow row = table.NewRow();
                        int num3 = 0;
                        while ((line = CsvHelper.ReadNextRecord(this._reader, properties)) != null)
                        {
                            object[] objArray = CsvHelper.ParseLine(line, properties);
                            object obj2 = DBNull.Value;
                            string name = "Column" + ++num3;
                            if (properties.FirstRowHeaders)
                            {
                                if (objArray.Length > 0)
                                {
                                    name = objArray[0].ToString();
                                }
                                if (objArray.Length > 1)
                                {
                                    obj2 = objArray[1];
                                }
                                if (table.Columns.Contains(name))
                                {
                                    row[name] = obj2;
                                }
                            }
                            else if ((objArray.Length > 0) && (table.Columns.Count >= num3))
                            {
                                row[num3 - 1] = objArray[0];
                            }
                        }
                        if (properties.IncludeFilename)
                        {
                            row[this._filenameColumnName] = Path.GetFileName(path);
                        }
                        table.LoadDataRow(row.ItemArray, true);
                        base.UpdateProcessing();
                        this._reader.Close();
                        this._reader = null;
                        if (properties.ArchiveAfterRead)
                        {
                            if (!Directory.Exists(properties.ArchiveFolder))
                            {
                                //if (output != null)
                                //{
                                //    output.WriteLine("Warning: Failed to move file '" + path + "' to archive folder: Folder '" + properties.ArchiveFolder + "' does not exist.");
                                //}
                            }
                            else
                            {
                                File.Move(path, Path.Combine(properties.ArchiveFolder, Path.GetFileName(path)));
                            }
                        }
                    }
                    else
                    {
                        line = CsvHelper.ReadNextRecord(this._reader, properties);
                        if (line == null)
                        {
                            this._reader.Close();
                            this._reader = null;
                            if (properties.ArchiveAfterRead)
                            {
                                if (!Directory.Exists(properties.ArchiveFolder))
                                {
                                    //if (output != null)
                                    //{
                                    //    output.WriteLine("Warning: Failed to move file '" + path + "' to archive folder: Folder '" + properties.ArchiveFolder + "' does not exist.");
                                    //}
                                }
                                else
                                {
                                    File.Move(path, Path.Combine(properties.ArchiveFolder, Path.GetFileName(path)));
                                }
                            }
                        }
                        else
                        {
                            object[] values = CsvHelper.ParseLine(line, properties);
                            if (properties.IncludeFilename)
                            {
                                object[] array = new object[values.Length + 1];
                                values.CopyTo(array, 0);
                                array[array.Length - 1] = Path.GetFileName(path);
                                table.LoadDataRow(array, true);
                            }
                            else
                            {
                                table.LoadDataRow(values, true);
                            }
                            base.UpdateProcessing();
                        }
                    }
                }
                try
                {
                    table.EndLoadData();
                }
                catch (ConstraintException)
                {
                    //if (output != null)
                    //{
                    //foreach (DataRow row2 in table.Rows)
                    //{
                    //    if (row2.HasErrors)
                    //    {
                    //        output.WriteLine(DataHelper.FormatRowError(row2));
                    //    }
                    //}
                    //}
                }
                catch (Exception exception)
                {
                    base.CancelExecute("   warning: " + exception.Message.Replace("\r\n", " "));
                }
                base.Output.Value = table;

            }
            catch (Exception exception2)
            {
                flag = false;
                base.CancelExecute(exception2.Message);
            }
            return flag;
        }

        public override void ExecuteBegin(uint totalObjects)
        {
            CsvReadProperties properties = this.Properties as CsvReadProperties;
            if (properties.Filename != "")
            {
                //base.ExecuteBegin(totalObjects);
                this._rowsTotal = 0;
                string extension = Path.GetExtension(properties.Filename);
                this._files = new string[] { properties.Filename };
                if (properties.MultipleFiles)
                {
                    if (extension == string.Empty)
                    {
                        this._files = Directory.GetFiles(Path.GetDirectoryName(properties.Filename));
                    }
                    else
                    {
                        this._files = Directory.GetFiles(Path.GetDirectoryName(properties.Filename), "*" + extension);
                    }
                    if (properties.VerticalRecords)
                    {
                        this._rowsTotal = (uint)this._files.Length;
                    }
                }

                base.ExecuteBegin(totalObjects);
                //base.BeginProcessing(this.rowsTotal);
            }
        }

        public static DataTable ImportOleDb(string fileName, bool firstRowHeader)
        {
            CsvReadProperties properties = new CsvReadProperties();
            properties.Filename = fileName;
            properties.FirstRowHeaders = firstRowHeader;

            string line = string.Empty;
            DataTable table = new DataTable();
            StreamReader reader = null;

            reader = new StreamReader(fileName, Encoding.GetEncoding(properties.Encoding));
            if (reader == null)
            {
                return null;
            }

            line = CsvHelper.ReadNextRecord(reader, properties);
            object[] values = CsvHelper.ParseLine(line, properties);

            if (firstRowHeader)//(properties.FileType == FileTypes.Delimited) && properties.FirstRowHeaders)
            {
                foreach (object o in values)
                {
                    table.Columns.Add(new DataColumn(o.ToString()));
                }
            }
            else
            {
                int col = 1;
                foreach (object o in values)
                {
                    table.Columns.Add(new DataColumn("Field" + col.ToString()));
                    col++;
                }
            }

            table.BeginLoadData();
            if (!firstRowHeader)
            {
                table.LoadDataRow(values, true);
            }

            while (!reader.EndOfStream)
            {
                line = CsvHelper.ReadNextRecord(reader, properties);
                if (line == null)
                {
                    reader.Close();
                    reader = null;
                    break;
                }
                else
                {
                    values = CsvHelper.ParseLine(line, properties);
                    table.LoadDataRow(values, true);
                }
            }
            table.EndLoadData();
            
            return table;
        }
        #region csv

        public static DataTable Import(string fileName, bool firstRowHeader)
        {
            return Import(fileName, new char[] { ',', '\t' }, firstRowHeader);
        }

        public static DataTable Import(string fileName, char[] charDelimiter, bool firstRowHeader)
        {
            return Import(fileName, Encoding.Default, charDelimiter, firstRowHeader);
        }

        public static DataTable Import(string file, Encoding encoding, char[] charDelimiter, bool firstRowHeader)
        {
            //_Message = "";
            DataTable tbl = null;
            try
            {
                //Set the delimiter character for use in splitting the copied data
                //char[] charDelimiter = new char[] { '\t' };
                //string[] charDelimiter = new string[] { delimiter };

                using (System.IO.StreamReader srReadExcel = new System.IO.StreamReader(file, encoding))
                {
                    string sFormattedData = "";
                    //string s = srReadExcel.ReadToEnd();


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
                        //char[] ary = sFormattedData.ToCharArray();

                        //Split the string contents into an array
                        arrSplitData = sFormattedData.Split(charDelimiter, StringSplitOptions.None);// (charDelimiterArray);

                        if (cols == 0)
                        {
                            for (i = 0; i < arrSplitData.Length; i++)
                            {
                                object o = arrSplitData.GetValue(i);
                                if (o == null)
                                    tbl.Columns.Add();
                                else
                                    tbl.Columns.Add(o.ToString());

                            }
                            i = 0;
                            cols = tbl.Columns.Count;
                            if (firstRowHeader)
                                continue;
                        }

                        //Row to hold a single row of the Excel Data
                        //DataRow rowNew ;
                        DataRow rowNew = tbl.NewRow();

                        int colSplit = arrSplitData.Length;
                        if (colSplit < cols)
                        {
                            if (colSplit <= 0)
                            {
                                rowNew = null;
                                continue;
                            }
                            for (i = 0; i < colSplit; i++)
                            {
                                rowNew[i] = arrSplitData.GetValue(i);//.ToString().TrimStart(new char[]{'"'}).TrimEnd(new char[]{'"'});
                            }

                            //throw new ArgumentException("Error split DataFormats , Try format cells to general format or remove Comma Separated from cells;");
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
                //Bind the data to the DataGrid
                //dgrExcelContents.DataSource = tbl.DefaultView();
                return tbl;
            }
            catch (Exception exp)
            {
               // _Message = (exp.Message);
                throw exp;
            }
            //return null;
        }


        public static DataTable ReadCsvData(string data, char[] charDelimiter, bool firstRowHeader)
        {
            DataTable tbl = null;
            //_Message = "";
            try
            {

                //Proceed if some copied data is present
                if (string.IsNullOrEmpty(data))
                {
                    //_Message = "Empty Data";
                    return null;
                }

                //Cast the copied data in the CommaSeparatedValue format & hold in a StreamReader Object
                using (System.IO.StringReader srReadExcel = new System.IO.StringReader(data))//(string)objData.GetData(dataFormats.ToString())))
                {
                    string sFormattedData = "";
                    //string s = srReadExcel.ReadToEnd();

                    //Set the delimiter character for use in splitting the copied data
                    //char[] charDelimiter = new char[] { '\t' };
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
                        //char[] ary = sFormattedData.ToCharArray();

                        //Split the string contents into an array
                        arrSplitData = sFormattedData.Split(charDelimiter, StringSplitOptions.None);// (charDelimiterArray);

                        if (cols == 0)
                        {
                            for (i = 0; i < arrSplitData.Length; i++)
                            {
                                object o = arrSplitData.GetValue(i);
                                if (o == null)
                                    tbl.Columns.Add();
                                else
                                    tbl.Columns.Add(o.ToString());
                            }
                            i = 0;
                            cols = tbl.Columns.Count;
                            if (firstRowHeader)
                                continue;
                        }

                        //Row to hold a single row of the Excel Data
                        //DataRow rowNew ;
                        DataRow rowNew = tbl.NewRow();

                        int colSplit = arrSplitData.Length;
                        if (colSplit < cols)
                        {
                            if (colSplit <= 0)
                            {
                                rowNew = null;
                                continue;
                            }
                            for (i = 0; i < colSplit; i++)
                            {
                                rowNew[i] = arrSplitData.GetValue(i);//.ToString().TrimStart(new char[]{'"'}).TrimEnd(new char[]{'"'});
                            }
                            //throw new ArgumentException("Error split DataFormats , Try format cells to general format or remove Comma Separated from cells;");
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
                //Bind the data to the DataGrid
                //dgrExcelContents.DataSource = tbl.DefaultView();
                return tbl;
            }
            catch (Exception exp)
            {
                //_Message = (exp.Message);
                throw exp;
            }
            //return null;

        }


  
        #endregion
    }
}

