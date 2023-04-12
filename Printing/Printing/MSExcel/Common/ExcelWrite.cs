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

    public class ExcelWriter : /*AdoMap,*/ IAdoWriter
    {

        public static IAdoWriter CreateWriter(ExcelWriteProperties properties)
        {
            ExcelWriterType type = properties.ExcelWriterType;

            switch (type)
            {
                case ExcelWriterType.ExcelOleDb:
                    return  new OleDb.ExcelWriter(properties) as IAdoWriter;
                case ExcelWriterType.ExcelXml:
                    return new Xml.ExcelWriter(properties) as IAdoWriter;
                default:
                    return new Xml.ExcelWriter(properties) as IAdoWriter;
            }
            //this.Properties = properties.Clone();// new ExcelWriteProperties();
            //base.Output=new AdoOutput("Records To Write", "Records successfully written to the configured MS Excel.");//, true);
        }

        IAdoWriter _writer;

        public ExcelWriter( ExcelWriteProperties properties)
        {
            ExcelWriterType type = properties.ExcelWriterType;

            switch (type)
            {
                case ExcelWriterType.ExcelOleDb:
                    _writer = new OleDb.ExcelWriter(properties);
                    break;
                case ExcelWriterType.ExcelXml:
                    _writer = new Xml.ExcelWriter(properties);
                    break;
                default:
                    _writer = new Xml.ExcelWriter(properties);
                    break;
            }
            //this.Properties = properties.Clone();// new ExcelWriteProperties();
            //base.Output=new AdoOutput("Records To Write", "Records successfully written to the configured MS Excel.");//, true);
        }


        public bool ExecuteBatch(uint batchSize)
        {
           return Execute();
        }

        public bool Execute()
        {
            return _writer.Execute();
        }

        public uint ExecuteCommit()
        {
            return _writer.ExecuteCommit();
        }
        public void ExecuteBegin(uint totalObjects)
        {
            _writer.ExecuteBegin(totalObjects);
        }
        public void CancelExecute(string message)
        {
            _writer.CancelExecute(message);
        }
        public AdoProperties Properties
        {
            get
            {
                return _writer.Properties;
            }
            set
            {
                _writer.Properties = value;
                //this.OnPropertiesChanged();
            }
        }

        public AdoOutput Output
        {
            get
            {
                return _writer.Output;
            }
            //set { _reader.Output = value; }
        }


        //public AdoTable GetSchema()
        //{
            
        //    ExcelWriteProperties properties = this.Properties as ExcelWriteProperties;
        //    return properties.DataSource;
        //}

 
    }
}

