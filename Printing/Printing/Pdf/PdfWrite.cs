namespace Nistec.Printing.Pdf
{
    using Nistec.Printing;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Reflection;
    using System.IO;
    using Nistec.Printing.Data;
    using Nistec.Printing.View.Pdf;
    using Nistec.Printing.View;


    public class PdfWriter : AdoMap, IAdoWriter
    {

        public PdfWriter(PdfWriteProperties properties)
        {
            this.Properties = properties.Clone();// new ExcelWriteProperties();
            base.Output = new AdoOutput("Records To Write", "Records successfully written to the configured Pdf Writer.");//, true);
        }


        public override bool ExecuteBatch(uint batchSize)
        {
            bool flag = true;
            PdfWriteProperties properties = this.Properties as PdfWriteProperties;

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

                this.UpdateSchema();
                WritePdf();
                //base.UpdateProcessing();
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


        public override AdoTable GetSchema()
        {
            PdfWriteProperties properties = this.Properties as PdfWriteProperties;
            return properties.DataSource;
        }


        private void WritePdf()
        {
            PdfWriteProperties properties = (PdfWriteProperties)this.Properties;
            PDFprop prop = new PDFprop();
            prop.Title = properties.DataSource.TableName;
            prop.Author = "Nistec.Net";
            prop.Creator = "Nistec.Printing.Pdf";
            DataTable table = base.Output.Value as DataTable;
            ViewBuilder builder = new ViewBuilder();
            builder.CreateReport(table);
            builder.Generate();
            builder.Export(prop, Nistec.Printing.View.ExportType.Pdf, properties.Filename, false);
            //DataViewer.Export(table,prop, null, null, Nistec.Printing.View.ExportType.Pdf, properties.Filename, false, Nistec.Printing.View.PageOrientation.Default);

            /*                
                            //Initialize a new PDF Document
                        Document _document = new Document();

                        _document.Title = properties.DataSource.TableName;// "Table Sample";
                        _document.Author = "Nistec.Net";
                        _document.Creator = "Nistec.Printing.Pdf";

                        //Compression is set to true by default.
                        //_document.Compress = false;

                        Page page = null;
                        PdfGraphics graphics = null;
                        Table _table = BuildTable();
                        while (_table != null)
                        {
                            //Initialize new page with PageSize A4
                            page = new Page(PageSize.A4);

                            //Add page to document
                            _document.Pages.Add(page);

                            //Get the PDFGraphics object for drawing to the page.
                            graphics = page.Graphics;

                            //DrawTable at x = 50; y =75 and height = 700
                            //Overflow is return as Table
                            //Loop until there is no overflow.
                            _table = graphics.DrawTable(50, 75, 700, _table);
                        }
                        FileStream _fs = new FileStream(properties.Filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                        try
                        {
                            //Generate PDF to the stream
                            _document.Generate(_fs);
                            //MessageBox.Show("xmlToPdf_Sample.pdf generated successfully in Bin Folder");
                        }
                        finally
                        {
                            _fs.Flush();
                            _fs.Close();
                        }
            */
        }

        //private Table BuildTable()
        //{

        //    PdfWriteProperties properties = (PdfWriteProperties)this.Properties;
        //    this.UpdateSchema();
        //    DataTable dt = base.Output.Value as DataTable;

        //    if (properties.DataSource.Columns.Count == 0)
        //    {
        //        properties.DataSource.CreateFields(dt);
        //    }
        //    properties.DataSource.ValidateTableName(dt.TableName);


        //    //bool altrow = false;

        //    PdfFont font1 = new PdfFont(StandardFonts.Helvetica, FontStyle.Bold);
        //    PdfFont font2 = new PdfFont(StandardFonts.Helvetica, FontStyle.Regular);

        //    Border border = new Border(1f, RGBColor.Red, LineStyle.Solid);

        //    TableStyle style1 = new TableStyle(font1, 8, RGBColor.Black);
        //    TableStyle style2 = new TableStyle(font1, 8, RGBColor.Black, TextAlignment.Center, ContentAlignment.MiddleCenter, true, new CMYKColor(0, 0, 255, 0), border, false);
        //    TableStyle style3 = new TableStyle(font2, 8, RGBColor.Black, TextAlignment.Center, ContentAlignment.MiddleCenter, true, RGBColor.Transparent, border, false);
        //    TableStyle style4 = new TableStyle(font2, 8, RGBColor.Black, TextAlignment.Center, ContentAlignment.MiddleCenter, true, RGBColor.Lavender, border, false);

        //    Table table = new Table(style1);
        //    table.CellPadding = 4f;
        //    //CreateColumns(table);
        //    foreach (AdoColumn c in properties.DataSource.Columns)
        //    {
        //        table.Columns.Add((float)c.Length);
        //    }

        //    //CreateRowHeadings(table, style2);
        //    Row row = null;
        //    row = new Row(table, style2);
        //    row.Height = 25;
        //    foreach (AdoColumn c in properties.DataSource.Columns)
        //    {
        //        row.Cells.Add(c.ToString());
        //    }
        //    table.Rows.Add(row);
        //    row = null;

        //    for (int r = 0; r < dt.Rows.Count; r++)
        //    {
        //        row = new Row(table, style3);
        //        row.Height = 24;
        //        row.KeepTogether = true;

        //        foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
        //        {

        //            row.Cells.Add(dt.Rows[r][f.ColumnName].ToString());

        //            //if (altrow == false)
        //            //{
        //            //    CreateRow(table, data, style3);
        //            //    altrow = true;
        //            //}
        //            //else
        //            //{
        //            //    CreateRow(table, data, style4);
        //            //    altrow = false;
        //            //}
        //        }
        //        table.Rows.Add(row);
        //        base.UpdateProcessing();
        //    }

        //    return table;
        //}

        //private void CreateRowHeadings(Table table, TableStyle style)
        //{
        //    Row row = new Row(table, style);
        //    row.Height = 25;
        //    row.Cells.Add("ID");
        //    row.Cells.Add("Product Name");
        //    row.Cells.Add("Quantity Per Unit");
        //    row.Cells.Add("Unit Price");
        //    table.Rows.Add(row);
        //}

        //int _index = 1;
        //private void CreateRow(Table table, XmlTextReader data, TableStyle style)
        //{
        //    Row row = new Row(table, style);
        //    row.Height = 24;
        //    row.KeepTogether = true;
        //    row.Cells.Add(_index.ToString());
        //    row.Cells.Add(data.GetAttribute("ProductName"));
        //    row.Cells.Add(data.GetAttribute("QuantityPerUnit"));
        //    row.Cells.Add("$" + data.GetAttribute("UnitPrice"));
        //    table.Rows.Add(row);
        //    _index++;
        //}

        //private void CreateColumns(Table table)
        //{
        //    table.Columns.Add(25);
        //    table.Columns.Add(180);
        //    table.Columns.Add(150);
        //    table.Columns.Add(60);
        //} 

        public static bool Export(DataTable table, string fileName, bool firstRowHeader)
        {
            return Export(table, fileName, firstRowHeader, null);
        }

        public static bool Export(DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            bool flag = true;
            PdfWriteProperties properties = new PdfWriteProperties();
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
            properties.DataSource = AdoTable.CreateSchema(table, fields,true);

            PDFprop prop = new PDFprop();
            prop.Title = properties.DataSource.TableName;
            prop.Author = "Nistec.Net";
            prop.Creator = "Nistec.Printing.Pdf";
            ViewBuilder builder = new ViewBuilder();
            builder.CreateReport(table);
            builder.Generate();
            builder.Export(prop, Nistec.Printing.View.ExportType.Pdf, properties.Filename, false);


            //Document _document = new Document();

            //_document.Title = properties.DataSource.TableName;// "Table Sample";
            //_document.Author = "Nistec.Net";
            //_document.Creator = "Nistec.Printing.Pdf";

            ////Compression is set to true by default.
            ////_document.Compress = false;

            //Page page = null;
            //PdfGraphics graphics = null;

            ////////////////////////////////

            //PdfFont font1 = new PdfFont(StandardFonts.Helvetica, FontStyle.Bold);
            //PdfFont font2 = new PdfFont(StandardFonts.Helvetica, FontStyle.Regular);

            //Border border = new Border(1f, RGBColor.Red, LineStyle.Solid);

            //TableStyle style1 = new TableStyle(font1, 8, RGBColor.Black);
            //TableStyle style2 = new TableStyle(font1, 8, RGBColor.Black, TextAlignment.Center, ContentAlignment.MiddleCenter, true, new CMYKColor(0, 0, 255, 0), border, false);
            //TableStyle style3 = new TableStyle(font2, 8, RGBColor.Black, TextAlignment.Center, ContentAlignment.MiddleCenter, true, RGBColor.Transparent, border, false);
            //TableStyle style4 = new TableStyle(font2, 8, RGBColor.Black, TextAlignment.Center, ContentAlignment.MiddleCenter, true, RGBColor.Lavender, border, false);

            //Table pdfTable = new Table(style1);
            //pdfTable.CellPadding = 4f;

            //float totalLength = AdoTable.GetTotalLength(properties.DataSource);

            ////CreateColumns(table);
            //foreach (AdoColumn c in properties.DataSource.Columns)
            //{
            //    //pdfTable.Columns.Add(AdoTable.CalcWidth((float)c.Length,totalLength,5f,600f));
            //    pdfTable.Columns.Add((float)c.Length);
            //}

            ////CreateRowHeadings(table, style2);
            //Row row = null;
            //row = new Row(pdfTable, style2);
            //row.Height = 25;
            //foreach (AdoColumn c in properties.DataSource.Columns)
            //{
            //    row.Cells.Add(c.ToString());
            //}
            //pdfTable.Rows.Add(row);
            //row = null;

            //for (int r = 0; r < table.Rows.Count; r++)
            //{
            //    row = new Row(pdfTable, style3);
            //    row.Height = 24;
            //    row.KeepTogether = true;

            //    foreach (AdoColumn f in properties.DataSource.Columns)// (int c = 0; c < sFields.Length; c++)
            //    {

            //        row.Cells.Add(table.Rows[r][f.ColumnName].ToString());

            //        //if (altrow == false)
            //        //{
            //        //    CreateRow(table, data, style3);
            //        //    altrow = true;
            //        //}
            //        //else
            //        //{
            //        //    CreateRow(table, data, style4);
            //        //    altrow = false;
            //        //}
            //    }
            //    pdfTable.Rows.Add(row);
            //}


            ////////////////////////////////////
            ////Table pdfTable = BuildTable();

            //while (pdfTable != null)
            //{
            //    //Initialize new page with PageSize A4
            //    page = new Page(PageSize.A4);

            //    //Add page to document
            //    _document.Pages.Add(page);

            //    //Get the PDFGraphics object for drawing to the page.
            //    graphics = page.Graphics;

            //    //DrawTable at x = 50; y =75 and height = 700
            //    //Overflow is return as Table
            //    //Loop until there is no overflow.
            //    pdfTable = graphics.DrawTable(50, 75, 700, pdfTable);
            //}

            //FileStream _fs = new FileStream(properties.Filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //try
            //{
            //    //Generate PDF to the stream
            //    _document.Generate(_fs);
            //    //MessageBox.Show("xmlToPdf_Sample.pdf generated successfully in Bin Folder");
            //}
            //finally
            //{
            //    _fs.Flush();
            //    _fs.Close();
            //}
            return flag;

        }


    }
}

