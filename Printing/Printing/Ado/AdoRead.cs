namespace Nistec.Printing.Data
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Reflection;
    //using System.Windows.Forms;
    using System.IO;

    public class AdoReader : IAdoReader
    {
        IAdoReader _reader;

        public AdoReader(ReaderType type, AdoProperties properties)
        {
            switch (type)
            {
                case ReaderType.Excel2003:
                    _reader = new Nistec.Printing.MSExcel.Bin2003.ExcelReader(properties as MSExcel.ExcelReadProperties);
                    break;
                case ReaderType.Excel2007:
                    throw new ArgumentException("Not implemented");
                case ReaderType.ExcelOleDb:
                    _reader = new Nistec.Printing.MSExcel.OleDb.ExcelReader(properties as MSExcel.ExcelReadProperties);
                    break;
                case ReaderType.ExcelXml:
                    _reader = new Nistec.Printing.MSExcel.Xml.ExcelReader(properties as MSExcel.ExcelReadProperties);
                    break;
                case ReaderType.Csv:
                    _reader = new Nistec.Printing.Csv.CsvReader(properties as Csv.CsvReadProperties);
                    break;
                case ReaderType.Xml:
                    _reader = new Nistec.Printing.Xml.XmlReader(properties as Xml.XmlReadProperties);
                    break;
                case ReaderType.Clipboard:
                    _reader = new Nistec.Printing.Clipboard.ClipboardReader(properties as Clipboard.ClipboardReadProperties);
                    break;
                default:
                    throw new ArgumentException("WriterType not supported");
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

        public AdoOutput Output
        {
            get { return _reader.Output; }
        }

        public AdoProperties Properties
        {
            get { return _reader.Properties; }
            set { _reader.Properties = value; }
        }
    }
}

