using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Data;
using System.Globalization;

namespace Nistec.Printing.MSExcel.Bin2003
{
    //FileStream fs = new FileStream(@"c:\file.xls", 
    //FileMode.Open, FileAccess.Read);
    //ExcelDataReader rd = new ExcelDataReader(fs);
    //fs.Close();

    //DataSet data = rd.WorkbookData;

    /// <summary>
    /// Main class implementation
    /// </summary>
    public class ExcelDataReader
    {
        private Stream m_file = null;
        private ExcelHeader m_hdr = null;
        private ExcelStream m_stream = null;
        private ExcelWorkbook m_globals = null;
        private List<ExcelWorksheet> m_sheets = new List<ExcelWorksheet>();
        private DataSet m_workbookData = null;
        private ushort m_version = 0x0600;
        private Encoding m_encoding = Encoding.Default;
        //private bool m_firstRowHeader;
        private uint m_MaxRows;
        private string m_fileName;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExcelDataReader(string file)
        {
            m_fileName = file;
        }

        ///// <summary>
        ///// Default constructor
        ///// </summary>
        ///// <param name="file">Stream with source data</param>
        //public ExcelDataReader(Stream file)
        //{
        //    m_file = file; 
        //}
 
        ///// <summary>
        ///// constructor
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="firstRowHeader"></param>
        ///// <param name="maxRows"></param>
        //public ExcelDataReader(Stream file, bool firstRowHeader, int maxRows)
        //{
        //    m_file = file; 
        //    Read(firstRowHeader, maxRows);
         
        //}

        ///// <summary>
        ///// constructor
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="firstRowHeader"></param>
        ///// <param name="maxRows"></param>
        // public ExcelDataReader(Stream file, bool firstRowHeader, int maxRows)
        //{
        //    m_MaxRows=maxRows;
        //    m_firstRowHeader = firstRowHeader;
        //    m_file = file; // new BufferedStream(file);
        //    m_hdr = ExcelHeader.ReadHeader(m_file);
        //    XlsRootDirectory dir = new XlsRootDirectory(m_hdr);
        //    XlsDirectoryEntry workbookEntry = dir.FindEntry("Workbook");
        //    if (workbookEntry == null)
        //        workbookEntry = dir.FindEntry("Book");
        //    if (workbookEntry == null)
        //        throw new FileNotFoundException(" Neither stream 'Workbook' nor 'Book' was found in file");
        //    if (workbookEntry.EntryType != STGTY.STGTY_STREAM)
        //        throw new FormatException(" Workbook directory entry is not a Stream");
        //    m_stream = new ExcelStream(m_hdr, workbookEntry.StreamFirstSector);
        //    ReadWorkbookGlobals();
        //    GC.Collect();
        //    m_workbookData = new DataSet();
        //    for (int i = 0; i < m_sheets.Count; i++)
        //        if (ReadWorksheet(m_sheets[i]))
        //            m_workbookData.Tables.Add(m_sheets[i].Data);
        //    m_globals.SST = null;
        //    m_globals = null;
        //    m_sheets = null;
        //    m_stream = null;
        //    m_hdr = null;
        //    GC.Collect();
        //}

         /// <summary>
        /// Read workbook
        /// </summary>
        /// <param name="maxRows"></param>
        /// <returns></returns>
         public DataSet Read(uint maxRows)
        {
            m_MaxRows = maxRows;
            //m_firstRowHeader = firstRowHeader;
            FileStream fs = new FileStream(m_fileName,FileMode.Open, FileAccess.Read);
            try
            {
                m_file = fs;
                ReadWorkbookInternal();
                ReadWorksheetInternal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
      
            return WorkbookData;
        }
        /// <summary>
        /// Read workbook
        /// </summary>
        /// <returns></returns>
        public DataSet Read()
        {
          return  Read(0);
        }
        /// <summary>
        /// Read workshhet by name
        /// </summary>
        /// <param name="maxRows"></param>
        /// <param name="firstRowHeader"></param>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public DataTable Read(string worksheet, bool firstRowHeader, uint maxRows)
        {
            m_MaxRows = maxRows;
            //m_firstRowHeader = firstRowHeader;
            FileStream fs = new FileStream(m_fileName, FileMode.Open, FileAccess.Read);
            try
            {
                m_file = fs;

                ReadWorkbookInternal();
                ReadWorksheetInternal(worksheet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
            return GetWorksheetData(firstRowHeader, worksheet);
        }
        /// <summary>
        /// Read workshhet by index
        /// </summary>
        /// <param name="maxRows"></param>
        /// <param name="firstRowHeader"></param>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public DataTable Read(int worksheet, bool firstRowHeader, uint maxRows)
        {
            m_MaxRows = maxRows;
            //m_firstRowHeader = firstRowHeader;
            FileStream fs = new FileStream(m_fileName, FileMode.Open, FileAccess.Read);
            try
            {
                m_file = fs;
                ReadWorkbookInternal();
                ReadWorksheetInternal(worksheet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
            return GetWorksheetData(firstRowHeader, worksheet);
        }

        /// <summary>
        /// Read Workbook named range only
        /// </summary>
        /// <returns></returns>
        public string[] ReadNamedRange()
        {
            FileStream fs = new FileStream(m_fileName, FileMode.Open, FileAccess.Read);
            try
            {
                m_file = fs;
                ReadWorkbookInternal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }

            string[] sheets = null;
            sheets = new string[m_sheets.Count];
            for (int i = 0; i < m_sheets.Count; i++)
                sheets[i] = m_sheets[i].Name;

            DisposReader();

            return sheets;
        }
        /// <summary>
        /// Get Workbook named range
        /// </summary>
        /// <returns></returns>
        public string[] GetWorkbookNamedRange()
        {
            if (this.WorkbookData == null || this.WorkbookData.Tables.Count == 0)
                return null;
            string[] named = new string[WorkbookData.Tables.Count];
            for (int i = 0; i < WorkbookData.Tables.Count; i++)
                named[i] = WorkbookData.Tables[i].TableName;
            return named;
        }
        /// <summary>
        /// GetWorksheetData
        /// </summary>
        /// <param name="firstRowHeader"></param>
        /// <param name="Worksheet"></param>
        /// <returns></returns>
        public DataTable GetWorksheetData(bool firstRowHeader, string Worksheet)
        {
            if (this.WorkbookData == null || this.WorkbookData.Tables.Count == 0 )
            {
                throw new ArgumentOutOfRangeException();
            }
            int index = WorkbookData.Tables.IndexOf(Worksheet);
            if (index == -1)
            {
                throw new ArgumentException();
            }
            return GetWorksheetDataInternal(firstRowHeader, index);
        }
        /// <summary>
        /// GetWorksheetData
        /// </summary>
        /// <param name="firstRowHeader"></param>
        /// <param name="Worksheet"></param>
        /// <returns></returns>
        public DataTable GetWorksheetData(bool firstRowHeader, int Worksheet)
        {
            if (this.WorkbookData == null || this.WorkbookData.Tables.Count == 0 || this.WorkbookData.Tables.Count <= Worksheet)
            {
                throw new ArgumentOutOfRangeException();
            }
            return GetWorksheetDataInternal(firstRowHeader, Worksheet);
        }

        private DataTable GetWorksheetDataInternal(bool firstRowHeader, int Worksheet)
        {
            DataTable dt = this.WorkbookData.Tables[Worksheet].Copy();
            if (firstRowHeader && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colName = dt.Rows[0][i].ToString();
                    if (!string.IsNullOrEmpty(colName))
                        dt.Columns[i].ColumnName = colName;
                }
                dt.Rows[0].Delete();
                dt.AcceptChanges();
            }
            return dt;
        }

        private void ReadWorkbookInternal()
        {
            m_hdr = ExcelHeader.ReadHeader(m_file);
            XlsRootDirectory dir = new XlsRootDirectory(m_hdr);
            XlsDirectoryEntry workbookEntry = dir.FindEntry("Workbook");
            if (workbookEntry == null)
                workbookEntry = dir.FindEntry("Book");
            if (workbookEntry == null)
                throw new FileNotFoundException(" Neither stream 'Workbook' nor 'Book' was found in file");
            if (workbookEntry.EntryType != STGTY.STGTY_STREAM)
                throw new FormatException(" Workbook directory entry is not a Stream");
            m_stream = new ExcelStream(m_hdr, workbookEntry.StreamFirstSector);
            ReadWorkbookGlobals();
            GC.Collect();
        }

        private void ReadWorksheetInternal()
        {

            m_workbookData = new DataSet();
            for (int i = 0; i < m_sheets.Count; i++)
                if (ReadWorksheet(m_sheets[i]))
                {
                    if (m_sheets[i].Data != null)
                    {
                        m_workbookData.Tables.Add(m_sheets[i].Data);
                    }
                }

            DisposReader();
        }


        private void ReadWorksheetInternal(string name)
        {

            m_workbookData = new DataSet();
            for (int i = 0; i < m_sheets.Count; i++)
            {
                if (m_sheets[i].Name.Equals(name))
                {
                    if (ReadWorksheet(m_sheets[i]))
                    {
                        m_workbookData.Tables.Add(m_sheets[i].Data);
                        return;
                    }
                }
            }
            DisposReader();
        }

        private void ReadWorksheetInternal(int index)
        {

            m_workbookData = new DataSet();
            if (ReadWorksheet(m_sheets[index]))
            {
                m_workbookData.Tables.Add(m_sheets[index].Data);
            }
            DisposReader();
        }

        private void DisposReader()
        {
           // GC.Collect();

            m_globals.SST = null;
            m_globals = null;
            //m_stream = null;
            m_hdr = null;
            GC.Collect();
            
            m_sheets = null;
            m_stream = null;
            GC.Collect();
        }

        /// <summary>
        /// DataSet with workbook data, Tables represent Sheets
        /// </summary>
        public DataSet WorkbookData
        {
            get { return m_workbookData; }
        }

        /// <summary>
        /// Private method, reads Workbook Globals section
        /// </summary>
        private void ReadWorkbookGlobals()
        {
            m_globals = new ExcelWorkbook();
            m_stream.Seek(0, SeekOrigin.Begin);
            ExcelRecord rec = m_stream.Read();
            ExcelBOF bof = rec as ExcelBOF;
            if (bof == null || bof.Type != BIFFTYPE.WorkbookGlobals)
                throw new InvalidDataException(" Stream has invalid data");
            m_version = bof.Version;
            m_encoding = Encoding.Unicode;
            bool isV8 = (m_version >= 0x600);
            bool sst = false;
            while ((rec = m_stream.Read()) != null)
            {
                switch (rec.ID)
                { 
                    case BIFFRECORDTYPE.INTERFACEHDR:
                        m_globals.InterfaceHdr = (ExcelInterfaceHdr)rec;
                        break;
                    case BIFFRECORDTYPE.BOUNDSHEET:
                        ExcelBoundSheet sheet = (ExcelBoundSheet)rec;
                        if (sheet.Type != ExcelBoundSheet.SheetType.Worksheet) break;
                        sheet.IsV8 = isV8;
                        sheet.UseEncoding = m_encoding;
                        m_sheets.Add(new ExcelWorksheet(m_globals.Sheets.Count, sheet));
                        m_globals.Sheets.Add(sheet);
                        break;
                    case BIFFRECORDTYPE.MMS:
                        m_globals.MMS = rec;
                        break;
                    case BIFFRECORDTYPE.COUNTRY:
                        m_globals.Country = rec;
                        break;
                    case BIFFRECORDTYPE.CODEPAGE:
                        m_globals.CodePage = (ExcelSimpleValueRecord)rec;
                        m_encoding = Encoding.GetEncoding(m_globals.CodePage.Value);
                        break;
                    case BIFFRECORDTYPE.FONT:
                    case BIFFRECORDTYPE.FONT_V34:
                        m_globals.Fonts.Add(rec);
                        break;
                    case BIFFRECORDTYPE.FORMAT:
                    case BIFFRECORDTYPE.FORMAT_V23:
                        m_globals.Formats.Add(rec);
                        break;
                    case BIFFRECORDTYPE.XF:
                    case BIFFRECORDTYPE.XF_V4:
                    case BIFFRECORDTYPE.XF_V3:
                    case BIFFRECORDTYPE.XF_V2:
                        m_globals.ExtendedFormats.Add(rec);
                        break;
                    case BIFFRECORDTYPE.SST:
                        m_globals.SST = (ExcelSST)rec;
                        sst = true;
                        break;
                    case BIFFRECORDTYPE.CONTINUE:
                        if (!sst) break;
                        ExcelContinue contSST = (ExcelContinue)rec;
                        m_globals.SST.Append(contSST);
                        break;
                    case BIFFRECORDTYPE.EXTSST:
                        m_globals.ExtSST = rec;
                        sst = false;
                        break;
                    case BIFFRECORDTYPE.EOF:
                        if (m_globals.SST != null)
                            m_globals.SST.ReadStrings();
                        return;
                    default:
                        continue;
                }
            }
        }

        ///// <summary>
        ///// private method, reads sheet data
        ///// </summary>
        ///// <param name="sheet">Sheet object, whose data to read</param>
        ///// <returns>True if sheet was read successfully, otherwise False</returns>
        //private bool ReadWorksheet_0(ExcelWorksheet sheet)
        //{

        //    m_stream.Seek((int)sheet.DataOffset, SeekOrigin.Begin);
        //    ExcelBOF bof = m_stream.Read() as ExcelBOF;
        //    if (bof == null || bof.Type != BIFFTYPE.Worksheet)
        //        return false;
        //    ExcelIndex idx = m_stream.Read() as ExcelIndex;
        //    bool isV8 = (m_version >= 0x600);
        //    if (idx != null)
        //    {
        //        idx.IsV8 = isV8;
        //        DataTable dt = new DataTable(sheet.Name);

        //        ExcelRecord trec;
        //        ExcelDimensions dims = null;
        //        do
        //        {
        //            trec = m_stream.Read();
        //            if (trec.ID == BIFFRECORDTYPE.DIMENSIONS)
        //            {
        //                dims = (ExcelDimensions)trec;
        //                break;
        //            }
        //        }
        //        while (trec.ID != BIFFRECORDTYPE.ROW);
        //        int maxCol = 256;
        //        if (dims != null)
        //        {
        //            dims.IsV8 = isV8;
        //            maxCol = dims.LastColumn;
        //            sheet.Dimensions = dims;
        //        }

        //        for (int i = 0; i < maxCol; i++)
        //            dt.Columns.Add("F" + (i + 1).ToString(), typeof(string));
        //        sheet.Data = dt;
        //        uint maxRow = idx.LastExistingRow;
        //        if (idx.LastExistingRow <= idx.FirstExistingRow)
        //            return true;
        //        if (m_MaxRows > 0 && m_MaxRows <= maxRow)
        //            maxRow = (uint)m_MaxRows;
        //        dt.BeginLoadData();
        //        for (int i = 0; i <= maxRow; i++)
        //            dt.Rows.Add(dt.NewRow());
        //        uint[] dbCellAddrs = idx.DbCellAddresses;
        //        for (int i = 0; i < dbCellAddrs.Length; i++)
        //        {
        //            ExcelDbCell dbCell = (ExcelDbCell)m_stream.ReadAt((int)dbCellAddrs[i]);
        //            ExcelRow row = null;
        //            int offs = (int)dbCell.RowAddress;
        //            do
        //            {
        //                row = m_stream.ReadAt(offs) as ExcelRow;
        //                if (row == null) break;
        //                offs += row.Size;
        //            }
        //            while (row != null);
        //            while (true)
        //            {
        //                ExcelRecord rec = m_stream.ReadAt(offs);
        //                offs += rec.Size;
        //                if (rec is ExcelDbCell) break;
        //                if (rec is ExcelEOF) break;
        //                ExcelBlankCell cell = rec as ExcelBlankCell;
        //                if (cell == null)
        //                {
        //                    continue;
        //                }
        //                if (cell.ColumnIndex >= maxCol) continue;
        //                if (cell.RowIndex > maxRow) continue;
        //                switch (cell.ID)
        //                {
        //                    case BIFFRECORDTYPE.INTEGER:
        //                    case BIFFRECORDTYPE.INTEGER_OLD:
        //                        dt.Rows[cell.RowIndex][cell.ColumnIndex] = ((ExcelIntegerCell)cell).Value.ToString();
        //                        break;
        //                    case BIFFRECORDTYPE.NUMBER:
        //                    case BIFFRECORDTYPE.NUMBER_OLD:
        //                        dt.Rows[cell.RowIndex][cell.ColumnIndex] = FormatNumber(((ExcelNumberCell)cell).Value);
        //                        break;
        //                    case BIFFRECORDTYPE.LABEL:
        //                    case BIFFRECORDTYPE.LABEL_OLD:
        //                    case BIFFRECORDTYPE.RSTRING:
        //                        dt.Rows[cell.RowIndex][cell.ColumnIndex] = ((ExcelLabelCell)cell).Value;
        //                        break;
        //                    case BIFFRECORDTYPE.LABELSST:
        //                        {
        //                            string tmp = m_globals.SST.GetString(((ExcelLabelSSTCell)cell).SSTIndex);
        //                            dt.Rows[cell.RowIndex][cell.ColumnIndex] = tmp;
        //                        }
        //                        break;
        //                    case BIFFRECORDTYPE.RK:
        //                        dt.Rows[cell.RowIndex][cell.ColumnIndex] = FormatNumber(((ExcelRKCell)cell).Value);
        //                        break;
        //                    case BIFFRECORDTYPE.MULRK:
        //                        for (ushort j = cell.ColumnIndex; j <= ((ExcelMulRKCell)cell).LastColumnIndex; j++)
        //                            dt.Rows[cell.RowIndex][j] = FormatNumber(((ExcelMulRKCell)cell).GetValue(j));
        //                        break;
        //                    case BIFFRECORDTYPE.BLANK:
        //                    case BIFFRECORDTYPE.BLANK_OLD:
        //                    case BIFFRECORDTYPE.MULBLANK:
        //                        // Skip blank cells
        //                        break;
        //                    case BIFFRECORDTYPE.FORMULA:
        //                    case BIFFRECORDTYPE.FORMULA_OLD:
        //                        ((ExcelFormulaCell)cell).UseEncoding = m_encoding; 
        //                        object val = ((ExcelFormulaCell)cell).Value;
        //                        if (val == null)
        //                            val = string.Empty;
        //                        else if (val is FORMULAERROR)
        //                            val = "#" + ((FORMULAERROR)val).ToString();
        //                        else if (val is double)
        //                            val = FormatNumber((double)val);
        //                        dt.Rows[cell.RowIndex][cell.ColumnIndex] = val.ToString();
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //        dt.EndLoadData();
        //        //if (m_firstRowHeader)
        //        //{
        //        //    for (int i = 0; i < maxCol; i++)
        //        //    {
        //        //        string colName = dt.Rows[0][i].ToString();
        //        //        if (!string.IsNullOrEmpty(colName))
        //        //            dt.Columns[i].ColumnName = colName;
        //        //    }
        //        //    dt.Rows[0].Delete();
        //        //    dt.AcceptChanges();
        //        //}
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        /// <summary>
        /// private method, reads sheet data
        /// </summary>
        /// <param name="sheet">Sheet object, whose data to read</param>
        /// <returns>True if sheet was read successfully, otherwise False</returns>
        private bool ReadWorksheet(ExcelWorksheet sheet)
        {

            m_stream.Seek((int)sheet.DataOffset, SeekOrigin.Begin);
            ExcelBOF bof = m_stream.Read() as ExcelBOF;
            if (bof == null || bof.Type != BIFFTYPE.Worksheet)
                return false;
            ExcelIndex idx = m_stream.Read() as ExcelIndex;
            bool isV8 = (m_version >= 0x600);
            if (idx != null)
            {
                idx.IsV8 = isV8;
                DataTable dt = new DataTable(sheet.Name);

                ExcelRecord trec;
                ExcelDimensions dims = null;
                do
                {
                    trec = m_stream.Read();
                    if (trec.ID == BIFFRECORDTYPE.DIMENSIONS)
                    {
                        dims = (ExcelDimensions)trec;
                        break;
                    }
                }
                while (trec.ID != BIFFRECORDTYPE.ROW);
                int maxCol = 256;
                if (dims != null)
                {
                    dims.IsV8 = isV8;
                    maxCol = dims.LastColumn;
                    sheet.Dimensions = dims;
                }
                //for (int i = 0; i < maxCol; i++)
                //    dt.Columns.Add("F" + (i + 1).ToString(), typeof(string));
                sheet.Data = dt;
                uint maxRow = idx.LastExistingRow;
                if (idx.LastExistingRow <= idx.FirstExistingRow)
                    return true;
                if (m_MaxRows > 0 && m_MaxRows <= maxRow)
                    maxRow = (uint)m_MaxRows;

                maxCol = 256;
                //uint mr = ((uint)(maxRow * 1.2)) > 65000 ? 65000 : ((uint)(maxRow * 1.2));
                string[,] dtRows = new string[maxRow, maxCol];
                int lastCol = 0;
                int lastRow = 0;
                //dt.BeginLoadData();
                //for (int i = 0; i <= maxRow; i++)
                //    dt.Rows.Add(dt.NewRow());
                uint[] dbCellAddrs = idx.DbCellAddresses;
                for (int i = 0; i < dbCellAddrs.Length; i++)
                {
                    ExcelDbCell dbCell = (ExcelDbCell)m_stream.ReadAt((int)dbCellAddrs[i]);
                    ExcelRow row = null;
                    int offs = (int)dbCell.RowAddress;
                    do
                    {
                        row = m_stream.ReadAt(offs) as ExcelRow;
                        if (row == null) break;
                        offs += row.Size;
                    }
                    while (row != null);
                    while (true)
                    {
                        ExcelRecord rec = m_stream.ReadAt(offs);
                        offs += rec.Size;
                        if (rec is ExcelDbCell) break;
                        if (rec is ExcelEOF) break;
                        ExcelBlankCell cell = rec as ExcelBlankCell;
                        if (cell == null)
                        {
                            continue;
                        }
                        if (cell.ColumnIndex >= maxCol) continue;
                        if (cell.RowIndex >= maxRow) break;
                        
                        switch (cell.ID)
                        {
                            case BIFFRECORDTYPE.INTEGER:
                            case BIFFRECORDTYPE.INTEGER_OLD:
                                dtRows[cell.RowIndex,cell.ColumnIndex] = ((ExcelIntegerCell)cell).Value.ToString();
                                break;
                            case BIFFRECORDTYPE.NUMBER:
                            case BIFFRECORDTYPE.NUMBER_OLD:
                                dtRows[cell.RowIndex,cell.ColumnIndex] = FormatNumber(((ExcelNumberCell)cell).Value);
                                break;
                            case BIFFRECORDTYPE.LABEL:
                            case BIFFRECORDTYPE.LABEL_OLD:
                            case BIFFRECORDTYPE.RSTRING:
                                dtRows[cell.RowIndex,cell.ColumnIndex] = ((ExcelLabelCell)cell).Value;
                                break;
                            case BIFFRECORDTYPE.LABELSST:
                                {
                                    string tmp = m_globals.SST.GetString(((ExcelLabelSSTCell)cell).SSTIndex);
                                    dtRows[cell.RowIndex,cell.ColumnIndex] = tmp;
                                }
                                break;
                            case BIFFRECORDTYPE.RK:
                                dtRows[cell.RowIndex,cell.ColumnIndex] = FormatNumber(((ExcelRKCell)cell).Value);
                                break;
                            case BIFFRECORDTYPE.MULRK:
                                for (ushort j = cell.ColumnIndex; j <= ((ExcelMulRKCell)cell).LastColumnIndex; j++)
                                    dtRows[cell.RowIndex,j] = FormatNumber(((ExcelMulRKCell)cell).GetValue(j));
                                break;
                            case BIFFRECORDTYPE.BLANK:
                            case BIFFRECORDTYPE.BLANK_OLD:
                            case BIFFRECORDTYPE.MULBLANK:
                                continue;
                                // Skip blank cells
                                //break;
                            case BIFFRECORDTYPE.FORMULA:
                            case BIFFRECORDTYPE.FORMULA_OLD:
                                ((ExcelFormulaCell)cell).UseEncoding = m_encoding;
                                object val = ((ExcelFormulaCell)cell).Value;
                                if (val == null)
                                    val = string.Empty;
                                else if (val is FORMULAERROR)
                                    val = "#" + ((FORMULAERROR)val).ToString();
                                else if (val is double)
                                    val = FormatNumber((double)val);
                                dtRows[cell.RowIndex,cell.ColumnIndex] = val.ToString();
                                break;
                            default:
                                break;
                        }
                        lastCol = Math.Max(lastCol, cell.ColumnIndex);
                        lastRow = Math.Max(lastRow, cell.RowIndex);
                    }
                }
                //dt.EndLoadData();

                lastRow = lastRow > maxRow ? (int)maxRow : lastRow;
                lastRow++;
                lastCol++;

                //DataTable dt = new DataTable(sheet.Name);
                for (int i = 0; i < lastCol; i++)
                    dt.Columns.Add("F" + (i + 1).ToString(), typeof(string));
                sheet.Data = dt;
                dt.BeginLoadData();
                object[] rows=new object[lastCol];

                for (int i = 0; i < lastRow; i++)
                {
                    //dt.Rows.Add(dt.NewRow());
                    for (int j = 0; j < lastCol; j++)
                        rows[j] = dtRows[i, j];
                    dt.Rows.Add(rows);
                }

                dt.EndLoadData();

            }
            else
            {
                return false;
            }

            return true;
        }

        private string FormatNumber(double x)
        {
            if (Math.Floor(x) == x)
                return Math.Floor(x).ToString();
            else
                return Math.Round(x, 2).ToString(CultureInfo.InvariantCulture);
        }

    }
}
