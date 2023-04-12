using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Xml;
using System.Threading;
using System.Xml.Xsl;
using System.Web.Mail;
using System.Net.Mail;
using Nistec.Win;



namespace Nistec.Printing.Data
{
    public static class AdoExport
    {

        public static bool Export(ExportType exType,DataTable dt,string fileName,bool firstRowHeader,AdoField[] fields)
        {
            switch (exType)
            {
                case ExportType.Excel:
                    return Nistec.Printing.ExcelXml.Workbook.Export(dt, fileName, firstRowHeader, fields);
                case ExportType.Csv:
                    return Nistec.Printing.Csv.CsvWriter.Export(dt, fileName, firstRowHeader, fields);
                case ExportType.Html:
                    return Nistec.Printing.Html.HtmlWriter.Export(dt, fileName, firstRowHeader, fields);
                case ExportType.Xml:
                    return Nistec.Printing.Xml.XmlWriter.Export(dt, fileName, firstRowHeader, fields);
                case ExportType.Pdf:
                    return Nistec.Printing.Pdf.PdfWriter.Export(dt, fileName, firstRowHeader, fields);
                default:
                    return Nistec.Printing.ExcelXml.Workbook.Export(dt, fileName, firstRowHeader, fields);
            }
        }

        public static bool Export(ExportType exType, DataTable dt, string fileName, bool firstRowHeader)
        {
            switch (exType)
            {
                case ExportType.Excel:
                    return Nistec.Printing.ExcelXml.Workbook.Export(dt, fileName, firstRowHeader,null);
                case ExportType.Csv:
                    return Nistec.Printing.Csv.CsvWriter.Export(dt, fileName, firstRowHeader);
                case ExportType.Html:
                    return Nistec.Printing.Html.HtmlWriter.Export(dt, fileName, firstRowHeader);
                case ExportType.Xml:
                    return Nistec.Printing.Xml.XmlWriter.Export(dt, fileName, firstRowHeader);
                case ExportType.Pdf:
                    return Nistec.Printing.Pdf.PdfWriter.Export(dt, fileName, firstRowHeader);
                default:
                    return Nistec.Printing.ExcelXml.Workbook.Export(dt, fileName, firstRowHeader,null);
            }
        }

        public static string GetFileExportName()
        {
            string filter = "XLS files (*.xls)|*.xls|CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml|HTML files (*.htm)|*.htm|Pdf files (*.pdf)|*.pdf";
            return CommonDlg.SaveAs(filter);
        }
        public static string GetExportFileName(string filter)
        {
            string fileName = "";
            System.Windows.Forms.SaveFileDialog objSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            objSaveFileDialog.Filter = "File " + filter + " (*." + filter + ")|*." + filter;
            objSaveFileDialog.Title = "Export Data";
            if (objSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = objSaveFileDialog.FileName;
            }
            objSaveFileDialog.Dispose();
            objSaveFileDialog = null;
            GC.Collect();
            return fileName;
        }

        public static void Export(DataTable dt, bool firstRowHeader, AdoField[] fields)
        {

            string fileName = GetFileExportName();
            if (fileName != "")
            {

                if (fileName.ToLower().EndsWith("csv"))
                    Export( ExportType.Csv, dt, fileName, firstRowHeader, fields);
                else if (fileName.ToLower().EndsWith("xls"))
                    Export(ExportType.Excel, dt, fileName, firstRowHeader, fields);
                else if (fileName.ToLower().EndsWith("xml"))
                    Export(ExportType.Xml, dt, fileName, firstRowHeader, fields);
                else if (fileName.ToLower().EndsWith("htm"))
                    Export(ExportType.Html, dt, fileName, firstRowHeader, fields);
                else if (fileName.ToLower().EndsWith("pdf"))
                    Export(ExportType.Pdf, dt, fileName, firstRowHeader, fields);

            }
        }

        public static void Export(DataTable dt, bool firstRowHeader)
        {
            string fileName = GetFileExportName();
            if (fileName != "")
            {

                if (fileName.ToLower().EndsWith("csv"))
                    Export(ExportType.Csv, dt, fileName, firstRowHeader);
                else if (fileName.ToLower().EndsWith("xls"))
                    Export(ExportType.Excel, dt, fileName, firstRowHeader);
                else if (fileName.ToLower().EndsWith("xml"))
                    Export(ExportType.Xml, dt, fileName, firstRowHeader);
                else if (fileName.ToLower().EndsWith("htm"))
                    Export(ExportType.Html, dt, fileName, firstRowHeader);
                else if (fileName.ToLower().EndsWith("pdf"))
                    Export(ExportType.Pdf, dt, fileName, firstRowHeader);

            }
        }


        public static void WebExport(ExportType exType, DataTable table, string fileName, bool firstRowHeader)
        {
            WebExport(exType, table, fileName, firstRowHeader, AdoField.CreateFields(table));
        }
        public static void WebExport(ExportType exType, DataTable table, string fileName, bool firstRowHeader, string[] fields)
        {
            WebExport(exType, table, fileName, firstRowHeader, AdoField.CreateFields(fields));
        }
        public static void WebExport(ExportType exType, DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            try
            {
                if (table.Rows.Count == 0)
                    throw new Exception("There are no details to export.");

                SetXSLT_Web(exType, table, fileName, firstRowHeader, fields);
 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        #region private CreateStylesheet


        // Function  : WriteStylesheet 
        // Arguments : writer, sHeaders, sFields, FormatType
        // Purpose   : Creates XSLT file to apply on dataset's XML file 

        private static void CreateStylesheet(XmlTextWriter writer, AdoField[] fields, ExportType exType)
        {
            try
            {
                // xsl:stylesheet
                string ns = "http://www.w3.org/1999/XSL/Transform";
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("xsl", "stylesheet", ns);
                writer.WriteAttributeString("version", "1.0");
                writer.WriteStartElement("xsl:output");
                writer.WriteAttributeString("method", "text");
                writer.WriteAttributeString("version", "4.0");
                writer.WriteEndElement();

                // xsl-template
                writer.WriteStartElement("xsl:template");
                writer.WriteAttributeString("match", "/");

                // xsl:value-of for headers
                for (int i = 0; i < fields.Length; i++)
                {
                    writer.WriteString("\"");
                    writer.WriteStartElement("xsl:value-of");
                    writer.WriteAttributeString("select", "'" + fields[i].ToString() + "'");
                    writer.WriteEndElement(); // xsl:value-of
                    writer.WriteString("\"");
                    if (i != fields.Length - 1) writer.WriteString((exType== ExportType.Csv) ? "," : "	");
                }

                // xsl:for-each
                writer.WriteStartElement("xsl:for-each");
                writer.WriteAttributeString("select", "Export/Values");
                writer.WriteString("\r\n");

                // xsl:value-of for data fields
                for (int i = 0; i < fields.Length; i++)
                {
                    writer.WriteString("\"");
                    writer.WriteStartElement("xsl:value-of");
                    writer.WriteAttributeString("select", fields[i].ColumnName);
                    writer.WriteEndElement(); // xsl:value-of
                    writer.WriteString("\"");
                    if (i != fields.Length - 1) writer.WriteString((exType == ExportType.Csv) ? "," : "	");
                }

                writer.WriteEndElement(); // xsl:for-each
                writer.WriteEndElement(); // xsl-template
                writer.WriteEndElement(); // xsl:stylesheet
                writer.WriteEndDocument();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion // WriteStylesheet

        #region private SetXSLT_Web

        static System.Web.HttpResponse response;


        // Function  : SetXSLT_Web 
        // Arguments : dsExport, sHeaders, sFields, FormatType, FileName
        // Purpose   : Exports dataset into CSV / Excel format

        private static void SetXSLT_Web(ExportType exType, DataTable table, string fileName, bool firstRowHeader, AdoField[] fields)
        {
            try
            {
                response = System.Web.HttpContext.Current.Response;

                // Create Dataset
                DataSet dsExport = new DataSet("Export");
                if (table.TableName == "")
                {
                    table.TableName = "ExportValues";
                }
                dsExport.Tables.Add(table.Copy());
                
                if (fields == null)
                {
                    fields = AdoField.CreateFields(table);
                }

                //string[] sFields = fields;
                //if (fields == null)
                //{
                //    sFields = new string[dtExport.Columns.Count];
                //    for (int i = 0; i < dtExport.Columns.Count; i++)
                //    {
                //        sFields[i] = dtExport.Columns[i].ColumnName;
                //    }
                //}
                // Appending Headers
                response.Clear();
                response.Buffer = true;

                switch (exType)
                {
                    case ExportType.Csv:
                        response.ContentType = "text/csv";
                        response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                        break;
                    case ExportType.Excel:
                        response.ContentType = "application/vnd.ms-excel";
                        response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                        break;
                    case ExportType.Html:
                        response.ContentType = "text/htm";
                        response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                        break;
                    case ExportType.Xml:
                        response.ContentType = "text/xml";
                        response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                        break;
                }


                // XSLT to use for transforming this dataset.						
                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default);//.UTF8);

                CreateStylesheet(writer, fields, exType);
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                XmlDataDocument xmlDoc = new XmlDataDocument(dsExport);
                //XslTransform xslTran = new XslTransform();				
                XslCompiledTransform xslTran = new XslCompiledTransform();
                //xslTran.Load(new XmlTextReader(stream), null, null);
                xslTran.Load(new XmlTextReader(stream));
                System.IO.StringWriter sw = new System.IO.StringWriter();
                //xslTran.Transform(xmlDoc, null, sw, null);
                xslTran.Transform(xmlDoc, null, sw);

                //Writeout the Content				
                response.Write(sw.ToString());
                sw.Close();
                writer.Close();
                stream.Close();
                //response.End();
            }
            catch (ThreadAbortException Ex)
            {
                string ErrMsg = Ex.Message;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion // Export_with_XSLT

        public static void SendMail(string FromAddress, string ToAddress, string Subject, string body, DataTable dt, ExportType  exType)
        {
            System.Net.Mail.MailMessage mail = null;
            Attachment ma = null;
            try
            {
                string tempName = Guid.NewGuid().ToString();
                string fileName = System.IO.Path.GetTempPath() + tempName + ".htm";// "default.htm";

                if (dt != null)
                {
                    switch (exType)
                    {
                        case ExportType.Csv:
                            {
                                fileName = System.IO.Path.GetTempPath() + tempName + ".csv";
                                Export(exType, dt, fileName,false);
                                break;
                            }
                        case ExportType.Excel:
                            {
                                fileName = System.IO.Path.GetTempPath() + tempName + ".xls";
                                Export(exType, dt, fileName, false);
                                break;
                            }
                        case ExportType.Html:
                            fileName = System.IO.Path.GetTempPath() + tempName + ".htm";
                            Export(exType, dt, fileName, false);
                            break;
                        case ExportType.Xml:
                            fileName = System.IO.Path.GetTempPath() + tempName + ".xml";
                            Export(exType, dt, fileName, false);
                            break;
                    }

                    mail = new System.Net.Mail.MailMessage(FromAddress, ToAddress);
                    mail.Subject = Subject;
                    mail.Body = body;

                    ma = new Attachment(fileName);
                    mail.Attachments.Add(ma);
                }

                //Send the message.
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                // Add credentials if the SMTP server requires them.
                //client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                client.Send(mail);
                if (ma != null)
                {
                    // Display the values in the ContentDisposition for the attachment.
                    System.Net.Mime.ContentDisposition cd = ma.ContentDisposition;
                    ma.Dispose();
                    System.IO.File.Delete(fileName);
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

    }
}
