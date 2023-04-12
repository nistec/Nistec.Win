namespace Nistec.Printing.MSExcel
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Reflection;
    using Nistec.Printing.Data;
    using System.IO;

    public class ExcelReader : /*AdoMap,*/ IAdoReader
    {

        public static IAdoReader CreateReader(ExcelReadProperties properties)
        {
            ExcelReaderType type = properties.ExcelReaderType;

            switch (type)
            {
                case ExcelReaderType.Excel2003:
                    return new Nistec.Printing.MSExcel.Bin2003.ExcelReader(properties) as IAdoReader;
                case ExcelReaderType.Excel2007:
                    throw new ArgumentException("Not implemented");
                case ExcelReaderType.ExcelOleDb:
                    return new OleDb.ExcelReader(properties) as IAdoReader;
                case ExcelReaderType.ExcelXml:
                    return new Xml.ExcelReader(properties) as IAdoReader;
                default:
                    return new Nistec.Printing.MSExcel.Bin2003.ExcelReader(properties) as IAdoReader;
            }
        }


        IAdoReader _reader;

        public ExcelReader(ExcelReadProperties properties)
        {
            ExcelReaderType type = properties.ExcelReaderType;

            switch (type)
            {
                case ExcelReaderType.Excel2003:
                    _reader = new Nistec.Printing.MSExcel.Bin2003.ExcelReader(properties);
                    break;
                case ExcelReaderType.Excel2007:
                    throw new ArgumentException("Not implemented");
                case ExcelReaderType.ExcelOleDb:
                    _reader = new OleDb.ExcelReader(properties);
                    break;
                case ExcelReaderType.ExcelXml:
                    _reader = new Xml.ExcelReader(properties);
                    break;
                default:
                    _reader = new Nistec.Printing.MSExcel.Bin2003.ExcelReader(properties);
                    break;
            }
        }

        public void ExecuteBegin(uint totalObjects)
        {
            _reader.ExecuteBegin(totalObjects);
        }

        public uint ExecuteCommit()
        {
            return _reader.ExecuteCommit();
        }

        public bool Execute()
        {
            return _reader.Execute();
        }

        public bool ExecuteBatch(uint batchSize)
        {
            return _reader.ExecuteBatch(batchSize);
        }

        public void CancelExecute(string message)
        {
            _reader.CancelExecute(message);
        }

        public AdoProperties Properties
        {
            get
            {
                return _reader.Properties;
            }
            set
            {
                _reader.Properties = value;
                //this.OnPropertiesChanged();
            }
        }

        public AdoOutput Output
        {
            get
            {
                return _reader.Output;
            }
            //set { _reader.Output = value; }
        }


    }
}

