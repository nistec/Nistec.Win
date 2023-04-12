namespace Nistec.Printing.MSExcel
{
    using Nistec.Printing;
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;


using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;

    using System.Xml;
    using System.Xml.Xsl;
    using Nistec.Win;

    public class ExcelTool 
    {
        private static string _path = string.Empty;

        static ExcelTool()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"Excel.Application\CLSID");
            if (key != null)
            {
                string str = key.GetValue(string.Empty).ToString();
                key.Close();
                key = Registry.ClassesRoot.OpenSubKey(@"CLSID\" + str + @"\LocalServer32");
                if (key != null)
                {
                    string path = key.GetValue(string.Empty).ToString();
                    if (path.IndexOf('/') > -1)
                    {
                        path = path.Substring(0, path.IndexOf('/'));
                    }
                    path = path.Replace("\"", "").Trim();
                    if (File.Exists(path))
                    {
                        _path = path;
                    }
                    key.Close();
                }
            }
        }

        public ExcelTool()
        {
            if (_path == string.Empty)
            {
                throw new InvalidOperationException("MS Excel is not installed.");
            }
        }

        public  void Execute()
        {
            try
            {
                Process.Start(_path);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to launch MS Excel: " + exception.Message);
            }
        }

        public static ExcelReaderType ConvertToExcelReaderType(string type)
        {
            return (ExcelReaderType)Enum.Parse(typeof(ExcelReaderType), type, true);
        }

        public static DataSet ReadExcelWorkbook(ExcelReaderType type, string fileName,bool  firstRowHeader, uint maxRows)
        {
            DataSet ds = null;
            ExcelReadProperties properties = new ExcelReadProperties();
            properties.Workbook = fileName;

            try
            {
                //FileStream fs = new FileStream(fileName,
                //FileMode.Open, FileAccess.Read);
                
                //ExcelDataReader rd = new ExcelDataReader(fs, firstRowHeader, maxRows);
                
                //fs.Close();


                switch (type)
                {
                    case ExcelReaderType.Excel2003:
                        MSExcel.Bin2003.ExcelDataReader rd = new MSExcel.Bin2003.ExcelDataReader(fileName);
                        ds= rd.Read(maxRows);
                        break;
                    case ExcelReaderType.Excel2007:
                        break;
                    case ExcelReaderType.ExcelOleDb:
                        ds=MSExcel.OleDb.ExcelHelper.Read(fileName, firstRowHeader, maxRows,0);
                        //MSExcel.OleDb.ExcelReader reader = new Nistec.Printing.MSExcel.OleDb.ExcelReader(properties);
                        
                        break;
                    case ExcelReaderType.ExcelXml:
                        ds = ExcelXml.Workbook.Import(fileName, firstRowHeader, maxRows);
                        break;
                    default:

                        MSExcel.Bin2003.ExcelDataReader erd = new MSExcel.Bin2003.ExcelDataReader(fileName);
                        return erd.Read(maxRows);

                        //break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
            return ds;
        }

        public static DataSet ReadExcelWorkbook(ExcelReaderType type, string fileName)
        {
            return ReadExcelWorkbook(type,fileName,false,0);
        }

        public static DataTable ReadExcelWorksheet(string fileName, uint maxRows, bool firstRowHeader, string worksheet)
        {
            try
            {
                MSExcel.Bin2003.ExcelDataReader rd = new MSExcel.Bin2003.ExcelDataReader(fileName);
                return rd.Read(worksheet, firstRowHeader, maxRows);

                //DataSet ds = ReadExcelWorkbook(fileName, maxRows, firstRowHeader);

                //return ds.Tables[worksheet];
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
            return null;
        }
        public static DataTable ReadExcelWorksheet(string fileName, bool firstRowHeader, string worksheet)
        {
            MSExcel.Bin2003.ExcelDataReader rd = new MSExcel.Bin2003.ExcelDataReader(fileName);
            return rd.Read(worksheet, firstRowHeader, 0);
            //return ReadExcelWorksheet(fileName, worksheet, firstRowHeader,0);
        }

        public static string[] ReadExcelNamedRange(string fileName)
        {
            MSExcel.Bin2003.ExcelDataReader rd = new MSExcel.Bin2003.ExcelDataReader(fileName);
            return rd.ReadNamedRange();
        }

        #region GenerateWorkSheets

        public static void GenerateWorkSheets(DataTable dt, string fileName)
        {
            //Nistec.Printing.MSExcel.Data.ExportUtil.GenerateWorkSheets(dt, fileName);
            //ExcelTool eu = new ExcelTool();
            //if (dt.TableName == "")
            //    dt.TableName = "WorkSheet";
            //eu.GenerateWorkSheet(dt, fileName, dt.TableName);
        }

   
        private void GenerateWorkSheet(DataTable dt,string fileName, string sheet)
        {
            const int maxRows = 65000;

            int k, iWorkSheet=1,iCol = 1, iRow = 1,maxStep=0, offset=0;
            System.Text.StringBuilder strExcelXml = new System.Text.StringBuilder();
            iCol = dt.Columns.Count; ;
            iRow = dt.Rows.Count;
            maxStep = iRow;
            if (iRow >= maxRows)
            {
                iWorkSheet = iRow / maxRows;
                iWorkSheet++;
                maxStep = maxRows;
                //offset=iRow-maxRows;
            }

            System.Diagnostics.Debug.WriteLine(" Start " + System.DateTime.Now.ToString());
            //First Write the Excel Header
            strExcelXml.Append(ExcelHeader());
            // Get all the Styles
            strExcelXml.Append(ExcelStyles());//"styles.config"));

            // Worksheet options Required only one time 
            strExcelXml.Append(ExcelWorkSheetOptions());

            string sheetName = sheet;
            object cell = null;
            string type = "String";

            for (int i = 0; i < iWorkSheet; i++)
            {
                if (i > 0)
                {
                    sheetName = sheet+i.ToString();
                }

                // Create First Worksheet tag
                strExcelXml.Append("<Worksheet ss:Name=\"" + sheetName + "\">");
                // Then Table Tag
                strExcelXml.Append("<Table>");


                strExcelXml.Append("<Row ss:AutoFitHeight=\"1\" >\n");// ("<tr>");
                for (int j = 0; j < iCol; j++)
                {
                    //Headers
                    strExcelXml.Append("<Cell><Data ss:Type=\"String\">");// ("<td>");
                    strExcelXml.Append(dt.Columns[j].ColumnName);
                    strExcelXml.Append("</Data></Cell>\n");// ("</td>");
                }
                strExcelXml.Append("</Row>\n");// ("</tr>");
               
                if (iWorkSheet > 1 && i== iWorkSheet-1)
                {
                    maxStep = iRow - ((iWorkSheet - 1) * maxRows);
                }
                offset = i * maxRows;


                for (k = 0; k < maxStep; k++)
                {
                    // Row Tag
                    strExcelXml.Append("<Row ss:AutoFitHeight=\"1\" >\n");// ("<tr>");
                    for (int j = 0; j < iCol; j++)
                    {
                        cell = dt.Rows[k + offset][j];
                        if (cell == null || cell == DBNull.Value)
                            cell = string.Empty;
                        string str = cell.GetType().Name;
                        type = "String";
                        
                        //if (Info.IsNumber(cell))
                        //{
                        //    type = "Number";
                        //}
                        if (!(str == "String"))
                        {
                            if (str.Contains("Int"))
                            {
                                type = "Number";
                            }
                            else if (str == "Decimal" || str == "Double" || str == "Float" || str == "Byte" || str == "Long" || str == "Short")
                            {
                                type = "Number";
                            }
                            else if (str == "Boolean")
                            {
                                cell = ((bool)cell) ? 1 : 0;
                                type = "Number";
                            }
                            else if (str == "Byte[]")
                            {
                                cell = Encoding.ASCII.GetString((byte[])cell);
                            }
                        }

                        // Cell Tags
                        strExcelXml.Append("<Cell><Data ss:Type=\"" + type + "\">");// ("<td>");
                        strExcelXml.Append(cell);
                        strExcelXml.Append("</Data></Cell>\n");// ("</td>");
                    }
                    strExcelXml.Append("</Row>\n");// ("</tr>");

                }

                strExcelXml.Append("</Table>");


                strExcelXml.Append("</Worksheet>");
            }
            // Close the Workbook tag (in Excel header you can see the Workbook tag)
            strExcelXml.Append("</Workbook>\n");

            #region "Write Into File"

            try
            {
                //string strFileName = string.Empty;
                //strFileName = "TestExcel.xls";

                //System.IO.File.Delete(strFileName);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false, System.Text.Encoding.Unicode);
                sw.Write(strExcelXml.ToString());// (ConvertHTMLToExcelXML(strExcelXml.ToString()));
                sw.Close();


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            //System.Windows.Forms.MessageBox.Show("Data Exported Successfully.");
            #endregion
        }

        /// <summary>
        /// Creates Excel Header 		
        /// </summary>
        /// <returns>Excel Header Strings</returns>
        private string ExcelHeader()
        {
            // Excel header
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\"?>\n");
            sb.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
            sb.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            sb.Append("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            sb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
            sb.Append("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            sb.Append("xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
            sb.Append("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sb.Append("<Author>Nistec</Author>");
            sb.Append("</DocumentProperties>");
            sb.Append("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
            sb.Append("<ProtectStructure>False</ProtectStructure>\n");
            sb.Append("<ProtectWindows>False</ProtectWindows>\n");
            sb.Append("</ExcelWorkbook>\n");

            return sb.ToString();
        }
        /// <summary>
        /// Read styles and copy it to the Excel string
        /// </summary>
        /// <param name="filename">Styles.config</param>
        /// <returns></returns>
        private string ExcelStyles()//string filename)
        {
            string strFileText = @"<Styles>
  <Style ss:ID='Default' ss:Name='Normal'>
   <Alignment ss:Vertical='Bottom'/>
   <Borders/>
   <Font/>
   <Interior/>
   <NumberFormat/>
   <Protection/>
  </Style>
  <Style ss:ID='s27' ss:Name='Hyperlink'>
   <Font ss:Color='#0000FF' ss:Underline='Single'/>
  </Style>
  <Style ss:ID='s24'>
   <Font x:Family='Swiss' ss:Bold='1'/>
  </Style>
  <Style ss:ID='s25'>
   <Font x:Family='Swiss' ss:Italic='1'/>
  </Style>
  <Style ss:ID='s26'>
   <Alignment ss:Horizontal='Center' ss:Vertical='Bottom'/>
  </Style>
 </Styles>";
            return strFileText;
        }

 
        private string ExcelWorkSheetOptions()
        {
            // This is Required Only Once ,	But this has to go after the First Worksheet's First Table		
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("\n<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n<Selected/>\n </WorksheetOptions>\n");
            return sb.ToString();
        }

        // Final Filtaration of String Code generated by above code
        private static string ConvertHTMLToExcelXML(string strHtml)
        {

            // Just to replace TR with Row
            strHtml = strHtml.Replace("<tr>", "<Row ss:AutoFitHeight=\"1\" >\n");
            strHtml = strHtml.Replace("</tr>", "</Row>\n");

            //replace the cell tags
            strHtml = strHtml.Replace("<td>", "<Cell><Data ss:Type=\"String\">");
            strHtml = strHtml.Replace("</td>", "</Data></Cell>\n");

            return strHtml;
        }
        #endregion
    }
}

